using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GNIDA
{
    public partial class Load : Form
    {
        public ListView ldrs() { return this.listView1; }
        public ListView dsmrs() { return this.listView2; }
        public Load()
        {
            InitializeComponent();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            button1.Enabled = (listView1.SelectedItems.Count == 1) & (listView2.SelectedItems.Count == 1);
        }

        private void Load_Shown(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) listView1.Items[0].Selected = true;
            if (listView2.SelectedItems.Count == 0) listView2.Items[0].Selected = true;
        }
    }
}
