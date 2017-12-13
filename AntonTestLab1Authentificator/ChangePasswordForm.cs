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
    public partial class ChangePasswordForm : Form
    {
        public UserInfo UserInfo { get; set; }

        public string Password { get; set; }
        public ChangePasswordForm()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxPassword.Text))
            {
                MessageBox.Show("password cannot be set to null");
                return;
            }
            if (textBoxPassword.Text.Equals(textBoxPasswordConfirm.Text))
            {
                if (UserInfo != null && UserInfo.PasswordRestriction)
                {
                    bool validPassword = StoredData.CheckPassword(textBoxPassword.Text);
                    if (!validPassword)
                    {
                        MessageBox.Show("password doesn't satisfy " +
                                        "the password policy");
                        return;
                    }
                }


                Password = textBoxPassword.Text;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("Passwords don't match!");
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
