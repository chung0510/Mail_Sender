using System;
using System.Windows.Forms;

namespace NetMail
{
    public partial class FormSelectTarget : Form
    {
        // "To", "Cc", "Bcc"
        public string SelectedTarget { get; private set; }

        public FormSelectTarget()
        {
            InitializeComponent();
        }

        private void btnTo_Click(object sender, EventArgs e)
        {
            SelectedTarget = "To";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCc_Click(object sender, EventArgs e)
        {
            SelectedTarget = "Cc";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnBcc_Click(object sender, EventArgs e)
        {
            SelectedTarget = "Bcc";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
