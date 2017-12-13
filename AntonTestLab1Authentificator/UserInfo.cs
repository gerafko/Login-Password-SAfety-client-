using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntonTestLab1Authentificator
{
    [Serializable]
    public class UserInfo : UserWithAuthenticationInfo
    {
        public bool Blocked { get; set; }
        public bool PasswordRestriction { get; set; }
    }
}
