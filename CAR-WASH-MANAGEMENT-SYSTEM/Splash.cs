using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAR_WASH_MANAGEMENT_SYSTEM
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        int start = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            start += 2;
            progressBar1.Value = start;
            if(progressBar1.Value == 100)
            {
                progressBar1.Value = 0;
                timer1.Stop();
                login log = new login();
                this.Hide();
                log.ShowDialog();
            }

        }

        private void Splash_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
