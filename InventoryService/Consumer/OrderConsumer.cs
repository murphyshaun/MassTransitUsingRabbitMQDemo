using MassTransit;
using Model;

namespace InventoryService.Consumer
{
    public class OrderConsumer : IConsumer<OrderModel>
    {
        public async Task Consume(ConsumeContext<OrderModel> context)
        {
            await Console.Out.WriteLineAsync($"Order name: {context.Message.Name}");
        }
    }
}
