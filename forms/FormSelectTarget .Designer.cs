namespace NetMail
{
    partial class FormSelectTarget
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnTo;
        private System.Windows.Forms.Button btnCc;
        private System.Windows.Forms.Button btnBcc;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnTo = new System.Windows.Forms.Button();
            this.btnCc = new System.Windows.Forms.Button();
            this.btnBcc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTo
            // 
            this.btnTo.Location = new System.Drawing.Point(20, 15);
            this.btnTo.Name = "btnTo";
            this.btnTo.Size = new System.Drawing.Size(100, 30);
            this.btnTo.TabIndex = 0;
            this.btnTo.Text = "To";
            this.btnTo.UseVisualStyleBackColor = true;
            this.btnTo.Click += new System.EventHandler(this.btnTo_Click);
            // 
            // btnCc
            // 
            this.btnCc.Location = new System.Drawing.Point(20, 55);
            this.btnCc.Name = "btnCc";
            this.btnCc.Size = new System.Drawing.Size(100, 30);
            this.btnCc.TabIndex = 1;
            this.btnCc.Text = "Cc";
            this.btnCc.UseVisualStyleBackColor = true;
            this.btnCc.Click += new System.EventHandler(this.btnCc_Click);
            // 
            // btnBcc
            // 
            this.btnBcc.Location = new System.Drawing.Point(20, 95);
            this.btnBcc.Name = "btnBcc";
            this.btnBcc.Size = new System.Drawing.Size(100, 30);
            this.btnBcc.TabIndex = 2;
            this.btnBcc.Text = "Bcc";
            this.btnBcc.UseVisualStyleBackColor = true;
            this.btnBcc.Click += new System.EventHandler(this.btnBcc_Click);
            // 
            // FormSelectTarget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(140, 145);
            this.Controls.Add(this.btnBcc);
            this.Controls.Add(this.btnCc);
            this.Controls.Add(this.btnTo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSelectTarget";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import vào";
            this.ResumeLayout(false);
        }
    }
}
