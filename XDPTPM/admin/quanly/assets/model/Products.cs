using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XDPTPM.admin.quanly.assets.model
{
    public class Products
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int UnitID { get; set; }
        public int Price { get; set; }
        public int CompletionTime { get; set; }
        public string ImagePath { get; set; }
        public bool Status { get; set; }
        public int ClassifyID {  get; set; }
    }
}