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
    public partial class Customer : Form
    {
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader dr;
        string title = "Car Wash Management System";
        public Customer()
        {
            InitializeComponent();
            loadCustomer();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CustomerModule module = new CustomerModule(this);
            module.btnUpdate.Enabled = false;
            module.ShowDialog();

        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                //to sent employer data to the employer module
                CustomerModule module = new CustomerModule(this);
                module.lblCid.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                module.txtName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
                module.txtPhone.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();
                module.txtCarNo.Text = dgvCustomer.Rows[e.RowIndex].Cells[4].Value.ToString();
                module.txtCarModel.Text = dgvCustomer.Rows[e.RowIndex].Cells[5].Value.ToString();
                module.cbCarType.Text = dgvCustomer.Rows[e.RowIndex].Cells[6].Value.ToString();
                module.txtAddress.Text = dgvCustomer.Rows[e.RowIndex].Cells[7].Value.ToString();
                module.udPoints.Text = dgvCustomer.Rows[e.RowIndex].Cells[8].Value.ToString();

                module.btnSave.Enabled = false;
                module.udPoints.Enabled = true;
                module.ShowDialog();
            }
            else if (colName == "Delete")// to delete the record
            {
                try
                {
                    if (MessageBox.Show("Are you sure you want to delete the record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("Delete from tbCustomer where id like '" + dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", dbcon.connect());
                        dbcon.open();
                        cm.ExecuteNonQuery();
                        dbcon.close();
                        loadCustomer();
                        MessageBox.Show("Customer Record Deleted Successfully", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, title);
                }
            }
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadCustomer();
        }

        #region
        public void loadCustomer()
        {
            try
            {
                int i = 0;
                dgvCustomer.Rows.Clear();
                cm = new SqlCommand("SELECT C.id,C.name,phone,carno,carmodel, V.name, address, points FROM tbCustomer AS C INNER JOIN tbVehicleType AS V ON V.id = C.vid WHERE  CONCAT (C.name,carno,carmodel,address) LIKE '%" + txtSearch.Text + "%'", dbcon.connect());
                dbcon.open();
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());
                }
                dbcon.close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
        }
        #endregion
    }
}
