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
    public partial class AddToCart : System.Web.UI.Page
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
                      
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                if (Session["buyitems"] == null)
                {
                    btnPlaceOrder.Enabled = false;
                }
                else
                {
                    btnPlaceOrder.Enabled = true;
                }

            
                //Adding product to GridView
                Session["additem"] = "false";
                DataTable dt = new DataTable();

                DataRow dr;
                dt.Columns.Add("pSec");
                dt.Columns.Add("id");
                dt.Columns.Add("Category");
                dt.Columns.Add("ImagePath");
                dt.Columns.Add("Pprice");
                dt.Columns.Add("pQuantity");
                dt.Columns.Add("pTotalPrice");

                if (Request.QueryString["id"] != null)
                {                   
                    if (Session["buyitems"] == null)
                    {
                        dr = dt.NewRow();
                        string cadena = "Select * From CatMap Where id =" + Request.QueryString["id"].ToString();
                        SqlDataAdapter da = new SqlDataAdapter(cadena, cn);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        dr["pSec"] = 1;
                        dr["id"] = ds.Tables[0].Rows[0]["id"].ToString();
                        dr["Category"] = ds.Tables[0].Rows[0]["Category"].ToString();
                        dr["ImagePath"] = ds.Tables[0].Rows[0]["ImagePath"].ToString();
                        dr["Pprice"] = ds.Tables[0].Rows[0]["WebPrice"].ToString();
                        dr["pQuantity"] = Request.QueryString["PQuantity"];

                        double price = Convert.ToDouble(ds.Tables[0].Rows[0]["WebPrice"].ToString());
                        int Quantity = Convert.ToInt16(Request.QueryString["PQuantity"].ToString());
                        double totalPrice = price * Quantity;
                        dr["pTotalPrice"] = totalPrice;

                        dt.Rows.Add(dr);
                        GridViewAddToCart.DataSource = dt;
                        GridViewAddToCart.DataBind();
                        Session["buyitems"] = dt;                        
                        btnPlaceOrder.Enabled = true;

                        GridViewAddToCart.FooterRow.Cells[5].Text = "Total Price";
                        GridViewAddToCart.FooterRow.Cells[6].Text = grandTotal().ToString();
                        Response.Redirect("AddToCart.aspx");
                    }
                    else
                    {
                        dt = (DataTable)Session["buyitems"];
                        int sr;
                        sr = dt.Rows.Count;
                        
                        dr = dt.NewRow();
                        SqlDataAdapter da = new SqlDataAdapter("Select * From CatMap Where id =" + Request.QueryString["id"], cn);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        dr["pSec"] = sr + 1;
                        dr["id"] = ds.Tables[0].Rows[0]["id"].ToString();
                        dr["Category"] = ds.Tables[0].Rows[0]["Category"].ToString();
                        dr["ImagePath"] = ds.Tables[0].Rows[0]["ImagePath"].ToString();
                        dr["Pprice"] = ds.Tables[0].Rows[0]["WebPrice"].ToString();
                        dr["PQuantity"] = Request.QueryString["PQuantity"];

                        double price = Convert.ToDouble(ds.Tables[0].Rows[0]["WebPrice"].ToString());
                        int Quantity = Convert.ToInt16(Request.QueryString["PQuantity"].ToString());
                        double totalPrice = price * Quantity;
                        dr["pTotalPrice"] = totalPrice;

                        dt.Rows.Add(dr);
                        GridViewAddToCart.DataSource = dt;
                        GridViewAddToCart.DataBind();
                        Session["buyitems"] = dt;
                        btnPlaceOrder.Enabled = true;

                        GridViewAddToCart.FooterRow.Cells[5].Text = "Total Price";
                        GridViewAddToCart.FooterRow.Cells[6].Text = grandTotal().ToString();
                        Response.Redirect("AddToCart.aspx");
                    }
                    
                }
                else
                {
                    dt = (DataTable)Session["buyitems"];
                    GridViewAddToCart.DataSource = dt;
                    GridViewAddToCart.DataBind();
                    if (GridViewAddToCart.Rows.Count > 0)
                    {
                        GridViewAddToCart.FooterRow.Cells[5].Text = "Total Amount";
                        GridViewAddToCart.FooterRow.Cells[6].Text = grandTotal().ToString();
                    }
                }
            }

            if (GridViewAddToCart.Rows.Count.ToString() == "0")
            {
                
                btnPlaceOrder.Enabled = false;
                btnPlaceOrder.Text = "Nothing purchased";
}
            else
            {
                btnPlaceOrder.Enabled = true;
            }

            string OrderDate = DateTime.Now.ToString("yyyy-MM-dd");
            Session["Orderdate"] = OrderDate;           
    }
               

        public double grandTotal()
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["buyitems"];
            int nrow = dt.Rows.Count;
            int i = 0;
            double totalprice = 0.0;
            while (i < nrow)
            {
                totalprice = totalprice + Convert.ToDouble(dt.Rows[i]["pTotalPrice"].ToString());
                i = i + 1;
            }
            return totalprice;

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

        protected void GridViewAddToCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["buyitems"];
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                int sr;
                int sr1;
                string qdata;
                string dtdata;
                sr = Convert.ToInt32(dt.Rows[i]["pSec"].ToString());
                TableCell cell = GridViewAddToCart.Rows[e.RowIndex].Cells[0];
                qdata = cell.Text;
                dtdata = sr.ToString();
                sr1 = Convert.ToInt32(qdata);
                if (sr == sr1)
                {
                    dt.Rows[i].Delete();
                    dt.AcceptChanges();
                    //Item Has Been Deleted From Shopping Cart
                    break;
                }
            }

            // 5.Setting sNo. after deleting Row item from cart

            for (int i = 1; i <= dt.Rows.Count; i++)
            {
                dt.Rows[i - 1]["id"] = i;
                dt.AcceptChanges();
            }
            Session["buyitems"] = dt;

            Response.Redirect("AddToCart.aspx");        
    }

        private Int64 RetrieveMaxId()
        {
            Int64 maxId = 0;

            try
            {                
                
                if (cn != null && cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                DataTable dt = new DataTable();
                SqlCommand command = new SqlCommand();
                SqlCommand commandInsert = new SqlCommand();

                // Para que el Combobox se alimenta de la base de datos:
                command.Connection = cn;
                command.CommandType = CommandType.Text;
                string query = @"SELECT MAX(id) AS id FROM SoldItems_Sync";
                command.CommandText = query;
                dt.TableName = "SoldItems_Sync";
                dt.Load(command.ExecuteReader());

                commandInsert.Connection = cn;
                commandInsert.CommandType = CommandType.Text;

                foreach (DataRow Fila in dt.Rows)
                {
                    maxId = (Int64)Fila["id"];                    
                }
                cn.Close();
                return maxId;
            }
            catch (Exception ex)
            {
                cn.Close();
                return maxId;
            }

        }

        private void InsertItems_soldItems_Sync()
        {
            Int64 maxId = RetrieveMaxId();

            try
            {
                if (cn != null && cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                string itemId;
                DataTable dt;
                dt = (DataTable)Session["buyitems"];
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    maxId = maxId + 1;
                    itemId = dt.Rows[i]["id"].ToString();
                    string cadena = "Insert into SoldItems_Sync (id, itemid, ClientId, SaleQuantity, SalePrice, SaleDate, SoldInApp, SoldInWeb, Sincronizado, Observations, OrderId, CodVendedor, StatusId) " +
                        "Values(" + maxId + ", " + itemId + ", " + Session["clientid"] + ", " + dt.Rows[i]["pQuantity"] + ", " + dt.Rows[i]["Pprice"] +
                        ", '" + String.Format(Session["OrderDate"].ToString(), "yyyy-MM-dd") + "', 0, 1, 0, 'Recorded in online shop', '" + Session["orderid"] + "', 1, 1)";
                    SqlCommand cmd = new SqlCommand(cadena, cn);
                    cmd.ExecuteNonQuery();
                }
                cn.Close();

            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }


        private void UpdateSoldItemsInSyncAudit()
        {            
            try
            {
                if (cn != null && cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                String ahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    
                    string cadena = "UPDATE SyncAudit SET id = 5, SyncDate = '" + ahora + "'";
                    SqlCommand cmd = new SqlCommand(cadena, cn);
                    cmd.ExecuteNonQuery();
                
                cn.Close();

            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        private void DeleteExistingOrderId()
        {
            try
            {               
                if (cn != null && cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                string orderId = Session["orderid"].ToString();
                string query = @"DELETE SoldItems_Sync WHERE OrderId = '" + orderId + "'";

                SqlCommand command = new SqlCommand(query, cn);               
                command.CommandText = query;
                command.ExecuteNonQuery();

                cn.Close();

                // MessageBox.Show("Reporte eliminado exitosamente!", "Eliminar!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                cn.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

          
            // if Session Is Null Redirecting to login else Placing the order
            if (Session["username"] == null)
            {
                Response.Redirect("Login.aspx");               
            }
            else
            {
                if (GridViewAddToCart.Rows.Count.ToString() == "0")
                {
                    Response.Write("<script>alert('Your Cart is Empty. You cannot place an Order');</script>");
                }
                else
                {
                   
                     DeleteExistingOrderId();
                     InsertItems_soldItems_Sync();
                    UpdateSoldItemsInSyncAudit();
                    // Response.Redirect("PlaceOrder.aspx");  // to pay with credit card
                    Response.Redirect("PlaceOrderBankDeposit.aspx"); // to pay with bank deposit or PayPal

                }
            }

        }

        protected void GridViewAddToCart_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}