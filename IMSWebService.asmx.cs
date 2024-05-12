using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using WebMatrix.Data;




namespace InternshipManagementSystem
{
    /// <summary>
    /// IMSWebService için özet açıklama
    /// </summary>
    [WebService(Namespace = "https://localhost:44358/Home/Index")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Bu Web Hizmeti'nin, ASP.NET AJAX kullanılarak komut dosyasından çağrılmasına, aşağıdaki satırı açıklamadan kaldırmasına olanak vermek için.
    // [System.Web.Script.Services.ScriptService]
    public class IMSWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public DataTable GetAllTable(string tablename)
        {
            SqlConnection con = new SqlConnection(@"data source=DESKTOP-55U0B5P\SQLEXPRESS;Initial Catalog=db_internship;Integrated security=True;");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from "+tablename, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = new DataTable(tablename);
            dt = ds.Tables[0]; //webservice
            return dt;
        }

        [WebMethod]
        public DataTable GetTable(string command,string tablename)
        {
            SqlConnection con = new SqlConnection(@"data source=DESKTOP-55U0B5P\SQLEXPRESS;Initial Catalog=db_internship;Integrated security=True;");
            con.Open();
            SqlCommand cmd = new SqlCommand(command, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = new DataTable(tablename);
            dt = ds.Tables[0];
            return dt;
        }
    }
}
