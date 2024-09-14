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
    public partial class Cash : Form
    {
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader dr;
        String title = "Car Wash Management System";
        public int customerId = 0, vehicleTypeId  = 0;
        public Cash()
        {
            InitializeComponent();
            getTransno();
            loadCash();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            openChildForm(new CashCustomer(this));
            btnAddServices.Enabled = true;
        }

        private void btnAddServices_Click(object sender, EventArgs e)
        {
            openChildForm(new CashService(this));
            btnAddCustomer.Enabled = false;
        }

        private void btnCash_Click(object sender, EventArgs e)
        {
            SettlePayment module = new SettlePayment(this);
            module.txtSale.Text = lblTotal.Text;
            module.ShowDialog();

        }



        #region method

        private Form activeForm = null;
        public void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelCash.Height = 200;
            panelCash.Controls.Add(childForm);
            panelCash.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            childForm.BringToFront();
            childForm.Activate();
        }

        //create a transno based on date
        public void getTransno()
        {
            try
            {
                string sdate = DateTime.Now.ToString("yyyyMMdd");
                int count;
                string transno;

                dbcon.open();
                cm = new SqlCommand("SELECT TOP 1 transno FROM tbCash WHERE transno LIKE '%" + sdate + "%' ORDER BY id DESC", dbcon.connect());
                dr = cm.ExecuteReader();
                dr.Read();

                if (dr.HasRows)
                {
                    transno = dr[0].ToString();
                    count = int.Parse(transno.Substring(8,4));
                    lblTransno.Text = sdate + (count + 1);
                }
                else
                {
                    transno = sdate + "1001";
                    lblTransno.Text = transno;
                }
                dbcon.close();
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
        }

        private void dgvCash_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCash.Columns[e.ColumnIndex].Name;
            if (colName == "Delete")// to delete the record
            {
                try
                {
                    if (MessageBox.Show("Are you sure you want to cancel the Service?", "Cancel Service", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("Delete from tbCash where id like '" + dgvCash.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", dbcon.connect());
                        dbcon.open();
                        cm.ExecuteNonQuery();
                        dbcon.close();
                        MessageBox.Show("Service Canceled Successfully", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, title);
                }
            }
            loadCash();
        }

        public void loadCash()
        {
            dgvCash.Rows.Clear();
            int i = 0;
            double total = 0;
            double price = 0;
           
            cm = new SqlCommand("Select ca.id, ca.transno, cu.name , cu.carno, cu.carmodel , v.name, v.class, s.name, ca.price,ca.date from tbCash AS Ca LEFT JOIN tbCustomer AS cu ON Ca.cid = Cu.id LEFT JOIN tbService AS s  ON Ca.sid= s.id LEFT JOIN tbVehicleType AS v ON Ca.vid = v.id WHERE status  LIKE 'Pending' AND Ca.transno =  '"+lblTransno.Text+"' ",dbcon.connect());
            dbcon.open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                price = int.Parse(dr[6].ToString()) * double.Parse(dr[8].ToString());
                dgvCash.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), price , dr[9].ToString());
                total += price;
            }
            dr.Close();
            dbcon.close();
            lblTotal.Text = total.ToString("#,##0.00");
        }
        #endregion method
    }
}
