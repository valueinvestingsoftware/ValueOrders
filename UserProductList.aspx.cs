using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Configuration;

namespace ValueOrders
{
    public partial class UserProductList : System.Web.UI.Page
    {

        DataSet dataSet = null;
        string servidor;
        string catalogo;
        string desencriptado;
        string encriptado;

        public SqlConnection cn;

        protected void Page_Load(object sender, EventArgs e)
        {
            String str = ConfigurationManager.ConnectionStrings["StocksConnectionString"].ConnectionString;
            NovoDataset();
            LeerXml();
            Connecting(str);

            string emailUser = Session["username"].ToString();
            
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }                            
                                  
                        string cadena = "SELECT dbo.CatMap.Category As ItemName, dbo.SoldItems_Sync.SaleQuantity, dbo.SoldItems_Sync.SalePrice, dbo.SoldItems_Sync.SaleDate, dbo.OrderStatus.StatusName, dbo.OrderStatus.OrderId AS StatusId, dbo.SoldItems_Sync.OrderId, dbo.CatMap.ImagePath AS Image" +
                         " FROM dbo.SoldItems_Sync INNER JOIN dbo.CatMap ON dbo.SoldItems_Sync.itemid = dbo.CatMap.id INNER JOIN" +
                        " dbo.Map ON dbo.SoldItems_Sync.ClientId = dbo.Map.id INNER JOIN dbo.OrderStatus ON dbo.SoldItems_Sync.StatusId = dbo.OrderStatus.id" +
                        " Where dbo.SoldItems_Sync.OrderId IS NOT NULL AND dbo.OrderStatus.LanguageId = 1 AND Map.Email = '" + emailUser + "' Order By dbo.SoldItems_Sync.SaleDate DESC";
                        SqlDataAdapter da = new SqlDataAdapter(cadena, cn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                       
                        GridViewProductList.DataSource = dt;
                        GridViewProductList.DataBind();               


            }

            if (GridViewProductList.Rows.Count.ToString() == "0")
            {
                Response.Write("<script>alert('You have no orders yet!')</script>");
            }
            else
            {
               
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                switch(e.Row.Cells[7].Text)
                {
                    case "1":
                        e.Row.Cells[5].ForeColor = System.Drawing.Color.White;
                        e.Row.Cells[5].BackColor = System.Drawing.Color.Red;
                        break;
                    case "2":
                        e.Row.Cells[5].ForeColor = System.Drawing.Color.White;
                        e.Row.Cells[5].BackColor = System.Drawing.Color.Orange;
                        break;
                    case "3":
                        e.Row.Cells[5].ForeColor = System.Drawing.Color.White;
                        e.Row.Cells[5].BackColor = System.Drawing.Color.Yellow;
                        break;
                    case "4":
                        e.Row.Cells[5].ForeColor = System.Drawing.Color.White;
                        e.Row.Cells[5].BackColor = System.Drawing.Color.Blue;
                        break;
                    default:
                        e.Row.Cells[5].ForeColor = System.Drawing.Color.White;
                        e.Row.Cells[5].BackColor = System.Drawing.Color.Green;
                        break;
                }

                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[6].Visible = false;
                GridViewProductList.HeaderRow.Cells[6].Visible = false;

                e.Row.Cells[8].Visible = false;
                GridViewProductList.HeaderRow.Cells[8].Visible = false;
            }
        }

        private void NovoDataset()
        {
            try
            {
                dataSet = null;
                dataSet = new DataSet();
                dataSet.Tables.Add();
                dataSet.Tables[0].Columns.Add("servidor", typeof(string));
                dataSet.Tables[0].Columns.Add("catalogo", typeof(string));
                dataSet.Tables[0].Columns.Add("token", typeof(string));
            }
            catch (Exception ex)
            {
                // eventViewerLog.Log(ex, EventLogEntryType.Error)

            }
        }

        public void Connecting(String str)
        {
            cn = new SqlConnection(str);
        }

        public void LeerXml()
        {
            XmlDocument documentoXML;
            XmlNodeList nodeList;
            System.Xml.XmlNode nodo;
            documentoXML = new XmlDocument();
            documentoXML.Load(Request.PhysicalApplicationPath + @"\Configuration.xml");
            nodeList = documentoXML.SelectNodes("NewDataSet/Table1");
            foreach (System.Xml.XmlNode nd in nodeList)
            {
                var nodo1 = nd.ChildNodes[0].InnerText;
                var nodo2 = nd.ChildNodes[1].InnerText;
                var nodo3 = nd.ChildNodes[2].InnerText;
                servidor = nodo1;
                catalogo = Desencriptar(nodo2);
            }
        }

        private string Desencriptar(string origen)
        {
            byte[] mesBytes = Convert.FromBase64String(origen);
            byte[] keyBytes = Encoding.UTF8.GetBytes("ciro2029");
            DESCryptoServiceProvider crypto = new DESCryptoServiceProvider();
            crypto.Key = keyBytes;
            crypto.IV = keyBytes;
            ICryptoTransform iCrypto = crypto.CreateDecryptor();
            byte[] resultatBytes = iCrypto.TransformFinalBlock(mesBytes, 0, mesBytes.Length);
            desencriptado = Encoding.UTF8.GetString(resultatBytes);

            return desencriptado;
        }

        private string Encriptar(string origen)
        {
            byte[] mesBytes = Encoding.UTF8.GetBytes(origen);
            byte[] keyBytes = Encoding.UTF8.GetBytes("ciro2029");
            DESCryptoServiceProvider crypto = new DESCryptoServiceProvider();
            crypto.Key = keyBytes;
            crypto.IV = keyBytes;
            ICryptoTransform iCrypto = crypto.CreateEncryptor();
            byte[] resultatBytes = iCrypto.TransformFinalBlock(mesBytes, 0, mesBytes.Length);
            encriptado = Convert.ToBase64String(resultatBytes);

            return encriptado;
        }

        protected void GridViewProductList_DataBound(object sender, EventArgs e)
        {

        }

        protected void GridViewProductList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "ViewPDF")
            {
                string saleDate = retrieveOrderDate(e.CommandArgument.ToString());
                string address = retrieveBillingAddress(e.CommandArgument.ToString());
                Session["orderid"] = e.CommandArgument.ToString();
                Session["Orderdate"] = saleDate;
                Session["address"] = address;
                Response.Redirect("Pdf_generate.aspx");
            }
        }

        private string retrieveOrderDate(string orderId)
        {
            string orderDate = string.Empty;

            string cadena = "Select * from SoldItems_Sync WHERE OrderId = '" + orderId + "'";
            SqlCommand cmd = new SqlCommand(cadena, cn);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                orderDate = ds.Tables[0].Rows[0]["SaleDate"].ToString();
            }
            cn.Close();
            return orderDate;
        }

        private string retrieveBillingAddress(string orderId)
        {
            string address = string.Empty;

            string cadena = "Select * from CardDetails WHERE OrderId = '" + orderId + "'";
            SqlCommand cmd = new SqlCommand(cadena, cn);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                address = ds.Tables[0].Rows[0]["BillingAddress"].ToString();
            }
            cn.Close();
            return address;
        }

    }
}