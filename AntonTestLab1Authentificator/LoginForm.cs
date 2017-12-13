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
    public partial class LoginForm : Form
    {
        public UserWithAuthenticationInfo CurrentUser { get; set; }
        StoredData StoredData { get; set; }
        int TriesLeft { get; set; }
        
        public AuthenticationStates AuthenticationState
        {
            get;
            set;
        }

        public LoginForm()
        {
            TriesLeft = 3;
            AuthenticationState = AuthenticationStates.None;

            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            string userName = textBoxUser.Text;
            string password = textBoxPassword.Text;

            if (StoredData.Admin.Name.Equals(userName))
            {
                if (StoredData.Admin.Password == null)
                {
                    ChangePasswordForm cpf = new ChangePasswordForm();
                    cpf.ShowDialog();

                    if (cpf.DialogResult != System.Windows.Forms.DialogResult.OK)
                        return;

                    StoredData.Admin.Password = cpf.Password;
                    StoredData.SaveToFile(StoredData.FileName);

                    // start as admin
                    CurrentUser = StoredData.Admin;
                    AuthenticationState = AuthenticationStates.Admin;
                    this.Close();
    
                }

                if (StoredData.Admin.Password.Equals(password))
                {
                    // start as admin
                    CurrentUser = StoredData.Admin;
                    AuthenticationState = AuthenticationStates.Admin;
                    this.Close();
                }
                else
                {
                    TreatIncorrectPassword();
                }
                return;
            }

            if (StoredData.Users.Any(u => u.Name.Equals(userName)))
            {
                UserInfo user = StoredData.Users.First(u =>
                    u.Name.Equals(userName));

              


                if (user.Password == null)
                {
                    if (user.Blocked)
                    {
                        MessageBox.Show("the user name is blocked.");
                        return;
                    }

                    ChangePasswordForm cpf = new ChangePasswordForm();
                    cpf.UserInfo = user as UserInfo;
                    cpf.ShowDialog();

                    if (cpf.DialogResult != System.Windows.Forms.DialogResult.OK)
                        return;

                    user.Password = cpf.Password;
                    StoredData.SaveToFile(StoredData.FileName);

                    // start as the selected user
                    CurrentUser = user;
                    AuthenticationState = AuthenticationStates.User;
                    this.Close();

                }
                

                if (user.Password.Equals(password))
                {
                    if (user.Blocked)
                    {
                        MessageBox.Show("the user name is blocked.");
                        return;
                    }

                    // start as the selected user
                    CurrentUser = user;
                    AuthenticationState = AuthenticationStates.User;
                    this.Close();
                }
                else
                {
                    TreatIncorrectPassword();
                }
                return;
                
            }
            
            MessageBox.Show("Username doesn't exist. Please try again");
            
        }

        private void TreatIncorrectPassword()
        {
            String msg = string.Format("Password is incorrect " +
                                       "{0} tries left", --TriesLeft);
            MessageBox.Show(msg);

            if (TriesLeft == 0)
                this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            AuthenticationState = AuthenticationStates.None;
            this.Close();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            
            this.textBoxUser.Text = "admin";
            //this.textBoxUser.Text = "xxx";

            StoredData users;
            bool firstRun = !StoredData.LoadUserList(StoredData.FileName, out users);

            if (!firstRun)
            {
                StoredData = users;
            }
            else
            {
                StoredData ui = StoredData.CreateFirstRunStoredData();
                ui.SaveToFile(StoredData.FileName);
                StoredData.LoadUserList(StoredData.FileName, out users);
                StoredData = users;
            }
        }
    }
}
