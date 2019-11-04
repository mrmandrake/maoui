using System;
using Maoui;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BugSweeperPage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Forms.Init();
            UI.Publish("/", new BugsPage().GetMaouiElement());
        }
    }
}
