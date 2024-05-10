using Confluent.Kafka;

class Program
{
    static void Main()
    {
        var config = KafkaConsumerConfig.GetConfig();
        string topic = "my-topic"; // Replace with your topic name
        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

        consumer.Subscribe(topic);
        while (true)
        {
            try
            {
                var consumeResult = consumer.Consume();
                Console.WriteLine($"Received message: {consumeResult.Message.Value}");

                // Process the message here
                consumer.Commit(consumeResult);
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"Error consuming message: {e.Error.Reason}");
            }
        }
    }
}