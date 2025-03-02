﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maoui.Forms.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Maoui.Forms
{
    public class Platform : BindableObject, INavigation, IDisposable
#pragma warning disable CS0618 // Type or member is obsolete
        , IPlatform
#pragma warning restore CS0618 // Type or member is obsolete
    {
        bool _disposed;

        readonly PlatformRenderer _renderer;

        public Maoui.Element Element => _renderer;

        public Page Page { get; private set; }

        IReadOnlyList<Page> INavigation.ModalStack => throw new NotImplementedException();

        IReadOnlyList<Page> INavigation.NavigationStack => throw new NotImplementedException();

        public static readonly BindableProperty RendererProperty = BindableProperty.CreateAttached("Renderer", typeof(IVisualElementRenderer), typeof(Platform), default(IVisualElementRenderer),
            propertyChanged: (bindable, oldvalue, newvalue) =>
            {
                var view = bindable as VisualElement;
                if (view != null)
                    view.IsPlatformEnabled = newvalue != null;
            });

        public Platform()
        {
            _renderer = new PlatformRenderer(this);

            _renderer.Style.PropertyChanged += HandleRendererStyle_PropertyChanged;

            MessagingCenter.Subscribe(this, Page.AlertSignalName, (Page sender, AlertArguments arguments) =>
            {
                var alert = new DisplayAlert(arguments);
                alert.Clicked += CloseAlert;

                _renderer.AppendChild(alert.Element);

                void CloseAlert(object s, EventArgs e)
                {
                    _renderer.RemoveChild(alert.Element);
                }
            });

            MessagingCenter.Subscribe(this, Page.ActionSheetSignalName, (Page sender, ActionSheetArguments arguments) =>
            {
                var sheet = new ActionSheet(arguments);
                sheet.Clicked += CloseSheet;

                _renderer.AppendChild(sheet.Element);

                void CloseSheet(object s, EventArgs e)
                {
                    _renderer.RemoveChild(sheet.Element);
                }
            });
        }

        void IDisposable.Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;

            MessagingCenter.Unsubscribe<Page, ActionSheetArguments>(this, Page.ActionSheetSignalName);
            MessagingCenter.Unsubscribe<Page, AlertArguments>(this, Page.AlertSignalName);
            MessagingCenter.Unsubscribe<Page, bool>(this, Page.BusySetSignalName);

            DisposeModelAndChildrenRenderers(Page);
        }

        public static IVisualElementRenderer CreateRenderer(VisualElement element)
        {
            var renderer = Registrar.Registered.GetHandler<IVisualElementRenderer>(element.GetType()) ?? new DefaultRenderer();
            renderer.SetElement(element);
            return renderer;
        }

        public static IVisualElementRenderer GetRenderer(VisualElement bindable)
        {
            return (IVisualElementRenderer)bindable.GetValue(RendererProperty);
        }

        public static void SetRenderer(VisualElement bindable, IVisualElementRenderer value)
        {
            bindable.SetValue(RendererProperty, value);
        }

        protected override void OnBindingContextChanged()
        {
            SetInheritedBindingContext(Page, BindingContext);

            base.OnBindingContextChanged();
        }

        public static SizeRequest GetNativeSize(VisualElement view, double widthConstraint, double heightConstraint)
        {
            var renderView = GetRenderer(view);
            if (renderView == null || renderView.NativeView == null)
                return new SizeRequest(Size.Zero);

            return renderView.GetDesiredSize(widthConstraint, heightConstraint);
        }

        public void SetPage(Page newRoot)
        {
            if (newRoot == null)
                return;
            if (Page != null)
                throw new NotImplementedException();
            Page = newRoot;

#pragma warning disable CS0618 // Type or member is obsolete
            // The Platform property is no longer necessary, but we have to set it because some third-party
            // library might still be retrieving it and using it
            Page.Platform = this;
#pragma warning restore CS0618 // Type or member is obsolete
            AddChild(Page);

            Page.DescendantRemoved += HandleChildRemoved;

            Application.Current.NavigationProxy.Inner = this;
        }

        void HandleChildRemoved(object sender, ElementEventArgs e)
        {
            var view = e.Element;
            DisposeModelAndChildrenRenderers(view);
        }

        void DisposeModelAndChildrenRenderers(Xamarin.Forms.Element view)
        {
            IVisualElementRenderer renderer;
            foreach (VisualElement child in view.Descendants())
            {
                renderer = GetRenderer(child);
                child.ClearValue(RendererProperty);

                if (renderer != null)
                    renderer.Dispose();
            }

            renderer = GetRenderer((VisualElement)view);
            if (renderer != null)
                renderer.Dispose();

            view.ClearValue(RendererProperty);
        }

        void AddChild(VisualElement view)
        {
            if (!Application.IsApplicationOrNull(view.RealParent))
                System.Diagnostics.Debug.WriteLine("Tried to add parented view to canvas directly");

            if (GetRenderer(view) == null)
            {
                var viewRenderer = CreateRenderer(view);
                SetRenderer(view, viewRenderer);

                _renderer.AppendChild(viewRenderer.NativeView);
                viewRenderer.SetElementSize(new Size(640, 480));
            }
            else
                System.Diagnostics.Debug.WriteLine("Potential view double add");
        }

        void HandleRendererStyle_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var pageRenderer = GetRenderer(Page);
            pageRenderer?.SetElementSize(Maoui.Forms.Extensions.ElementExtensions.GetSizeRequest(_renderer, double.PositiveInfinity, double.PositiveInfinity).Request);
        }

        void INavigation.InsertPageBefore(Page page, Page before)
        {
            throw new NotImplementedException();
        }

        Task<Page> INavigation.PopAsync()
        {
            throw new NotImplementedException();
        }

        Task<Page> INavigation.PopAsync(bool animated)
        {
            throw new NotImplementedException();
        }

        Task<Page> INavigation.PopModalAsync()
        {
            throw new NotImplementedException();
        }

        Task<Page> INavigation.PopModalAsync(bool animated)
        {
            throw new NotImplementedException();
        }

        Task INavigation.PopToRootAsync()
        {
            throw new NotImplementedException();
        }

        Task INavigation.PopToRootAsync(bool animated)
        {
            throw new NotImplementedException();
        }

        Task INavigation.PushAsync(Page page)
        {
            throw new NotImplementedException();
        }

        Task INavigation.PushAsync(Page page, bool animated)
        {
            throw new NotImplementedException();
        }

        Task INavigation.PushModalAsync(Page page)
        {
            throw new NotImplementedException();
        }

        Task INavigation.PushModalAsync(Page page, bool animated)
        {
            throw new NotImplementedException();
        }

        void INavigation.RemovePage(Page page)
        {
            throw new NotImplementedException();
        }

        #region obsolete
        SizeRequest IPlatform.GetNativeSize(VisualElement view, double widthConstraint, double heightConstraint)
        {
            return GetNativeSize(view, widthConstraint, heightConstraint);
        }
        #endregion
    }
}
