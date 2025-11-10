public class ListaEnlazada<T> 
{
    private Nodo<T> cabeza;
    private Nodo<T> ultimo;
    private int cantidad = 0;

    public Nodo<T> Cabeza
    {
        get { return this.cabeza; }
    }
    
    public Nodo<T> Ultimo
    {
        get { return this.ultimo; }
    }
    
    public int Cantidad
    {
        get { return this.cantidad; }
    }

    public void Agregar(T valor)
    {
        var nuevo = new Nodo<T>(valor);
        cantidad++;

        if (cabeza == null)
        {
            cabeza = nuevo;
            ultimo = nuevo;
            return;
        }

        ultimo!.Siguiente = nuevo;
        ultimo = nuevo;
    }

    public void AgregarAlInicio(T valor)
    {
        var nuevo = new Nodo<T>(valor)
        {
            Siguiente = cabeza
        };

        cabeza = nuevo;

        if (ultimo == null)
        {
            ultimo = nuevo;
        }

        cantidad++;
    }

    public T ObtenerEn(int posicion)
    {
        if (posicion < 0 || posicion >= cantidad)
        {
            throw new ArgumentOutOfRangeException(nameof(posicion));
        }

        var actual = cabeza;

        for (int i = 0; i < posicion; i++)
        {
            actual = actual!.Siguiente;
        }

        return actual!.Valor;
    }

    public void InsertarEnPosicion(T valor, int posicion)
    {
        if (posicion < 0 || posicion > cantidad)
        {
            throw new ArgumentOutOfRangeException(nameof(posicion));
        }

        if (posicion == 0)
        {
            AgregarAlInicio(valor);
            return;
        }

        var actual = cabeza!;
        for (int i = 0; i < posicion - 1; i++)
        {
            actual = actual.Siguiente!;
        }

        var nuevo = new Nodo<T>(valor)
        {
            Siguiente = actual.Siguiente
        };

        actual.Siguiente = nuevo;

        if (nuevo.Siguiente == null)
        {
            ultimo = nuevo;
        }

        cantidad++;
    }

    public void EliminarPosicion(int posicion)
    {
        if (cabeza == null || posicion < 0 || posicion >= cantidad)
        {
            throw new ArgumentOutOfRangeException(nameof(posicion));
        }

        if (posicion == 0)
        {
            cabeza = cabeza.Siguiente;

            if (cabeza == null)
            {
                ultimo = null;
            }

            cantidad--;
            return;
        }

        var actual = cabeza;

        for (int i = 0; i < posicion - 1; i++)
        {
            actual = actual!.Siguiente!;
        }

        var aEliminar = actual!.Siguiente!;
        actual.Siguiente = aEliminar.Siguiente;

        if (actual.Siguiente == null)
        {
            ultimo = actual;
        }
        
        cantidad--;
    }

    public void Revertir()
    {
        var cabezaOriginal = cabeza;
        Nodo<T> previo = null;
        var actual = cabeza;

        while (actual != null)
        {
            var sig = actual.Siguiente;
            actual.Siguiente = previo;
            previo = actual;
            actual = sig;
        }

        cabeza = previo;
        ultimo = cabeza == null ? null : cabezaOriginal;
    }

    public void Vaciar()
    {
        cabeza = null;
        ultimo = null;
        cantidad = 0;
    }
}