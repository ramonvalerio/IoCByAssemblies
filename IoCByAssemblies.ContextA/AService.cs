using System;

namespace IoCByAssemblies.ContextA
{
    public class AService : IAService
    {
        public void Write()
        {
            Console.WriteLine("Service A!");
        }
    }
}