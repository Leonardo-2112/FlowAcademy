namespace FlowAcademyF
{
    partial class FrmNota
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
            cmbDisciplina = new ComboBox();
            cmbMatricula = new ComboBox();
            dgvNota = new DataGridView();
            label1 = new Label();
            label4 = new Label();
            txtNota1 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            txtNota2 = new TextBox();
            label5 = new Label();
            txtMedia = new TextBox();
            label6 = new Label();
            txtStatus = new TextBox();
            btnEditar = new Button();
            btnSalvar = new Button();
            btnCalcular = new Button();
            btnExcluir = new Button();
            btnCancelar = new Button();
            dtpData = new DateTimePicker();
            label7 = new Label();
            txtTrabalho = new TextBox();
            txtComportamento = new TextBox();
            label8 = new Label();
            label10 = new Label();
            label9 = new Label();
            txtPesquisa = new TextBox();
            btnPesquisar = new Button();
            lblTurma = new Label();
            cmbTurma = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvNota).BeginInit();
            SuspendLayout();
            // 
            // cmbDisciplina
            // 
            cmbDisciplina.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDisciplina.FormattingEnabled = true;
            cmbDisciplina.Location = new Point(485, 48);
            cmbDisciplina.Name = "cmbDisciplina";
            cmbDisciplina.Size = new Size(190, 23);
            cmbDisciplina.TabIndex = 84;
            // 
            // cmbMatricula
            // 
            cmbMatricula.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMatricula.FormattingEnabled = true;
            cmbMatricula.Location = new Point(255, 48);
            cmbMatricula.Name = "cmbMatricula";
            cmbMatricula.Size = new Size(190, 23);
            cmbMatricula.TabIndex = 81;
            // 
            // dgvNota
            // 
            dgvNota.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNota.Location = new Point(75, 386);
            dgvNota.Name = "dgvNota";
            dgvNota.Size = new Size(668, 243);
            dgvNota.TabIndex = 76;
            dgvNota.CellDoubleClick += dgvNota_CellDoubleClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(485, 31);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 73;
            label1.Text = "Disciplina";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(255, 30);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 71;
            label4.Text = "Aluno";
            // 
            // txtNota1
            // 
            txtNota1.Location = new Point(92, 114);
            txtNota1.Name = "txtNota1";
            txtNota1.Size = new Size(100, 23);
            txtNota1.TabIndex = 86;
            txtNota1.TextChanged += txtNota1_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(92, 96);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 72;
            label2.Text = "Nota 1";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(295, 96);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 72;
            label3.Text = "Nota 2";
            // 
            // txtNota2
            // 
            txtNota2.Location = new Point(295, 114);
            txtNota2.Name = "txtNota2";
            txtNota2.Size = new Size(100, 23);
            txtNota2.TabIndex = 86;
            txtNota2.TextChanged += txtNota2_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(93, 179);
            label5.Name = "label5";
            label5.Size = new Size(68, 15);
            label5.TabIndex = 72;
            label5.Text = "Média Final";
            // 
            // txtMedia
            // 
            txtMedia.Location = new Point(93, 197);
            txtMedia.Name = "txtMedia";
            txtMedia.ReadOnly = true;
            txtMedia.Size = new Size(100, 23);
            txtMedia.TabIndex = 86;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(355, 179);
            label6.Name = "label6";
            label6.Size = new Size(39, 15);
            label6.TabIndex = 72;
            label6.Text = "Status";
            // 
            // txtStatus
            // 
            txtStatus.Location = new Point(355, 197);
            txtStatus.Name = "txtStatus";
            txtStatus.ReadOnly = true;
            txtStatus.Size = new Size(100, 23);
            txtStatus.TabIndex = 86;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(492, 270);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(97, 64);
            btnEditar.TabIndex = 90;
            btnEditar.Text = "&Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(358, 270);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(97, 64);
            btnSalvar.TabIndex = 89;
            btnSalvar.Text = "&Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(182, 270);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(93, 67);
            btnCalcular.TabIndex = 88;
            btnCalcular.Text = "&Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // btnExcluir
            // 
            btnExcluir.Location = new Point(626, 270);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(93, 64);
            btnExcluir.TabIndex = 94;
            btnExcluir.Text = "E&xcluir";
            btnExcluir.UseVisualStyleBackColor = true;
            btnExcluir.Click += btnExcluir_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(758, 270);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(93, 64);
            btnCancelar.TabIndex = 95;
            btnCancelar.Text = "&Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // dtpData
            // 
            dtpData.Location = new Point(701, 45);
            dtpData.Name = "dtpData";
            dtpData.Size = new Size(200, 23);
            dtpData.TabIndex = 91;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(492, 96);
            label7.Name = "label7";
            label7.Size = new Size(53, 15);
            label7.TabIndex = 92;
            label7.Text = "Trabalho";
            // 
            // txtTrabalho
            // 
            txtTrabalho.Location = new Point(498, 114);
            txtTrabalho.Name = "txtTrabalho";
            txtTrabalho.Size = new Size(100, 23);
            txtTrabalho.TabIndex = 93;
            txtTrabalho.TextChanged += txtTrabalho_TextChanged;
            // 
            // txtComportamento
            // 
            txtComportamento.Location = new Point(701, 114);
            txtComportamento.Name = "txtComportamento";
            txtComportamento.Size = new Size(100, 23);
            txtComportamento.TabIndex = 93;
            txtComportamento.TextChanged += txtComportamento_TextChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(701, 96);
            label8.Name = "label8";
            label8.Size = new Size(96, 15);
            label8.TabIndex = 92;
            label8.Text = "Comportamento";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(701, 27);
            label10.Name = "label10";
            label10.Size = new Size(31, 15);
            label10.TabIndex = 92;
            label10.Text = "Data";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(75, 354);
            label9.Name = "label9";
            label9.Size = new Size(57, 15);
            label9.TabIndex = 96;
            label9.Text = "Pesquisar";
            // 
            // txtPesquisa
            // 
            txtPesquisa.Location = new Point(138, 350);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(461, 23);
            txtPesquisa.TabIndex = 97;
            // 
            // btnPesquisar
            // 
            btnPesquisar.Location = new Point(619, 349);
            btnPesquisar.Name = "btnPesquisar";
            btnPesquisar.Size = new Size(124, 25);
            btnPesquisar.TabIndex = 98;
            btnPesquisar.Text = "&Pesquisar";
            btnPesquisar.UseVisualStyleBackColor = true;
            btnPesquisar.Click += btnPesquisar_Click;
            // 
            // lblTurma
            // 
            lblTurma.AutoSize = true;
            lblTurma.Location = new Point(92, 30);
            lblTurma.Name = "lblTurma";
            lblTurma.Size = new Size(42, 15);
            lblTurma.TabIndex = 99;
            lblTurma.Text = "Turma";
            // 
            // cmbTurma
            // 
            cmbTurma.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTurma.FormattingEnabled = true;
            cmbTurma.Location = new Point(92, 48);
            cmbTurma.Name = "cmbTurma";
            cmbTurma.Size = new Size(130, 23);
            cmbTurma.TabIndex = 100;
            cmbTurma.SelectedIndexChanged += cmbTurma_SelectedIndexChanged;
            // 
            // FrmNota
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(937, 684);
            Controls.Add(cmbTurma);
            Controls.Add(lblTurma);
            Controls.Add(btnPesquisar);
            Controls.Add(txtPesquisa);
            Controls.Add(label9);
            Controls.Add(btnCancelar);
            Controls.Add(btnExcluir);
            Controls.Add(txtComportamento);
            Controls.Add(txtTrabalho);
            Controls.Add(label10);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(dtpData);
            Controls.Add(btnEditar);
            Controls.Add(btnSalvar);
            Controls.Add(btnCalcular);
            Controls.Add(txtStatus);
            Controls.Add(txtMedia);
            Controls.Add(txtNota2);
            Controls.Add(txtNota1);
            Controls.Add(cmbDisciplina);
            Controls.Add(cmbMatricula);
            Controls.Add(label6);
            Controls.Add(dgvNota);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(label4);
            Name = "FrmNota";
            Text = "FrmNota";
            Load += FrmNota_Load;
            ((System.ComponentModel.ISupportInitialize)dgvNota).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbDisciplina;
        private ComboBox cmbMatricula;
        private DataGridView dgvNota;
        private Label label1;
        private Label label4;
        private TextBox txtNota1;
        private Label label2;
        private Label label3;
        private TextBox txtNota2;
        private Label label5;
        private TextBox txtMedia;
        private Label label6;
        private TextBox txtStatus;
        private Button btnEditar;
        private Button btnSalvar;
        private Button btnCalcular;
        private Button btnExcluir;
        private Button btnCancelar;
        private DateTimePicker dtpData;
        private Label label7;
        private TextBox txtTrabalho;
        private TextBox txtComportamento;
        private Label label8;
        private Label label10;
        private Label label9;
        private TextBox txtPesquisa;
        private Button btnPesquisar;
        private Label lblTurma;
        private ComboBox cmbTurma;
    }
}
