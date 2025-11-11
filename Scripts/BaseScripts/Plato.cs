public class Plato
{
    #region VariablesPrivadas

    private string codigo;
    private string nombre;
    private string descripcion;
    private decimal precio;

    #endregion

    #region Propiedades

    // Propiedades de las variables anteriores con sus respectivas validaciones
    public string Codigo
    {
        get => codigo;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El código del plato es obligatorio.");
            }

            codigo = value.Trim();
        }
    }

    public string Nombre
    {
        get => nombre;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El nombre del plato es obligatorio.");
            }

            nombre = value.Trim();
        }
    }

    public string Descripcion
    {
        get => descripcion;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("La descripción del plato es obligatoria.");
            }

            descripcion = value.Trim();
        }
    }

    public decimal Precio
    {
        get => precio;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("El precio del plato debe ser mayor a cero.");
            }

            precio = value;
        }
    }

    #endregion

    #region Constructor

    // Constructor de la clase Plato
    public Plato(string codigo, string nombre, string descripcion, decimal precio)
    {
        Codigo = codigo;
        Nombre = nombre;
        Descripcion = descripcion;
        Precio = precio;
    }

    #endregion
}