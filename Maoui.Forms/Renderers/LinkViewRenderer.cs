﻿using System;
using System.ComponentModel;

namespace Maoui.Forms.Renderers
{
    public class LinkViewRenderer : ViewRenderer<LinkView, Maoui.Anchor>
    {
        public LinkViewRenderer()
            : base("a")
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<LinkView> e)
        {
            base.OnElementChanged(e);

            UpdateHRef();
            UpdateTarget();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null)
                return;

            if (e.PropertyName == Maoui.Forms.LinkView.HRefProperty.PropertyName)
                UpdateHRef();

            if (e.PropertyName == Maoui.Forms.LinkView.TargetProperty.PropertyName)
                UpdateTarget();
        }

        void UpdateHRef()
        {
            this.SetAttribute("href", Element.HRef);
        }

        void UpdateTarget()
        {
            this.SetAttribute("target", Element.Target);
        }
    }
}
