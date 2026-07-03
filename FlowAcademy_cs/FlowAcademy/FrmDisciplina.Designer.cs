namespace FlowAcademyF
{
    partial class FrmDisciplina
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
            cmbCurso = new ComboBox();
            nudCargaHoraria = new NumericUpDown();
            dgvDisciplina = new DataGridView();
            label3 = new Label();
            txtNome = new TextBox();
            label4 = new Label();
            label1 = new Label();
            btnEditar = new Button();
            btnSalvar = new Button();
            btnCancelar = new Button();
            btnExcluir = new Button();
            label2 = new Label();
            txtPesquisa = new TextBox();
            btnPesquisar = new Button();
            ((System.ComponentModel.ISupportInitialize)nudCargaHoraria).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDisciplina).BeginInit();
            SuspendLayout();
            // 
            // cmbCurso
            // 
            cmbCurso.FormattingEnabled = true;
            cmbCurso.Location = new Point(99, 50);
            cmbCurso.Name = "cmbCurso";
            cmbCurso.Size = new Size(121, 23);
            cmbCurso.TabIndex = 0;
            // 
            // nudCargaHoraria
            // 
            nudCargaHoraria.Location = new Point(658, 50);
            nudCargaHoraria.Name = "nudCargaHoraria";
            nudCargaHoraria.Size = new Size(120, 23);
            nudCargaHoraria.TabIndex = 2;
            // 
            // dgvDisciplina
            // 
            dgvDisciplina.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDisciplina.Location = new Point(67, 256);
            dgvDisciplina.Name = "dgvDisciplina";
            dgvDisciplina.Size = new Size(668, 243);
            dgvDisciplina.TabIndex = 9;
            dgvDisciplina.CellDoubleClick += dgvDisciplina_CellDoubleClick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(658, 32);
            label3.Name = "label3";
            label3.Size = new Size(80, 15);
            label3.TabIndex = 12;
            label3.Text = "Carga Horária";
            // 
            // txtNome
            // 
            txtNome.Location = new Point(266, 50);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(306, 23);
            txtNome.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(99, 32);
            label4.Name = "label4";
            label4.Size = new Size(38, 15);
            label4.TabIndex = 10;
            label4.Text = "Curso";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(266, 32);
            label1.Name = "label1";
            label1.Size = new Size(40, 15);
            label1.TabIndex = 11;
            label1.Text = "Nome";
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(288, 136);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(97, 64);
            btnEditar.TabIndex = 4;
            btnEditar.Text = "&Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(143, 136);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(97, 64);
            btnSalvar.TabIndex = 3;
            btnSalvar.Text = "&Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(433, 133);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(93, 67);
            btnCancelar.TabIndex = 5;
            btnCancelar.Text = "&Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnExcluir
            // 
            btnExcluir.Location = new Point(574, 133);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(93, 67);
            btnExcluir.TabIndex = 6;
            btnExcluir.Text = "E&xcluir";
            btnExcluir.UseVisualStyleBackColor = true;
            btnExcluir.Click += btnExcluir_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(67, 225);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 13;
            label2.Text = "Pesquisar";
            // 
            // txtPesquisa
            // 
            txtPesquisa.Location = new Point(130, 221);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(461, 23);
            txtPesquisa.TabIndex = 7;
            // 
            // btnPesquisar
            // 
            btnPesquisar.Location = new Point(611, 220);
            btnPesquisar.Name = "btnPesquisar";
            btnPesquisar.Size = new Size(124, 25);
            btnPesquisar.TabIndex = 8;
            btnPesquisar.Text = "&Pesquisar";
            btnPesquisar.UseVisualStyleBackColor = true;
            btnPesquisar.Click += btnPesquisar_Click;
            // 
            // FrmDisciplina
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 597);
            Controls.Add(btnPesquisar);
            Controls.Add(txtPesquisa);
            Controls.Add(label2);
            Controls.Add(btnEditar);
            Controls.Add(btnSalvar);
            Controls.Add(btnCancelar);
            Controls.Add(btnExcluir);
            Controls.Add(cmbCurso);
            Controls.Add(nudCargaHoraria);
            Controls.Add(dgvDisciplina);
            Controls.Add(label3);
            Controls.Add(txtNome);
            Controls.Add(label4);
            Controls.Add(label1);
            Name = "FrmDisciplina";
            Text = "FrmDisciplina";
            Load += FrmDisciplina_Load;
            ((System.ComponentModel.ISupportInitialize)nudCargaHoraria).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDisciplina).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbCurso;
        private NumericUpDown nudCargaHoraria;
        private DataGridView dgvDisciplina;
        private Label label3;
        private TextBox txtNome;
        private Label label4;
        private Label label1;
        private Button btnEditar;
        private Button btnSalvar;
        private Button btnCancelar;
        private Button btnExcluir;
        private Label label2;
        private TextBox txtPesquisa;
        private Button btnPesquisar;
    }
}
