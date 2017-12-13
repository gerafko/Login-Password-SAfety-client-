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
    public partial class UserListAsOneTableForm : Form
    {
        StoredData Users { get; set; }


        public UserListAsOneTableForm()
        {
            InitializeComponent();
        }

        private void UserListAsOneTableForm_Load(object sender, EventArgs e)
        {
            StoredData users;
            StoredData.LoadUserList(StoredData.FileName, out users);
            Users = users;


            var list = new BindingList<UserInfo>(Users.Users);
            dataGridViewUsers.DataSource = list;

            dataGridViewUsers.Columns["Name"].ReadOnly = true;
            dataGridViewUsers.Columns["Password"].ReadOnly = true;
            //dataGridViewUsers.Columns.Remove(
            //    dataGridViewUsers.Columns["Password"]);
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
    }
}
