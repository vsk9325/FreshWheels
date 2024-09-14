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
using System.Linq.Expressions;

namespace CAR_WASH_MANAGEMENT_SYSTEM
{
    public partial class VehicleTypeManage : Form
    {
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        Setting setting;
        String title = "Car Wash Management System";
        public VehicleTypeManage(Setting setting)
        {
            InitializeComponent();
            this.setting = setting;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtName.Text == "")
                {
                    MessageBox.Show("Required Vehicle type name! Warning!");
                    return;
                }
                if (MessageBox.Show("Are you sure you want to register the Vehicle Type?", "Vehicle Type Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT into tbVehicleType(name,class)VALUES(@name,@class)", dbcon.connect());
                    cm.Parameters.AddWithValue("@name", txtName.Text);
                    cm.Parameters.AddWithValue("@class",cbClass.Text);

                    dbcon.open();
                    cm.ExecuteNonQuery();
                    dbcon.close();
                    MessageBox.Show("Vehicle Type is Successfully Registered", title);
                    setting.loadVehicle();
                    Clear();
                    this.Dispose();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to Edit the Vehicle Type?", "Vehicle Type Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbVehicleType SET name=@name,class=@class WHERE id = @id", dbcon.connect());
                    cm.Parameters.AddWithValue("@id", lblVid.Text);
                    cm.Parameters.AddWithValue("@name", txtName.Text);
                    cm.Parameters.AddWithValue("@class", cbClass.Text);

                    dbcon.open();
                    cm.ExecuteNonQuery();
                    dbcon.close();
                    MessageBox.Show("Vehicle Type is Successfully Edited", title);
                    Clear();
                    setting.loadVehicle();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        #region method
        public void Clear()
        {
            txtName.Clear();
            cbClass.SelectedIndex = 0;
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }
        #endregion method
    }
}
