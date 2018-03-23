using System;

namespace IoCByAssemblies.ContextC
{
    public class AService : IAService
    {
        public void Write()
        {
            Console.WriteLine("Service A by Context C!");
        }
    }
}