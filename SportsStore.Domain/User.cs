//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Store.Domain
{
    public partial class User
    {
        public User()
        {
            this.Roles = new HashSet<Role>();
        }
    
        public int UserID { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string CustomerId { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
    
}