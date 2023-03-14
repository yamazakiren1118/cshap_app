using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;



namespace multMemoTest2._6
{
    class Common
    {

        public static List<RenTextBox> renTextBoxs = new List<RenTextBox>();
        public static int counts = 1;
        public static string openFilePath = null;
        


        public static void addTextBox()
        {
            Panel panel1 = Common.getPanel1();
            RenTextBox renTextBox = new RenTextBox();
            int y = 0;

            renTextBox.Name = "rentextBox" + counts;
            renTextBox.renIndex = counts;
            panel1.Controls.Add(renTextBox);
            if (renTextBoxs.Count != 0)
            {
                y = renTextBoxs[renTextBoxs.Count - 1].Location.Y + renTextBoxs[renTextBoxs.Count - 1].Size.Height;
                renTextBox.Location = new Point(renTextBox.Location.X, y);
            }
            renTextBoxs.Add(renTextBox);
            counts += 1;
        }

        public static Panel getPanel1()
        {
            Panel panel = (Panel)(Form1.getForm().Controls.Find("panel1", true)[0]);
            return panel;
        }
        public static void delTextBox(RenTextBox c)
        {
            Panel panel1 = Common.getPanel1();
            panel1.Controls.Remove(c);
        }


        public static void delAllTextBox()
        {
            Panel panel1 = Common.getPanel1();
            foreach (RenTextBox c in renTextBoxs)
            {
                panel1.Controls.Remove(c);

            }
        }

        public static void showAllTextBox()
        {
            Panel panel1 = Common.getPanel1();
            RenTextBox prevTextBox = null;
            int count = 0;
            int y = 0;
            foreach(RenTextBox c in renTextBoxs)
            {
                if(count != 0)
                {
                    y = prevTextBox.Location.Y + prevTextBox.Size.Height;
                    c.Location = new Point(c.Location.X, y);
                    panel1.Controls.Add(c);
                    prevTextBox = c;
              
                }
                else
                {
                    c.Location = new Point(c.Location.X, 0);
                    panel1.Controls.Add(c);
                    prevTextBox = c;
                    count = 1;
                    

                }

            }

        }

        public static void getAllTextBox()
        {
            Panel panel1 = Common.getPanel1();
            renTextBoxs.Clear();
            foreach(RenTextBox c in panel1.Controls)
            {
                renTextBoxs.Add(c);
            }
        }

        public static void backScroll(int n)
        {
            Panel panel1 = Common.getPanel1();
            panel1.AutoScrollPosition = new Point(0, -n);
        }

        public static void sortAllTextBox()
        {
            var sort = renTextBoxs.OrderBy(n => n.Location.Y);
            renTextBoxs = sort.ToList<RenTextBox>();
        }

        public static void saveFile()
        {
            List<Json> json = new List<Json>();
            SaveFileDialog dialog = new SaveFileDialog();
            foreach (RenTextBox c in renTextBoxs)
            {
                json.Add(new Json { text = c.getText() });
            }
            if (openFilePath != null)
            {
                File.WriteAllText(openFilePath, JsonSerializer.Serialize(json));
            }
            else
            {
                if(renTextBoxs.Count <= 0)
                {
                    return;
                }
                dialog.FileName = "ドキュメント.txt";
                dialog.Filter = "テキストファイル|*.txt|推奨形式|*.ren.json|すべてのファイル|*.*";
                
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(dialog.FileName, JsonSerializer.Serialize(json));
                    openFilePath = dialog.FileName;
                }
            }
        }

        public static void openFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            StreamReader file;
            List<Json> json;
            Panel panel1 = Common.getPanel1();
            RenTextBox prevTextBox = null;
            int count = 1;
            int y = 0;

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                renTextBoxs.Clear();
                file = new StreamReader(dialog.FileName);
                openFilePath = dialog.FileName;
                
                json = JsonSerializer.Deserialize<List<Json>>(file.ReadToEnd());
                foreach(Json j in json)
                {
                    RenTextBox renTextBox = new RenTextBox();
                    renTextBox.Name = "renTextBox" + count;
                    renTextBox.setText(j.text);
                    if(renTextBoxs.Count != 0)
                    {
                        y = prevTextBox.Location.Y + prevTextBox.Size.Height;
                        renTextBox.Location = new Point(renTextBox.Location.X, y);
                    }
                    panel1.Controls.Add(renTextBox);
                    renTextBoxs.Add(renTextBox);
                    prevTextBox = renTextBox;
                    count += 1;
                }
                file.Close();
            }
        }

        public static void readFile()
        {
            StreamReader file;
            List<Json> json;
            Panel panel1 = Common.getPanel1();
            RenTextBox prevTextBox = null;
            int count = 1;
            int y = 0;
            file = new StreamReader(Common.openFilePath);
            json = JsonSerializer.Deserialize<List<Json>>(file.ReadToEnd());
            foreach (Json j in json)
            {
                RenTextBox renTextBox = new RenTextBox();
                renTextBox.Name = "renTextBox" + count;
                renTextBox.setText(j.text);
                if (renTextBoxs.Count != 0)
                {
                    y = prevTextBox.Location.Y + prevTextBox.Size.Height;
                    renTextBox.Location = new Point(renTextBox.Location.X, y);
                }
                panel1.Controls.Add(renTextBox);
                renTextBoxs.Add(renTextBox);
                prevTextBox = renTextBox;
                count += 1;
            }
            file.Close();
        }
    }
}
