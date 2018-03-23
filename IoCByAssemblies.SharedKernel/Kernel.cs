using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IoCByAssemblies.SharedKernel
{
    public abstract class Kernel
    {
        private static Dictionary<string, IContainer> _containers;

        static Kernel()
        {
            _containers = new Dictionary<string, IContainer>();
        }

        public static void RegisterAssemblyName(string assemblyName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.GetName().Name == assemblyName).ToArray();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(assemblies).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblies).Where(x => x.Name.EndsWith("Factory")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblies).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblies).Where(x => x.Name.EndsWith("Cache")).AsImplementedInterfaces().SingleInstance();

            _containers.Add(assemblyName, builder.Build());
        }

        public static T GetInstance<T>()
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