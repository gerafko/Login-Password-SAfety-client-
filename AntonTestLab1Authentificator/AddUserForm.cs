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
    public partial class AddUserForm : Form
    {
        public AddUserForm()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text;

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("user name cannot be an empty string. " +
                                "please choose another one");
                return;
            }



            if (name.Equals("admin"))
            {
                MessageBox.Show("user name \"admin\" is reserved. " +
                                "please choose another one");
                return;
            }

            StoredData data;
            StoredData.LoadUserList(StoredData.FileName, out data);

            if (data.Users.Any(u => u.Name.Equals(name)))
            {
                MessageBox.Show("user with such name already exists." +
                                "please choose another one");
                return;
            }

            UserInfo ui = new UserInfo()
            {
                Name = name,
                Password = null,
                Blocked = false,
                PasswordRestriction = false
            };

            data.Users.Add(ui);
            data.SaveToFile(StoredData.FileName);

            MessageBox.Show(string.Format("user {0} added successfully",
                name));
            this.Close();

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
