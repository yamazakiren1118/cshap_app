using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace multMemoTest2._6
{
    public partial class RenTextBox : UserControl
    {
        private int renIndexValue;
        private bool renTextBoxClick = false;
        private int renTextBoxPositionDafault;
        private int renTextBoxPosition;
        private int renTextBoxPositionDafaultScreen;
        private int renTextBoxScroll;



        public string getText()
        {
            return this.richTextBox1.Text;
        }

        public void setText(string s)
        {
            this.richTextBox1.Text = s;
        }

        public RenTextBox()
        {
            InitializeComponent();
        }


        [DefaultValue(0)]
        [Browsable(true)]
        public int renIndex
        {
            get
            {
                return this.renIndexValue;
            }

            set
            {
                this.renIndexValue = value;
            }
        }

        private void RenTextBox_MouseMove(object sender, MouseEventArgs e)
        {
            int abs = Math.Abs(renTextBoxPositionDafaultScreen - PointToScreen(new Point(e.X, e.Y)).Y);
            if (renTextBoxClick && abs > 10)
            {
                renTextBoxPosition = e.Y - renTextBoxPositionDafault + this.Location.Y;
                if (renTextBoxPosition >= 0)
                {
                    this.Location = new Point(this.Location.X, renTextBoxPosition);
                }
            }
        }

        private void RenTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            renTextBoxClick = false;
            int abs = Math.Abs(renTextBoxPositionDafaultScreen - PointToScreen(new Point(e.X, e.Y)).Y);
            if (abs > 10)
            {
                Common.getAllTextBox();
                Common.delAllTextBox();
                Common.sortAllTextBox();
                Common.showAllTextBox();
            }
            Common.backScroll(renTextBoxScroll);

        }

        private void RenTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            renTextBoxClick = true;
            // e.Locationで取得する座標はイベントが発生した要素内での座標
            renTextBoxPositionDafault = e.Y;
            renTextBoxPositionDafaultScreen = PointToScreen(new Point(e.X, e.Y)).Y;
            renTextBoxScroll = Common.getPanel1().AutoScrollPosition.Y;
            //最前面へ移動
            this.BringToFront();
            
        }

        private void del_Click(object sender, EventArgs e)
        {
            renTextBoxScroll = Common.getPanel1().AutoScrollPosition.Y;
            Common.delTextBox(this);
            Common.getAllTextBox();
            Common.delAllTextBox();
            Common.sortAllTextBox();
            Common.showAllTextBox();
            Common.backScroll(renTextBoxScroll);
        }
    }
}
