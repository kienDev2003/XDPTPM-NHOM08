﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XDPTPM.client.thanks
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["OrderList"] != null)
            {
                Session.Remove("OrderList");
            }

            if (Session["checkOut"] != null)
            {
                Session.Remove("checkOut");
            }
        }
    }
}