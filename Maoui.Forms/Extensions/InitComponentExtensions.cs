using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maoui.Forms
{
    public static class InitializeComponentExtension
    {
        public static void InitializeComponent(this VisualElement el)
        {   
            var tp = el.GetType();
            var asm = el.GetType().Assembly;
            using (var s = asm.GetManifestResourceStream($"{tp.Namespace}.{tp.Name}.xaml"))
            {
                if (s == null)
                    throw new Exception($"Missing resource {asm.FullName}.{tp.Name}.xaml");

                using (var r = new StreamReader(s))
                    global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(el, r.ReadToEnd());
            }
        }
    }
}
