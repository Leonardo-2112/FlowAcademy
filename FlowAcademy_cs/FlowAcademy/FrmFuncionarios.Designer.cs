namespace FlowAcademyF
{
    partial class FrmFuncionarios
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
            lblPerfil = new Label();
            tlpFormulario = new TableLayoutPanel();
            lblNome = new Label();
            txtNome = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblSenha = new Label();
            txtSenha = new TextBox();
            lblStatus = new Label();
            cmbStatus = new ComboBox();
            lblPerfilFuncionario = new Label();
            cmbPerfilFuncionario = new ComboBox();
            flpBotoes = new FlowLayoutPanel();
            btnSalvar = new Button();
            btnEditar = new Button();
            btnLimpar = new Button();
            flpPesquisa = new FlowLayoutPanel();
            lblPesquisar = new Label();
            txtPesquisa = new TextBox();
            btnPesquisar = new Button();
            dgvUsuarios = new DataGridView();
            tlpFormulario.SuspendLayout();
            flpBotoes.SuspendLayout();
            flpPesquisa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Dock = DockStyle.Top;
            lblTitulo.Location = new Point(12, 12);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(776, 34);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Cadastro de Funcionarios";
            lblTitulo.Click += lblTitulo_Click;
            // 
            // lblPerfil
            // 
            lblPerfil.Dock = DockStyle.Top;
            lblPerfil.Location = new Point(12, 46);
            lblPerfil.Name = "lblPerfil";
            lblPerfil.Size = new Size(776, 24);
            lblPerfil.TabIndex = 1;
            lblPerfil.Text = "Perfis exibidos: coordenacao e administrativo";
            // 
            // tlpFormulario
            // 
            tlpFormulario.ColumnCount = 4;
            tlpFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
            tlpFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            tlpFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
            tlpFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            tlpFormulario.Controls.Add(lblNome, 0, 0);
            tlpFormulario.Controls.Add(txtNome, 1, 0);
            tlpFormulario.Controls.Add(lblEmail, 2, 0);
            tlpFormulario.Controls.Add(txtEmail, 3, 0);
            tlpFormulario.Controls.Add(lblSenha, 0, 1);
            tlpFormulario.Controls.Add(txtSenha, 1, 1);
            tlpFormulario.Controls.Add(lblStatus, 2, 1);
            tlpFormulario.Controls.Add(cmbStatus, 3, 1);
            tlpFormulario.Controls.Add(lblPerfilFuncionario, 0, 2);
            tlpFormulario.Controls.Add(cmbPerfilFuncionario, 1, 2);
            tlpFormulario.Dock = DockStyle.Top;
            tlpFormulario.Location = new Point(12, 70);
            tlpFormulario.Name = "tlpFormulario";
            tlpFormulario.RowCount = 4;
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpFormulario.Size = new Size(776, 144);
            tlpFormulario.TabIndex = 2;
            // 
            // lblNome
            // 
            lblNome.Dock = DockStyle.Fill;
            lblNome.Location = new Point(3, 0);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(133, 36);
            lblNome.TabIndex = 0;
            lblNome.Text = "Nome";
            lblNome.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtNome
            // 
            txtNome.Dock = DockStyle.Fill;
            txtNome.Location = new Point(142, 3);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(242, 23);
            txtNome.TabIndex = 1;
            // 
            // lblEmail
            // 
            lblEmail.Dock = DockStyle.Fill;
            lblEmail.Location = new Point(390, 0);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(133, 36);
            lblEmail.TabIndex = 2;
            lblEmail.Text = "E-mail";
            lblEmail.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtEmail
            // 
            txtEmail.Dock = DockStyle.Fill;
            txtEmail.Location = new Point(529, 3);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(244, 23);
            txtEmail.TabIndex = 3;
            // 
            // lblSenha
            // 
            lblSenha.Dock = DockStyle.Fill;
            lblSenha.Location = new Point(3, 36);
            lblSenha.Name = "lblSenha";
            lblSenha.Size = new Size(133, 36);
            lblSenha.TabIndex = 4;
            lblSenha.Text = "Senha";
            lblSenha.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtSenha
            // 
            txtSenha.Dock = DockStyle.Fill;
            txtSenha.Location = new Point(142, 39);
            txtSenha.Name = "txtSenha";
            txtSenha.PasswordChar = '*';
            txtSenha.Size = new Size(242, 23);
            txtSenha.TabIndex = 5;
            // 
            // lblStatus
            // 
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Location = new Point(390, 36);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(133, 36);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Status";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbStatus
            // 
            cmbStatus.Dock = DockStyle.Fill;
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(529, 39);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(244, 23);
            cmbStatus.TabIndex = 7;
            // 
            // lblPerfilFuncionario
            // 
            lblPerfilFuncionario.Dock = DockStyle.Fill;
            lblPerfilFuncionario.Location = new Point(3, 72);
            lblPerfilFuncionario.Name = "lblPerfilFuncionario";
            lblPerfilFuncionario.Size = new Size(133, 36);
            lblPerfilFuncionario.TabIndex = 8;
            lblPerfilFuncionario.Text = "Perfil / Tipo";
            lblPerfilFuncionario.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbPerfilFuncionario
            // 
            cmbPerfilFuncionario.Dock = DockStyle.Fill;
            cmbPerfilFuncionario.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPerfilFuncionario.FormattingEnabled = true;
            cmbPerfilFuncionario.Location = new Point(142, 75);
            cmbPerfilFuncionario.Name = "cmbPerfilFuncionario";
            cmbPerfilFuncionario.Size = new Size(242, 23);
            cmbPerfilFuncionario.TabIndex = 9;
            // 
            // flpBotoes
            // 
            flpBotoes.Controls.Add(btnSalvar);
            flpBotoes.Controls.Add(btnEditar);
            flpBotoes.Controls.Add(btnLimpar);
            flpBotoes.Dock = DockStyle.Top;
            flpBotoes.Location = new Point(12, 214);
            flpBotoes.Name = "flpBotoes";
            flpBotoes.Size = new Size(776, 42);
            flpBotoes.TabIndex = 3;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(3, 3);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(120, 23);
            btnSalvar.TabIndex = 0;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(129, 3);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(140, 23);
            btnEditar.TabIndex = 1;
            btnEditar.Text = "Editar selecionado";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnLimpar
            // 
            btnLimpar.Location = new Point(275, 3);
            btnLimpar.Name = "btnLimpar";
            btnLimpar.Size = new Size(100, 23);
            btnLimpar.TabIndex = 2;
            btnLimpar.Text = "Limpar";
            btnLimpar.UseVisualStyleBackColor = true;
            btnLimpar.Click += btnLimpar_Click;
            // 
            // flpPesquisa
            // 
            flpPesquisa.Controls.Add(lblPesquisar);
            flpPesquisa.Controls.Add(txtPesquisa);
            flpPesquisa.Controls.Add(btnPesquisar);
            flpPesquisa.Dock = DockStyle.Top;
            flpPesquisa.Location = new Point(12, 256);
            flpPesquisa.Name = "flpPesquisa";
            flpPesquisa.Size = new Size(776, 38);
            flpPesquisa.TabIndex = 4;
            // 
            // lblPesquisar
            // 
            lblPesquisar.Location = new Point(3, 0);
            lblPesquisar.Name = "lblPesquisar";
            lblPesquisar.Size = new Size(80, 23);
            lblPesquisar.TabIndex = 0;
            lblPesquisar.Text = "Pesquisar:";
            lblPesquisar.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtPesquisa
            // 
            txtPesquisa.Location = new Point(89, 3);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(280, 23);
            txtPesquisa.TabIndex = 1;
            // 
            // btnPesquisar
            // 
            btnPesquisar.Location = new Point(375, 3);
            btnPesquisar.Name = "btnPesquisar";
            btnPesquisar.Size = new Size(100, 23);
            btnPesquisar.TabIndex = 2;
            btnPesquisar.Text = "Pesquisar";
            btnPesquisar.UseVisualStyleBackColor = true;
            btnPesquisar.Click += btnPesquisar_Click;
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToDeleteRows = false;
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.Dock = DockStyle.Fill;
            dgvUsuarios.Location = new Point(12, 294);
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.Size = new Size(776, 144);
            dgvUsuarios.TabIndex = 5;
            dgvUsuarios.CellDoubleClick += dgvUsuarios_CellDoubleClick;
            // 
            // FrmFuncionarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvUsuarios);
            Controls.Add(flpPesquisa);
            Controls.Add(flpBotoes);
            Controls.Add(tlpFormulario);
            Controls.Add(lblPerfil);
            Controls.Add(lblTitulo);
            Name = "FrmFuncionarios";
            Padding = new Padding(12);
            Text = "Cadastro de Funcionarios";
            Load += FrmFuncionarios_Load;
            tlpFormulario.ResumeLayout(false);
            tlpFormulario.PerformLayout();
            flpBotoes.ResumeLayout(false);
            flpPesquisa.ResumeLayout(false);
            flpPesquisa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitulo;
        private Label lblPerfil;
        private TableLayoutPanel tlpFormulario;
        private Label lblNome;
        private TextBox txtNome;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblSenha;
        private TextBox txtSenha;
        private Label lblPerfilFuncionario;
        private ComboBox cmbPerfilFuncionario;
        private Label lblStatus;
        private ComboBox cmbStatus;
        private FlowLayoutPanel flpBotoes;
        private Button btnSalvar;
        private Button btnEditar;
        private Button btnLimpar;
        private FlowLayoutPanel flpPesquisa;
        private Label lblPesquisar;
        private TextBox txtPesquisa;
        private Button btnPesquisar;
        private DataGridView dgvUsuarios;
    }
}
