public class Pila<T>
{
    private Nodo<T> cima;
    private int tamano;

    public int Tamano => tamano;

    public bool EstaVacia() => tamano == 0;

    public void Apilar(T valor) => AgregarElemento(valor);

    public T Desapilar()
    {
        if (EstaVacia())
        {
            throw new InvalidOperationException("La pila está vacía.");
        }
        
        var valor = cima.Valor;
        EliminarElemento();
        return valor;
    }

    public T Cima()
    {
        if (EstaVacia())
        {
            throw new InvalidOperationException("La pila está vacía.");
        }
        
        return cima.Valor;
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