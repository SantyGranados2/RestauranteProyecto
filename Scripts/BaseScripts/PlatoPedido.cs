public class PlatoPedido
{
    #region VariablesPrivadas
    
    private string codigoPlato;
    private int cantidad;
    private decimal precioUnitario;

    #endregion

    #region Propiedades

    public string CodigoPlato
    {
        get => codigoPlato;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El código del plato es obligatorio.");
            }

            codigoPlato = value.Trim();
        }
    }

    public int Cantidad
    {
        get => cantidad;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("La cantidad debe ser mayor a cero.");
            }

            cantidad = value;
        }
    }

    public decimal PrecioUnitario
    {
        get => precioUnitario;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("El precio unitario debe ser mayor a cero.");
            }

            precioUnitario = value;
        }
    }

    public decimal Subtotal => cantidad * precioUnitario;

    #endregion

    #region Constructor

    public PlatoPedido(string codigoPlato, int cantidad, decimal precioUnitario)
    {
        CodigoPlato = codigoPlato;
        Cantidad = cantidad;
        PrecioUnitario = precioUnitario;
    }

    #endregion
}