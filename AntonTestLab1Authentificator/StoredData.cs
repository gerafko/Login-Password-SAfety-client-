using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AntonTestLab1Authentificator
{
    [Serializable]
    class StoredData
    {
        public const string FileName = "userlist";

        public UserWithAuthenticationInfo Admin { get; set; }
        public List<UserInfo> Users { get; set; }

        public StoredData()
        {
            Users = new List<UserInfo>();
            Admin = new UserWithAuthenticationInfo();
        }

        public void SaveToFile(string fileName)
        {
            Stream testFileStream = File.Create(fileName);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(testFileStream, this);
            testFileStream.Close();  
        }

        public static bool LoadUserList(string fileName, out StoredData res)
        {
            if (File.Exists(fileName))
            {
                Stream testFileStream = File.OpenRead(fileName);
                BinaryFormatter deserializer = new BinaryFormatter();
                res = (StoredData)deserializer.Deserialize(testFileStream);
                testFileStream.Close();
                return true;
            }
            
            res = null;
            return false;
        }

        public static StoredData CreateFirstRunStoredData()
        {
            StoredData res = new StoredData();

            res.Admin = new UserWithAuthenticationInfo()
            {
                Name = "admin",
                Password = null
            };

            return res;
        }

        public static StoredData CreateTestUserList()
        {
            StoredData res = new StoredData();

            res.Admin = new UserWithAuthenticationInfo()
            {
                Name = "Sexgay Simonov",
                Password = "tolyan"
            };

            res.Users.Add(new UserInfo
            {
                Name = "Anton",
                Password = "pass1",
                Blocked = false,
                PasswordRestriction = true
            });

            res.Users.Add(new UserInfo
            {
                Name = "John Doe",
                Password = "123",
                Blocked = false,
                PasswordRestriction = true
            });

            res.Users.Add(new UserInfo
            {
                Name = "Sergey Simonov",
                Password = null,
                Blocked = false,
                PasswordRestriction = true
            });

            res.Users.Add(new UserInfo
            {
                Name = "Anatoliy Markin",
                Password = null,
                Blocked = false,
                PasswordRestriction = true
            });

            return res;
        }

        public static bool CheckPassword(string password)
        {
            if (!password.Any(c => char.IsUpper(c)))
                return false;
            if (!password.Any(c => char.IsLower(c)))
                return false;
            if (!password.Any(c => char.IsPunctuation(c)))
                return false;
            return true;
        }
    }
}
