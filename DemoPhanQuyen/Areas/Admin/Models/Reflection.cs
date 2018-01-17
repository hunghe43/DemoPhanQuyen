using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace DemoPhanQuyen.Areas.Admin.Models
{
    public class Reflection
    {
        //lấy danh sách các Controller
        public List<Type> GetController(string namespaces)
        {
            List<Type> ls = new List<Type>();
            Assembly asm = Assembly.GetExecutingAssembly();

            IEnumerable<Type> types = asm.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type) && type.Namespace.Contains(namespaces)).OrderBy(x => x.Name);
            return types.ToList();

        }
        // lấy danh sách các action theo controller
        public List<string> GetAction(Type controller)
        {
            List<string> listAction = new List<string>();
            IEnumerable<MemberInfo> mems = controller.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public).Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any()).OrderBy(x => x.Name);
            foreach (MemberInfo method in mems)
            {
                if (method.ReflectedType.IsPublic && !method.IsDefined(typeof(NonActionAttribute)))
                {
                    listAction.Add(method.Name.ToString());
                }
            }
            return listAction;
        }
    }
}