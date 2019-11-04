using System;
using Xamarin.Forms;

namespace Xamarin.Forms
{
    public static class PageExtensions
    {
        public static void Publish(this Xamarin.Forms.Page page, string path)
        {
            Maoui.UI.Publish(path, () => page.CreateElement());
        }

        public static void PublishShared(this Xamarin.Forms.Page page, string path)
        {
            var lazyPage = new Lazy<Maoui.Element>((() => page.CreateElement()), true);
            Maoui.UI.Publish(path, () => lazyPage.Value);
        }

        public static Maoui.Element GetMaouiElement(this Xamarin.Forms.Page page)
        {
            if (!Xamarin.Forms.Forms.IsInitialized)
                throw new InvalidOperationException("call Forms.Init() before this");

            var existingRenderer = Maoui.Forms.Platform.GetRenderer(page);
            if (existingRenderer != null)
                return existingRenderer.NativeView;

            ((IPageController)page).SendAppearing();

            return CreateElement(page);
        }

        static Maoui.Element CreateElement(this Xamarin.Forms.Page page)
        {
            if (!(page.RealParent is Application))
            {
                var app = new DefaultApplication();
                app.MainPage = page;
            }
            var result = new Maoui.Forms.Platform();
            result.SetPage(page);
            return result.Element;
        }

        class DefaultApplication : Application
        {
        }
    }
}
