using MassTransit;
using Model;

namespace InventoryService.Consumer
{
    public class ProductConsumer : IConsumer<ProductModel>
    {
        public async Task Consume(ConsumeContext<ProductModel> context)
        {
            await Console.Out.WriteLineAsync($"Product name: {context.Message.Name}");
        }
    }
}
