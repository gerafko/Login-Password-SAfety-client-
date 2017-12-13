using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;  

namespace AntonTestLab1Authentificator
{
    static class Program
    {
        static bool createTestFile = false;

        static void CreateTestFile()
        {
            StoredData ui = StoredData.CreateTestUserList();

            ui.SaveToFile(StoredData.FileName);

        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // StoredData ui = StoredData.LoadUserList(StoredData.FileName);            

            if (createTestFile)
            {
                CreateTestFile();
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoginForm loginForm = new LoginForm();
            Application.Run(loginForm);

            switch (loginForm.AuthenticationState)
            {
                case AuthenticationStates.User:
                    Application.Run(new MainForm()
                    {
                        AuthenticationState = AuthenticationStates.User,
                        CurrentUser = loginForm.CurrentUser
                    });
                    break;
                case AuthenticationStates.Admin:
                    Application.Run(new MainForm()
                    {
                        AuthenticationState = AuthenticationStates.Admin,
                        CurrentUser = loginForm.CurrentUser
                    });
                    break;
                case AuthenticationStates.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
