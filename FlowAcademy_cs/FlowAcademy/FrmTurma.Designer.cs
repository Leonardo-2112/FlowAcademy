namespace FlowAcademyF
{
    partial class FrmTurma
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTurma));
            cmbCurso = new ComboBox();
            dgvTurma = new DataGridView();
            label3 = new Label();
            label4 = new Label();
            label1 = new Label();
            cmbProfessor = new ComboBox();
            txtCodTurma = new TextBox();
            label2 = new Label();
            cmbTurno = new ComboBox();
            nudCapacidade = new NumericUpDown();
            label5 = new Label();
            label8 = new Label();
            txtStatus = new ComboBox();
            btnEditar = new Button();
            btnSalvar = new Button();
            btnCancelar = new Button();
            btnExcluir = new Button();
            txtPeriodoLetivo = new TextBox();
            label6 = new Label();
            label7 = new Label();
            txtPesquisa = new TextBox();
            btnPesquisar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvTurma).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCapacidade).BeginInit();
            SuspendLayout();
            // 
            // cmbCurso
            // 
            cmbCurso.FormattingEnabled = true;
            cmbCurso.Location = new Point(70, 46);
            cmbCurso.Name = "cmbCurso";
            cmbCurso.Size = new Size(178, 23);
            cmbCurso.TabIndex = 45;
            // 
            // dgvTurma
            // 
            dgvTurma.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTurma.Location = new Point(70, 373);
            dgvTurma.Name = "dgvTurma";
            dgvTurma.Size = new Size(667, 131);
            dgvTurma.TabIndex = 39;
            dgvTurma.CellDoubleClick += dgvTurma_CellDoubleClick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(70, 124);
            label3.Name = "label3";
            label3.Size = new Size(84, 15);
            label3.TabIndex = 35;
            label3.Text = "Código Turma";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(71, 28);
            label4.Name = "label4";
            label4.Size = new Size(38, 15);
            label4.TabIndex = 36;
            label4.Text = "Curso";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(343, 28);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 36;
            label1.Text = "Professor";
            // 
            // cmbProfessor
            // 
            cmbProfessor.FormattingEnabled = true;
            cmbProfessor.Location = new Point(343, 46);
            cmbProfessor.Name = "cmbProfessor";
            cmbProfessor.Size = new Size(197, 23);
            cmbProfessor.TabIndex = 45;
            // 
            // txtCodTurma
            // 
            txtCodTurma.Location = new Point(71, 144);
            txtCodTurma.Name = "txtCodTurma";
            txtCodTurma.Size = new Size(100, 23);
            txtCodTurma.TabIndex = 46;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(616, 29);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 36;
            label2.Text = "Turno";
            // 
            // cmbTurno
            // 
            cmbTurno.FormattingEnabled = true;
            cmbTurno.Location = new Point(616, 46);
            cmbTurno.Name = "cmbTurno";
            cmbTurno.Size = new Size(121, 23);
            cmbTurno.TabIndex = 45;
            // 
            // nudCapacidade
            // 
            nudCapacidade.Location = new Point(614, 144);
            nudCapacidade.Name = "nudCapacidade";
            nudCapacidade.Size = new Size(100, 23);
            nudCapacidade.TabIndex = 47;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(614, 124);
            label5.Name = "label5";
            label5.Size = new Size(69, 15);
            label5.TabIndex = 36;
            label5.Text = "Capacidade";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(252, 124);
            label8.Name = "label8";
            label8.Size = new Size(39, 15);
            label8.TabIndex = 36;
            label8.Text = "Status";
            // 
            // txtStatus
            // 
            txtStatus.FormattingEnabled = true;
            txtStatus.Location = new Point(252, 142);
            txtStatus.Name = "txtStatus";
            txtStatus.Size = new Size(100, 23);
            txtStatus.TabIndex = 45;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(283, 266);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(97, 64);
            btnEditar.TabIndex = 52;
            btnEditar.Text = "&Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(138, 266);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(97, 64);
            btnSalvar.TabIndex = 51;
            btnSalvar.Text = "&Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(428, 263);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(93, 67);
            btnCancelar.TabIndex = 50;
            btnCancelar.Text = "&Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnExcluir
            // 
            btnExcluir.Location = new Point(569, 263);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(93, 67);
            btnExcluir.TabIndex = 49;
            btnExcluir.Text = "E&xcluir";
            btnExcluir.UseVisualStyleBackColor = true;
            btnExcluir.Click += btnExcluir_Click;
            // 
            // txtPeriodoLetivo
            // 
            txtPeriodoLetivo.Location = new Point(433, 144);
            txtPeriodoLetivo.Name = "txtPeriodoLetivo";
            txtPeriodoLetivo.Size = new Size(100, 23);
            txtPeriodoLetivo.TabIndex = 53;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(433, 124);
            label6.Name = "label6";
            label6.Size = new Size(83, 15);
            label6.TabIndex = 36;
            label6.Text = "Periodo Letivo";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(70, 342);
            label7.Name = "label7";
            label7.Size = new Size(57, 15);
            label7.TabIndex = 54;
            label7.Text = "Pesquisar";
            // 
            // txtPesquisa
            // 
            txtPesquisa.Location = new Point(133, 338);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(461, 23);
            txtPesquisa.TabIndex = 55;
            // 
            // btnPesquisar
            // 
            btnPesquisar.Location = new Point(613, 337);
            btnPesquisar.Name = "btnPesquisar";
            btnPesquisar.Size = new Size(124, 25);
            btnPesquisar.TabIndex = 56;
            btnPesquisar.Text = "&Pesquisar";
            btnPesquisar.UseVisualStyleBackColor = true;
            btnPesquisar.Click += btnPesquisar_Click;
            // 
            // FrmTurma
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 593);
            Controls.Add(btnPesquisar);
            Controls.Add(txtPesquisa);
            Controls.Add(label7);
            Controls.Add(txtPeriodoLetivo);
            Controls.Add(btnEditar);
            Controls.Add(btnSalvar);
            Controls.Add(btnCancelar);
            Controls.Add(btnExcluir);
            Controls.Add(nudCapacidade);
            Controls.Add(txtCodTurma);
            Controls.Add(cmbTurno);
            Controls.Add(cmbProfessor);
            Controls.Add(txtStatus);
            Controls.Add(cmbCurso);
            Controls.Add(label2);
            Controls.Add(dgvTurma);
            Controls.Add(label5);
            Controls.Add(label1);
            Controls.Add(label6);
            Controls.Add(label8);
            Controls.Add(label3);
            Controls.Add(label4);
            Name = "FrmTurma";
            Text = "FrmTurma";
            Load += FrmTurma_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTurma).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCapacidade).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbCurso;
        private DataGridView dgvTurma;
        private Label label3;
        private Label label4;
        private Label label1;
        private ComboBox cmbProfessor;
        private TextBox txtCodTurma;
        private Label label2;
        private ComboBox cmbTurno;
        private NumericUpDown nudCapacidade;
        private Label label5;
        private Label label8;
        private ComboBox txtStatus;
        private Button btnEditar;
        private Button btnSalvar;
        private Button btnCancelar;
        private Button btnExcluir;
        private TextBox txtPeriodoLetivo;
        private Label label6;
        private Label label7;
        private TextBox txtPesquisa;
        private Button btnPesquisar;
    }
}
