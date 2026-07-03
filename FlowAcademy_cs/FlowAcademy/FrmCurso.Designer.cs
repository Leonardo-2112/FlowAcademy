namespace FlowAcademyF
{
    partial class FrmCurso
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
            label1 = new Label();
            txtNome = new TextBox();
            label2 = new Label();
            txtDescricao = new TextBox();
            label3 = new Label();
            dgvCurso = new DataGridView();
            nudCargaHoraria = new NumericUpDown();
            txtStatus = new ComboBox();
            label4 = new Label();
            btnEditar = new Button();
            btnSalvar = new Button();
            btnCancelar = new Button();
            btnExcluir = new Button();
            label5 = new Label();
            txtPesquisa = new TextBox();
            btnPesquisar = new Button();
            lblDisciplinas = new Label();
            lstDisciplinas = new ListBox();
            ((System.ComponentModel.ISupportInitialize)dgvCurso).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCargaHoraria).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(53, 46);
            label1.Name = "label1";
            label1.Size = new Size(40, 15);
            label1.TabIndex = 11;
            label1.Text = "Nome";
            // 
            // txtNome
            // 
            txtNome.Location = new Point(53, 64);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(306, 23);
            txtNome.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(213, 99);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 17;
            label2.Text = "Descrição";
            // 
            // txtDescricao
            // 
            txtDescricao.Location = new Point(213, 117);
            txtDescricao.Name = "txtDescricao";
            txtDescricao.Size = new Size(306, 23);
            txtDescricao.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(399, 46);
            label3.Name = "label3";
            label3.Size = new Size(80, 15);
            label3.TabIndex = 12;
            label3.Text = "Carga Horária";
            // 
            // dgvCurso
            // 
            dgvCurso.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCurso.Location = new Point(103, 373);
            dgvCurso.Name = "dgvCurso";
            dgvCurso.RowHeadersWidth = 51;
            dgvCurso.Size = new Size(668, 243);
            dgvCurso.TabIndex = 15;
            // 
            // nudCargaHoraria
            // 
            nudCargaHoraria.Location = new Point(399, 65);
            nudCargaHoraria.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudCargaHoraria.Name = "nudCargaHoraria";
            nudCargaHoraria.Size = new Size(120, 23);
            nudCargaHoraria.TabIndex = 1;
            // 
            // txtStatus
            // 
            txtStatus.FormattingEnabled = true;
            txtStatus.Location = new Point(56, 117);
            txtStatus.Name = "txtStatus";
            txtStatus.Size = new Size(121, 23);
            txtStatus.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(56, 99);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 16;
            label4.Text = "Status";
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(334, 252);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(97, 64);
            btnEditar.TabIndex = 5;
            btnEditar.Text = "&Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(189, 252);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(97, 64);
            btnSalvar.TabIndex = 4;
            btnSalvar.Text = "&Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(479, 249);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(93, 67);
            btnCancelar.TabIndex = 6;
            btnCancelar.Text = "&Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnExcluir
            // 
            btnExcluir.Location = new Point(620, 249);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(93, 67);
            btnExcluir.TabIndex = 7;
            btnExcluir.Text = "E&xcluir";
            btnExcluir.UseVisualStyleBackColor = true;
            btnExcluir.Click += btnExcluir_Click_1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(103, 342);
            label5.Name = "label5";
            label5.Size = new Size(57, 15);
            label5.TabIndex = 14;
            label5.Text = "Pesquisar";
            // 
            // txtPesquisa
            // 
            txtPesquisa.Location = new Point(166, 338);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(461, 23);
            txtPesquisa.TabIndex = 8;
            // 
            // btnPesquisar
            // 
            btnPesquisar.Location = new Point(647, 337);
            btnPesquisar.Name = "btnPesquisar";
            btnPesquisar.Size = new Size(124, 25);
            btnPesquisar.TabIndex = 9;
            btnPesquisar.Text = "&Pesquisar";
            btnPesquisar.UseVisualStyleBackColor = true;
            btnPesquisar.Click += btnPesquisar_Click;
            // 
            // lblDisciplinas
            // 
            lblDisciplinas.AutoSize = true;
            lblDisciplinas.Location = new Point(578, 47);
            lblDisciplinas.Name = "lblDisciplinas";
            lblDisciplinas.Size = new Size(112, 15);
            lblDisciplinas.TabIndex = 13;
            lblDisciplinas.Text = "Disciplinas do curso";
            // 
            // lstDisciplinas
            // 
            lstDisciplinas.DisplayMember = "Nome";
            lstDisciplinas.FormattingEnabled = true;
            lstDisciplinas.ItemHeight = 15;
            lstDisciplinas.Location = new Point(578, 65);
            lstDisciplinas.Margin = new Padding(3, 2, 3, 2);
            lstDisciplinas.Name = "lstDisciplinas";
            lstDisciplinas.Size = new Size(306, 124);
            lstDisciplinas.TabIndex = 10;
            lstDisciplinas.SelectedIndexChanged += lstDisciplinas_SelectedIndexChanged;
            // 
            // FrmCurso
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(906, 637);
            Controls.Add(lstDisciplinas);
            Controls.Add(lblDisciplinas);
            Controls.Add(btnPesquisar);
            Controls.Add(txtPesquisa);
            Controls.Add(label5);
            Controls.Add(btnEditar);
            Controls.Add(btnSalvar);
            Controls.Add(btnCancelar);
            Controls.Add(btnExcluir);
            Controls.Add(txtStatus);
            Controls.Add(nudCargaHoraria);
            Controls.Add(dgvCurso);
            Controls.Add(label3);
            Controls.Add(txtDescricao);
            Controls.Add(label2);
            Controls.Add(txtNome);
            Controls.Add(label4);
            Controls.Add(label1);
            Name = "FrmCurso";
            Text = "FrmCurso";
            Load += FrmCurso_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCurso).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCargaHoraria).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtNome;
        private Label label2;
        private TextBox txtDescricao;
        private Label label3;
        private DataGridView dgvCurso;
        private NumericUpDown nudCargaHoraria;
        private ComboBox txtStatus;
        private Label label4;
        private Button btnEditar;
        private Button btnSalvar;
        private Button btnCancelar;
        private Button btnExcluir;
        private Label label5;
        private TextBox txtPesquisa;
        private Button btnPesquisar;
        private Label lblDisciplinas;
        private ListBox lstDisciplinas;
    }
}
