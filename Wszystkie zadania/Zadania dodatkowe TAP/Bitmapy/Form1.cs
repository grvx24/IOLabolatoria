using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bitmapy
{
    public partial class Form1 : Form
    {
        BitmapController bmpController = new BitmapController();

        public Form1()
        {
            InitializeComponent();
        }


        private void BrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;


            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                try
                {
                    PathTextBox.Text = openFileDialog1.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }


        }

        private void LoadImgButton_Click(object sender, EventArgs e)
        {
            try
            {
                MainPictureBox.Image=bmpController.LoadBitmap(PathTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            
        }

        private async void LoadAsyncButton_Click(object sender, EventArgs e)
        {
            try
            {
                MainPictureBox.Image = await bmpController.LoadBitmapAsync(PathTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            
        }
    }
}
