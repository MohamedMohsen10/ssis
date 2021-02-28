using CropsV4.Alm;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CropsV4.DataBaseConnection
{
    public class DbConnection
    {
        private string DataSource { get; set; } = @"10.1.22.189\CROPSDB";
        private string DataBase { get; set; } = "CropsDB_Stagging";
        private string UserName { get; set; } = "AppUserDevQC";
        private string Password { get; set; } = "P@ssw0rd";
        private static SqlConnection conn { get; set; }

        public DbConnection()
        {
            //your connection string 
            string connString = @"Data Source=" + DataSource + ";Initial Catalog="
                        + DataBase + ";Persist Security Info=True;User ID=" + UserName + ";Password=" + Password;

            //create instanace of database connection
            conn = new SqlConnection(connString);
            
            //open connection
            conn.Open();
        }

        public List<AlmProject> GetProjects()
        {
            var Projectslist = new List<AlmProject>();
            string query = "select * from Project";

            using (SqlCommand command = new SqlCommand(query, conn)) //pass SQL query created above and connection
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Projectslist.Add(new AlmProject(reader["ServerPath"].ToString(), reader["ProjectName"].ToString(), reader["Password"].ToString(), reader["ProjectApiSourceId"].ToString()));
                    }
                }
            }

            return Projectslist.Where(e=>e.ProjectName == "TFS Reports").ToList();
        }

        
    }
}
