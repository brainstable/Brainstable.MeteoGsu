using System.Collections.ObjectModel;

namespace Brainstable.Filial.Location
{
    public class PlotCollection : Collection<Plot>
    {
        private District district;

        protected override void InsertItem(int index, Plot item)
        {
            base.InsertItem(index, item);
            item.District = district;
        }

        protected override void SetItem(int index, Plot item)
        {
            base.SetItem(index, item);
            item.District = district;
        }

        protected override void RemoveItem(int index)
        {
            this[index].District = null;
            base.RemoveItem(index);
        }

        protected override void ClearItems()
        {
            foreach (Plot plot in this)
            {
                plot.District = null;
            }
            base.ClearItems();
        }
    }
}