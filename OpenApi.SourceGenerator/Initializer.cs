using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;

namespace OpenApi.SourceGenerator
{
    internal class Initializer
    {
        [ModuleInitializer]
        public static void Initialize()
        {
            Assembly assembly = typeof(Initializer).Assembly;
            AppDomain.CurrentDomain.UnhandledException += OnException;
            AppDomain.CurrentDomain.FirstChanceException += OnFirstChanceException;
            AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoaded;
        }

        private static void OnAssemblyLoaded(object sender, AssemblyLoadEventArgs args)
        {
        }

        private static void OnFirstChanceException(object sender, FirstChanceExceptionEventArgs e)
        {
        }

        private static void OnException(object sender, UnhandledExceptionEventArgs e)
        {
        }
    }
}
