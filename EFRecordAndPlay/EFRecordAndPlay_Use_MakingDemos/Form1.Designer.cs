namespace EFRecordAndPlay_Use_MakingDemos
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.cmbDepartments = new System.Windows.Forms.ComboBox();
            this.departmentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.firstNameEmployeeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastNameEmployeeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employeeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnAddDepartment = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.departmentBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.employeeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbDepartments
            // 
            this.cmbDepartments.DataSource = this.departmentBindingSource;
            this.cmbDepartments.DisplayMember = "NameDepartment";
            this.cmbDepartments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartments.FormattingEnabled = true;
            this.cmbDepartments.Location = new System.Drawing.Point(58, 28);
            this.cmbDepartments.Name = "cmbDepartments";
            this.cmbDepartments.Size = new System.Drawing.Size(121, 21);
            this.cmbDepartments.TabIndex = 0;
            // 
            // departmentBindingSource
            // 
            this.departmentBindingSource.DataSource = typeof(EFRecordAndPlay_Use_MakingDemos.DatabaseRelated.Department);
            this.departmentBindingSource.CurrentChanged += new System.EventHandler(this.departmentBindingSource_CurrentChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.firstNameEmployeeDataGridViewTextBoxColumn,
            this.lastNameEmployeeDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.employeeBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(23, 72);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(586, 280);
            this.dataGridView1.TabIndex = 1;
            // 
            // firstNameEmployeeDataGridViewTextBoxColumn
            // 
            this.firstNameEmployeeDataGridViewTextBoxColumn.DataPropertyName = "FirstNameEmployee";
            this.firstNameEmployeeDataGridViewTextBoxColumn.HeaderText = "FirstNameEmployee";
            this.firstNameEmployeeDataGridViewTextBoxColumn.Name = "firstNameEmployeeDataGridViewTextBoxColumn";
            this.firstNameEmployeeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lastNameEmployeeDataGridViewTextBoxColumn
            // 
            this.lastNameEmployeeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.lastNameEmployeeDataGridViewTextBoxColumn.DataPropertyName = "LastNameEmployee";
            this.lastNameEmployeeDataGridViewTextBoxColumn.HeaderText = "LastNameEmployee";
            this.lastNameEmployeeDataGridViewTextBoxColumn.Name = "lastNameEmployeeDataGridViewTextBoxColumn";
            this.lastNameEmployeeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // employeeBindingSource
            // 
            this.employeeBindingSource.DataSource = typeof(EFRecordAndPlay_Use_MakingDemos.DatabaseRelated.Employee);
            // 
            // btnAddDepartment
            // 
            this.btnAddDepartment.Location = new System.Drawing.Point(263, 28);
            this.btnAddDepartment.Name = "btnAddDepartment";
            this.btnAddDepartment.Size = new System.Drawing.Size(115, 23);
            this.btnAddDepartment.TabIndex = 2;
            this.btnAddDepartment.Text = "Add Department";
            this.btnAddDepartment.UseVisualStyleBackColor = true;
            this.btnAddDepartment.Click += new System.EventHandler(this.btnAddDepartment_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 364);
            this.Controls.Add(this.btnAddDepartment);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmbDepartments);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.departmentBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.employeeBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDepartments;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource employeeBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstNameEmployeeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastNameEmployeeDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource departmentBindingSource;
        private System.Windows.Forms.Button btnAddDepartment;
    }
}

