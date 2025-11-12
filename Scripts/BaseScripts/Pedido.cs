public class Pedido
{
    #region VariablesPrivadas

    // Contador estático para generar IDs únicos
    private static int contadorPedidos = 0;

    // Variables sobre características del pedido
    private string idPedido;
    private string clienteCedula;
    private decimal total;
    private DateTime fechaHora;
    private EstadoPedido estado;

    #endregion

    #region ListasDeDatos

    // Lista de platos en el pedido
    private ListaEnlazada<PlatoPedido> platos = new ListaEnlazada<PlatoPedido>();

    #endregion
    
    #region Propiedades

    public string IdPedido
    {
        get => idPedido;
        private set => idPedido = value;
    }

    public string ClienteCedula
    {
        get => clienteCedula;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("La cédula del cliente es obligatoria.");
            }

            clienteCedula = value.Trim();
        }
    }

    public decimal Total => total;

    public DateTime FechaHora
    {
        get => fechaHora;
        private set => fechaHora = value;
    }

    public EstadoPedido Estado
    {
        get => estado;
        private set => estado = value;
    }
    
    public int CantidadPlatos => platos.Cantidad;

    #endregion

    #region Constructor

    // Constructor de la clase Restaurante
    public Pedido(string clienteCedula)
    {
        IdPedido = GenerarIdPedido();
        ClienteCedula = clienteCedula;
        FechaHora = DateTime.Now;
        Estado = EstadoPedido.Pendiente;
        total = 0;
    }

    #endregion

    #region Metodos

    private static string GenerarIdPedido()
    {
        contadorPedidos++;
        return $"PED-{contadorPedidos:D6}";
    }

    public void AgregarItem(PlatoPedido plato)
    {
        if (Estado != EstadoPedido.Pendiente)
        {
            throw new InvalidOperationException("No se pueden agregar items a un pedido que ya fue despachado.");
        }

        platos.Agregar(plato);
        RecalcularTotal();
    }

    public bool EliminarPrimerItemPorCodigo(string codigoPlato)
    {
        if (string.IsNullOrWhiteSpace(codigoPlato))
        {
            throw new ArgumentException("El código del plato es obligatorio para eliminar un item.");
        }

        for (int i = 0; i < platos.Cantidad; i++)
        {
            var platoActual = platos.ObtenerEn(i);

            if (platoActual.CodigoPlato == codigoPlato)
            {
                platos.EliminarPosicion(i);
                RecalcularTotal();
                return true;
            }
        }

        return false;
    }

    public PlatoPedido ObtenerPlatoEn(int posicion)
    {
        return platos.ObtenerEn(posicion);
    }

    public void RecalcularTotal()
    {
        decimal suma = 0;

        for (int i = 0; i < platos.Cantidad; i++)
        {
            var platoActual = platos.ObtenerEn(i);
            suma += (platoActual.PrecioUnitario * platoActual.Cantidad);
        }

        total = suma;
    }

    public void MarcarDespachado()
    {
        if (Estado == EstadoPedido.Despachado) return;
        estado = EstadoPedido.Despachado;
    }

    #endregion
}