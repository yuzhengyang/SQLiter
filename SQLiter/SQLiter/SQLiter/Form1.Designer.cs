namespace SQLiter
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.BtInsert = new System.Windows.Forms.Button();
            this.BtDelete = new System.Windows.Forms.Button();
            this.BtUpdate = new System.Windows.Forms.Button();
            this.BtSelect = new System.Windows.Forms.Button();
            this.TbInsert = new System.Windows.Forms.TextBox();
            this.TbDelete = new System.Windows.Forms.TextBox();
            this.TbUpdate = new System.Windows.Forms.TextBox();
            this.TbSelect = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TbTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // BtInsert
            // 
            this.BtInsert.Location = new System.Drawing.Point(277, 30);
            this.BtInsert.Name = "BtInsert";
            this.BtInsert.Size = new System.Drawing.Size(75, 23);
            this.BtInsert.TabIndex = 0;
            this.BtInsert.Text = "Insert";
            this.BtInsert.UseVisualStyleBackColor = true;
            this.BtInsert.Click += new System.EventHandler(this.BtInsert_Click);
            // 
            // BtDelete
            // 
            this.BtDelete.Location = new System.Drawing.Point(277, 63);
            this.BtDelete.Name = "BtDelete";
            this.BtDelete.Size = new System.Drawing.Size(75, 23);
            this.BtDelete.TabIndex = 1;
            this.BtDelete.Text = "Delete";
            this.BtDelete.UseVisualStyleBackColor = true;
            this.BtDelete.Click += new System.EventHandler(this.BtDelete_Click);
            // 
            // BtUpdate
            // 
            this.BtUpdate.Location = new System.Drawing.Point(626, 30);
            this.BtUpdate.Name = "BtUpdate";
            this.BtUpdate.Size = new System.Drawing.Size(75, 23);
            this.BtUpdate.TabIndex = 2;
            this.BtUpdate.Text = "Update";
            this.BtUpdate.UseVisualStyleBackColor = true;
            this.BtUpdate.Click += new System.EventHandler(this.BtUpdate_Click);
            // 
            // BtSelect
            // 
            this.BtSelect.Location = new System.Drawing.Point(626, 63);
            this.BtSelect.Name = "BtSelect";
            this.BtSelect.Size = new System.Drawing.Size(75, 23);
            this.BtSelect.TabIndex = 3;
            this.BtSelect.Text = "Select";
            this.BtSelect.UseVisualStyleBackColor = true;
            this.BtSelect.Click += new System.EventHandler(this.BtSelect_Click);
            // 
            // TbInsert
            // 
            this.TbInsert.Location = new System.Drawing.Point(110, 30);
            this.TbInsert.Multiline = true;
            this.TbInsert.Name = "TbInsert";
            this.TbInsert.Size = new System.Drawing.Size(159, 23);
            this.TbInsert.TabIndex = 4;
            // 
            // TbDelete
            // 
            this.TbDelete.Location = new System.Drawing.Point(110, 63);
            this.TbDelete.Multiline = true;
            this.TbDelete.Name = "TbDelete";
            this.TbDelete.Size = new System.Drawing.Size(159, 23);
            this.TbDelete.TabIndex = 5;
            // 
            // TbUpdate
            // 
            this.TbUpdate.Location = new System.Drawing.Point(457, 30);
            this.TbUpdate.Multiline = true;
            this.TbUpdate.Name = "TbUpdate";
            this.TbUpdate.Size = new System.Drawing.Size(159, 23);
            this.TbUpdate.TabIndex = 6;
            // 
            // TbSelect
            // 
            this.TbSelect.Location = new System.Drawing.Point(457, 63);
            this.TbSelect.Multiline = true;
            this.TbSelect.Name = "TbSelect";
            this.TbSelect.Size = new System.Drawing.Size(159, 23);
            this.TbSelect.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(27, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "输入 Name：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(27, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 23);
            this.label2.TabIndex = 9;
            this.label2.Text = "输入 Id：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(374, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 23);
            this.label3.TabIndex = 10;
            this.label3.Text = "输入 Id：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(374, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 23);
            this.label4.TabIndex = 11;
            this.label4.Text = "输入 Id：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TbTxt
            // 
            this.TbTxt.Location = new System.Drawing.Point(29, 122);
            this.TbTxt.Multiline = true;
            this.TbTxt.Name = "TbTxt";
            this.TbTxt.Size = new System.Drawing.Size(672, 330);
            this.TbTxt.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 494);
            this.Controls.Add(this.TbTxt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TbSelect);
            this.Controls.Add(this.TbUpdate);
            this.Controls.Add(this.TbDelete);
            this.Controls.Add(this.TbInsert);
            this.Controls.Add(this.BtSelect);
            this.Controls.Add(this.BtUpdate);
            this.Controls.Add(this.BtDelete);
            this.Controls.Add(this.BtInsert);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtInsert;
        private System.Windows.Forms.Button BtDelete;
        private System.Windows.Forms.Button BtUpdate;
        private System.Windows.Forms.Button BtSelect;
        private System.Windows.Forms.TextBox TbInsert;
        private System.Windows.Forms.TextBox TbDelete;
        private System.Windows.Forms.TextBox TbUpdate;
        private System.Windows.Forms.TextBox TbSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TbTxt;
    }
}

