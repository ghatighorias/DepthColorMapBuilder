using System.Windows.Forms;

namespace DepthColorMap
{
    internal class ListViewItemListViewSubItem : ListViewItem.ListViewSubItem
    {
        private string v;

        public ListViewItemListViewSubItem(string v)
        {
            this.v = v;
        }
    }
}