using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoPhanQuyen.Areas.Admin.Models.EF
{
    public class MyViewModel
    {
        public List<Role> l_role { get; set; }
        public List<Group_Permission> l_per_group { get; set; }

        public List<UserGroup> l_usergroup { get; set; }
        public List<NguoiDung> l_user { get; set; }
        public List<User_Permission> l_per_user { get; set; }
    }
}