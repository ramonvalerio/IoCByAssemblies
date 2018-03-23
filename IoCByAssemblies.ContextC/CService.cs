using System;

namespace IoCByAssemblies.ContextC
{
    public class CService : ICService
    {
        public void Write()
        {
            Console.WriteLine("Service C!");
        }
    }
}