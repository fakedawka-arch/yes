using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Turtuvshn
{
    public partial class Mainform : Form
    {
        public Mainform()
        {
            InitializeComponent();
        }

        private void movieListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Welcome to Movie List");
            MovieForm mo = new MovieForm();
            mo.Show();

            MovieForm m = new MovieForm(); m.Show();



            // ЭНЭ МӨР ДУТУУ БАЙНА:
            m.MdiParent = this;

            m.Show();
        }
    }
}
