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
using System.Xml.Linq;

namespace CAR_WASH_MANAGEMENT_SYSTEM
{
    public partial class ManageCostOfGoodSold : Form
    {
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        Setting setting;
        String title = "Car Wash Management System";
        bool check = false;
        public ManageCostOfGoodSold(Setting sett)
        {
            InitializeComponent();
            setting = sett;
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
                    if (MessageBox.Show("Are you sure you want to register the Cost of Goods?", "Cost of Good Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("INSERT into tbCostOfGoods(costname,cost,date)VALUES(@costname,@cost,@date)", dbcon.connect());
                        cm.Parameters.AddWithValue("@costname", txtCostName.Text);
                        cm.Parameters.AddWithValue("@cost", txtCost.Text);
                        cm.Parameters.AddWithValue("@date", dtCoG.Value);

                        dbcon.open();
                        cm.ExecuteNonQuery();
                        dbcon.close();
                        MessageBox.Show("Vehicle Type is Successfully Registered", title);
                        setting.loadCostOfGood();
                        Clear();
                        //this.Dispose();

                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            checkField();
            if (check)
            {
                if(MessageBox.Show("Are you sure you want to Edit your Record!",title,MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbCostOfGoods SET costname = @costname , cost = @cost, date = @date WHERE id = @id",dbcon.connect());
                    cm.Parameters.AddWithValue("@id", lblCid.Text);
                    cm.Parameters.AddWithValue("@costname",txtCostName.Text);
                    cm.Parameters.AddWithValue("@cost", txtCost.Text);
                    cm.Parameters.AddWithValue("@date", dtCoG.Value);

                    dbcon.open();
                    cm.ExecuteNonQuery();
                    dbcon.close();

                    MessageBox.Show("Your record is Editied Successully", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    this.Dispose();

                }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void txtCost_KeyPress(object sender, KeyPressEventArgs e)
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

        #region method
        public void checkField()
        {
            if (txtCostName.Text == "" || txtCost.Text == "")
            {
                MessageBox.Show("All fields are required!","Warning");
                return;
            }
            check = true;
        }
        public void Clear()
        {
            txtCostName.Text = "";
            txtCost.Text = "";
            dtCoG.Value = DateTime.Now;

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }
        #endregion method

       
    }
}
