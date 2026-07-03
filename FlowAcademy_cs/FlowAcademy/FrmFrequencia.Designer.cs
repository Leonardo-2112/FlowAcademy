namespace FlowAcademyF
{
    partial class FrmFrequencia
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
            txtPresencas = new TextBox();
            txtTotalAulas = new TextBox();
            txtPercentual = new TextBox();
            cmbDisciplina = new ComboBox();
            cmbMatricula = new ComboBox();
            label6 = new Label();
            dgvFrequencia = new DataGridView();
            label5 = new Label();
            label1 = new Label();
            label2 = new Label();
            label4 = new Label();
            btnEditar = new Button();
            btnSalvar = new Button();
            btnCalcular = new Button();
            btnExcluir = new Button();
            btnCancelar = new Button();
            label3 = new Label();
            txtPesquisa = new TextBox();
            btnPesquisar = new Button();
            lblTurma = new Label();
            cmbTurma = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvFrequencia).BeginInit();
            SuspendLayout();
            // 
            // txtPresencas
            // 
            txtPresencas.Location = new Point(83, 218);
            txtPresencas.Name = "txtPresencas";
            txtPresencas.Size = new Size(100, 23);
            txtPresencas.TabIndex = 5;
            txtPresencas.TextChanged += txtPresencas_TextChanged;
            // 
            // txtTotalAulas
            // 
            txtTotalAulas.Location = new Point(84, 92);
            txtTotalAulas.Name = "txtTotalAulas";
            txtTotalAulas.Size = new Size(100, 23);
            txtTotalAulas.TabIndex = 3;
            txtTotalAulas.TextChanged += txtTotalAulas_TextChanged_1;
            // 
            // txtPercentual
            // 
            txtPercentual.Location = new Point(83, 154);
            txtPercentual.Name = "txtPercentual";
            txtPercentual.ReadOnly = true;
            txtPercentual.Size = new Size(100, 23);
            txtPercentual.TabIndex = 4;
            // 
            // cmbDisciplina
            // 
            cmbDisciplina.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDisciplina.FormattingEnabled = true;
            cmbDisciplina.Location = new Point(475, 26);
            cmbDisciplina.Name = "cmbDisciplina";
            cmbDisciplina.Size = new Size(190, 23);
            cmbDisciplina.TabIndex = 2;
            // 
            // cmbMatricula
            // 
            cmbMatricula.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMatricula.FormattingEnabled = true;
            cmbMatricula.Location = new Point(244, 26);
            cmbMatricula.Name = "cmbMatricula";
            cmbMatricula.Size = new Size(190, 23);
            cmbMatricula.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(83, 200);
            label6.Name = "label6";
            label6.Size = new Size(59, 15);
            label6.TabIndex = 15;
            label6.Text = "Presenças";
            // 
            // dgvFrequencia
            // 
            dgvFrequencia.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFrequencia.Location = new Point(84, 410);
            dgvFrequencia.Name = "dgvFrequencia";
            dgvFrequencia.Size = new Size(668, 243);
            dgvFrequencia.TabIndex = 13;
            dgvFrequencia.CellClick += dgvFrequencia_CellClick;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(84, 74);
            label5.Name = "label5";
            label5.Size = new Size(81, 15);
            label5.TabIndex = 17;
            label5.Text = "Total de Aulas";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(475, 9);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 20;
            label1.Text = "Disciplina";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(83, 136);
            label2.Name = "label2";
            label2.Size = new Size(63, 15);
            label2.TabIndex = 16;
            label2.Text = "Percentual";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(245, 8);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 19;
            label4.Text = "Aluno";
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(445, 282);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(97, 67);
            btnEditar.TabIndex = 8;
            btnEditar.Text = "&Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(290, 282);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(97, 67);
            btnSalvar.TabIndex = 7;
            btnSalvar.Text = "&Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click_1;
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(139, 282);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(93, 67);
            btnCalcular.TabIndex = 6;
            btnCalcular.Text = "&Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // btnExcluir
            // 
            btnExcluir.Location = new Point(600, 279);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(93, 67);
            btnExcluir.TabIndex = 9;
            btnExcluir.Text = "E&xcluir";
            btnExcluir.UseVisualStyleBackColor = true;
            btnExcluir.Click += btnExcluir_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(739, 279);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(93, 67);
            btnCancelar.TabIndex = 10;
            btnCancelar.Text = "&Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(84, 379);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 14;
            label3.Text = "Pesquisar";
            // 
            // txtPesquisa
            // 
            txtPesquisa.Location = new Point(147, 375);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(461, 23);
            txtPesquisa.TabIndex = 11;
            // 
            // btnPesquisar
            // 
            btnPesquisar.Location = new Point(628, 374);
            btnPesquisar.Name = "btnPesquisar";
            btnPesquisar.Size = new Size(124, 25);
            btnPesquisar.TabIndex = 12;
            btnPesquisar.Text = "&Pesquisar";
            btnPesquisar.UseVisualStyleBackColor = true;
            btnPesquisar.Click += btnPesquisar_Click;
            // 
            // lblTurma
            // 
            lblTurma.AutoSize = true;
            lblTurma.Location = new Point(84, 8);
            lblTurma.Name = "lblTurma";
            lblTurma.Size = new Size(42, 15);
            lblTurma.TabIndex = 18;
            lblTurma.Text = "Turma";
            // 
            // cmbTurma
            // 
            cmbTurma.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTurma.FormattingEnabled = true;
            cmbTurma.Location = new Point(83, 26);
            cmbTurma.Name = "cmbTurma";
            cmbTurma.Size = new Size(130, 23);
            cmbTurma.TabIndex = 0;
            cmbTurma.SelectedIndexChanged += cmbTurma_SelectedIndexChanged;
            // 
            // FrmFrequencia
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(928, 682);
            Controls.Add(cmbTurma);
            Controls.Add(lblTurma);
            Controls.Add(btnPesquisar);
            Controls.Add(txtPesquisa);
            Controls.Add(label3);
            Controls.Add(btnCancelar);
            Controls.Add(btnExcluir);
            Controls.Add(btnEditar);
            Controls.Add(btnSalvar);
            Controls.Add(btnCalcular);
            Controls.Add(txtPresencas);
            Controls.Add(txtTotalAulas);
            Controls.Add(txtPercentual);
            Controls.Add(cmbDisciplina);
            Controls.Add(cmbMatricula);
            Controls.Add(label6);
            Controls.Add(dgvFrequencia);
            Controls.Add(label5);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(label4);
            Name = "FrmFrequencia";
            Text = "FrmFrequencia";
            Load += FrmFrequencia_Load;
            ((System.ComponentModel.ISupportInitialize)dgvFrequencia).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPresencas;
        private TextBox txtTotalAulas;
        private TextBox txtPercentual;
        private ComboBox cmbDisciplina;
        private ComboBox cmbMatricula;
        private Label label6;
        private DataGridView dgvFrequencia;
        private Label label5;
        private Label label1;
        private Label label2;
        private Label label4;
        private Button btnEditar;
        private Button btnSalvar;
        private Button btnCalcular;
        private Button btnExcluir;
        private Button btnCancelar;
        private Label label3;
        private TextBox txtPesquisa;
        private Button btnPesquisar;
        private Label lblTurma;
        private ComboBox cmbTurma;
    }
}
