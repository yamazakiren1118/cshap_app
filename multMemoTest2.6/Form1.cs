using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;


namespace multMemoTest2._6
{
    public partial class Form1 : Form
    {
        public static Control form;


        public Form1()
        {
            
            InitializeComponent();
            form = this;
            if(Environment.GetCommandLineArgs().Length > 1)
            {
                Common.openFilePath = Environment.GetCommandLineArgs()[1];
                Common.readFile();
            }
        }

        public static Control getForm()
        {
            return form;
        }


        private void add_Click(object sender, EventArgs e)
        {
            Common.addTextBox();

        }

        private void save_Click(object sender, EventArgs e)
        {
            Common.saveFile();
        }

        private void open_Click(object sender, EventArgs e)
        {
            Common.openFile();
        }

        private void delAll_Click(object sender, EventArgs e)
        {
            Common.delAllTextBox();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == (Keys.S | Keys.Control))
            {
                Common.saveFile();
            }
        }
    }
}
