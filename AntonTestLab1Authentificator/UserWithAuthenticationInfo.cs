using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntonTestLab1Authentificator
{
    [Serializable]
    public class UserWithAuthenticationInfo
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
