using SallingApiClientAndParser;
using SallingApiJsonClass;

namespace DiscountAppSallingGroup
{
    public partial class Form1 : Form
    {
        public string zipCode = "";
        public string store = "";


        public Form1()
        {
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
            //stores user input for desired store name
            store = textBox2.Text;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //Clears current panel on button click
            panel1.Controls.Clear();
            //Makes a HttpClient that contacts the SallingGroup API
            SallingApiClient api = new SallingApiClient(zipCode);
            //Makes a Json parser to parse the response from the API GET request.
            JsonParser parser = new JsonParser(api.GetApiResponseAsync().Result);
            //Parses the Json string and stores the result in a list
            List<DiscountProducts> products = parser.ParseJsonText();
            if(store != "")
            {
                Label header = new Label();
                header.Location = new Point(panel1.Location.X, panel1.Location.Y - 50);
                header.AutoSize = true;
                header.Font = new Font("Arial", 18, FontStyle.Regular);
                header.Text = "Midlertidigt nedsatte varer i " + store + "er i " + zipCode + ":";
                panel1.Controls.Add(header);
            }
            int currentLabelPositionY = panel1.Location.Y;
           
            
            foreach (DiscountProducts p in products)
            {
                //Checks if any userinput for store. If so, skips any products listed in other stores.
                if(store != "")
                {
                    if(p.store.brand != store.ToLower())
                    {
                        continue;
                    }
                }
                //Iterate through all clearances from the api response. Should probably be made into a seperate method at some point... 
                foreach (Clearance c in p.clearances)
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
                    //Important to set add the label before calling .Height due to autosize!
                    description.Text = c.product.description;
                    description.Location = new Point(panel1.Location.X, currentLabelPositionY);
                    panel1.Controls.Add(description);
                    currentLabelPositionY += description.Height;

                    discount.Location = new Point(panel1.Location.X, currentLabelPositionY);
                    discount.Text ="Varen er sat ned med " + c.offer.percentDiscount + "% fra " + c.offer.originalPrice 
                                  + " " + c.offer.currency + " til " + c.offer.newPrice + " " + c.offer.currency + "!\n"
                                  + "Den kan findes på " + p.store.address.street;
                    
                    panel1.Controls.Add(discount);
                    currentLabelPositionY += discount.Height;

                    //Update button text and location. Then update currentLabelPositionY to account for button size.
                    pictureButton.Text = "Se billede af varen";
                    pictureButton.Location = new Point(panel1.Location.X, currentLabelPositionY);
                    panel1.Controls.Add(pictureButton);
                    currentLabelPositionY += pictureButton.Height+15;

                    // Handle the button click event
                    pictureButton.Click += Button_Click;

                    // Set the image URL as a tag property of the button, to be used in the Button_Click method
                    pictureButton.Tag = c.product.image;

                    
                }
            }
        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // Get the URL from Tag of the button
                if (clickedButton.Tag != null)
                {
                    //Gets the url for the image to display.
                    string imageUrl = clickedButton.Tag.ToString();

                    // Makes a seperate window to display the image
                    ImageDisplayer imageForm = new ImageDisplayer(imageUrl);
                    imageForm.ShowDialog();
                }
                else 
                {
                    MessageBox.Show("Der er ikke noget billede af denne vare!");
                }

                
;
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