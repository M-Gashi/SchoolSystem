using SchoolSystem.Enums;
using SchoolSystem.Presentation.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

using SchoolSystem.Business;
using SchoolSystem.Business.People;

namespace SchoolSystem.Presentation.People
{
    public partial class ctrPerson : UserControl
    {
        public  ctrPerson()
        {
            InitializeComponent();
        }

        private clsPerson _Person;

        private int       _PersonID = -1;

        public  int       personID
        {
            get { return _PersonID; }
        }

        //_________________________________________________________________________________________________________
        public void resetPersonInfo()
        {
            _PersonID           = -1;
            lblPersonID.Text    = "";
            lblFirstName.Text    = "";
            lblCountry.Text     = "";
            lblDateOfBirth.Text = "";         
            lblGender.Text      = "";
            lblEmail.Text       = "";
            lblPhone.Text       = "";     
            lblAddress.Text     = "";
            //pbPersonImage.Image = Resources.Male_512;
        }
        //_______________________________________________________
        private void _LoadPersonImage()
        {
         
            string ImagePath = _Person.imagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        //_______________________________________________________
        private void _FillPersonInfo()
        {
           //llEditPhoto.Enabled = true;
            _PersonID           = _Person.personID;
            lblPersonID.Text    = _Person.personID.ToString();
            lblFirstName.Text   = _Person.firstName;
            lblMidelName.Text   = _Person.middleName;
            lblLastName.Text    = _Person.lastName;
            lblDateOfBirth.Text = _Person.birthDate.ToShortDateString();
            lblGender.Text      = _Person.gender.ToString();
            lblPhone.Text       = _Person.phone;
            lblEmail.Text       = _Person.email;
            lblDateOfBirth.Text = _Person.birthDate.ToShortDateString();
            lblAddress.Text     = _Person.address;
            lblCountry.Text     = clsCountry.find(_Person.countryID).countryName;

            _LoadPersonImage();
        }
        //_______________________________________________________
        public void loadPersonInfo(int PersonID)
        {
            _Person = clsPerson.find(PersonID);
            if (_Person == null)
            {
                resetPersonInfo();
                MessageBox.Show("No Person with PersonID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        private void ctrPerson_Load(object sender, EventArgs e)
        {

        }

        private void lblPersonID_Click(object sender, EventArgs e)
        {

        }

        private void pbPersonImage_Click(object sender, EventArgs e)
        {

        }


        //_______________________________________________________
    }
}
