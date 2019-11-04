﻿using System;
using System.ComponentModel;
using Maoui.Forms.Extensions;
using Maoui.Forms.Renderers;
using Xamarin.Forms;

namespace Maoui.Forms.Cells
{
    public class SwitchCellElement : CellElement
    {
        public Label TextLabel { get; } = new Label();
        public SwitchRenderer.SwitchElement Switch { get; } = new SwitchRenderer.SwitchElement();

        public SwitchCellElement()
        {
            AppendChild(TextLabel);
            AppendChild(Switch);

            Switch.Style.Display = "inline-block";

            Switch.Change += Switch_Change;
        }

        protected override void BindCell()
        {
            Cell.PropertyChanged += Cell_PropertyChanged;

            if (Cell is SwitchCell cell)
            {
                UpdateText(cell);
                UpdateOn(cell);
            }

            base.BindCell();
        }

        protected override void UnbindCell()
        {
            Cell.PropertyChanged -= Cell_PropertyChanged;

            base.UnbindCell();
        }

        void Cell_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var cell = (SwitchCell)sender;

            if (e.PropertyName == SwitchCell.TextProperty.PropertyName)
                UpdateText(cell);
            else if (e.PropertyName == SwitchCell.OnProperty.PropertyName)
                UpdateOn(cell);
        }

        void UpdateText(SwitchCell cell)
        {
            TextLabel.Text = cell.Text ?? string.Empty;
        }

        void UpdateOn(SwitchCell cell)
        {
            Switch.IsChecked = cell.On;
        }

        void Switch_Change(object sender, EventArgs e)
        {
            if (Cell is SwitchCell cell)
                cell.On = Switch.IsChecked;
        }
    }
}
