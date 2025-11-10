public class Pila<T>
{
    private Nodo<T> cima;
    private int tamano;

    public int Tamano
    {
        get { return tamano; }
    }

    public void AgregarElemento(T valor)
    {
        Nodo<T> nuevoNodo = new Nodo<T>(valor);

        if (cima == null)
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
        if (cima == null)
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