namespace controle_de_estoque1
{
    partial class FrmRelatorio
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
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGerar1 = new System.Windows.Forms.TextBox();
            this.txtGerar2 = new System.Windows.Forms.TextBox();
            this.butGerar1 = new System.Windows.Forms.Button();
            this.butGerar2 = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(571, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 52);
            this.label1.TabIndex = 0;
            this.label1.Text = "Relatorio";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Blue;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 50);
            this.button1.TabIndex = 4;
            this.button1.Text = "Voltar";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 162);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(576, 547);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Relatorio de Produto";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(727, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(246, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "Relatorio do Funcionario";
            // 
            // txtGerar1
            // 
            this.txtGerar1.Location = new System.Drawing.Point(17, 136);
            this.txtGerar1.Name = "txtGerar1";
            this.txtGerar1.Size = new System.Drawing.Size(420, 22);
            this.txtGerar1.TabIndex = 9;
            // 
            // txtGerar2
            // 
            this.txtGerar2.Location = new System.Drawing.Point(732, 134);
            this.txtGerar2.Name = "txtGerar2";
            this.txtGerar2.Size = new System.Drawing.Size(453, 22);
            this.txtGerar2.TabIndex = 10;
            // 
            // butGerar1
            // 
            this.butGerar1.BackColor = System.Drawing.Color.Blue;
            this.butGerar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butGerar1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.butGerar1.Location = new System.Drawing.Point(448, 106);
            this.butGerar1.Name = "butGerar1";
            this.butGerar1.Size = new System.Drawing.Size(140, 50);
            this.butGerar1.TabIndex = 11;
            this.butGerar1.Text = "Gerar";
            this.butGerar1.UseVisualStyleBackColor = false;
            this.butGerar1.Click += new System.EventHandler(this.butGerar1_Click);
            // 
            // butGerar2
            // 
            this.butGerar2.BackColor = System.Drawing.Color.Blue;
            this.butGerar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butGerar2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.butGerar2.Location = new System.Drawing.Point(1191, 106);
            this.butGerar2.Name = "butGerar2";
            this.butGerar2.Size = new System.Drawing.Size(140, 50);
            this.butGerar2.TabIndex = 12;
            this.butGerar2.Text = "Gerar";
            this.butGerar2.UseVisualStyleBackColor = false;
            this.butGerar2.Click += new System.EventHandler(this.butGerar2_Click);
            // 
            // listView2
            // 
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(732, 162);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(599, 556);
            this.listView2.TabIndex = 13;
            this.listView2.UseCompatibleStateImageBehavior = false;
            // 
            // FrmRelatorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(1348, 721);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.butGerar2);
            this.Controls.Add(this.butGerar1);
            this.Controls.Add(this.txtGerar2);
            this.Controls.Add(this.txtGerar1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmRelatorio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmRelatorio";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGerar1;
        private System.Windows.Forms.TextBox txtGerar2;
        private System.Windows.Forms.Button butGerar1;
        private System.Windows.Forms.Button butGerar2;
        private System.Windows.Forms.ListView listView2;
    }
}