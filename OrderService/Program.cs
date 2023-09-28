using MassTransit;
using Model;

namespace OrderService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMassTransit(mass =>
            {
                mass.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint("temp-queue", c =>
                    {
                        //c.ExchangeType = "topic";
                        c.Handler<OrderModel>(context =>
                        {
                            return Console.Out.WriteAsync(context.Message.Name);
                        });
                    });
                });
            });
            builder.Services.AddMassTransitHostedService();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void CreateBus()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint("temp-queue", c =>
                {
                    c.Handler<OrderModel>(ctx =>
                    {
                        return Console.Out.WriteAsync(ctx.Message.Name);
                    });
                });
            });

            bus.Start();

            bus.Publish(new OrderModel { Name = "Test name" });
        }
    }
}