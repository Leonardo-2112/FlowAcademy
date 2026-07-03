namespace FlowAcademyF
{
    partial class FrmBoletimAluno
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblResumo = new Label();
            dgvBoletim = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvBoletim).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(336, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(102, 32);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Boletim";
            lblTitulo.Click += lblTitulo_Click;
            // 
            // lblResumo
            // 
            lblResumo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblResumo.Location = new Point(276, 61);
            lblResumo.Name = "lblResumo";
            lblResumo.Size = new Size(235, 23);
            lblResumo.TabIndex = 2;
            lblResumo.Text = "Consulta de notas por unidade curricular.";
            // 
            // dgvBoletim
            // 
            dgvBoletim.AllowUserToAddRows = false;
            dgvBoletim.AllowUserToDeleteRows = false;
            dgvBoletim.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvBoletim.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBoletim.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBoletim.Location = new Point(12, 105);
            dgvBoletim.Name = "dgvBoletim";
            dgvBoletim.ReadOnly = true;
            dgvBoletim.Size = new Size(776, 333);
            dgvBoletim.TabIndex = 0;
            // 
            // FrmBoletimAluno
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvBoletim);
            Controls.Add(lblResumo);
            Controls.Add(lblTitulo);
            Name = "FrmBoletimAluno";
            Text = "Boletim / Notas";
            Load += FrmBoletimAluno_Load;
            ((System.ComponentModel.ISupportInitialize)dgvBoletim).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblResumo;
        private DataGridView dgvBoletim;
    }
}
