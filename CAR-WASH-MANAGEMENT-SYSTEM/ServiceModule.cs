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
    public partial class ServiceModule : Form
    {
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        String title = "Car Wash Management System";
        Service service;
        public ServiceModule( Service service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            //only allow digit number
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "" || txtPrice.Text == "")
                {
                    MessageBox.Show("Required Data Field!", "Warning");
                    return;
                }
                if (MessageBox.Show("Are you sure to register to this Service?", "Service Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbService(name,price) VALUES(@name,@price)", dbcon.connect());
                    cm.Parameters.AddWithValue("@name", txtName.Text);
                    cm.Parameters.AddWithValue("@price", txtPrice.Text);
                    dbcon.open();
                    cm.ExecuteNonQuery();
                    dbcon.close();
                    MessageBox.Show("Service register successfully!", title);
                    Clear();
                    service.loadService();
                    
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
                if (txtName.Text == "" || txtPrice.Text == "")
                {
                    MessageBox.Show("Required Data Field!", "Warning");
                    return;
                }
                if (MessageBox.Show("Are you sure to Edit this Service?", "Service Editing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbService SET name = @name,price = @price WHERE id = @id", dbcon.connect());
                    cm.Parameters.AddWithValue("@id", lblSid.Text);
                    cm.Parameters.AddWithValue("@name", txtName.Text);
                    cm.Parameters.AddWithValue("@price", txtPrice.Text);
                    dbcon.open();
                    cm.ExecuteNonQuery();
                    dbcon.close();
                    MessageBox.Show("Service Edited successfully!", title);
                    Clear();
                    this.Dispose();
                    service.loadService();

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

        #region
        public void Clear()
        {
            txtName.Text = "";
            txtPrice.Text = "";

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }
        #endregion
    }

}
