public class Cola<T>
{
    private Nodo<T> cabeza;
    private Nodo<T> cola;
    private int tamano;

    public Cola()
    {
        cabeza = null;
        cola = null;
        tamano = 0;
    }

    public int Tamano() => tamano;

    public bool EstaVacia() => tamano == 0;

    public void Encolar(T valor) => Agregar(valor);

    public T Desencolar()
    {
        if (EstaVacia())
        {
            throw new InvalidOperationException("La cola está vacía.");
        }

        var valor = cabeza.Valor;
        Eliminar();
        return valor;
    }

    public T Frente() => Primero();

    public void Agregar(T valor)
    {
        Nodo<T> nuevoNodo = new Nodo<T>(valor);

        if (EstaVacia())
        {
            cabeza = nuevoNodo;
            cola = nuevoNodo;
        }
        else
        {
            cola.Siguiente = nuevoNodo;
            cola = nuevoNodo;
        }

        tamano++;
    }

    public void Eliminar()
    {
        if (EstaVacia())
        {
            Console.WriteLine("La cola está vacía, no se puede eliminar.");
            return;
        }

        cabeza = cabeza.Siguiente;
        tamano--;

        if (cabeza == null) 
        {
            cola = null;
        }   
    }

    public T Primero()
    {
        if (EstaVacia())
        {
            throw new InvalidOperationException("La cola está vacía.");
        }

        return cabeza.Valor;
    }

    public void Recorrer(Action<T> accion)
    {
        var actual = cabeza;

        while (actual != null)
        {
            accion(actual.Valor);
            actual = actual.Siguiente;
        }
    }

    public bool Existe(Predicate<T> predicado)
    {
        var actual = cabeza;

        while (actual != null)
        {
            if (predicado(actual.Valor))
            {
                return true;
            }
            
            actual = actual.Siguiente;
        }
        
        return false;
        
    }

    public void Vaciar()
    {
        cabeza = null;
        cola = null;
        tamano = 0;
    }
}