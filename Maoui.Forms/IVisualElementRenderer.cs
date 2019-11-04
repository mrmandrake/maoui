﻿using System;
using Xamarin.Forms;

namespace Maoui.Forms
{
    public interface IVisualElementRenderer : IRegisterable, IDisposable
    {
        event EventHandler<VisualElementChangedEventArgs> ElementChanged;

        VisualElement Element { get; }

        Maoui.Element NativeView { get; }

        void SetElement(VisualElement element);

        void SetElementSize(Size size);

        SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint);

        void SetControlSize(Size size);
    }
}
