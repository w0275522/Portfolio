using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace CarInfoForm
{
    public partial class Form1 : Form
    {
        //instance of the db
        Db db;

        //keep track of if we are editing a record
        bool currentlyEditing = false;

        //English == True, French == false
        bool currentLang = true;
        public Form1()
        {
            InitializeComponent();
            db = new Db();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbmLanguage.Items.Add("English");
            cbmLanguage.Items.Add("French");
            cbmLanguage.SelectedIndex = 0;
            cbmType.SelectedIndex = 0;
            printDocument1.PrintPage += printDocument1_PrintPage;
        }



        private void cbmLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbmLanguage.SelectedItem.ToString() == "English" || cbmLanguage.SelectedItem.ToString() == "Anglais")
            {
                currentLang = true;

                //Get selected index, change the contents, reset index
                cbmLanguage.Items.Clear();
                cbmLanguage.Items.Add("English");
                cbmLanguage.Items.Add("French");
                ChangeLanguage("en");
            }
            else if (cbmLanguage.SelectedItem.ToString() == "French" || cbmLanguage.SelectedItem.ToString() == "français")
            {
                currentLang = false;
                cbmLanguage.Items.Clear();
                cbmLanguage.Items.Add("Anglais");
                cbmLanguage.Items.Add("français");
                ChangeLanguage("fr-CA");
            } 


        }

        private void ChangeLanguage(string lang)
        {
            foreach (Control c in this.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
                resources.ApplyResources(c, c.Name, new CultureInfo(lang));
                if (c.ToString().StartsWith("System.Windows.Forms.GroupBox"))
                {
                    foreach (Control child in c.Controls)
                    {
                        ComponentResourceManager resources_child = new ComponentResourceManager(typeof(Form1));
                        resources_child.ApplyResources(child, child.Name, new CultureInfo(lang));
                    }
                }
                foreach (ToolStripItem item in menuStrip1.Items)
                {
                    if (item is ToolStripDropDownItem)
                        foreach (ToolStripItem dropDownItem in ((ToolStripDropDownItem)item).DropDownItems)
                        {
                            resources.ApplyResources(dropDownItem, dropDownItem.Name, new CultureInfo(lang));
                        }
                    //Also apply resources to main toolstrip items. 
                    resources.ApplyResources(item, item.Name, new CultureInfo(lang));
                }

            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Exit the application
            Application.Exit();
        }

        

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (currentlyEditing == true)
            {
                var chosenCar = db.cars.Where(p => p.VIN == txtVin.Text);

                foreach (var car in chosenCar)
                {
                    ((Car)car).Model = txtModel.Text;
                    ((Car)car).Make = txtMake.Text;
                    ((Car)car).Year = txtYear.Text;
                    ((Car)car).type = cbmType.Text;
                    ((Car)car).VIN = txtVin.Text;
                }
                db.SubmitChanges();
                currentlyEditing = false;
            }
            else
            {
                //Save the data
                SaveCar();
            }
            //Clear the text boxes
            ClearTextBoxes();
        }

        private void SaveCar()
        {
            if (cbmType.SelectedItem.ToString() == "T")
            {
                Truck truck = new Truck();
                truck.Model = txtModel.Text;
                truck.Make = txtMake.Text;
                truck.Year = txtYear.Text;
                truck.type = cbmType.SelectedItem.ToString();
                truck.VIN = txtVin.Text;
                truck.Axles = txtAxles.Text;
                truck.Tonnage = txtTonnage.Text;
                //Save the object
                db.GetTable<Car>().InsertOnSubmit(truck);
            }
            else if (cbmType.SelectedItem.ToString() == "P")
            {
                Passenger passenger = new Passenger();
                passenger.Model = txtModel.Text;
                passenger.Make = txtMake.Text;
                passenger.Year = txtYear.Text;
                passenger.type = cbmType.SelectedItem.ToString();
                passenger.VIN = txtVin.Text;
                passenger.TrimCode = txtTrimCode.Text;
                //Save the object
                db.GetTable<Car>().InsertOnSubmit(passenger);
            }
            else if (cbmType.SelectedItem.ToString() == "C")
            {
                Car car = new Car();
                car.Model = txtModel.Text;
                car.Make = txtMake.Text;
                car.Year = txtYear.Text;
                car.type = cbmType.SelectedItem.ToString();
                car.VIN = txtVin.Text;
                //Save the object
                db.GetTable<Car>().InsertOnSubmit(car);
            }
            //Send off the object to the database
            db.SubmitChanges();
        }

        private void ClearTextBoxes()
        {
            //method called to clear all the text in the text boxes
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };

            func(Controls);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteRecord();
        }

        public void deleteRecord()
        {
            if (currentlyEditing == true)
            {
                IEnumerable<Car> searchedCar = from s in db.cars
                                               where s.VIN == txtVin.Text
                                               select s;

                db.GetTable<Car>().DeleteAllOnSubmit(searchedCar);
                db.SubmitChanges();
                currentlyEditing = false;
                ClearTextBoxes();
            }
            


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            //Search the database using the VIN number
            IEnumerable<Car> searchedCar = from s in db.cars
                                           where s.VIN.ToString() == txtVin.Text
                                           select s;
            
            //fill the form with the search values
            if (searchedCar.Count() >= 1)
            {
                foreach (Car car in searchedCar)
                {

                    //tell the app we are currently editing
                    currentlyEditing = true;
                    //set the text fields to have to have the searched car in it
                    txtModel.Text = car.Model;
                    txtMake.Text = car.Make;
                    txtYear.Text = car.Year;
                    //Set the type C P T
                    if (car.type == "C")
                    {
                        cbmType.SelectedItem = 0;
                    }
                    else if (car.type == "P")
                    {
                        cbmType.SelectedItem = 1;
                    }
                    else if (car.type == "T")
                    {
                        cbmType.SelectedItem = 2;
                    }
                    txtVin.Text = car.VIN;

                }
            }
            else
            {
                if (currentLang == true)
                {
                    MessageBox.Show("There was no car found.");
                }
                else
                {
                    MessageBox.Show("il n'y a pas de voiture trouvé.");
                }
            }
        }

        void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            int y = e.MarginBounds.Top;
            int x0 = e.MarginBounds.Left;
            int increment = 130;
            int x2;

            Car car;
            using (Font font = new Font("Times New Roman", 20))
            {
                e.Graphics.DrawString("VIN", font, Brushes.Red, x0, y);
                e.Graphics.DrawString("Make", font, Brushes.Red, x0 + increment, y);
                e.Graphics.DrawString("Model", font, Brushes.Red, x0 + (2 * increment), y);
                e.Graphics.DrawString("Year", font, Brushes.Red, x0 + (3 * increment), y);
                e.Graphics.DrawString("Type", font, Brushes.Red, x0 + (4 * increment), y);

                //draw horizontal line
                x2 = x0 + (5 * increment) + 20;
                e.Graphics.DrawLine(Pens.Green, x0 - 10, y + (int)(font.Size * 1.5), x2, y + (int)(font.Size * 1.3));


                y += (int)(font.Size * 1.5);
                IEnumerator<Car> ppl = db.GetTable<Car>().GetEnumerator();
                while (ppl.MoveNext())
                {
                    car = ppl.Current;
                    e.Graphics.DrawString(car.VIN, font, Brushes.Black, x0, y);
                    e.Graphics.DrawString(car.Make, font, Brushes.Black, x0 + increment, y);
                    e.Graphics.DrawString(car.Model, font, Brushes.Black, x0 + (2 * increment), y);
                    e.Graphics.DrawString(car.Year, font, Brushes.Black, x0 + (3 * increment), y);
                    e.Graphics.DrawString(car.type, font, Brushes.Black, x0 + (4 * increment), y);


                    y += (int)(font.Size * 2);
                }
            }
            //draw enclosing rectangle
            float width = x2 - x0 + 10;
            float height = y - e.MarginBounds.Top + 10;
            e.Graphics.DrawRectangle(Pens.Green, x0 - 10, e.MarginBounds.Top - 10, width, height);
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }
    }
}
