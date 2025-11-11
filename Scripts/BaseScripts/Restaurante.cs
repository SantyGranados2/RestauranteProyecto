using RestauranteProyecto.Scripts.Components;
using RestauranteProyecto.Scripts.DataScripts;

namespace RestauranteProyecto.Scripts.BaseScripts;

public class Restaurante
{
    #region VariablesPrivadas

    // Variables sobre características del restaurante
    private string nit;
    private string nombre;
    private string dueño;
    private string celular;
    private string direccion;
    
    // Variables para manejar las ganancias del día
    private DateTime fechaGanancias = DateTime.Today;
    public decimal GananciasDelDia { get; private set; }

    #endregion

    #region ListasDeDatos

    // Listas para manejar los datos
    public ListaEnlazada<Cliente> ClientesLista { get; } = new ListaEnlazada<Cliente>();
    public ListaEnlazada<Plato> PlatosLista { get; } = new ListaEnlazada<Plato>();
    public Cola<Pedido> ColaPedidosLista { get; } = new Cola<Pedido>();
    public ListaEnlazada<Pedido> PedidosDespachadosLista { get; } = new ListaEnlazada<Pedido>();
    public Pila<PlatoPedido> HistorialPlatosServidosLista { get; } = new Pila<PlatoPedido>();

    #endregion

    #region Propiedades

    // Propiedades de las variables anteriores con sus respectivas validaciones
    public string Nit
    {
        get => nit;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El NIT del restaurante es obligatorio.");
            }

            nit = value.Trim();
        }
    }

    public string Nombre
    {
        get => nombre;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El nombre del restaurante es obligatorio.");
            }

            nombre = value.Trim();
        }
    }

    public string Dueño
    {
        get => dueño;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El nombre del dueño es obligatorio.");
            }

            dueño = value.Trim();
        }
    }

    public string Celular
    {
        get => celular;
        set
        {
            if (value.Length != 10)
            {
                throw new ArgumentException("El número de celular no fue ingresado correctamente.");
            }

            celular = value.Trim();
        }
    }

    public string Direccion
    {
        get => direccion;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("La dirección del restaurante es obligatoria.");
            }

            direccion = value.Trim();
        }
    }

    #endregion

    #region Constructor

    // Constructor de la clase Restaurante
    public Restaurante(string nit, string nombre, string dueño, string celular, string direccion)
    {
        Nit = nit;
        Nombre = nombre;
        Dueño = dueño;
        Celular = celular;
        Direccion = direccion;
    }

    #endregion

    #region GestionRestaurante

    // Métodos necesarios para manejar el restaurante
    public Cliente BuscarClientePorCedula(string cedula)
    {
        for (int i = 0; i < ClientesLista.Cantidad; i++)
        {
            var cliente = ClientesLista.ObtenerEn(i);

            if (cliente.Cedula == cedula)
            {
                return cliente;
            }
        }

        return null;

    }

    public Plato BuscarPlatoPorCodigo(string codigo)
    {
        for (int i = 0; i < PlatosLista.Cantidad; i++)
        {
            var plato = PlatosLista.ObtenerEn(i);

            if (plato.Codigo == codigo)
            {
                return plato;
            }
        }

        return null;

    }

    private bool ClienteTienePedidosPendientes(string cedula)
    {
        bool pendiente = false;
        
        ColaPedidosLista.Recorrer(pedido =>
        {
            if (pedido.ClienteCedula == cedula && pedido.Estado == EstadoPedido.Pendiente)
            {
                pendiente = true;
            }
        });
        
        return pendiente;
        
    }

    #endregion

    #region GestionClientes

    // Métodos encargados de la gestión de clientes
    public void AgregarCliente(Cliente cliente)
    {
        if (BuscarClientePorCedula(cliente.Cedula) != null)
        {
            throw new InvalidOperationException("El cliente ya existe.");
        }
        
        ClientesLista.Agregar(cliente);
    }

    public bool EditarCliente(string cedula, string nuevoNombre, string nuevoCelular, string nuevoEmail)
    {
        var cliente = BuscarClientePorCedula(cedula);

        if (cliente == null)
        {
            return false;
        }
        
        cliente.NombreCompleto = nuevoNombre;
        cliente.Celular = nuevoCelular;
        cliente.Email = nuevoEmail;
        return true;
    }

    public bool EliminarCliente(string cedula)
    {
        if (ClienteTienePedidosPendientes(cedula))
        {
            throw new InvalidOperationException("El cliente no se puede borrar porque tiene pedidos pendientes.");
        }

        for (int i = 0; i < ClientesLista.Cantidad; i++)
        {
            if (ClientesLista.ObtenerEn(i).Cedula == cedula)
            {
                ClientesLista.EliminarPosicion(i);
                return true;
            }
        }

        return false;

    }

    #endregion

    #region GestionPlatos

    // Métodos para la gestión de platos
    public void AgregarPlato(Plato plato)
    {
        if (BuscarPlatoPorCodigo(plato.Codigo) != null)
        {
            throw new InvalidOperationException("El plato ya existe.");
        }
        
        PlatosLista.Agregar(plato);
    }

    public bool EditarPlato(string codigo, string nuevoNombre, string nuevaDescripcion, decimal nuevoPrecio)
    {
        var plato = BuscarPlatoPorCodigo(codigo);

        if (plato == null)
        {
            return false;
        }
        
        plato.Nombre = nuevoNombre;
        plato.Descripcion = nuevaDescripcion;
        plato.Precio = nuevoPrecio;
        return true;
    }

    public bool EliminarPlato(string codigo)
    {
        if (ExistePlatoEnPedidosPendientes(codigo))
        {
            throw new InvalidOperationException("El plato no se puede borrar, está referenciado a un pedido pendiente.");
        }

        for (int i = 0; i < PlatosLista.Cantidad; i++)
        {
            if (PlatosLista.ObtenerEn(i).Codigo == codigo)
            {
                PlatosLista.EliminarPosicion(i);
                return true;
            }
        }
        
        return false;
        
    }

    private bool ExistePlatoEnPedidosPendientes(string codigo)
    {
        bool existe = false;
        
        ColaPedidosLista.Recorrer(pedido =>
        {
            for (int i = 0; i < pedido.CantidadPlatos; i++)
            {
                var plato = pedido.ObtenerPlatoEn(i);

                if (plato.CodigoPlato == codigo)
                {
                    existe = true;
                    break;
                }
            }
        });

        return existe;

    }

    private void ResetearGananciasSiCambioDeDia()
    {
        if (DateTime.Today != fechaGanancias)
        {
            fechaGanancias = DateTime.Today;
            GananciasDelDia = 0;
        }
    }

    #endregion

    #region GestionPedidos

    public Pedido CrearPedidoYEncolar(string cedulaCliente, ListaEnlazada<PlatoPedido> platos)
    {
        var cliente = BuscarClientePorCedula(cedulaCliente);

        if (cliente == null)
        {
            throw new InvalidOperationException("El cliente no existe.");
        }

        for (int i = 0; i < platos.Cantidad; i++)
        {
            var plato = platos.ObtenerEn(i);

            if (BuscarPlatoPorCodigo(plato.CodigoPlato) == null)
            {
                throw new InvalidOperationException("El plato no existe.");
            }
        }

        var pedido = new Pedido(cedulaCliente);

        for (int i = 0; i < platos.Cantidad; i++)
        {
            pedido.AgregarItem(platos.ObtenerEn(i));
        }
        
        ColaPedidosLista.Encolar(pedido);
        return pedido;
    }

    public bool DespacharPedido()
    {
        if (ColaPedidosLista.EstaVacia())
        {
            return false;
        }
        
        ResetearGananciasSiCambioDeDia();

        var pedido = ColaPedidosLista.Desencolar();
        pedido.MarcarDespachado();
        GananciasDelDia += pedido.Total;

        for (int i = 0; i < pedido.CantidadPlatos; i++)
        {
            var plato = pedido.ObtenerPlatoEn(i);
            HistorialPlatosServidosLista.Apilar(plato);
        }
        
        PedidosDespachadosLista.Agregar(pedido);
        return true;
    }

    public decimal CalcularGananciasDelDia(DateTime fecha)
    {
        decimal suma = 0;

        for (int i = 0; i < PedidosDespachadosLista.Cantidad; i++)
        {
            var pedido = PedidosDespachadosLista.ObtenerEn(i);

            if (pedido.Estado == EstadoPedido.Despachado && pedido.FechaHora.Date == fecha.Date)
            {
                suma += pedido.Total;
            }
        }
        
        return suma;
    }

    #endregion
}