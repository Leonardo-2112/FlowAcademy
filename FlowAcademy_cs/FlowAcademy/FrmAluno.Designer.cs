namespace FlowAcademy
{
    partial class FrmAluno
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
            label2 = new Label();
            dgvAluno = new DataGridView();
            label5 = new Label();
            label6 = new Label();
            txtMatricula = new TextBox();
            label7 = new Label();
            label8 = new Label();
            txtEndereco = new TextBox();
            btnSalvar = new Button();
            btnCancelar = new Button();
            btnExluir = new Button();
            mtbCpf = new MaskedTextBox();
            mtbTelefone = new MaskedTextBox();
            btnEditar = new Button();
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
            lblDataNascimento = new Label();
            dtpDataNascimento = new DateTimePicker();
            lblStatusAcademico = new Label();
            cmbStatusAcademico = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvAluno).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(337, 21);
            label1.Name = "label1";
            label1.Size = new Size(204, 30);
            label1.TabIndex = 21;
            label1.Text = "Cadastro de Alunos";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(93, 77);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 18;
            label2.Text = "Nome";
            // 
            // dgvAluno
            // 
            dgvAluno.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAluno.Location = new Point(113, 393);
            dgvAluno.Name = "dgvAluno";
            dgvAluno.RowHeadersWidth = 51;
            dgvAluno.Size = new Size(668, 243);
            dgvAluno.TabIndex = 16;
            dgvAluno.CellDoubleClick += dgvAluno_CellDoubleClick;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(93, 132);
            label5.Name = "label5";
            label5.Size = new Size(28, 15);
            label5.TabIndex = 23;
            label5.Text = "CPF";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(244, 187);
            label6.Name = "label6";
            label6.Size = new Size(57, 15);
            label6.TabIndex = 25;
            label6.Text = "Matricula";
            // 
            // txtMatricula
            // 
            txtMatricula.Location = new Point(244, 207);
            txtMatricula.Name = "txtMatricula";
            txtMatricula.Size = new Size(130, 23);
            txtMatricula.TabIndex = 8;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(93, 184);
            label7.Name = "label7";
            label7.Size = new Size(52, 15);
            label7.TabIndex = 24;
            label7.Text = "Telefone";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(424, 187);
            label8.Name = "label8";
            label8.Size = new Size(56, 15);
            label8.TabIndex = 26;
            label8.Text = "Endereco";
            // 
            // txtEndereco
            // 
            txtEndereco.Location = new Point(424, 207);
            txtEndereco.Name = "txtEndereco";
            txtEndereco.Size = new Size(130, 23);
            txtEndereco.TabIndex = 9;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(194, 267);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(97, 64);
            btnSalvar.TabIndex = 10;
            btnSalvar.Text = "&Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click_1;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(484, 264);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(93, 67);
            btnCancelar.TabIndex = 12;
            btnCancelar.Text = "&Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click_1;
            // 
            // btnExluir
            // 
            btnExluir.Location = new Point(625, 264);
            btnExluir.Name = "btnExluir";
            btnExluir.Size = new Size(93, 67);
            btnExluir.TabIndex = 13;
            btnExluir.Text = "E&xcluir";
            btnExluir.UseVisualStyleBackColor = true;
            btnExluir.Click += btnExluir_Click;
            // 
            // mtbCpf
            // 
            mtbCpf.Location = new Point(93, 150);
            mtbCpf.Mask = "000.000.000-00";
            mtbCpf.Name = "mtbCpf";
            mtbCpf.Size = new Size(121, 23);
            mtbCpf.TabIndex = 4;
            // 
            // mtbTelefone
            // 
            mtbTelefone.Location = new Point(93, 207);
            mtbTelefone.Mask = "(99) 99999-9999";
            mtbTelefone.Name = "mtbTelefone";
            mtbTelefone.Size = new Size(121, 23);
            mtbTelefone.TabIndex = 7;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(337, 267);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(97, 64);
            btnEditar.TabIndex = 11;
            btnEditar.Text = "&Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(116, 368);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 17;
            label3.Text = "Pesquisar";
            // 
            // txtPesquisa
            // 
            txtPesquisa.Location = new Point(179, 364);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(441, 23);
            txtPesquisa.TabIndex = 14;
            // 
            // btnPesquisar
            // 
            btnPesquisar.Location = new Point(640, 363);
            btnPesquisar.Name = "btnPesquisar";
            btnPesquisar.Size = new Size(144, 25);
            btnPesquisar.TabIndex = 15;
            btnPesquisar.Text = "&Pesquisar";
            btnPesquisar.UseVisualStyleBackColor = true;
            btnPesquisar.Click += btnPesquisar_Click;
            // 
            // txtNomeUsuario
            // 
            txtNomeUsuario.Location = new Point(93, 97);
            txtNomeUsuario.Name = "txtNomeUsuario";
            txtNomeUsuario.Size = new Size(130, 23);
            txtNomeUsuario.TabIndex = 0;
            // 
            // lblEmailUsuario
            // 
            lblEmailUsuario.AutoSize = true;
            lblEmailUsuario.Location = new Point(240, 77);
            lblEmailUsuario.Name = "lblEmailUsuario";
            lblEmailUsuario.Size = new Size(41, 15);
            lblEmailUsuario.TabIndex = 19;
            lblEmailUsuario.Text = "E-mail";
            // 
            // txtEmailUsuario
            // 
            txtEmailUsuario.Location = new Point(240, 97);
            txtEmailUsuario.Name = "txtEmailUsuario";
            txtEmailUsuario.Size = new Size(180, 23);
            txtEmailUsuario.TabIndex = 1;
            // 
            // lblSenhaUsuario
            // 
            lblSenhaUsuario.AutoSize = true;
            lblSenhaUsuario.Location = new Point(440, 77);
            lblSenhaUsuario.Name = "lblSenhaUsuario";
            lblSenhaUsuario.Size = new Size(39, 15);
            lblSenhaUsuario.TabIndex = 20;
            lblSenhaUsuario.Text = "Senha";
            // 
            // txtSenhaUsuario
            // 
            txtSenhaUsuario.Location = new Point(440, 97);
            txtSenhaUsuario.Name = "txtSenhaUsuario";
            txtSenhaUsuario.PasswordChar = '*';
            txtSenhaUsuario.Size = new Size(130, 23);
            txtSenhaUsuario.TabIndex = 2;
            // 
            // lblStatusUsuario
            // 
            lblStatusUsuario.AutoSize = true;
            lblStatusUsuario.Location = new Point(590, 77);
            lblStatusUsuario.Name = "lblStatusUsuario";
            lblStatusUsuario.Size = new Size(39, 15);
            lblStatusUsuario.TabIndex = 22;
            lblStatusUsuario.Text = "Status";
            // 
            // cmbStatusUsuario
            // 
            cmbStatusUsuario.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatusUsuario.FormattingEnabled = true;
            cmbStatusUsuario.Location = new Point(590, 97);
            cmbStatusUsuario.Name = "cmbStatusUsuario";
            cmbStatusUsuario.Size = new Size(110, 23);
            cmbStatusUsuario.TabIndex = 3;
            // 
            // lblDataNascimento
            // 
            lblDataNascimento.AutoSize = true;
            lblDataNascimento.Location = new Point(260, 127);
            lblDataNascimento.Name = "lblDataNascimento";
            lblDataNascimento.Size = new Size(112, 15);
            lblDataNascimento.TabIndex = 27;
            lblDataNascimento.Text = "Data de nascimento";
            // 
            // dtpDataNascimento
            // 
            dtpDataNascimento.Format = DateTimePickerFormat.Short;
            dtpDataNascimento.Location = new Point(260, 150);
            dtpDataNascimento.Name = "dtpDataNascimento";
            dtpDataNascimento.Size = new Size(180, 23);
            dtpDataNascimento.TabIndex = 5;
            // 
            // lblStatusAcademico
            // 
            lblStatusAcademico.AutoSize = true;
            lblStatusAcademico.Location = new Point(520, 130);
            lblStatusAcademico.Name = "lblStatusAcademico";
            lblStatusAcademico.Size = new Size(100, 15);
            lblStatusAcademico.TabIndex = 28;
            lblStatusAcademico.Text = "Status academico";
            // 
            // cmbStatusAcademico
            // 
            cmbStatusAcademico.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatusAcademico.FormattingEnabled = true;
            cmbStatusAcademico.Location = new Point(520, 150);
            cmbStatusAcademico.Name = "cmbStatusAcademico";
            cmbStatusAcademico.Size = new Size(180, 23);
            cmbStatusAcademico.TabIndex = 6;
            // 
            // FrmAluno
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(912, 635);
            Controls.Add(cmbStatusAcademico);
            Controls.Add(lblStatusAcademico);
            Controls.Add(dtpDataNascimento);
            Controls.Add(lblDataNascimento);
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
            Controls.Add(mtbTelefone);
            Controls.Add(mtbCpf);
            Controls.Add(btnSalvar);
            Controls.Add(btnCancelar);
            Controls.Add(btnExluir);
            Controls.Add(dgvAluno);
            Controls.Add(txtEndereco);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(txtMatricula);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FrmAluno";
            Text = "FrmAluno";
            Load += FrmAluno_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAluno).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private DataGridView dgvAluno;
        private Label label5;
        private Label label6;
        private TextBox txtMatricula;
        private Label label7;
        private Label label8;
        private TextBox txtEndereco;
        private Button btnSalvar;
        private Button btnCancelar;
        private Button btnExluir;
        private MaskedTextBox mtbCpf;
        private MaskedTextBox mtbTelefone;
        private Button btnEditar;
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
        private Label lblDataNascimento;
        private DateTimePicker dtpDataNascimento;
        private Label lblStatusAcademico;
        private ComboBox cmbStatusAcademico;
    }
}
