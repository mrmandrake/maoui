﻿using System;
using System.ComponentModel;
using Maoui.Forms.Extensions;
using Xamarin.Forms;

namespace Maoui.Forms.Cells
{
    public class TextCellElement : CellElement
    {
        public Label TextLabel { get; } = new Label();

        public Label DetailTextLabel { get; } = new Label();

        public TextCellElement()
        {
            AppendChild(TextLabel);
            AppendChild(DetailTextLabel);
        }

        protected override void BindCell()
        {
            Cell.PropertyChanged += Cell_PropertyChanged;

            if (Cell is TextCell textCell)
            {
                TextLabel.Text = textCell.Text ?? string.Empty;
                DetailTextLabel.Text = textCell.Detail ?? string.Empty;
                TextLabel.Style.Color = textCell.TextColor.ToMaouiColor(MaouiTheme.TextColor);
                DetailTextLabel.Style.Color = textCell.DetailColor.ToMaouiColor(MaouiTheme.SecondaryTextColor);
            }

            base.BindCell();
        }

        protected override void UnbindCell()
        {
            Cell.PropertyChanged -= Cell_PropertyChanged;
            base.UnbindCell();
        }

        void Cell_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (!(Cell is TextCell textCell))
                return;

            if (args.PropertyName == TextCell.TextProperty.PropertyName)
                TextLabel.Text = textCell.Text ?? string.Empty;
            else if (args.PropertyName == TextCell.DetailProperty.PropertyName)
                DetailTextLabel.Text = textCell.Detail ?? string.Empty;
            else if (args.PropertyName == TextCell.TextColorProperty.PropertyName)
                TextLabel.Style.Color = textCell.TextColor.ToMaouiColor(MaouiTheme.TextColor);
            else if (args.PropertyName == TextCell.DetailColorProperty.PropertyName)
                DetailTextLabel.Style.Color = textCell.DetailColor.ToMaouiColor(MaouiTheme.SecondaryTextColor);
        }
    }
}
