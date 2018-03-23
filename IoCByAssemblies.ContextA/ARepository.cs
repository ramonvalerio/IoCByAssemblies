using System;

namespace IoCByAssemblies.ContextA
{
    public class ARepository : IARepository
    {
        public void Write()
        {
            Console.WriteLine("Repository A!");
        }
    }
}
