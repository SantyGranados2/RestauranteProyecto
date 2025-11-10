public class Pila<T>
{
    private Nodo<T> cima;
    private int tamano;

    public int Tamano
    {
        get { return tamano; }
    }

    public bool EstaVacia()
    {
        return tamano == 0;
    }

    public void AgregarElemento(T valor)
    {
        Nodo<T> nuevoNodo = new Nodo<T>(valor);

        if (EstaVacia())
        {
            cima = nuevoNodo;
        }
        else
        {
            nuevoNodo.Siguiente = cima;
            cima = nuevoNodo;
        }

        tamano++;
    }

    public void EliminarElemento()
    {
        if (EstaVacia())    
        {
            return;
        }

        cima = cima.Siguiente;
        tamano--;
    }

    public void Vaciar()
    {
        cima = null;
        tamano = 0;
    }
}