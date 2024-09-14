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
using static CAR_WASH_MANAGEMENT_SYSTEM.dbConnect;

namespace CAR_WASH_MANAGEMENT_SYSTEM
{   
    public partial class EmployerModule : Form
    {
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        String title = "Car Wash Management System";
        bool check = false;
        Employer employer;
        public EmployerModule(Employer emp)
        {
            InitializeComponent();
            employer = emp;
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
                    if (MessageBox.Show("Are you sure you want to register this employee?", "Employer Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("INSERT INTO tbEmployer(name,phone,address,dob,gender,role,salary,password) VALUES(@name,@phone,@address,@dob,@gender,@role,@salary,@password)", dbcon.connect());
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@dob", dtDob.Value);
                        cm.Parameters.AddWithValue("@gender", rdMale.Checked ? "Male" : "Female");
                        cm.Parameters.AddWithValue("@role", cbRole.Text);
                        cm.Parameters.AddWithValue("@salary", txtSalary.Text);
                        cm.Parameters.AddWithValue("@password", txtPassword.Text);

                        dbcon.open();// to open connection
                        cm.ExecuteNonQuery();
                        dbcon.close();// to close connection
                        MessageBox.Show("Employer is Registered Successfully", title);
                        Clear();//to clear data\
                        employer.loadEmployer();
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
                    if (MessageBox.Show("Are you sure you want to update this employee Information?", "Employer Info Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("Update tbEmployer SET name=@name,phone=@phone,address=@address,dob=@dob,gender=@gender,role=@role,salary=@salary,password=@password WHERE id=@id", dbcon.connect());
                        cm.Parameters.AddWithValue("@id", lblEid.Text);
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@dob", dtDob.Value);
                        cm.Parameters.AddWithValue("@gender", rdMale.Checked ? "Male" : "Female");
                        cm.Parameters.AddWithValue("@role", cbRole.Text);
                        cm.Parameters.AddWithValue("@salary", txtSalary.Text);
                        cm.Parameters.AddWithValue("@password", txtPassword.Text);

                        dbcon.open();// to open connection
                        cm.ExecuteNonQuery();
                        dbcon.close();// to close connection
                        MessageBox.Show("Employer is Registered Successfully", title);
                        this.Dispose();
                        employer.loadEmployer();
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
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            //only allow digit number
            if(!char.IsControl(e.KeyChar)&& !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if((e.KeyChar=='.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        // to clear the textfield 
        #region method
        public void Clear()
        {
            txtName.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtSalary.Clear();
            txtPassword.Clear();

            dtDob.Value = DateTime.Now;
            cbRole.SelectedIndex = 3;
        }

        // to check textfield and age
        public void checkField()
        {
            if(txtName.Text==""|| txtAddress.Text == ""|| txtPassword.Text == "" || txtPhone.Text == "")
            {
                MessageBox.Show("All details are required. Please fill all data",title);
                return;
            }

            if(checkAge(dtDob.Value) < 14)
            {
                MessageBox.Show("Age is less than 14. Not Applicable",title);
                return;
            }
            check = true;
        }

        public static  int checkAge(DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;
            return age;
        }

        #endregion method

    }
}
