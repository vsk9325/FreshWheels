using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CAR_WASH_MANAGEMENT_SYSTEM
{
    public partial class Employer : Form
    {
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader dr;
        string title = "Car Wash Management System";
        public Employer()
        {
            InitializeComponent();
            loadEmployer();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EmployerModule module = new EmployerModule(this);
            module.btnUpdate.Enabled = false;
            module.ShowDialog();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadEmployer();
        }

        private void dgvEmployer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvEmployer.Columns[e.ColumnIndex].Name;
            if(colName == "Edit")
            {
                //to sent employer data to the employer module
                EmployerModule module = new EmployerModule(this);
                module.lblEid.Text = dgvEmployer.Rows[e.RowIndex].Cells[1].Value.ToString();
                module.txtName.Text = dgvEmployer.Rows[e.RowIndex].Cells[2].Value.ToString();
                module.txtPhone.Text = dgvEmployer.Rows[e.RowIndex].Cells[3].Value.ToString();
                module.txtAddress.Text = dgvEmployer.Rows[e.RowIndex].Cells[4].Value.ToString();
                module.dtDob.Text = dgvEmployer.Rows[e.RowIndex].Cells[5].Value.ToString();
                module.rdMale.Checked = dgvEmployer.Rows[e.RowIndex].Cells[6].Value.ToString() == "Male" ? true : false;
                module.cbRole.Text = dgvEmployer.Rows[e.RowIndex].Cells[7].Value.ToString();
                module.txtSalary.Text = dgvEmployer.Rows[e.RowIndex].Cells[8].Value.ToString();
                module.txtPassword.Text = dgvEmployer.Rows[e.RowIndex].Cells[9].Value.ToString();

                module.btnSave.Enabled = false;
                module.ShowDialog();
            }
            else if(colName == "Delete")// to delete the record
            {
                try
                {
                    if(MessageBox.Show("Are you sure you want to delete the record?","Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("Delete from tbEmployer where id like '" + dgvEmployer.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", dbcon.connect());
                        dbcon.open();
                        cm.ExecuteNonQuery();
                        dbcon.close();
                        MessageBox.Show("Record Deleted Successfully",title,MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, title);
                }
            }
        }

        #region method
        public void loadEmployer()
        {
            try
            {
                int i = 0;
                dgvEmployer.Rows.Clear();
                cm = new SqlCommand("SELECT * FROM tbEmployer WHERE CONCAT (name,address,role) LIKE '%" + txtSearch.Text + "%'", dbcon.connect());
                dbcon.open();
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvEmployer.Rows.Add(i,dr[0].ToString(),dr[1].ToString(),dr[2].ToString(), dr[3].ToString(), DateTime.Parse(dr[4].ToString()).ToShortDateString(),  dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString());
                }
                dbcon.close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,title);
            }
        }

        #endregion method

        
    }
}
