﻿using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Maoui.Forms.Extensions;
using Maoui.Forms.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Maoui.Forms.Cells
{
    public class ImageCellElement : CellElement
    {
        public Image ImageView { get; } = new Image();

        public Label TextLabel { get; } = new Label();

        public Label DetailTextLabel { get; } = new Label();

        public ImageCellElement()
        {
            AppendChild(ImageView);
            AppendChild(TextLabel);
            AppendChild(DetailTextLabel);
        }

        protected override void BindCell()
        {
            Cell.PropertyChanged += Cell_PropertyChanged;

            if (Cell is ImageCell cell)
            {
                TextLabel.Text = cell.Text ?? string.Empty;
                DetailTextLabel.Text = cell.Detail ?? string.Empty;
                TextLabel.Style.Color = cell.TextColor.ToMaouiColor(MaouiTheme.TextColor);
                DetailTextLabel.Style.Color = cell.DetailColor.ToMaouiColor(MaouiTheme.SecondaryTextColor);
            }

            base.BindCell();
        }

        protected override void UnbindCell()
        {
            Cell.PropertyChanged -= Cell_PropertyChanged;

            base.UnbindCell();
        }

        async void Cell_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (!(Cell is ImageCell cell))
                return;

            if (args.PropertyName == TextCell.TextProperty.PropertyName)
                TextLabel.Text = cell.Text ?? string.Empty;
            else if (args.PropertyName == TextCell.DetailProperty.PropertyName)
                DetailTextLabel.Text = cell.Detail ?? string.Empty;
            else if (args.PropertyName == TextCell.TextColorProperty.PropertyName)
                TextLabel.Style.Color = cell.TextColor.ToMaouiColor(MaouiTheme.TextColor);
            else if (args.PropertyName == TextCell.DetailColorProperty.PropertyName)
                DetailTextLabel.Style.Color = cell.DetailColor.ToMaouiColor(MaouiTheme.SecondaryTextColor);
            else if (args.PropertyName == ImageCell.ImageSourceProperty.PropertyName)
                await SetImage(cell.ImageSource).ConfigureAwait(false);
        }

        async Task SetImage(ImageSource source)
        {
            ImageView.Source = null;

            IImageSourceHandler handler;

            if (source != null && (handler = Registrar.Registered.GetHandlerForObject<Renderers.IImageSourceHandler>(source)) != null)
            {
                string image;
                try
                {
                    image = await handler.LoadImageAsync(source).ConfigureAwait(false);
                }
                catch (TaskCanceledException)
                {
                    image = null;
                }
                ImageView.Source = image;
            }
            else
            {
                ImageView.Source = null;
            }
        }
    }
}
