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
    public partial class PlaceOrder : System.Web.UI.Page
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
                }
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

        public void Connecting(string str)
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            InsertCardpayment();
        }

        private void InsertCardpayment()
        {
            try
            {
                if (cn != null && cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                string orderId = Session["orderid"].ToString();

                string cadena = "Insert into CardDetails (ClientId, FirstName, LastName, CardNumber, ExpiryDate, CVV, BillingAddress, OrderId, PaymentType) " +
                    "Values(" + Session["clientid"] + ", @firstname, @lastname, @cardnumber, @expirydate, @cvv, @billingaddress, @orderid, 1)";
                SqlCommand cmd = new SqlCommand(cadena, cn);
                cmd.Parameters.AddWithValue("@firstname", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@lastname", txtLastName.Text);
                cmd.Parameters.AddWithValue("@cardnumber", txtCard.Text);
                cmd.Parameters.AddWithValue("@expirydate", txtExpiryDate.Text);
                cmd.Parameters.AddWithValue("@cvv", txtCVV.Text);
                cmd.Parameters.AddWithValue("@billingaddress", txtBillingAddress.Text);
                cmd.Parameters.AddWithValue("@orderid", orderId);
                cmd.ExecuteNonQuery();
                cn.Close();

                Response.Write("<script>alert('Payment Made Successful');</script>");
                Session["address"] = txtBillingAddress.Text;
                Session["Orderdate"] = DateTime.Now.ToString("yyyy-MM-dd");
                Session["buyitems"] = null; //esta sesion es una data table

                Response.Redirect("Pdf_generate.aspx", true);
            }

            catch (Exception ex)
            {
                cn.Close();
            }
        }
    }
}