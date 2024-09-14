using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAR_WASH_MANAGEMENT_SYSTEM
{
    public partial class CustomerModule : Form
    {
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        String title = "Car Wash Management System";
        bool check = false;
        Customer customer;
        public CustomerModule(Customer cust)
        {
            InitializeComponent();
            customer = cust;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                checkField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to register this Customer?", "Customer Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("INSERT INTO tbCustomer(vid,name,phone,carno, carmodel, address, points)VALUES(@vid,@name,@phone,@carno, @carmodel, @address, @points)", dbcon.connect());
                        cm.Parameters.AddWithValue("@vid", cbCarType.SelectedValue);
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cm.Parameters.AddWithValue("@carno", txtCarNo.Text);
                        cm.Parameters.AddWithValue("@carmodel", txtCarModel.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@points", udPoints.Text);

                        dbcon.open();// to open connection
                        cm.ExecuteNonQuery();
                        dbcon.close();// to close connection
                        MessageBox.Show("Customer is Registered Successfully", title);
                        Clear();//to clear data\
                        customer.loadCustomer();
                    }

                }
                check = false;

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
                checkField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to Edit this Customer?", "Customer Editing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("UPDATE tbCustomer SET vid = @vid,name = @name,phone = @phone,carno = @carno, carmodel = @carmodel, address = @address, points = @points WHERE id = @id", dbcon.connect());
                        cm.Parameters.AddWithValue("@id", lblCid.Text);
                        cm.Parameters.AddWithValue("@vid", cbCarType.SelectedValue);
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cm.Parameters.AddWithValue("@carno", txtCarNo.Text);
                        cm.Parameters.AddWithValue("@carmodel", txtCarModel.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@points", udPoints.Text);

                        dbcon.open();// to open connection
                        cm.ExecuteNonQuery();
                        dbcon.close();// to close connection
                        MessageBox.Show("Customer is Edited Successfully", title);
                        customer.loadCustomer();
                        this.Dispose();
                    }

                }
                check = false;

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
        
        private void CustomerModule_Load(object sender, EventArgs e)
        {
            cbCarType.DataSource = vehicleType();
            cbCarType.DisplayMember = "name";
            cbCarType.ValueMember = "id";
        }

        #region method
        public DataTable vehicleType()
        {
            cm = new SqlCommand("SELECT * FROM tbVehicleType", dbcon.connect());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            adapter.SelectCommand = cm;
            adapter.Fill(dataTable);

            return dataTable;

        }

        public void Clear()
        {
            txtAddress.Clear();
            txtCarModel.Clear();
            txtCarNo.Clear();
            txtName.Clear();
            txtPhone.Clear();

            cbCarType.SelectedIndex = 0;
            udPoints.Value = 0;

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        public void checkField()
        {
            if (txtName.Text == "" || txtAddress.Text == "" || txtCarModel.Text == "" || txtCarNo.Text == "" || txtPhone.Text == "")
            {
                MessageBox.Show("All details are required. Please fill all data", title);
                return;
            }
            check = true;
        }
        #endregion method


    }
}
