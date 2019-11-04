using Xamarin.Forms;

namespace Maoui.Forms.Cells
{
    public class ViewCellRenderer : CellRenderer
    {
        protected override CellElement CreateCellElement(Cell cell)
        {
            return new ViewCellElement();
        }
    }
}
