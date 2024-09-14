﻿using System;
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
    public partial class CashCustomer : Form
    {
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader dr;
        String title = "Car Wash Management System";
        Cash cash;
        public CashCustomer(Cash cash)
        {
            InitializeComponent();
            loadCustomer();
            this.cash = cash;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadCustomer();
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (colName == "Select")
            {
                cash.customerId = int.Parse(dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString());
                cash.vehicleTypeId = int.Parse(dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString());
            }
            else return;
            this.Dispose();
            cash.panelCash.Height = 1;
        }


        #region method
        //create fuction to load customer

        public void loadCustomer()
        {
            try
            {
                int i = 0;
                dgvCustomer.Rows.Clear();
                cm = new SqlCommand("SELECT * FROM tbCustomer WHERE  CONCAT (name,phone,address) LIKE '%" + txtSearch.Text + "%'", dbcon.connect());
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

        #endregion method

        
    }
}
