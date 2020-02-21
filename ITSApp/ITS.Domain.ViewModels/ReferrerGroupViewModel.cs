using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLModel =  ITS.Domain.Models;

namespace ITS.Domain.ViewModels
{
    public class ReferrerGroupViewModel
    {

        public string UserName { get; set; }
        public int GroupID { get; set; }
        public int UserID { get; set; }
        public string GroupName { get; set; }
        public int ReferrerID { get; set; }
        public IList<DLModel.UserModel.User> Users { get; set; }
    }
}
