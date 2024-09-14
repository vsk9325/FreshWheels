using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace CAR_WASH_MANAGEMENT_SYSTEM
{
     class dbConnect
    {
        SqlCommand cm = new SqlCommand();
        public SqlConnection cn = new SqlConnection("Data Source=LAPTOP-I6VB6L2B\\SQLEXPRESS;Initial Catalog=DbCarWash;Integrated Security=True");
        public SqlConnection connect()
        {
            return cn;
        }
        public void open()
        {
            if (cn.State == System.Data.ConnectionState.Closed)
                cn.Open();
        }
        public void close()
        {
            if(cn.State == System.Data.ConnectionState.Open)
            {
                cn.Close();
            }
        }

        public void executeQuery(string sql)
        {
            try
            {
                open();
                cm = new SqlCommand(sql,connect());
                cm.ExecuteNonQuery();
                close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
