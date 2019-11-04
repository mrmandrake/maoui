using System;
using Maoui;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamlEditor;

namespace EntryNamespace
{
    class EntryClass
    {       
        static void EntryPoint()
        {
            Forms.Init();
            var page = new XamlEditorPage();
            UI.Publish("/", page.GetMaouiElement());
        }
    }
}