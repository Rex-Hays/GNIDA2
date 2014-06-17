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
        public Load()
        {
            InitializeComponent();
        }

        private void Load_Shown(object sender, EventArgs e)
        {
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            button1.Enabled = (listView1.SelectedItems.Count == 1);
        }
    }
}
