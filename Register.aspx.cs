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
    public partial class Register : System.Web.UI.Page
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

        public void Connecting(String str)
        {
            cn = new SqlConnection(str);
        }

        private string RetrieveCompanyName()
        {
            string companyName = "";

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
                string query = @"SELECT OnlineShop FROM Config";
                command.CommandText = query;
                dt.TableName = "Map";
                dt.Load(command.ExecuteReader());

                commandInsert.Connection = cn;
                commandInsert.CommandType = CommandType.Text;

                foreach (DataRow Fila in dt.Rows)
                {
                    companyName = Fila["OnlineShop"].ToString();
                }

                return companyName;
            }
            catch (Exception ex)
            {
                return companyName;
            }

        }

        private void RegistroNuevoUsuario() //No estoy usando este metodo
        {
            if (cn.State != ConnectionState.Open)
                cn.Open();
            try
            {               
                string cadena;
                cadena = "INSERT INTO UserPasswords (CompanyId, Name, Password, Clue, Email, Administrator, MachineName, AccessStocks, " +
                    "AccessSpences, AccesMap, AccessBuySell, PhoneNumber) VALUES (" + 1 + ", @Name, @Password, 'Customer recorded in online shop', " +
                    "@Email, 0, 'unknown', 0, 0, 0, 0, @PhoneNumber)";
                SqlCommand command = new SqlCommand();
                command.Connection = cn;
                command.CommandType = CommandType.Text;
                command.CommandText = cadena;
                command.Parameters.AddWithValue("@Name", txtName.Text + " " +  txtLastName.Text);
                command.Parameters.AddWithValue("@Password", Encriptar(txtPassword.Text));
                command.Parameters.AddWithValue("@Email", txtEmail.Text);
                command.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text);              
                command.ExecuteNonQuery();
                lblRegistration.Text = "Registered successfully!";
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
            }
        }

        private void RegistroNuevoCliente(string companyCod) //No estoy usando este metodo
        {
            if (cn.State != ConnectionState.Open)
                cn.Open();
            try
            {
                string cadena;
                cadena = "INSERT INTO Map (Supplier, CompanyCod, Contacto, Address, City, telefono, email, Valor, " +
                    "Latitud, Longitud, Comment, FechaCreacion, FechaEdicion, CreatedInApp) VALUES (0, @companyCod, @contacto, " +
                    "@address, @city, " + "@telefono, " + "@email, " + "0, 0.00, 0.00, 'Created in online shop', '" + 
                    DateTime.Now.ToString("yyyy-MM-dd") + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "', 0)";
                SqlCommand command = new SqlCommand();
                command.Connection = cn;
                command.CommandType = CommandType.Text;
                command.CommandText = cadena;
                command.Parameters.AddWithValue("@companyCod", companyCod);
                command.Parameters.AddWithValue("@contacto", txtName.Text + " " + txtLastName.Text);
                command.Parameters.AddWithValue("@address", txtAddress.Text);
                command.Parameters.AddWithValue("@city", txtCity.Text);
                command.Parameters.AddWithValue("@telefono", txtPhoneNumber.Text);
                command.Parameters.AddWithValue("@email", txtEmail.Text);
                command.ExecuteNonQuery();
                lblRegistration.Text = "Client was created successfully!";
                btnSave.Enabled = false;
                cn.Close();
            }
            catch (Exception ex)
            {
                lblRegistration.Text = ex.Message;
                cn.Close();
            }
        }

        private bool CadenaDevuelveFilas(string cadena)
        {
            bool tieneRegistro = false;

            if (cn.State != ConnectionState.Open)
                cn.Open();
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
        protected void Button1_Click(object sender, EventArgs e)
        {
            string cadena = "SELECT * FROM UserPasswords WHERE Email = " + "'" + txtEmail.Text + "'";
            Boolean tieneFilas = CadenaDevuelveFilas(cadena);

            if(tieneFilas == false)
            {
                string nombreEmpresa = RetrieveCompanyName();
                RegistroNuevoUsuario();
                RegistroNuevoCliente(nombreEmpresa);
            }
            else
            {
                lblRegistration.Text = "Email " + txtEmail.Text + " has already been recorded";
            }
           
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
    }
}