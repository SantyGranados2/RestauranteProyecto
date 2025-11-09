public class Pedido
{
    private string idPedido;
    private Cliente clienteCedula; //TODO: Sacar la cedula del cliente
    //private ListaEnlazada<Plato> platos; //TODO: Agregar la clase ListaEnlazada
    private decimal total;
    private string fechaHora;
    private string estado; //TODO: Debe tener estado de "Pendiente" y "Despachado"
    //TODO: Agregar platoPedido y a este agregarle el codigo, la cantidad y el precio unitario
    //TODO: Cuando se crea el plato debe validarse la existencia de clientes y platos.
}