using RestauranteProyecto.Scripts.BaseScripts;
using RestauranteProyecto.Scripts.DataScripts;

public static class Program
{
    static ListaEnlazada<Restaurante> restaurantes = new ListaEnlazada<Restaurante>();

    #region Main

    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("--- SISTEMA DE RESTAURANTE (Estructura de Datos) ---");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("1. Gestionar Restaurantes");
            Console.WriteLine("2. Seleccionar Restaurante y Gestionar");
            Console.WriteLine("0. Salir");
            Console.Write("Opción: ");
            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1": MenuRestaurantes(); break;
                case "2": SeleccionarRestauranteYMenu(); break;
                case "0": return;
                default: Console.WriteLine("Opción inválida."); break;
            }
        }
    }

    #endregion

    #region MenuRestaurantes

    static void MenuRestaurantes()
    {
        while (true)
        {
            Console.WriteLine("--- RESTAURANTES ---");
            ListarRestaurantes();
            Console.WriteLine("\n1. Crear Restaurante");
            Console.WriteLine("2. Editar Restaurante");
            Console.WriteLine("3. Borrar Restaurante");
            Console.WriteLine("9. Volver");
            Console.Write("Opcion: ");
            
            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1": CrearRestaurante(); break;
                case "2": EditarRestaurante(); break;
                case "3": BorrarRestaurante(); break;
                case "9": return;
                default: Console.WriteLine("Opción inválida."); break;
            }
        }
    }

    static void ListarRestaurantes()
    {
        Console.WriteLine("Listado:");

        if (restaurantes.Cantidad == 0)
        {
            Console.WriteLine("  (sin restaurantes)");
            return;
        }

        for (int i = 0; i < restaurantes.Cantidad; i++)
        {
            var restaurante = restaurantes.ObtenerEn(i);
            Console.WriteLine($"  [{i}] {restaurante.Nit} - {restaurante.Nombre} - {restaurante.Direccion} - Cel: {restaurante.Celular}");
        }
    }

    static Restaurante BuscarRestaurantePorNit(string nit)
    {
        for (int i = 0; i < restaurantes.Cantidad; i++)
        {
            var restaurante = restaurantes.ObtenerEn(i);

            if (restaurante.Nit == nit)
            {
                return restaurante;
            }
        }
        
        return null;
    }

    static void CrearRestaurante()
    {
        try
        {
            Console.Write("NIT: ");
            var nit = Console.ReadLine().Trim();

            if (BuscarRestaurantePorNit(nit) != null)
            {
                throw new InvalidOperationException("Ya existe un restaurante con el NIT ingresado.");
            }
            
            Console.Write("Nombre: ");
            var nombre = Console.ReadLine().Trim();
            Console.Write("Dueño: ");
            var dueño = Console.ReadLine().Trim();
            Console.Write("Celular: ");
            var celular = Console.ReadLine().Trim();
            Console.Write("Dirección: ");
            var direccion = Console.ReadLine().Trim();
            
            var restaurante = new Restaurante(nit, nombre, dueño, celular, direccion);
            restaurantes.Agregar(restaurante);
            Console.WriteLine("Restaurante creado correctamente.");
        }
        catch (Exception e)
        {
            Mensaje("Error: " + e.Message);
        }
    }

    static void EditarRestaurante()
    {
        Console.Write("Ingrese NIT del restaurante a editar: ");
        var nit = Console.ReadLine().Trim();
        var restaurante = BuscarRestaurantePorNit(nit);

        if (restaurante == null)
        {
            Console.WriteLine("El restaurante no fue encontrado.");
            return;
        }

        try
        {
            Console.Write($"Nombre ({restaurante.Nombre}): ");
            var nombre = LeerOPorDefecto(restaurante.Nombre);
            Console.Write($"Dueño ({restaurante.Dueño}): ");
            var dueño = LeerOPorDefecto(restaurante.Dueño);
            Console.Write($"Celular ({restaurante.Celular}): ");
            var celular = LeerOPorDefecto(restaurante.Celular);
            Console.Write($"Dirección ({restaurante.Direccion}): ");
            var direccion = LeerOPorDefecto(restaurante.Direccion);

            restaurante.Nombre = nombre;
            restaurante.Dueño = dueño;
            restaurante.Celular = celular;
            restaurante.Direccion = direccion;
            Console.WriteLine("Información de restaurante actualizada.");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }

    static void BorrarRestaurante()
    {
        Console.Write("Ingrese el NIT del restaurante a borrar: ");
        var nit = Console.ReadLine().Trim();

        for (int i = 0; i < restaurantes.Cantidad; i++)
        {
            if (restaurantes.ObtenerEn(i).Nit == nit)
            {
                restaurantes.EliminarPosicion(i);
                Console.WriteLine("El restaurante fue borrado exitosamente.");
                return;
            }
        }
        
        Console.WriteLine("El restaurante no fue encontrado.");
    }

    static void SeleccionarRestauranteYMenu()
    {
        Console.Write("Ingrese el nit del restaurante: ");
        var nit = Console.ReadLine().Trim();
        var restaurante = BuscarRestaurantePorNit(nit);

        if (restaurante == null)
        {
            Console.WriteLine("El restaurante no fue encontrado.");
            return;
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"--- Gestión de: {restaurante.Nombre} {restaurante.Nit} ---");
            Console.WriteLine("1. Clientes");
            Console.WriteLine("2. Platos");
            Console.WriteLine("3. Pedidos");
            Console.WriteLine("4. Reportes");
            Console.WriteLine("9. Volver");
            Console.Write("Opción: ");
            
            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1": MenuClientes(restaurante); break;
                case "2": MenuPlatos(restaurante); break;
                case "3": MenuPedidos(restaurante); break;
                case "4": MenuReportes(restaurante); break;
                case "9": return;
                default: Console.WriteLine("Opción inválida."); break;
            }
        }
    }

    #endregion

    #region MenuClientes

    static void MenuClientes(Restaurante restaurante)
    {
        while (true)
        {
            Console.WriteLine("--- CLIENTES ---");
            ListarClientes(restaurante);
            Console.WriteLine("\n1. Agregar Cliente");
            Console.WriteLine("2. Editar Cliente");
            Console.WriteLine("3. Borrar Cliente");
            Console.WriteLine("9. Volver");
            Console.Write("Opcion: ");
            
            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1": CrearCliente(restaurante); break;
                case "2": EditarCliente(restaurante); break;
                case "3": BorrarCliente(restaurante); break;
                case "9": return;
                default: Console.WriteLine("Opción inválida."); break;
            }
        }
    }

    static void ListarClientes(Restaurante restaurante)
    {
        Console.WriteLine("Listado: ");

        if (restaurante.ClientesLista.Cantidad == 0)
        {
            Console.WriteLine("  (sin clientes)");
            return;
        }

        for (int i = 0; i < restaurante.ClientesLista.Cantidad; i++)
        {
            var cliente = restaurante.ClientesLista.ObtenerEn(i);
            Console.WriteLine($"  [{i}] {cliente.Cedula} - {cliente.NombreCompleto} - {cliente.Email} - {cliente.Celular}");
        }
    }

    static void CrearCliente(Restaurante restaurante)
    {
        try
        {
            Console.Write("Ingrese la cédula del cliente: ");
            var cedula = Console.ReadLine().Trim();

            if (restaurante.BuscarClientePorCedula(cedula) != null)
            {
                throw new InvalidOperationException("El cliente ya se encuentra registrado en el sistema.");
            }
            
            Console.Write("Nombre Completo: ");
            var nombre = Console.ReadLine().Trim();
            Console.Write("Celular: ");
            var celular = Console.ReadLine().Trim();
            Console.Write("Email: ");
            var email = Console.ReadLine().Trim();
            
            var cliente = new Cliente(cedula, nombre, celular, email);
            restaurante.AgregarCliente(cliente);
            Console.WriteLine("Cliente creado exitosamente.");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }

    static void EditarCliente(Restaurante restaurante)
    {
        Console.Write("Ingrese la cédula del cliente: ");
        var cedula = Console.ReadLine().Trim();
        var cliente = restaurante.BuscarClientePorCedula(cedula);

        if (cliente == null)
        {
            Console.WriteLine("Cliente no encontrado.");
            return;
        }

        try
        {
            Console.Write($"Nombre ({cliente.NombreCompleto}): ");
            var nombre = LeerOPorDefecto(cliente.NombreCompleto);
            Console.Write($"Celular ({cliente.Celular}): ");
            var celular = LeerOPorDefecto(cliente.Celular);
            Console.Write($"Email ({cliente.Email}): ");
            var email = LeerOPorDefecto(cliente.Email);
            
            restaurante.EditarCliente(cedula, nombre, celular, email);
            Console.WriteLine("La información del cliente fue actualizada exitosamente.");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }

    static void BorrarCliente(Restaurante restaurante)
    {
        Console.Write("Ingrese la cédula del cliente a borrar: ");
        var cedula = Console.ReadLine().Trim();

        try
        {
            if (restaurante.EliminarCliente(cedula))
            {
                Console.WriteLine("El cliente fue borrado del sistema exitosamente.");
            }
            else
            {
                Console.WriteLine("No se encontró al cliente.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }

    #endregion

    #region MenuPlatos

    static void MenuPlatos(Restaurante restaurante)
    {
        while (true)
        {
            Console.WriteLine("--- PLATOS ---");
            ListarPlatos(restaurante);
            Console.WriteLine("\n1. Crear plato");
            Console.WriteLine("2. Editar plato");
            Console.WriteLine("3. Borrar plato");
            Console.WriteLine("9. Volver");
            Console.Write("Opción: ");

            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1": CrearPlato(restaurante); break;
                case "2": EditarPlato(restaurante); break;
                case "3": BorrarPlato(restaurante); break;
                case "9": return;
                default: Mensaje("Opción inválida."); break;
            }
        }
    }

    static void ListarPlatos(Restaurante restaurante)
    {
        Console.WriteLine("Listado: ");

        if (restaurante.PlatosLista.Cantidad == 0)
        {
            Console.WriteLine("  (sin platos)");
            return;
        }

        for (int i = 0; i < restaurante.PlatosLista.Cantidad; i++)
        {
            var plato = restaurante.PlatosLista.ObtenerEn(i);
            Console.WriteLine($"  [{i}] {plato.Codigo} - {plato.Nombre} - ${plato.Precio} - {plato.Descripcion}");
        }
    }

    static void CrearPlato(Restaurante restaurante)
    {
        try
        {
            Console.Write("Ingrese el código del plato: ");
            var codigo = Console.ReadLine().Trim();

            if (restaurante.BuscarPlatoPorCodigo(codigo) != null)
            {
                throw new InvalidOperationException("Ya existe un plato con el código ingresado.");
            }
            
            Console.Write("Nombre: ");
            var nombre = Console.ReadLine().Trim();
            Console.Write("Descripción: ");
            var descripcion = Console.ReadLine().Trim();
            Console.Write("Precio: ");
            var precioStr = Console.ReadLine().Trim();

            if (!decimal.TryParse(precioStr, out var precio))
            {
                throw new InvalidOperationException("El precio ingresado inválido.");
            }
            
            var plato = new Plato(codigo, nombre, descripcion, precio);
            restaurante.AgregarPlato(plato);
            Mensaje("El plato fue creado exitosamente.");
        }
        catch (Exception e)
        {
            Mensaje("Error: " + e.Message);
        }
    }

    static void EditarPlato(Restaurante restaurante)
    {
        Console.Write("Ingrese el código del plato: ");
        var codigo = Console.ReadLine().Trim();
        var plato = restaurante.BuscarPlatoPorCodigo(codigo);

        if (plato == null)
        {
            Mensaje("El plato no fue encontrado.");
            return;
        }

        try
        {
            Console.Write($"Nombre ({plato.Nombre}): ");
            var nombre = LeerOPorDefecto(plato.Nombre);
            Console.Write($"Descripción ({plato.Descripcion}): ");
            var descripcion = LeerOPorDefecto(plato.Descripcion);
            Console.Write($"Precio ({plato.Precio}): ");
            var precioTxt = LeerOPorDefecto(plato.Precio.ToString());

            if (!decimal.TryParse(precioTxt, out var precio))
            {
                throw new InvalidOperationException("El precio ingresado inválido.");
            }
            
            restaurante.EditarPlato(codigo, nombre, descripcion, precio);
            Mensaje("La información del plato fue actualizada exitosamente.");
        }
        catch (Exception e)
        {
            Mensaje("Error: " + e.Message);
        }
    }

    static void BorrarPlato(Restaurante restaurante)
    {
        Console.Write("Ingrese el codigo del plato a borrar: ");
        var codigo = Console.ReadLine().Trim();

        try
        {
            if (restaurante.EliminarPlato(codigo))
            {
                Mensaje("El plato fue borrado exitosamente.");
            }
            else
            {
                Mensaje("El plato no fue encontrado.");
            }
        }
        catch (Exception e)
        {
            Mensaje("Error: " + e.Message);
        }
    }

    #endregion

    #region MenuPedidos

    static void MenuPedidos(Restaurante restaurante)
    {
        while (true)
        {
            Console.WriteLine("--- PEDIDOS ---");
            Console.WriteLine($"Pedidos pendientes en cola: {restaurante.ColaPedidosLista.Tamano()}");
            Console.WriteLine("1. Tomar pedido");
            Console.WriteLine("2. Despachar siguiente");
            Console.WriteLine("9. Volver");
            Console.Write("Opción: ");
            
            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1": TomarPedido(restaurante); break;
                case "2": Despachar(restaurante); break;
                case "9": return;
                default: Mensaje("Opción inválida."); break;
            }
        }
    }

    static void TomarPedido(Restaurante restaurante)
    {
        try
        {
            Console.Write("Ingrese la cédula del cliente: ");
            var cedula = Console.ReadLine().Trim();

            if (restaurante.BuscarClientePorCedula(cedula) == null)
            {
                throw new InvalidOperationException(
                    "No existe el cliente. Debes crearlo primero en el módulo de clientes");
            }

            var platos = new ListaEnlazada<PlatoPedido>();

            while (true)
            {
                Console.Write("Código del plato: ");
                var codigo = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(codigo)) break;

                var pl = restaurante.BuscarPlatoPorCodigo(codigo);
                if (pl == null)
                {
                    Console.WriteLine("  No existe ese código en el menú.");
                    continue;
                }
                
                Console.Write("Cantidad: ");
                var cantidadStr = Console.ReadLine()?.Trim();
                if (!int.TryParse(cantidadStr, out var cantidad) || cantidad <= 0)
                {
                    Console.WriteLine("  Cantidad inválida.");
                    continue;
                }
                
                Console.Write("Desea confirmar el pedido? (S/N): ");
                var confirmar = Console.ReadLine().Trim().ToUpperInvariant();

                if (confirmar == "N")
                {
                    TomarPedido(restaurante);
                    break;
                }

                var plato = new PlatoPedido(codigo, cantidad, pl.Precio);
                platos.Agregar(plato);
                Console.WriteLine("  Pedido agregado exitosamente.");

                Console.Write("¿Desea agregar otro plato? (S/N): ");
                var seguir = Console.ReadLine().Trim().ToUpperInvariant();

                if (seguir == "N")
                {
                    break;
                }
            }

            if (platos.Cantidad == 0)
            {
                Mensaje("Pedido cancelado (sin platos).");
                return;
            }
            
            var pedido = restaurante.CrearPedidoYEncolar(cedula, platos);
            Mensaje($"Pedido {pedido.IdPedido} encolado. Total: ${pedido.Total}");
        }
        catch (Exception e)
        {
            Mensaje("Error: " + e.Message);
        }
    }
    
    static void Despachar(Restaurante restaurante)
    {
        if (restaurante.DespacharPedido())
        {
            Mensaje("Pedido despachado y registrado en historial/ganancias.");
        }
        else
        {
            Mensaje("No hay pedidos en cola.");
        }
    }

    #endregion

    #region MenuReportes

    static void MenuReportes(Restaurante restaurante)
    {
        while (true)
        {
            Console.WriteLine("--- REPORTES ---");
            Console.WriteLine("1. Ganancias del día (hoy)");
            Console.WriteLine("2. Último platos servidos (historial/pila)");
            Console.WriteLine("9. Volver");
            Console.Write("Opcion: ");
            
            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    VerGanancias(restaurante);
                    break;
                case "2":
                    MostrarUltimosPlatos(restaurante);
                    break;
                case "9": return;
                default: Mensaje("Opción inválida."); break;
            }
        }
    }

    static void MostrarUltimosPlatos(Restaurante restaurante)
    {
        if (restaurante.HistorialPlatosServidosLista.EstaVacia())
        {
            Mensaje("No hay platos en el historial.");
            return;
        }
        
        Console.Write("¿Cuántos registros desea ver? ");
        var nStr = Console.ReadLine().Trim();

        if (!int.TryParse(nStr, out var n) || n <= 0)
        {
            n = 5;
        }

        var temp = new Pila<PlatoPedido>();
        int mostrados = 0;

        while (!restaurante.HistorialPlatosServidosLista.EstaVacia() && mostrados < n)
        {
            var plato = restaurante.HistorialPlatosServidosLista.Desapilar();
            Console.WriteLine($"  Plato {plato.CodigoPlato} x{plato.Cantidad}  (PU: ${plato.PrecioUnitario})  Subtotal: ${plato.Subtotal}");
            temp.Apilar(plato);
            mostrados++;
        }

        while (!temp.EstaVacia())
        {
            restaurante.HistorialPlatosServidosLista.Apilar(temp.Desapilar());
        }
        
        Console.WriteLine("Presione una tecla para continuar...");
        Console.ReadKey();
    }
    
    static void VerGanancias(Restaurante restaurante)
    {
        var hoy = DateTime.Today;
        var ganancia = restaurante.CalcularGananciasDelDia(hoy);
        Mensaje($"Ganancias de hoy ({hoy:yyyy-MM-dd}) : ${ganancia}");
    }

    #endregion

    #region Utilidades UI

    static void Mensaje(string msg)
    {
        Console.WriteLine(msg);
        Console.WriteLine("Presione una tecla para continuar...");
        Console.ReadKey();
    }

    static string LeerOPorDefecto(string actual)
    {
        var v = Console.ReadLine();
        return string.IsNullOrWhiteSpace(v) ? actual : v.Trim();
    }

    #endregion
}
