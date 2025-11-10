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

    public int Tamano()
    {
        return tamano;
    }

    public bool EstaVacia()
    {
        return tamano == 0;
    }

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
            cola = null;
    }

    public T Primero()
    {
        if (EstaVacia())
        {
            throw new InvalidOperationException("La cola está vacía.");
        }

        return cabeza.Valor;
    }

    public void Vaciar()
    {
        cabeza = null;
        cola = null;
        tamano = 0;
    }
}