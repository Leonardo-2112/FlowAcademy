namespace FlowAcademyF
{
    partial class FrmMatricula
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
            dtpDataMatricula = new DateTimePicker();
            cmbTurma = new ComboBox();
            cmbStatus = new ComboBox();
            cmbAluno = new ComboBox();
            dgvMatricula = new DataGridView();
            label6 = new Label();
            label1 = new Label();
            label8 = new Label();
            label4 = new Label();
            btnEditar = new Button();
            btnSalvar = new Button();
            btnCancelar = new Button();
            btnExcluir = new Button();
            label2 = new Label();
            txtPesquisa = new TextBox();
            btnPesquisar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvMatricula).BeginInit();
            SuspendLayout();
            // 
            // dtpDataMatricula
            // 
            dtpDataMatricula.Location = new Point(452, 57);
            dtpDataMatricula.Name = "dtpDataMatricula";
            dtpDataMatricula.Size = new Size(200, 23);
            dtpDataMatricula.TabIndex = 2;
            // 
            // cmbTurma
            // 
            cmbTurma.FormattingEnabled = true;
            cmbTurma.Location = new Point(267, 57);
            cmbTurma.Name = "cmbTurma";
            cmbTurma.Size = new Size(121, 23);
            cmbTurma.TabIndex = 1;
            // 
            // cmbStatus
            // 
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(90, 142);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(121, 23);
            cmbStatus.TabIndex = 3;
            // 
            // cmbAluno
            // 
            cmbAluno.FormattingEnabled = true;
            cmbAluno.Location = new Point(90, 57);
            cmbAluno.Name = "cmbAluno";
            cmbAluno.Size = new Size(121, 23);
            cmbAluno.TabIndex = 0;
            // 
            // dgvMatricula
            // 
            dgvMatricula.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMatricula.Location = new Point(66, 364);
            dgvMatricula.Name = "dgvMatricula";
            dgvMatricula.Size = new Size(668, 243);
            dgvMatricula.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(452, 39);
            label6.Name = "label6";
            label6.Size = new Size(84, 15);
            label6.TabIndex = 14;
            label6.Text = "Data Matrícula";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(267, 40);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 13;
            label1.Text = "Turma";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(90, 123);
            label8.Name = "label8";
            label8.Size = new Size(39, 15);
            label8.TabIndex = 15;
            label8.Text = "Status";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(91, 39);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 12;
            label4.Text = "Aluno";
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(291, 245);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(97, 64);
            btnEditar.TabIndex = 5;
            btnEditar.Text = "&Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(146, 245);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(97, 64);
            btnSalvar.TabIndex = 4;
            btnSalvar.Text = "&Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(436, 242);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(93, 67);
            btnCancelar.TabIndex = 6;
            btnCancelar.Text = "&Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnExcluir
            // 
            btnExcluir.Location = new Point(577, 242);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(93, 67);
            btnExcluir.TabIndex = 7;
            btnExcluir.Text = "E&xcluir";
            btnExcluir.UseVisualStyleBackColor = true;
            btnExcluir.Click += btnExcluir_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(66, 333);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 11;
            label2.Text = "Pesquisar";
            // 
            // txtPesquisa
            // 
            txtPesquisa.Location = new Point(129, 329);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(461, 23);
            txtPesquisa.TabIndex = 8;
            // 
            // btnPesquisar
            // 
            btnPesquisar.Location = new Point(610, 328);
            btnPesquisar.Name = "btnPesquisar";
            btnPesquisar.Size = new Size(124, 25);
            btnPesquisar.TabIndex = 9;
            btnPesquisar.Text = "&Pesquisar";
            btnPesquisar.UseVisualStyleBackColor = true;
            btnPesquisar.Click += btnPesquisar_Click;
            // 
            // FrmMatricula
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 622);
            Controls.Add(btnPesquisar);
            Controls.Add(txtPesquisa);
            Controls.Add(label2);
            Controls.Add(btnEditar);
            Controls.Add(btnSalvar);
            Controls.Add(btnCancelar);
            Controls.Add(btnExcluir);
            Controls.Add(dtpDataMatricula);
            Controls.Add(cmbTurma);
            Controls.Add(cmbStatus);
            Controls.Add(cmbAluno);
            Controls.Add(dgvMatricula);
            Controls.Add(label6);
            Controls.Add(label1);
            Controls.Add(label8);
            Controls.Add(label4);
            Name = "FrmMatricula";
            Text = "FrmMatricula";
            Load += FrmMatricula_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMatricula).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker dtpDataMatricula;
        private ComboBox cmbTurma;
        private ComboBox cmbStatus;
        private ComboBox cmbAluno;
        private DataGridView dgvMatricula;
        private Label label6;
        private Label label1;
        private Label label8;
        private Label label4;
        private Button btnEditar;
        private Button btnSalvar;
        private Button btnCancelar;
        private Button btnExcluir;
        private Label label2;
        private TextBox txtPesquisa;
        private Button btnPesquisar;
    }
}
