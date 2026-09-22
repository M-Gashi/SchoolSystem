using SchoolSystem.Enums;
using SchoolSystem.Business;
using SchoolSystem.Presentation.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SchoolSystem.Business.People;

namespace SchoolSystem.Presentation.People
{
    public partial class frmAddUpdatePerson : Form
    {
        public delegate void dataBackEventHandler(object sender, int personID);
        public event dataBackEventHandler dataBack;
        
        //_______________________________
        public enum enMode { addNew = 0, update = 1 };
        public enum enGendor { male = 0, female = 1 };
        //_______________________________
        clsPerson _Person;

        private enMode _Mode;

        private int    _PersonID = -1;
        //_______________________________
        public frmAddUpdatePerson()
        {
            InitializeComponent();

            _Mode = enMode.addNew;
        }
        public frmAddUpdatePerson(int personID)
        {
            InitializeComponent();

            _PersonID = personID;
            _Mode = enMode.update;
        }
        //_________________________________________________________________________________________________________
        //_________________________________________________________________
        private void _LoadData()
        {

            _Person = clsPerson.find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            //the following code will not be executed if the person was not found
            lblPersonID.Text     = _PersonID.ToString();
            txtFirstName.Text    = _Person.firstName;
            txtMiddleName.Text   = _Person.middleName;
            txtLastName.Text     = _Person.lastName;
            dtpDateOfBirth.Value = _Person.birthDate;

            if (_Person.gender == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            txtPhone.Text        = _Person.phone;
            txtEmail.Text        = _Person.email;
            txtAddress.Text      = _Person.address;

            if (_Person.CountryInfo != null)
            {
                cbCountry.SelectedIndex = cbCountry.FindString(_Person.CountryInfo.countryName);
            }
           

            //load person image incase it was set.
            if (_Person.imagePath != "")
            {
                pbPersonImage.ImageLocation = _Person.imagePath;

            }

            //hide/show the remove linke incase there is no image for the person.
            llRemoveImage.Visible = (_Person.imagePath != "");

        }
        //_________________________________________________________________________________________________________
        private void _FillCountriesInComoboBox()
        {
            DataTable dtCountries = clsCountry.getAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }
        }
        //_________________________________________________________________
        private void _ResetDefualtValues()
        {
            _FillCountriesInComoboBox();

            if (_Mode == enMode.addNew)
            {
                lblTitle.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                lblTitle.Text = "Update Person";
            }
            //________________________________

            //we set the max date to 18 years from today, and set the default value the same.
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            //should not allow adding age more than 100 years
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
            //________________________________
            //this will set default country to jordan.
            cbCountry.SelectedIndex = cbCountry.FindString("Turkey");
            //________________________________

            txtFirstName.Text = "";
            txtMiddleName.Text = "";
            txtLastName.Text = "";
            rbMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";


        }
        //_________________________________________________________________
        //________________________________
        //_________________
        //_______
        private void frmAddUpdateStudent_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.update)
                _LoadData();
        }
        //_______
        //_________________
        //________________________________
        //_________________________________________________________________
        private void btnSaveNewEditP_Click(object sender, EventArgs e)
        {
            int NationalityCountryID = clsCountry.find(cbCountry.Text).ID;

            _Person.firstName  = txtFirstName.Text.Trim();
            _Person.middleName = txtMiddleName.Text.Trim();
            _Person.lastName   = txtLastName.Text.Trim();
            _Person.birthDate  = dtpDateOfBirth.Value;
            _Person.phone      = txtPhone.Text.Trim();
            _Person.email      = txtEmail.Text.Trim();
            _Person.address    = txtAddress.Text.Trim();

            if (rbMale.Checked)
                _Person.gender = (short)enGendor.male;
            else
                _Person.gender = (short)enGendor.female;

            _Person.countryID  = NationalityCountryID;

            if (pbPersonImage.ImageLocation != null)
                _Person.imagePath = pbPersonImage.ImageLocation;
            else
                _Person.imagePath = "";
            //_______________________________
            if (_Person.save())
            {
                lblPersonID.Text = _Person.personID.ToString();
                //change form mode to update.
                _Mode = enMode.update;
                lblTitle.Text = "Update Person";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);


                dataBack?.Invoke(this, _Person.personID);
                
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }
        //_________________________________________________________________
        private void btnCloseNewEditP_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                pbPersonImage.Load(selectedFilePath);
                llRemoveImage.Visible = true;
                // ...
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;


            llRemoveImage.Visible = false;
        }

       

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

      
    }
}
