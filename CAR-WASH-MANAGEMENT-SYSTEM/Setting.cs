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
    public partial class Setting : Form
    {
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader dr;
        String title = "Car Wash Management System";
        bool hasDetails = false;
        public Setting()
        {
            InitializeComponent();
            loadVehicle();
            loadCostOfGood();
            loadCompany();
        }

        

        #region VehicleType

        private void txtSearchVT_TextChanged(object sender, EventArgs e)
        {
            loadVehicle();
        }

        private void btnAddVT_Click(object sender, EventArgs e)
        {
            VehicleTypeManage module = new VehicleTypeManage(this);
            module.btnUpdate.Enabled = false;
            module.ShowDialog();

        }

        private void dgvVehicleType_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dgvVehicleType.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                //to sent vehicle data to the vehicle module
                VehicleTypeManage module = new VehicleTypeManage(this);
                module.lblVid.Text = dgvVehicleType.Rows[e.RowIndex].Cells[1].Value.ToString();
                module.txtName.Text = dgvVehicleType.Rows[e.RowIndex].Cells[2].Value.ToString();
                module.cbClass.Text = dgvVehicleType.Rows[e.RowIndex].Cells[3].Value.ToString();

                module.btnSave.Enabled = false;
                module.ShowDialog();
            }
            else if (colName == "Delete")
            {
                try
                {
                    if (MessageBox.Show("Are you sure you want to delete this record", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("DELETE FROM tbVehicleType WHERE id LIKE '%" + dgvVehicleType.Rows[e.RowIndex].Cells[1].Value.ToString() + "%'",dbcon.connect());
                        dbcon.open();
                        cm.ExecuteNonQuery();
                        dbcon.close();
                        MessageBox.Show("Vehicle type data has been deleted Successfully!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, title);
                }
            }
            loadVehicle();
        }
        public void loadVehicle()
        {
            try
            {
                int i = 0;
                dgvVehicleType.Rows.Clear();
                cm = new SqlCommand("SELECT * FROM tbVehicleType WHERE CONCAT (name,class) LIKE '%"+txtSearchVT.Text+"%'", dbcon.connect());
                dbcon.open();
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvVehicleType.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
                }
                dbcon.close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
        }


        #endregion VehicleType

        #region CostOfGood
        private void btnAddCoG_Click(object sender, EventArgs e)
        {
            ManageCostOfGoodSold module = new ManageCostOfGoodSold(this);
            module.btnUpdate.Enabled = false;
            module.ShowDialog();
        }

        private void txtSearchCoG_TextChanged(object sender, EventArgs e)
        {
            loadCostOfGood();
        }

        private void dgvCostOfGood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //send data to the manage cost of goods if clicked on edit or delete option
            String colName = dgvCostOfGood.Columns[e.ColumnIndex].Name;
            if(colName == "EditCoG")
            {
                ManageCostOfGoodSold module = new ManageCostOfGoodSold(this);
                module.lblCid.Text = dgvCostOfGood.Rows[e.RowIndex].Cells[1].Value.ToString();
                module.txtCostName.Text = dgvCostOfGood.Rows[e.RowIndex].Cells[2].Value.ToString();
                module.txtCost.Text = dgvCostOfGood.Rows[e.RowIndex].Cells[3].Value.ToString();
                module.dtCoG.Text = dgvCostOfGood.Rows[e.RowIndex].Cells[4].Value.ToString();

                module.btnSave.Enabled = false;
                module.ShowDialog();
            }
            else if(colName == "DeleteCoG"){
                try
                {
                    if(MessageBox.Show("Are you sure you want to delete this record","Delete Record",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("DELETE FROM tbCostOfGoods WHERE id LIKE '%" + dgvCostOfGood.Rows[e.RowIndex].Cells[1].Value.ToString() + "%'", dbcon.connect());
                        dbcon.open();
                        cm.ExecuteNonQuery();
                        dbcon.close();
                        MessageBox.Show("Your record is Deleted Successfully",title , MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);                  
                }
            }
            // to reload the cost of goods after edit or delete operation
            loadCostOfGood();
        }
        public void loadCostOfGood()
        {
            try
            {
                int i = 0;
                dgvCostOfGood.Rows.Clear();
                cm = new SqlCommand("SELECT * FROM tbCostOfGoods WHERE CONCAT(costname,date) LIKE '%"+txtSearchCoG.Text+"%' ", dbcon.connect());
                dbcon.open();
                dr = cm.ExecuteReader();
                while(dr.Read())
                {
                    i++;
                    dgvCostOfGood.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), DateTime.Parse(dr[3].ToString()).ToShortDateString());
                }
                dbcon.close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        #endregion CostOfGood

        #region Company Details
        public void loadCompany()
        {
            try
            {
                dbcon.open();
                cm = new SqlCommand("SELECT * FROM tbCompany",dbcon.connect());
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    hasDetails = true;
                    txtComName.Text = dr["name"].ToString();
                    txtComAddress.Text = dr["address"].ToString();
                }
                else
                {
                    txtComName.Clear();
                    txtComAddress.Clear();
                }
                dr.Close();
                dbcon.close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
        }
       

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Save Company Details?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (hasDetails)
                    {
                        dbcon.executeQuery("UPDATE tbCompany SET name = '" + txtComName.Text + "',address='" + txtComAddress.Text + "'");
                    }
                    else
                    {
                        dbcon.executeQuery("INSERT INTO tbCompany(name,address) VALUES('" + txtComName.Text + "','" + txtComAddress.Text + "')");
                    }
                    MessageBox.Show("Comapny Details has been Saved Successfuly", "Save Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtComName.Clear();
            txtComAddress.Clear();
        }

        #endregion Company Details
    }
}
