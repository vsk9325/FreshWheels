using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAR_WASH_MANAGEMENT_SYSTEM
{
    public partial class Service : Form
    {

        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader dr;
        String title = "Car Wash Management System";
        public Service()
        {
            InitializeComponent();
            loadService();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ServiceModule module = new ServiceModule(this);
            module.btnUpdate.Enabled = true;
            module.ShowDialog();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadService();
        }

        private void dgvService_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dgvService.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                //to sent sercive data to the sercive module
                ServiceModule module = new ServiceModule(this);
                module.lblSid.Text = dgvService.Rows[e.RowIndex].Cells[1].Value.ToString();
                module.txtName.Text = dgvService.Rows[e.RowIndex].Cells[2].Value.ToString();
                module.txtPrice.Text = dgvService.Rows[e.RowIndex].Cells[3].Value.ToString();

                module.btnSave.Enabled = false;
                module.ShowDialog();
            }
            else if (colName == "Delete")
            {
                try
                {
                    if (MessageBox.Show("Are you sure you want to delete this service", "Delete Service", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("DELETE FROM tbService WHERE id LIKE '%" + dgvService.Rows[e.RowIndex].Cells[1].Value.ToString() + "%'", dbcon.connect());
                        dbcon.open();
                        cm.ExecuteNonQuery();
                        dbcon.close();
                        MessageBox.Show("Service  has been deleted Successfully!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, title);
                }
            }
            loadService();
        }

        #region method

        public void loadService()
        {
            try
            {
                int i = 0;
                dgvService.Rows.Clear();
                cm = new SqlCommand("SELECT * FROM tbService WHERE name LIKE '%" + txtSearch.Text + "%'", dbcon.connect());
                dbcon.open();
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvService.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
                }
                dbcon.close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
        }

        #endregion method

        
    }
}
