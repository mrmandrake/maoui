﻿using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Maoui.Forms.Renderers
{
    public class ImageRenderer : ViewRenderer<Xamarin.Forms.Image, Maoui.Image>
    {
        bool _isDisposed;
        double ClientHeight = -1;
        double ClientWidth = -1;

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            base.Dispose(disposing);
        }

        protected override async void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Image> e)
        {
            if (Control == null)
            {
                var imageView = new Maoui.Image();
                SetNativeControl(imageView);
                this.Style.Overflow = "hidden";

                Control.Loaded += OnLoad;
            }

            if (e.NewElement != null)
            {
                SetAspect();
                await TrySetImage(e.OldElement);
                SetOpacity();
            }

            base.OnElementChanged(e);
        }

        protected override async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == Xamarin.Forms.Image.SourceProperty.PropertyName)
                await TrySetImage();
            else if (e.PropertyName == Xamarin.Forms.Image.IsOpaqueProperty.PropertyName)
                SetOpacity();
            else if (e.PropertyName == Xamarin.Forms.Image.AspectProperty.PropertyName)
                SetAspect();
            else if (e.PropertyName == VisualElement.WidthProperty.PropertyName)
                SetDimensions();
        }

        void OnLoad(object sender, EventArgs eventArgs)
        {
            var args = (TargetEventArgs)eventArgs;
            ClientHeight = args.ClientHeight;
            ClientWidth = args.ClientWidth;

            SetDimensions();
        }

        void SetDimensions()
        {
            var b = Element.Bounds;
            double scale = 1;

            if (ClientWidth < 0 || ClientHeight < 0)
                return;

            if (Math.Abs(b.Width) > 0)
            {
                scale = b.Width / ClientWidth;
                Element.WidthRequest = b.Width;
                Element.HeightRequest = scale * ClientHeight;
            }
            else if (Math.Abs(b.Height) > 0)
            {
                scale = b.Height / ClientHeight;
                Element.WidthRequest = scale * ClientWidth;
                Element.HeightRequest = b.Height;
            }
        }

        void SetAspect()
        {
            if (_isDisposed || Element == null || Control == null)
                return;
        }

        protected virtual async Task TrySetImage(Xamarin.Forms.Image previous = null)
        {
            // By default we'll just catch and log any exceptions thrown by SetImage so they don't bring down
            // the application; a custom renderer can override this method and handle exceptions from
            // SetImage differently if it wants to

            try
            {
                await SetImage(previous).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading image: {0}", ex);
            }
            finally
            {
                ((IImageController)Element)?.SetIsLoading(false);
            }
        }

        protected async Task SetImage(Xamarin.Forms.Image oldElement = null)
        {
            if (_isDisposed || Element == null || Control == null)
                return;

            var source = Element.Source;

            if (oldElement != null)
            {
                var oldSource = oldElement.Source;
                if (Equals(oldSource, source))
                    return;

                if (oldSource is FileImageSource && source is FileImageSource && ((FileImageSource)oldSource).File == ((FileImageSource)source).File)
                    return;

                Control.Source = "";
            }

            IImageSourceHandler handler;

            Element.SetIsLoading(true);

            if (source != null &&
                (handler = Xamarin.Forms.Internals.Registrar.Registered.GetHandler<IImageSourceHandler>(source.GetType())) != null)
            {
                string uiimage;
                try
                {
                    uiimage = await handler.LoadImageAsync(source, scale: 1.0f);
                }
                catch (OperationCanceledException)
                {
                    uiimage = null;
                }

                if (_isDisposed)
                    return;

                var imageView = Control;
                if (imageView != null && uiimage != null)
                    imageView.Source = uiimage;

                ((IVisualElementController)Element).NativeSizeChanged();
            }
            else
                Control.Source = "";

            Element.SetIsLoading(false);
        }

        void SetOpacity()
        {
            if (_isDisposed || Element == null || Control == null)
                return;
        }
    }

    public interface IImageSourceHandler : IRegisterable
    {
        Task<string> LoadImageAsync(ImageSource imagesource, CancellationToken cancelationToken = default(CancellationToken), float scale = 1);
    }

    public sealed class FileImageSourceHandler : IImageSourceHandler
    {
#pragma warning disable 1998
        public async Task<string> LoadImageAsync(ImageSource imagesource, CancellationToken cancelationToken = default(CancellationToken), float scale = 1f)
        {
            string image = null;
            var filesource = imagesource as FileImageSource;
            var file = filesource?.File;
            if (!string.IsNullOrEmpty(file))
            {
                var name = System.IO.Path.GetFileName(file);
                image = "/images/" + name;
                if (!Maoui.UI.TryGetFileContentAtPath(image, out var f))
                    await Task.Run(() => Maoui.UI.PublishFile(image, file), cancelationToken);
            }
            return image;
        }
    }

    public sealed class StreamImagesourceHandler : IImageSourceHandler
    {
        public async Task<string> LoadImageAsync(ImageSource imagesource, CancellationToken cancelationToken = default(CancellationToken), float scale = 1f)
        {
            string image = null;
            var streamsource = imagesource as StreamImageSource;
            if (streamsource?.Stream != null)
            {
                using (var streamImage = await ((IStreamImageSource)streamsource).GetStreamAsync(cancelationToken).ConfigureAwait(false))
                {
                    if (streamImage != null)
                    {
                        var data = new byte[streamImage.Length];
                        using (var outputStream = new System.IO.MemoryStream(data))
                        {
                            await streamImage.CopyToAsync(outputStream, 4096, cancelationToken).ConfigureAwait(false);
                        }
                        var hash = Maoui.Utilities.GetShaHash(data);
                        var etag = "\"" + hash + "\"";
                        image = "/images/" + hash;
                        if (!(Maoui.UI.TryGetFileContentAtPath(image, out var file) && file.Etag == etag))
                            Maoui.UI.PublishFile(image, data, etag, "image");
                    }
                }
            }

            if (image == null)
                System.Diagnostics.Debug.WriteLine("Could not load image: {0}", streamsource);

            return image;
        }
    }

    public sealed class ImageLoaderSourceHandler : IImageSourceHandler
    {
        public Task<string> LoadImageAsync(ImageSource imagesource, CancellationToken cancelationToken = default(CancellationToken), float scale = 1f)
        {
            var imageLoader = imagesource as UriImageSource;
            return Task.FromResult(imageLoader?.Uri.ToString() ?? "");
        }
    }
}
