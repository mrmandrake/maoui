using System;
using Maoui;
using Xamarin.Forms;

namespace sample
{
    class Program
    {
        public static void Main()
        {
            Forms.Init();            
            Maoui.UI.Publish("/", new DisplayAlertPage().GetMaouiElement());
        }
    }
}
