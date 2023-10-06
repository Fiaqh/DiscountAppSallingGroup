using SallingApiClientAndParser;
using SallingApiJsonClass;

namespace DiscountAppSallingGroup
{
    internal class DiscountAppController
    {
        private string Varen_er_sat_ned_med = "Varen er sat ned med ";
        private string procent_fra = "% fra ";
        private string til = " til ";
        private string denKanFindesPå = "Den kan findes på ";
        private string Midlertidigt_nedsatte_varer_i_FØTEXER_i = "Midlertidigt nedsatte varer i FØTEX i ";
        private string Midlertidigt_nedsatte_varer_i = "Midlertidigt nedsatte varer i ";
        SallingApiClient api = new SallingApiClient();
        public DiscountAppController()
        {

        }

        //Returns string descriping discount information and location of the relevant store
        public string GetDiscountText(Clearance c, DiscountProducts p)
        {
            return Varen_er_sat_ned_med + c.offer.percentDiscount + procent_fra + c.offer.originalPrice
                          + " " + c.offer.currency + til + c.offer.newPrice + " " + c.offer.currency + "!\n"
                          + denKanFindesPå + p.store.address.street;
        }

        //Gets and parses Json data from SallingGroupApi
        public List<DiscountProducts> GetDiscountProducts(string zipCode)
        {
            //Makes a Json parser to parse the response from the API GET request.
            JsonParser parser = new JsonParser(api.GetApiResponseAsync(zipCode).Result);

            //Parses the Json string and stores the result in a list
            List<DiscountProducts> products = parser.ParseJsonText();

            return products;
        }

        //Updates the Y coordinate information when adding a label
        public int UpdateLabelPositionY(int labelPositionY, Label label)
        {
            return labelPositionY + label.Height;
        }

        //Adds label to left side of panel at desired Y position
        public void AddLabelToPanel(Label label, Panel panel, int currentLabelPositionY)
        {
            label.Location = new Point(panel.Location.X, currentLabelPositionY);
            panel.Controls.Add(label);
        }

        //Sets button text, location and adds the button to a panel
        public void AddPictureButtonToPanel(Button pictureButton, Panel panel, int currentLabelPositionY)
        {
            pictureButton.Location = new Point(panel.Location.X, currentLabelPositionY);
            panel.Controls.Add(pictureButton);
        }

        //Updates the label position when adding a button
        public int UpdateButtonPosition(Button pictureButton, int currentLabelPositionY)
        {
            return currentLabelPositionY + pictureButton.Height + 15;
        }

        //Adds the text to the header of the panel. Since the API writes "Føtex" as "foetex" we need to handle this event seperatly
        public string GetHeaderText(string store, string zipCode)
        {
            if (store == "foetex")
            {
                return Midlertidigt_nedsatte_varer_i_FØTEXER_i + zipCode + ":";
            }
            else
            {
                return Midlertidigt_nedsatte_varer_i + store.ToUpper() + " i " + zipCode + ":";
            }
        }

        //Makes a seperate window to view images from imageUrl stored in the button tag.
        public void ButtonClick(object sender, EventArgs e)
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
            }
        }


    }
}
