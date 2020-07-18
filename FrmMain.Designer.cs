namespace WinStudent
{
    partial class FrmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.miStudent = new System.Windows.Forms.ToolStripMenuItem();
            this.subAddStudent = new System.Windows.Forms.ToolStripMenuItem();
            this.subStudentList = new System.Windows.Forms.ToolStripMenuItem();
            this.miClass = new System.Windows.Forms.ToolStripMenuItem();
            this.subAddClass = new System.Windows.Forms.ToolStripMenuItem();
            this.subClassList = new System.Windows.Forms.ToolStripMenuItem();
            this.miGrade = new System.Windows.Forms.ToolStripMenuItem();
            this.subAddGrade = new System.Windows.Forms.ToolStripMenuItem();
            this.subGradeList = new System.Windows.Forms.ToolStripMenuItem();
            this.exitSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miStudent,
            this.miClass,
            this.miGrade,
            this.exitSystem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(604, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // miStudent
            // 
            this.miStudent.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subAddStudent,
            this.subStudentList});
            this.miStudent.Name = "miStudent";
            this.miStudent.Size = new System.Drawing.Size(68, 21);
            this.miStudent.Text = "学生管理";
            // 
            // subAddStudent
            // 
            this.subAddStudent.Name = "subAddStudent";
            this.subAddStudent.Size = new System.Drawing.Size(124, 22);
            this.subAddStudent.Text = "新增学生";
            this.subAddStudent.Click += new System.EventHandler(this.subAddStudent_Click);
            // 
            // subStudentList
            // 
            this.subStudentList.Name = "subStudentList";
            this.subStudentList.Size = new System.Drawing.Size(124, 22);
            this.subStudentList.Text = "学生列表";
            this.subStudentList.Click += new System.EventHandler(this.subStudentList_Click);
            // 
            // miClass
            // 
            this.miClass.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subAddClass,
            this.subClassList});
            this.miClass.Name = "miClass";
            this.miClass.Size = new System.Drawing.Size(68, 21);
            this.miClass.Text = "班级管理";
            // 
            // subAddClass
            // 
            this.subAddClass.Name = "subAddClass";
            this.subAddClass.Size = new System.Drawing.Size(124, 22);
            this.subAddClass.Text = "新增班级";
            this.subAddClass.Click += new System.EventHandler(this.subAddClass_Click);
            // 
            // subClassList
            // 
            this.subClassList.Name = "subClassList";
            this.subClassList.Size = new System.Drawing.Size(124, 22);
            this.subClassList.Text = "班级列表";
            this.subClassList.Click += new System.EventHandler(this.subClassList_Click);
            // 
            // miGrade
            // 
            this.miGrade.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subAddGrade,
            this.subGradeList});
            this.miGrade.Name = "miGrade";
            this.miGrade.Size = new System.Drawing.Size(68, 21);
            this.miGrade.Text = "年级管理";
            // 
            // subAddGrade
            // 
            this.subAddGrade.Name = "subAddGrade";
            this.subAddGrade.Size = new System.Drawing.Size(180, 22);
            this.subAddGrade.Text = "新增年级";
            this.subAddGrade.Click += new System.EventHandler(this.subAddGrade_Click);
            // 
            // subGradeList
            // 
            this.subGradeList.Name = "subGradeList";
            this.subGradeList.Size = new System.Drawing.Size(180, 22);
            this.subGradeList.Text = "年级列表";
            this.subGradeList.Click += new System.EventHandler(this.subGradeList_Click);
            // 
            // exitSystem
            // 
            this.exitSystem.Name = "exitSystem";
            this.exitSystem.Size = new System.Drawing.Size(68, 21);
            this.exitSystem.Text = "退出系统";
            this.exitSystem.Click += new System.EventHandler(this.exitSystem_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 461);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmMain";
            this.Text = "学生管理系统主页面";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miStudent;
        private System.Windows.Forms.ToolStripMenuItem subAddStudent;
        private System.Windows.Forms.ToolStripMenuItem subStudentList;
        private System.Windows.Forms.ToolStripMenuItem miClass;
        private System.Windows.Forms.ToolStripMenuItem subAddClass;
        private System.Windows.Forms.ToolStripMenuItem subClassList;
        private System.Windows.Forms.ToolStripMenuItem miGrade;
        private System.Windows.Forms.ToolStripMenuItem subAddGrade;
        private System.Windows.Forms.ToolStripMenuItem subGradeList;
        private System.Windows.Forms.ToolStripMenuItem exitSystem;
    }
}