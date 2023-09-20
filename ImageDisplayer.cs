using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscountAppSallingGroup
{
    public partial class ImageDisplayer : Form
    {
        public ImageDisplayer(string url)
        {
            InitializeComponent();

            //Display the image from url on the form
            LoadImage(url);
        }

        private void LoadImage(string imageUrl)
        {
            try
            {
                // Create a PictureBox control and set its properties
                PictureBox pictureBox = new PictureBox();
                pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBox.ImageLocation = imageUrl;
                // Add the PictureBox control to the form's controls
                this.Controls.Add(pictureBox);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message);
            }
        }

        private void ImageDisplayer_Load(object sender, EventArgs e)
        {

        }
    }
}
