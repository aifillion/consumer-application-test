namespace ConsumerApplication
{
    public class Pedido
    {
        public Guid Id { get; set; }

        public int? NumeroPedido { get; set; }

        public string CicloDelPedido { get; set; } = string.Empty;

        public long CodigoDeContratoInterno { get; set; }

        public EstadoDelPedido EstadoDelPedido { get; set; }

        public string CuentaCorriente { get; set; } = string.Empty;

        public DateTime Cuando { get; set; }
    }
}
