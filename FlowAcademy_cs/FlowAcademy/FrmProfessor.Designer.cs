namespace FlowAcademyF
{
    partial class FrmProfessor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProfessor));
            cmbUsuario = new ComboBox();
            dgvProfessores = new DataGridView();
            txtEspecialidade = new TextBox();
            label7 = new Label();
            txtCpf = new TextBox();
            label5 = new Label();
            label2 = new Label();
            btnEditar = new Button();
            btnSalvar = new Button();
            btnCancelar = new Button();
            btnExcluir = new Button();
            label3 = new Label();
            txtPesquisa = new TextBox();
            btnPesquisar = new Button();
            txtNomeUsuario = new TextBox();
            lblEmailUsuario = new Label();
            txtEmailUsuario = new TextBox();
            lblSenhaUsuario = new Label();
            txtSenhaUsuario = new TextBox();
            lblStatusUsuario = new Label();
            cmbStatusUsuario = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvProfessores).BeginInit();
            SuspendLayout();
            // 
            // cmbUsuario
            // 
            cmbUsuario.FormattingEnabled = true;
            cmbUsuario.Location = new Point(75, 49);
            cmbUsuario.Name = "cmbUsuario";
            cmbUsuario.Size = new Size(121, 23);
            cmbUsuario.TabIndex = 31;
            cmbUsuario.Visible = false;
            // 
            // dgvProfessores
            // 
            dgvProfessores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProfessores.Location = new Point(75, 332);
            dgvProfessores.Name = "dgvProfessores";
            dgvProfessores.Size = new Size(668, 243);
            dgvProfessores.TabIndex = 25;
            dgvProfessores.CellDoubleClick += dgvProfessores_CellDoubleClick;
            // 
            // txtEspecialidade
            // 
            txtEspecialidade.Location = new Point(75, 157);
            txtEspecialidade.Name = "txtEspecialidade";
            txtEspecialidade.Size = new Size(130, 23);
            txtEspecialidade.TabIndex = 22;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(75, 134);
            label7.Name = "label7";
            label7.Size = new Size(78, 15);
            label7.TabIndex = 18;
            label7.Text = "Especialidade";
            // 
            // txtCpf
            // 
            txtCpf.Location = new Point(75, 103);
            txtCpf.Name = "txtCpf";
            txtCpf.Size = new Size(130, 23);
            txtCpf.TabIndex = 24;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(75, 80);
            label5.Name = "label5";
            label5.Size = new Size(28, 15);
            label5.TabIndex = 16;
            label5.Text = "CPF";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(75, 26);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 19;
            label2.Text = "Nome";
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(257, 213);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(97, 64);
            btnEditar.TabIndex = 35;
            btnEditar.Text = "&Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(112, 213);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(97, 64);
            btnSalvar.TabIndex = 34;
            btnSalvar.Text = "&Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(402, 210);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(93, 67);
            btnCancelar.TabIndex = 33;
            btnCancelar.Text = "&Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnExcluir
            // 
            btnExcluir.Location = new Point(543, 210);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(93, 67);
            btnExcluir.TabIndex = 32;
            btnExcluir.Text = "E&xcluir";
            btnExcluir.UseVisualStyleBackColor = true;
            btnExcluir.Click += btnExcluir_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(75, 304);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 36;
            label3.Text = "Pesquisar";
            // 
            // txtPesquisa
            // 
            txtPesquisa.Location = new Point(138, 300);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(446, 23);
            txtPesquisa.TabIndex = 37;
            // 
            // btnPesquisar
            // 
            btnPesquisar.Location = new Point(599, 299);
            btnPesquisar.Name = "btnPesquisar";
            btnPesquisar.Size = new Size(144, 25);
            btnPesquisar.TabIndex = 38;
            btnPesquisar.Text = "&Pesquisar";
            btnPesquisar.UseVisualStyleBackColor = true;
            btnPesquisar.Click += btnPesquisar_Click;
            // 
            // txtNomeUsuario
            // 
            txtNomeUsuario.Location = new Point(75, 49);
            txtNomeUsuario.Name = "txtNomeUsuario";
            txtNomeUsuario.Size = new Size(130, 23);
            txtNomeUsuario.TabIndex = 39;
            // 
            // lblEmailUsuario
            // 
            lblEmailUsuario.AutoSize = true;
            lblEmailUsuario.Location = new Point(225, 26);
            lblEmailUsuario.Name = "lblEmailUsuario";
            lblEmailUsuario.Size = new Size(41, 15);
            lblEmailUsuario.TabIndex = 40;
            lblEmailUsuario.Text = "E-mail";
            // 
            // txtEmailUsuario
            // 
            txtEmailUsuario.Location = new Point(225, 49);
            txtEmailUsuario.Name = "txtEmailUsuario";
            txtEmailUsuario.Size = new Size(180, 23);
            txtEmailUsuario.TabIndex = 41;
            // 
            // lblSenhaUsuario
            // 
            lblSenhaUsuario.AutoSize = true;
            lblSenhaUsuario.Location = new Point(430, 26);
            lblSenhaUsuario.Name = "lblSenhaUsuario";
            lblSenhaUsuario.Size = new Size(39, 15);
            lblSenhaUsuario.TabIndex = 42;
            lblSenhaUsuario.Text = "Senha";
            // 
            // txtSenhaUsuario
            // 
            txtSenhaUsuario.Location = new Point(430, 49);
            txtSenhaUsuario.Name = "txtSenhaUsuario";
            txtSenhaUsuario.PasswordChar = '*';
            txtSenhaUsuario.Size = new Size(130, 23);
            txtSenhaUsuario.TabIndex = 43;
            // 
            // lblStatusUsuario
            // 
            lblStatusUsuario.AutoSize = true;
            lblStatusUsuario.Location = new Point(585, 26);
            lblStatusUsuario.Name = "lblStatusUsuario";
            lblStatusUsuario.Size = new Size(39, 15);
            lblStatusUsuario.TabIndex = 44;
            lblStatusUsuario.Text = "Status";
            // 
            // cmbStatusUsuario
            // 
            cmbStatusUsuario.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatusUsuario.FormattingEnabled = true;
            cmbStatusUsuario.Location = new Point(585, 49);
            cmbStatusUsuario.Name = "cmbStatusUsuario";
            cmbStatusUsuario.Size = new Size(110, 23);
            cmbStatusUsuario.TabIndex = 45;
            // 
            // FrmProfessor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 618);
            Controls.Add(cmbStatusUsuario);
            Controls.Add(lblStatusUsuario);
            Controls.Add(txtSenhaUsuario);
            Controls.Add(lblSenhaUsuario);
            Controls.Add(txtEmailUsuario);
            Controls.Add(lblEmailUsuario);
            Controls.Add(txtNomeUsuario);
            Controls.Add(btnPesquisar);
            Controls.Add(txtPesquisa);
            Controls.Add(label3);
            Controls.Add(btnEditar);
            Controls.Add(btnSalvar);
            Controls.Add(btnCancelar);
            Controls.Add(btnExcluir);
            Controls.Add(cmbUsuario);
            Controls.Add(dgvProfessores);
            Controls.Add(txtEspecialidade);
            Controls.Add(label7);
            Controls.Add(txtCpf);
            Controls.Add(label5);
            Controls.Add(label2);
            Name = "FrmProfessor";
            Text = "FrmProfessor";
            Load += FrmProfessor_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProfessores).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbUsuario;
        private DataGridView dgvProfessores;
        private TextBox txtEspecialidade;
        private Label label7;
        private TextBox txtCpf;
        private Label label5;
        private Label label2;
        private Button btnEditar;
        private Button btnSalvar;
        private Button btnCancelar;
        private Button btnExcluir;
        private Label label3;
        private TextBox txtPesquisa;
        private Button btnPesquisar;
        private TextBox txtNomeUsuario;
        private Label lblEmailUsuario;
        private TextBox txtEmailUsuario;
        private Label lblSenhaUsuario;
        private TextBox txtSenhaUsuario;
        private Label lblStatusUsuario;
        private ComboBox cmbStatusUsuario;
    }
}
