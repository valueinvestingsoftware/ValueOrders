using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ValueOrders
{
    public partial class Default : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            dt = (DataTable)Session["buyitems"];
            if (dt != null)
            {
                lblItemsInCart.Text = dt.Rows.Count.ToString();
            }
            else
            {
                lblItemsInCart.Text = "0";
            }
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            Timer1.Enabled = true;
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {

            try
            {
                 Random ran = new Random();
            int i = ran.Next(1, 5);
            string rutaImagen = "~/ImagesWebSite/" + i.ToString() + ".jpg";
            ImageesToDisplay.ImageUrl = rutaImagen;
                  
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }

                

        }

        protected void IBGoToCart_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("AddToCart.aspx");
        }

        
    }
}