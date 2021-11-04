using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using TrickyBookStore.Services.Books;
using TrickyBookStore.Services.Customers;
using TrickyBookStore.Services.Payment;
using TrickyBookStore.Services.PurchaseTransactions;
using TrickyBookStore.Services.Subscriptions;

namespace TrickyBookStore.App
{
    class Program
    {
        static Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            Console.WriteLine("Hello World!");
            Execute(host.Services);
            return host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddTransient<IBookService, BookService>()
                            .AddTransient<ICustomerService, CustomerService>()
                            .AddTransient<ISubscriptionService, SubscriptionService>()
                            .AddTransient<IPurchaseTransactionService, PurchaseTransactionService>()
                            .AddTransient<PaymentService>());

        static void Execute(IServiceProvider services)
        {
            using IServiceScope serviceScope = services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            PaymentService paymentService = provider.GetRequiredService<PaymentService>();
            var result = paymentService.GetPaymentAmount(7, new DateTimeOffset(new DateTime(2018, 2, 1)), new DateTimeOffset(new DateTime(2018, 2, 25)));
            Console.WriteLine(result);
        }
    }
}
