namespace FlowAcademyF
{
    partial class FrmFrequenciaAluno
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
            dgvFrequencia = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvFrequencia).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(331, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(139, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Frequência";
            // 
            // lblResumo
            // 
            lblResumo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblResumo.Location = new Point(270, 67);
            lblResumo.Name = "lblResumo";
            lblResumo.Size = new Size(270, 23);
            lblResumo.TabIndex = 1;
            lblResumo.Text = "Consulta de frequência por unidade curricular.";
            // 
            // dgvFrequencia
            // 
            dgvFrequencia.AllowUserToAddRows = false;
            dgvFrequencia.AllowUserToDeleteRows = false;
            dgvFrequencia.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvFrequencia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFrequencia.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFrequencia.Location = new Point(12, 111);
            dgvFrequencia.Name = "dgvFrequencia";
            dgvFrequencia.ReadOnly = true;
            dgvFrequencia.Size = new Size(776, 327);
            dgvFrequencia.TabIndex = 2;
            // 
            // FrmFrequenciaAluno
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvFrequencia);
            Controls.Add(lblResumo);
            Controls.Add(lblTitulo);
            Name = "FrmFrequenciaAluno";
            Text = "Frequência";
            Load += FrmFrequenciaAluno_Load;
            ((System.ComponentModel.ISupportInitialize)dgvFrequencia).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblResumo;
        private DataGridView dgvFrequencia;
    }
}
