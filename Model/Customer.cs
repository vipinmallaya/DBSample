using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace DBSample.Model
{
    public class Customer
    {
        [PrimaryKey]
        public long Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int Age { get; set; } 
    }
}