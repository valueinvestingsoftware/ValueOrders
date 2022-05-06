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
    public partial class Default1 : System.Web.UI.Page
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

            Session["additem"] = "false";

            if (Session["username"]!= null)
            {
                labelUsername.Text = "Logged in as: " + Session["username"].ToString();
                HyperLinkLogIn.Visible = false;
                ButtonLogOut.Visible = true;
            }
            else
            {
                labelUsername.Text = "You can login here... ";
                HyperLinkLogIn.Visible = true;
                ButtonLogOut.Visible = false;
            }

            if (!IsPostBack)
            {
                Drp_ProductCategory();
            }

        }

        protected void ButtonLogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx");
            labelUsername.Text = "You have logged out successfully";
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            string categoria = ProductCategories.SelectedItem.Text;
            if(categoria == "All Products")
            {
                categoria = "";
            }

            string cadena = "Select * from CatMap WHERE (Cod) LIKE '" + categoria + "%' AND (Category) Like '%" + TextBoxSearchItem.Text + "%' And Nivel = 4 AND VisibleInApps = 1";
            SqlDataAdapter sda = new SqlDataAdapter(cadena, cn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DataList1.DataSourceID = null;
            DataList1.DataSource = dt;
            DataList1.DataBind();
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

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
           
            if (Session["username"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
               
                Session["additem"] = "true";
                if (e.CommandName == "AddToCart")
                {           
                    Label lbl = (Label)(e.Item.FindControl("Label1"));
                    DropDownList list = (DropDownList)(e.Item.FindControl("DropDownList1"));
                    int qty = Int32.Parse(list.SelectedItem.ToString());
                    int existencia = Int32.Parse(lbl.Text.ToString());

                    bool suficientes = TotalQuantityAvailable(e.CommandArgument.ToString(), qty, existencia);
                    if (suficientes == true)
                    {                        
                        string id = e.CommandArgument.ToString();
                        string cantidad = list.SelectedItem.ToString();

                        Response.Redirect("AddToCart.aspx?id=" + id + "&pQuantity=" + cantidad);                    
                    }
                    else
                    {
                        Response.Write("You are trying to buy more than what is available!");
                    }
                    
                }
            }
            
        }

        DataTable dt = new DataTable();
        DataRow dr;
        int cantidadTotal = 0;

        private bool TotalQuantityAvailable(string id, int qty, int existencia)
        {
            bool suficientes = false;

            int itemId = (int)Int64.Parse(id);            
           
            if(dt.Rows.Count == 0)
            {
                dt.Columns.Add("id");
                dt.Columns.Add("qty");
            }
            
            dr = dt.NewRow();
            dr["id"] = itemId;
            dr["qty"] = qty;
            dt.Rows.Add(dr);
            
            foreach (DataRow row in dt.Rows)
            {
                if ((int)Int64.Parse(row["id"].ToString()) == itemId)
                {
                    cantidadTotal = qty + cantidadTotal;
                }
            }
            if (existencia >= cantidadTotal) {
                suficientes = true;
            }

            return suficientes;
        }

        private void Drp_ProductCategory()
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand("SELECT MAX(id) AS id, Category FROM CatMap WHERE Nivel = 3 AND VisibleInApps = 1 GROUP BY Category", cn);
            ProductCategories.DataSource = cmd.ExecuteReader();
            ProductCategories.DataTextField = "Category";
            ProductCategories.DataValueField = "id";
            ProductCategories.DataBind();
            ProductCategories.Items.Insert(0, "All Products");
            cn.Close();
        }
       
    }
}