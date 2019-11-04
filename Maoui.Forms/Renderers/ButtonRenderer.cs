using System;
using System.ComponentModel;
using System.Diagnostics;
using Maoui.Forms.Extensions;
using Xamarin.Forms;

namespace Maoui.Forms.Renderers
{
    public class ButtonRenderer : ViewRenderer<Xamarin.Forms.Button, Maoui.Button>
    {
        Maoui.Color _buttonTextColorDefaultDisabled;
        Maoui.Color _buttonTextColorDefaultHighlighted;
        Maoui.Color _buttonTextColorDefaultNormal;

        public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
        {
            var size = Element.Text.MeasureSize(Element.FontFamily, Element.FontSize, Element.FontAttributes, widthConstraint, heightConstraint);
            size = new Size(size.Width + 32, size.Height + 16);
            return new SizeRequest(size, size);
        }

        protected override void Dispose(bool disposing)
        {
            if (Control != null)
            {
                Control.Click -= OnButtonTouchUpInside;
            }

            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    SetNativeControl(new Maoui.Button());

                    Debug.Assert(Control != null, "Control != null");

                    Control.ClassName = "btn btn-primary";

                    _buttonTextColorDefaultNormal = Maoui.Colors.Black;
                    _buttonTextColorDefaultHighlighted = Maoui.Colors.Black;
                    _buttonTextColorDefaultDisabled = Maoui.Colors.Black;

                    Control.Click += OnButtonTouchUpInside;
                }

                UpdateText();
                UpdateFont();
                UpdateBorder();
                UpdateImage();
                UpdateTextColor();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Xamarin.Forms.Button.TextProperty.PropertyName)
                UpdateText();
            else if (e.PropertyName == Xamarin.Forms.Button.TextColorProperty.PropertyName)
                UpdateTextColor();
            else if (e.PropertyName == Xamarin.Forms.Button.FontProperty.PropertyName)
                UpdateFont();
            else if (e.PropertyName == Xamarin.Forms.Button.BorderWidthProperty.PropertyName || e.PropertyName == Xamarin.Forms.Button.CornerRadiusProperty.PropertyName || e.PropertyName == Xamarin.Forms.Button.BorderColorProperty.PropertyName)
                UpdateBorder();
            else if (e.PropertyName == Xamarin.Forms.Button.ImageSourceProperty.PropertyName)
                UpdateImage();
        }

        void OnButtonTouchUpInside(object sender, EventArgs eventArgs)
        {
            ((IButtonController)Element)?.SendPressed();
            ((IButtonController)Element)?.SendReleased();
            ((IButtonController)Element)?.SendClicked();
        }

        void UpdateBorder()
        {
            var uiButton = Control;
            var button = Element;

            if (button.BorderColor != Xamarin.Forms.Color.Default)
                uiButton.Style.BorderColor = button.BorderColor.ToMaouiColor(MaouiTheme.ButtonBorderColor);
            else
                uiButton.Style.BorderColor = null;

            float bw = Math.Max(0f, (float)button.BorderWidth);
            if (bw > 0)
            {
                uiButton.Style.BorderWidth = bw + "px";
            }
            else
            {
                uiButton.Style.BorderWidth = null;
            }

            var br = button.CornerRadius;
            if (br > 0 && (bw > 0 || br != 5))
            { // 5 is the default
                uiButton.Style.BorderRadius = br + "px";
            }
            else
            {
                uiButton.Style.BorderRadius = null;
            }
        }

        void UpdateFont()
        {
            Element.SetStyleFont(Element.FontFamily, Element.FontSize, Element.FontAttributes, Control.Style);
        }

        async void UpdateImage()
        {
            IImageSourceHandler handler;
            ImageSource source = Element.ImageSource;

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

                var Button = Control;
                if (Button != null && uiimage != null)
                {
                    Button.Style.BackgroundImage = uiimage;
                    Button.Style.BackgroundPosition = "center";
                }
            }
            else
            {
                Control.Style.BackgroundImage = null;
                Control.Style.BackgroundPosition = null;
            }
            ((IVisualElementController)Element).NativeSizeChanged();
        }

        void UpdateText()
        {
            var newText = Element.Text;

            if (Control.Text != newText)
            {
                Control.Text = Element.Text;
            }
        }

        void UpdateTextColor()
        {
            Control.Style.Color = Element.TextColor.ToMaouiColor(MaouiTheme.ButtonTextColor);
        }
    }
}
