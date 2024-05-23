using Klasy;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbonentB
{
    class Consumer : IConsumer<IWyd>, IConsumer<Fault<IAbonB>>
    {
        public Task Consume(ConsumeContext<IWyd> context)
        {
            return Task.Run(() =>
            {
                if (context.Message.Number % 3 == 0)
                    context.RespondAsync(new AbonB() { Who = "AbonentB" });
                Console.WriteLine($"Otrzymano wiadomosc: {context.Message.Number} " +
                    $"{(context.Message.Number % 3 == 0 ? "(wyslano odpowiedz)" : "")}");
            });
        }

        public Task Consume(ConsumeContext<Fault<IAbonB>> context)
        {
            return Task.Run(() =>
            {
                foreach (var e in context.Message.Exceptions)
                {
                    Console.WriteLine($"Nastąpił wyjątek {e.Message}");
                }
            });
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var consumer = new Consumer();
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(new Uri("rabbitmq://localhost/"),
                h => { h.Username("guest"); h.Password("guest"); });
                sbc.ReceiveEndpoint("AbonentB", ep => {
                    ep.Instance(consumer);
                });
            });
            bus.Start();
            Console.WriteLine("Rozpoczął AbonentB");
            Console.ReadKey();
            bus.Stop();
            Console.WriteLine("Zakończył AbonentB");
        }
    }
}