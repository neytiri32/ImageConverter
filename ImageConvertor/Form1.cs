using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageConvertor
{
    public partial class Form1 : Form
    {

        private Bitmap m_bitmap = null;

        public Form1()
        {
            InitializeComponent();
        }

        private Bitmap ResizeBitmap(Bitmap img)
        {
            int newWidth = 0;
            int newHeight = 0;
            double imgRatio;

            if (img.Width > img.Height)
            {
                imgRatio = ((double)img.Height / (double)img.Width) * 100;
                newWidth = pictureBox1.Width;
                newHeight = (int)(((double)newWidth / 100) * imgRatio);
            }
            else
            {
                imgRatio = ((double)img.Width / (double)img.Height) * 100;
                newHeight = pictureBox1.Height;
                newWidth = (int)(((double)newHeight / 100) * imgRatio);
            }

            Bitmap newImg = new Bitmap(newWidth, newHeight);

            using (Graphics g = Graphics.FromImage(newImg))
                g.DrawImage(img, 0, 0, newWidth, newHeight);

            return newImg;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open image...";
            ofd.Filter = comboBox1.Text + "|" + comboBox1.Text;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                m_bitmap = new Bitmap(ofd.FileName, true);
                Bitmap resized = ResizeBitmap(m_bitmap);
                pictureBox1.Image = resized;
            }    
        }

        private void SaveAsButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save image as...";
            sfd.Filter = comboBox2.Text + "|" + comboBox2.Text;

            if (m_bitmap == null)
            {
                MessageBox.Show("You first need to upload image.", 
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            }
            else if (sfd.ShowDialog() == DialogResult.OK)
            {
                m_bitmap.Save(sfd.FileName);
                MessageBox.Show("Picture saved",
                "Info",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }
        }
    }
}
