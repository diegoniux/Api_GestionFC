using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Models
{
    public class LDAP
    {
        public string LDAPService { get; set; }
        public string ADUser { get; set; }
        public string ADPaswsword { get; set; }
        public string KLDAPService { get; set; }
        public string KADUser { get; set; }
        public string KADPassword { get; set; }
        public string Domain { get; set; }
    }
}
