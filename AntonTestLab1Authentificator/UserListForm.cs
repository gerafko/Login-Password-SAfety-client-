using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AntonTestLab1Authentificator
{
    public partial class UserListForm : Form
    {
        StoredData Users { get; set; }
        int CurrentUser { get; set; }

        public void ValidateControls()
        {
            if (CurrentUser < Users.Users.Count)
            {
                textBoxName.Text = Users.Users[CurrentUser].Name;
                checkBoxBlocked.Checked = Users.Users[CurrentUser].Blocked;
                checkBoxPasswordRestricted.Checked =
                    Users.Users[CurrentUser].PasswordRestriction;
            }
        }

        public void ValidateData()
        {
            Users.Users[CurrentUser].Name = textBoxName.Text;
            Users.Users[CurrentUser].Blocked = checkBoxBlocked.Checked;
            Users.Users[CurrentUser].PasswordRestriction =
                checkBoxPasswordRestricted.Checked;
                ;
        }

        public UserListForm()
        {
            InitializeComponent();
        }

        private void UserListForm_Load(object sender, EventArgs e)
        {
            StoredData users;
            StoredData.LoadUserList(StoredData.FileName, out users);
            Users = users;

            CurrentUser = 0;

            ValidateControls();

        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (CurrentUser == 0)
                CurrentUser = Users.Users.Count - 1;
            else
                CurrentUser--;

            ValidateControls();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (CurrentUser == Users.Users.Count - 1)
                CurrentUser = 0;
            else
                CurrentUser++;

            ValidateControls();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Users.SaveToFile(StoredData.FileName);
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonFirst_Click(object sender, EventArgs e)
        {
            CurrentUser = 0;
            ValidateControls();
        }

        private void buttonLast_Click(object sender, EventArgs e)
        {
            CurrentUser = Users.Users.Count - 1;
            ValidateControls();
        }


    }
}
