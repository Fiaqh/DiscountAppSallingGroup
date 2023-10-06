using SallingApiJsonClass;

namespace DiscountAppSallingGroup
{
    public partial class DiscountAppMainWindow : Form
    {
        public string zipCode = "";
        public string store = "";
        private DiscountAppController controller;

        public DiscountAppMainWindow()
        {
            controller = new DiscountAppController();
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Stores user input for desired zipcode
            zipCode = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //stores user input for desired store name. Since the api labels the store "Føtex" as "foetex" we handle this event seperatly.
            if (textBox2.Text.ToLower() == "føtex")
            {
                store = "foetex";
            }
            else
            {
                store = textBox2.Text;
            }

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //Clears current panel on button click
            panel1.Controls.Clear();

            //Gets the DiscountProducts from the Salling group API
            List<DiscountProducts> discountProducts = controller.GetDiscountProducts(zipCode);

            //Keeps track of the horizontal height of labels in panel1
            int currentLabelPositionY = panel1.Location.Y;

            //If there is a user input for store we make a header customized for the input store.
            if (store != "")
            {
                Label header = new Label();
                header.AutoSize = true;
                header.Font = new Font("Arial", 18, FontStyle.Regular);
                header.Location = new Point(panel1.Location.X, panel1.Location.Y - 50);
                header.Text = controller.GetHeaderText(store, zipCode);
                panel1.Controls.Add(header);
            }


            foreach (DiscountProducts product in discountProducts)
            {
                //Checks if any user input for store. If so, skips any products listed in other stores.
                if (store != "")
                {
                    if (product.store.brand != store.ToLower())
                    {
                        continue;
                    }
                }
                //Iterate through all clearances from the api response.
                foreach (Clearance clearance in product.clearances)
                {

                    //Make labels and button
                    Label description = new Label();
                    Label discount = new Label();
                    Button pictureButton = new Button();

                    //Enable AutoSize
                    description.AutoSize = true;
                    discount.AutoSize = true;
                    pictureButton.AutoSize = true;

                    //Set description and discount labels text and locations. Then update currentLabelPositionY to account for the label height. 
                    //Important to add the label before updating label position due to autosize!
                    description.Text = clearance.product.description;
                    controller.AddLabelToPanel(description, panel1, currentLabelPositionY);
                    currentLabelPositionY = controller.UpdateLabelPositionY(currentLabelPositionY, description);

                    discount.Text = controller.GetDiscountText(clearance, product);
                    controller.AddLabelToPanel(discount, panel1, currentLabelPositionY);
                    currentLabelPositionY = controller.UpdateLabelPositionY(currentLabelPositionY, discount);

                    //Sets pictureButton text and add it to the panel
                    pictureButton.Text = "Se billede af varen";
                    controller.AddPictureButtonToPanel(pictureButton, panel1, currentLabelPositionY);
                    currentLabelPositionY = controller.UpdateButtonPosition(pictureButton, currentLabelPositionY);

                    // Handle the button click event
                    pictureButton.Click += controller.ButtonClick;

                    // Set the image URL as a tag property of the button, to be used in the Button_Click method
                    pictureButton.Tag = clearance.product.image;

                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}