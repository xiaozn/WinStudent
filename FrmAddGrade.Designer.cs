namespace WinStudent
{
    partial class FrmAddGrade
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtGradeName = new System.Windows.Forms.TextBox();
            this.btnAddGrade = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "年级名称：";
            // 
            // txtGradeName
            // 
            this.txtGradeName.Location = new System.Drawing.Point(150, 95);
            this.txtGradeName.Name = "txtGradeName";
            this.txtGradeName.Size = new System.Drawing.Size(135, 21);
            this.txtGradeName.TabIndex = 1;
            // 
            // btnAddGrade
            // 
            this.btnAddGrade.Location = new System.Drawing.Point(306, 95);
            this.btnAddGrade.Name = "btnAddGrade";
            this.btnAddGrade.Size = new System.Drawing.Size(75, 23);
            this.btnAddGrade.TabIndex = 2;
            this.btnAddGrade.Text = "添加年级";
            this.btnAddGrade.UseVisualStyleBackColor = true;
            // 
            // FrmAddGrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 333);
            this.Controls.Add(this.btnAddGrade);
            this.Controls.Add(this.txtGradeName);
            this.Controls.Add(this.label1);
            this.Name = "FrmAddGrade";
            this.Text = "FrmAddGrade";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGradeName;
        private System.Windows.Forms.Button btnAddGrade;
    }
}