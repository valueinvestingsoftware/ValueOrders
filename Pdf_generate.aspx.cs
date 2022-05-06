using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Configuration;

namespace ValueOrders
{
    public partial class Pdf_generate : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("Login.aspx");                   
                }
            }

           
            NovoDataset();
            LeerXml();
            Connecting(str);

            string Orderid = Session["orderid"].ToString();
            lblOrderId.Text = Orderid;
            //findorderdate();  //Para sacar esa informacion de la tabla SoldItems_Sync pero no estoy guardando horas, minutos y segundos

            lblOrderDate.Text = Session["Orderdate"].ToString(); //Aqui si estoy guardando horas minutos y segundos
            showgrid();

            String orderid;    //solo una vez mostradoel invoice genero una nueva orden id para si se quisiese enviar una nueva orden de compra de otros items
            orderid = Guid.NewGuid().ToString("N");
            Session["orderid"] = orderid;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
           // base.VerifyRenderingInServerForm(control);
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            exportpdf();
        }

        private void exportpdf()
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=OrderInvoice.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            Panel1.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }

        private void findorderdate()
        {
            string cadena = "Select * from SoldItems_Sync WHERE OrderId = '" + Session["orderid"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(cadena, cn);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblOrderDate.Text = ds.Tables[0].Rows[0]["SaleDate"].ToString();
            }
            cn.Close();

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

        private void showgrid()
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("pSec");
            dt.Columns.Add("id");
            dt.Columns.Add("Category");           
            dt.Columns.Add("Pprice");
            dt.Columns.Add("pQuantity");
            dt.Columns.Add("pTotalPrice");                    
             
             string cadena = "SELECT dbo.SoldItems_Sync.id AS Id, dbo.CatMap.id AS ItemId, dbo.CatMap.Category AS Item, dbo.CatMap.WebPrice AS Price, dbo.SoldItems_Sync.SaleQuantity As Quantity " +
             " FROM dbo.CatMap INNER JOIN " +
             " dbo.SoldItems_Sync ON dbo.CatMap.id = dbo.SoldItems_Sync.itemid Where dbo.SoldItems_Sync.OrderId = '" + Session["orderid"].ToString() + "'";
             SqlDataAdapter da = new SqlDataAdapter(cadena, cn);
             DataSet ds = new DataSet();
             da.Fill(ds);

            double grandtotal = 0.0;
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                dr = dt.NewRow();
                dr["pSec"] = ds.Tables[0].Rows[i]["Id"].ToString(); 
                dr["id"] = ds.Tables[0].Rows[i]["ItemId"].ToString();
                dr["Category"] = ds.Tables[0].Rows[i]["Item"].ToString();                
                dr["Pprice"] = ds.Tables[0].Rows[i]["Price"].ToString();
                dr["pQuantity"] = ds.Tables[0].Rows[i]["Quantity"].ToString();

                double price = Convert.ToDouble(ds.Tables[0].Rows[i]["Price"].ToString());
                double Quantity = Convert.ToDouble(ds.Tables[0].Rows[i]["Quantity"]);
                double totalPrice = price * Quantity;
                dr["pTotalPrice"] = totalPrice;
                grandtotal = grandtotal + totalPrice;
                dt.Rows.Add(dr);                       
            }
            lblGrandTotal.Text = grandtotal.ToString();
            GridView1.DataSource = dt;
            GridView1.DataBind();

            lblBuyerAddress.Text = Session["address"].ToString();
            lblOrderId.Text = Session["orderid"].ToString();
            
        }
    }
    }