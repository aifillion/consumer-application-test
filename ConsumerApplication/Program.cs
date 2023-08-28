using Confluent.Kafka;
using ConsumerApplication;
using System.Text.Json;

var consumerConfig = new ConsumerConfig
{
    GroupId = "Pedido",
    BootstrapServers = "134.209.172.61:29093", //"localhost:29092",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

var producerConfig = new ProducerConfig
{
    BootstrapServers = "134.209.172.61:29093"
};

using (var consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build())
{
    consumer.Subscribe("PedidoCreado");
    while (true)
    {
        var consumeResult = consumer.Consume();

        if (consumeResult != null)
        {
            Pedido? pedido = JsonSerializer.Deserialize<Pedido>(consumeResult.Message.Value);

            Random random = new Random();
            int numeroPedido = random.Next(100000000, 999999999);

            string datosPedido = $"{pedido?.Id}/{numeroPedido}";

            using (var producer = new ProducerBuilder<Null, string>(producerConfig).Build())
            {
                await producer.ProduceAsync("PedidoAsignado", new Message<Null, string> { Value = datosPedido });
                producer.Flush(TimeSpan.FromSeconds(10));
            }
        }
    }
}