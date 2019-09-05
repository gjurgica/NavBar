using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
   public class NavBarClass
    {
        public string ID { get; set; }
        public string MenuName { get; set; }
        public string ParentID { get; set; }
        public string isHidden { get; set; }
        public string LinkURL { get; set; }

        public List<NavBarClass> Children = new List<NavBarClass>();
    }
}
