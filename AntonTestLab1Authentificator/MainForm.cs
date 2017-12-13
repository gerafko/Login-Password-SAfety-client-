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
    public partial class MainForm : Form
    {
        public UserWithAuthenticationInfo CurrentUser { get; set; }

        public AuthenticationStates AuthenticationState
        {
            get;
            set;
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void userListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserListForm userListForm = new UserListForm();
            userListForm.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            switch (AuthenticationState)
            {
                case AuthenticationStates.User:
                    this.addUserToolStripMenuItem.Enabled = false;
                    this.userListAsOneTableToolStripMenuItem.Enabled = false;
                    this.userListToolStripMenuItem.Enabled = false;

                    this.Text = String.Format("Main Form: User Mode[{0}]", 
                        CurrentUser.Name);
                    break;
                case AuthenticationStates.Admin:
                    this.Text = String.Format("Main Form: Admin Mode[{0}]",
                        CurrentUser.Name);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckPasswordForm checkPasswordForm = new CheckPasswordForm()
            {
                Password = this.CurrentUser.Password
            };

            checkPasswordForm.ShowDialog();
            if (checkPasswordForm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                ChangePasswordForm cpf = new ChangePasswordForm();

                if (AuthenticationState == AuthenticationStates.User)
                    cpf.UserInfo = CurrentUser as UserInfo;
                else
                    cpf.UserInfo = null;

                cpf.ShowDialog();

                if (cpf.DialogResult != System.Windows.Forms.DialogResult.OK)
                    return;

                CurrentUser.Password = cpf.Password;
                StoredData data;
                StoredData.LoadUserList(StoredData.FileName, out data);
                if (this.AuthenticationState == AuthenticationStates.User)
                    data.Users.First(u =>
                        u.Name.Equals(CurrentUser.Name)).
                        Password =
                        CurrentUser.Password;
                else
                    data.Admin.Password = CurrentUser.Password;

                data.SaveToFile(StoredData.FileName);
            }

        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUserForm cnuf = new AddUserForm();
            cnuf.ShowDialog();
        }

        private void userListAsOneTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserListAsOneTableForm userListForm = new UserListAsOneTableForm();
            userListForm.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }
    }
}
