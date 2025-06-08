// Program.cs

using System;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleDIDemo
{
    // 1. Define some service interfaces and implementations

    // A simple transient service: each time you ask for IOperation, you get a new instance.
    public interface IOperation
    {
        Guid OperationId { get; }
    }

    public class Operation : IOperation
    {
        public Guid OperationId { get; } = Guid.NewGuid();
    }

    // A scoped service: within one "scope" it returns the same instance,
    // but across different scopes it returns different instances.
    public interface IScopedOperation
    {
        Guid OperationId { get; }
    }

    public class ScopedOperation : IScopedOperation
    {
        public Guid OperationId { get; } = Guid.NewGuid();
    }

    // A singleton service: the same instance is shared application-wide.
    public interface ISingletonOperation
    {
        Guid OperationId { get; }
    }

    public class SingletonOperation : ISingletonOperation
    {
        public Guid OperationId { get; } = Guid.NewGuid();
    }

    // 2. Consumer class that depends on all three lifetimes
    //    Demonstrates constructor injection.
    public class OperationConsumer
    {
        private readonly IOperation _transientOp;
        private readonly IScopedOperation _scopedOp;
        private readonly ISingletonOperation _singletonOp;

        public OperationConsumer(
            IOperation transientOp,
            IScopedOperation scopedOp,
            ISingletonOperation singletonOp)
        {
            _transientOp  = transientOp;
            _scopedOp     = scopedOp;
            _singletonOp  = singletonOp;
        }

        public void ShowOperationIds(string callerContext)
        {
            Console.WriteLine($"{callerContext}:");
            Console.WriteLine($"  Transient  → {_transientOp.OperationId}");
            Console.WriteLine($"  Scoped     → {_scopedOp.OperationId}");
            Console.WriteLine($"  Singleton  → {_singletonOp.OperationId}");
            Console.WriteLine(new string('-', 50));
        }
    }

    class Program
    {
        static void Main4567(string[] args)
        {
            // 3. Create a ServiceCollection (the DI container in-memory registry)
            var services = new ServiceCollection();

            // 4. Register services with appropriate lifetimes:
            //    - AddTransient: every time IOperation is requested, a new Operation() is created
            //    - AddScoped: within one scope, the same ScopedOperation instance is used
            //    - AddSingleton: a single instance of SingletonOperation is created and shared
            services.AddTransient<IOperation, Operation>();
            services.AddScoped<IScopedOperation, ScopedOperation>();
            services.AddSingleton<ISingletonOperation, SingletonOperation>();

            //    We also register our consumer so it can be resolved/injected
            services.AddTransient<OperationConsumer>();

            // 5. Build the ServiceProvider (the “container” you’ll use to resolve/inject)
            var serviceProvider = services.BuildServiceProvider();

            // 6. Demonstrate resolution in the “root” (no explicit scope)
            //    Since we registered OperationConsumer as Transient, each GetRequiredService
            //    returns a new instance — but note how lifetimes behave for each service type.
            Console.WriteLine("=== From Root ServiceProvider (no custom scope) ===");
            var consumer1 = serviceProvider.GetRequiredService<OperationConsumer>();
            consumer1.ShowOperationIds("Root Resolve #1");

            var consumer2 = serviceProvider.GetRequiredService<OperationConsumer>();
            consumer2.ShowOperationIds("Root Resolve #2");

            // 7. Now demonstrate Scoped behavior by creating a child scope:
            using (var scopeA = serviceProvider.CreateScope())
            {
                Console.WriteLine("=== From Scope A ===");

                // Within the same scope, scoped service IDs remain the same,
                // but transient IDs change on each resolution call:
                var scopedConsumerA1 = scopeA.ServiceProvider.GetRequiredService<OperationConsumer>();
                scopedConsumerA1.ShowOperationIds("Scope A → Resolve #1");

                var scopedConsumerA2 = scopeA.ServiceProvider.GetRequiredService<OperationConsumer>();
                scopedConsumerA2.ShowOperationIds("Scope A → Resolve #2");
            }

            // 8. Create a second scope to show that scoped services differ across scopes:
            using (var scopeB = serviceProvider.CreateScope())
            {
                Console.WriteLine("=== From Scope B ===");

                var scopedConsumerB = scopeB.ServiceProvider.GetRequiredService<OperationConsumer>();
                scopedConsumerB.ShowOperationIds("Scope B → Resolve #1");
            }

            // 9. Finally, show once more from the root to confirm Singleton behavior:
            Console.WriteLine("=== Back to Root (confirming Singleton persists) ===");
            var consumer3 = serviceProvider.GetRequiredService<OperationConsumer>();
            consumer3.ShowOperationIds("Root Resolve #3");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
