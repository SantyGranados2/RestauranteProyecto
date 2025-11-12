public class Cliente
{
    #region VariablesPrivadas

    // Variables sobre características del cliente
    private string cedula;
    private string nombreCompleto;
    private string celular;
    private string email;

    #endregion

    #region Propiedades

    // Propiedades de las variables anteriores con sus respectivas validaciones
    public string Cedula
    {
        get => cedula;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("La cédula no fue ingresada correctamente.");
            }

            cedula = value.Trim();
        }
    }

    public string NombreCompleto
    {
        get => nombreCompleto;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("El nombre completo no puede estar vacío.");
            }

            nombreCompleto = value.Trim();
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

    public string Email
    {
        get => email;
        set
        {
            var v = value.Trim();

            if (!new EmailAddressAttribute().IsValid(v))
            {
                throw new ArgumentException("El email tiene un formato inválido.");
            }

            email = value.Trim();
        }
    }

    #endregion

    #region Constructor

    // Constructor de la clase Cliente
    public Cliente(string cedula, string nombreCompleto, string celular, string email)
    {
        Cedula = cedula;
        NombreCompleto = nombreCompleto;
        Celular = celular;
        Email = email;
    }

    #endregion
}