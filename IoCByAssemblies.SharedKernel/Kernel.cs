using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IoCByAssemblies.SharedKernel
{
    public abstract class Kernel
    {
        private readonly static Dictionary<string, IContainer> _containers;

        static Kernel()
        {
            _containers = new Dictionary<string, IContainer>();
        }

        public static void RegisterAssembliesBySolutionName(string solutionName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains(solutionName)).ToArray();

            foreach (var assembly in assemblies)
            {
                var builder = new ContainerBuilder();

                builder.RegisterAssemblyTypes(assembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces();
                builder.RegisterAssemblyTypes(assembly).Where(x => x.Name.EndsWith("Factory")).AsImplementedInterfaces();
                builder.RegisterAssemblyTypes(assembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces();
                builder.RegisterAssemblyTypes(assembly).Where(x => x.Name.EndsWith("Cache")).AsImplementedInterfaces().SingleInstance();

                var container = builder.Build();

                _containers.Add(assembly.GetName().Name, container);
            }
        }

        public static T GetInstance<T>() where T : class
        {
            var assemblyName = typeof(T).Assembly.GetName().Name;

            if (!_containers.ContainsKey(assemblyName))
                throw new Exception("Instance not found.");

            if (!_containers[assemblyName].IsRegistered<T>())
                throw new Exception("Instance not found.");

            return _containers[assemblyName].Resolve<T>();
        }
    }
}