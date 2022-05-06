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
using System.Net.Configuration;
using System.Net.Mail;
using System.Net;

namespace ValueOrders
{
    public partial class Login : System.Web.UI.Page
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

        protected void btnSignIn_Click(object sender, EventArgs e)
        {

            if (cn.State != ConnectionState.Open)
            {
                cn.Open();
            }

            try
            {
                string passwordEncriptada = Encriptar(txtPassword.Text);
                string cadena = "SELECT * FROM UserPasswords WHERE CompanyId = 1 AND Email = '" + txtEmail.Text + "' AND Password = '" + passwordEncriptada + "'";
                Boolean tieneRegistros = CadenaDevuelveFilas(cadena);
                if (tieneRegistros == true)
                {
                    lblSignIn.Text = "Login successful!";
                    lblSignIn.ForeColor = System.Drawing.Color.Green;
                    Session["username"] = txtEmail.Text;
                    Session["clientid"] = RetrieveClientId(txtEmail.Text).ToString();

                    String orderid;
                    orderid = Guid.NewGuid().ToString("N");
                    Session["orderid"] = orderid;

                    Response.Redirect("Default.aspx");
                }
                else
                {
                    lblSignIn.Text = "Unsuccessful login!";
                    lblSignIn.ForeColor = System.Drawing.Color.Red;
                }
                cn.Close();
            }
            catch(Exception ex)
            {
                cn.Close();
            }

            
        }

        public bool CadenaDevuelveFilas(string cadena)
        {
            bool tieneRegistro = false;

            if (cn.State != ConnectionState.Open)
            {
                cn.Open();
            }
              
            try
            {
                SqlCommand command = new SqlCommand(cadena, cn);
                using (SqlDataReader myreader = command.ExecuteReader())
                {
                    myreader.Read();

                    if (myreader.HasRows)
                    {
                        cn.Close();

                        tieneRegistro = true;
                        return tieneRegistro;
                    }
                    else
                    {
                        tieneRegistro = false;
                        return tieneRegistro;
                    }
                }
                
            }
          
            catch (Exception ex)
            {
                cn.Close();
                return tieneRegistro;
            }

        }

        private Int64 RetrieveClientId(string email)
        {
            Int64 clientId = 0;

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
                string query = @"SELECT id FROM Map WHERE email = '" + email + "' AND Supplier = 0";
                command.CommandText = query;
                dt.TableName = "Map";
                dt.Load(command.ExecuteReader());

                commandInsert.Connection = cn;
                commandInsert.CommandType = CommandType.Text;

                foreach (DataRow Fila in dt.Rows)
                {
                    clientId = (Int64)Fila["id"];
                }

                cn.Close();
                return clientId;
            }
            catch (Exception ex)
            {
                cn.Close();
                return clientId;
            }

        }

        private String RetrievePassword(string email)
        {
            String password = "";

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
                string query = @"SELECT * FROM UserPasswords WHERE Email = '" + email + "'";
                command.CommandText = query;
                dt.TableName = "UserPasswords";
                dt.Load(command.ExecuteReader());

                commandInsert.Connection = cn;
                commandInsert.CommandType = CommandType.Text;

                foreach (DataRow Fila in dt.Rows)
                {
                    password =  Fila["Password"].ToString();
                }

                cn.Close();
                return password;
            }
            catch (Exception ex)
            {
                cn.Close();
                return password;
            }

        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }

        protected void LinkButtonForgotPassword_Click(object sender, EventArgs e)
        {
            String password = RetrievePassword(txtEmail.Text);
            String desencryptedPassword = Desencriptar(password);
            if (password.Length == 0)
            {
                lblSignIn.Text = "Your email is not present in our database, please sign up!";
            }
            else
            {
                String asunto = "Your password to access the Online shop Value Orders";
                string message = "Your password to access the Online shop Value Orders is " + desencryptedPassword;
                EnviaEmail(message, asunto, true);
            }
        }

        public void EnviaEmail(string mensaje, string asunto, bool notificar)
        {
            MailMessage posta = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");

            try
            {
                posta = new MailMessage();
                posta.From = new MailAddress("softwarebolsadevalores@gmail.com");
                posta.To.Add(txtEmail.Text);
                posta.Subject = asunto;
                posta.Body = mensaje;
                posta.Priority = MailPriority.High;

                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential("softwarebolsadevalores@gmail.com", "c2029820298");
                smtp.Port = 587;
                smtp.Send(posta);
                if (notificar == true)
                {
                    lblSignIn.Text = "The password has been sent to your email address!";
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}