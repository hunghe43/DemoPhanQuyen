using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoPhanQuyen.Areas.Admin.Models.EF
{
    public class UserDAO
    {
        PhanQuyenEntities db = new PhanQuyenEntities();
        public List<String> GetAllPermission(string username)
        {
            var user = db.NguoiDung.Single(n => n.UserName == username);
            var data = (from a in db.Group_Permission
                        join b in db.UserGroup on a.UserGroupID equals b.ID
                        join c in db.Role on a.RoleID equals c.ID
                        where b.ID == user.UserGroupID
                        select new
                        {
                            RoleID = a.RoleID,
                            UserGroupID = a.UserGroupID
                        }).AsEnumerable().Select(n => new Group_Permission()
                        {
                            RoleID = n.RoleID,
                            UserGroupID = n.UserGroupID
                        }
            );
            return data.Select(n => n.RoleID).ToList();
        }
        public List<String> GetUserPermission(string name)
        {
            return db.User_Permission.Where(n => n.UserName == name).Select(n => n.RoleID).ToList();
        }
    }
}