using System;
using Xamarin.Forms;

namespace Maoui.Forms.Cells
{
    public class SwitchCellRenderer : CellRenderer
    {
        protected override CellElement CreateCellElement(Cell cell)
        {
            return new SwitchCellElement();
        }
    }
}
