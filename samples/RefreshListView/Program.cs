using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Samples
{
    class Program
    {
        public static void Main()
        {
            Forms.Init();
            Maoui.UI.Publish("/", RefreshListView.InstanceFromXaml().GetMaouiElement());
        }
    }
}