using IoCByAssemblies.ContextA;
using IoCByAssemblies.ContextC;
using IoCByAssemblies.SharedKernel;
using System;

namespace IoCByAssemblies.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            Kernel.RegisterAssembliesBySolutionName("IoCByAssemblies");

            var serviceA = Kernel.GetInstance<ContextA.IAService>();
            var serviceByContextC = Kernel.GetInstance<ContextC.IAService>();
            var serviceC = Kernel.GetInstance<ICService>();
            var repositoryA = Kernel.GetInstance<IARepository>();

            serviceA?.Write();
            serviceByContextC?.Write();
            serviceC?.Write();
            repositoryA?.Write();

            Console.ReadLine();
        }
    }
}