using System;
using System.Reflection;
using Maoui;
using Maoui.Forms;

namespace Moana
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine($"loading assmebly {args[0]}");
                Assembly asm = Assembly.LoadFrom(args[0]);
                foreach (Type type in asm.GetTypes())
                {
                    Console.WriteLine($"Found class {type.Name}");
                    if (type.Name.Contains("Program"))
                    {
                        var methodInfo = type.GetMethod("Main");
                        if (methodInfo != null)
                        {
                            var instance = Activator.CreateInstance(type);
                            var result = methodInfo.Invoke(instance, null);
                        }

                        // Maoui.UI.ReceiveWebAssemblySessionMessageJson("sessionid",@"{ ""m"":""event"",""id"":""window"",""k"":""resize"", ""v"":""(1920,1080)""}");

                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Maoui.UI.StartWebAssemblySession(640, 480);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
