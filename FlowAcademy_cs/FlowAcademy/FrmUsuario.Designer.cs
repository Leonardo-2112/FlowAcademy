namespace FlowAcademyF
{
    partial class FrmUsuario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUsuario));
            label4 = new Label();
            cmbPerfil = new ComboBox();
            label5 = new Label();
            txtStatus = new ComboBox();
            dgvUsers = new DataGridView();
            txtSenha = new TextBox();
            txtEmail = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            txtNome = new TextBox();
            btnEditar = new Button();
            btnSalvar = new Button();
            btnCancelar = new Button();
            btnExcluir = new Button();
            label6 = new Label();
            txtPesquisa = new TextBox();
            btnPesquisar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(420, 108);
            label4.Name = "label4";
            label4.Size = new Size(34, 15);
            label4.TabIndex = 2;
            label4.Text = "Perfil";
            // 
            // cmbPerfil
            // 
            cmbPerfil.FormattingEnabled = true;
            cmbPerfil.Location = new Point(418, 126);
            cmbPerfil.Name = "cmbPerfil";
            cmbPerfil.Size = new Size(121, 23);
            cmbPerfil.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(65, 178);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 2;
            label5.Text = "Status";
            // 
            // txtStatus
            // 
            txtStatus.FormattingEnabled = true;
            txtStatus.Location = new Point(65, 196);
            txtStatus.Name = "txtStatus";
            txtStatus.Size = new Size(121, 23);
            txtStatus.TabIndex = 3;
            // 
            // dgvUsers
            // 
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Location = new Point(109, 393);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.Size = new Size(530, 150);
            dgvUsers.TabIndex = 19;
            // 
            // txtSenha
            // 
            txtSenha.Location = new Point(65, 132);
            txtSenha.Name = "txtSenha";
            txtSenha.Size = new Size(211, 23);
            txtSenha.TabIndex = 22;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(418, 66);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(211, 23);
            txtEmail.TabIndex = 23;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(66, 108);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 21;
            label3.Text = "Senha";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(418, 48);
            label2.Name = "label2";
            label2.Size = new Size(36, 15);
            label2.TabIndex = 20;
            label2.Text = "Email";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(64, 50);
            label1.Name = "label1";
            label1.Size = new Size(40, 15);
            label1.TabIndex = 20;
            label1.Text = "Nome";
            // 
            // txtNome
            // 
            txtNome.Location = new Point(65, 68);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(211, 23);
            txtNome.TabIndex = 23;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(257, 292);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(97, 64);
            btnEditar.TabIndex = 27;
            btnEditar.Text = "&Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(109, 292);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(97, 64);
            btnSalvar.TabIndex = 26;
            btnSalvar.Text = "&Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click_1;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(402, 289);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(93, 67);
            btnCancelar.TabIndex = 25;
            btnCancelar.Text = "&Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnExcluir
            // 
            btnExcluir.Location = new Point(546, 292);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(93, 67);
            btnExcluir.TabIndex = 24;
            btnExcluir.Text = "E&xcluir";
            btnExcluir.UseVisualStyleBackColor = true;
            btnExcluir.Click += btnExcluir_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(109, 369);
            label6.Name = "label6";
            label6.Size = new Size(57, 15);
            label6.TabIndex = 28;
            label6.Text = "Pesquisar";
            // 
            // txtPesquisa
            // 
            txtPesquisa.Location = new Point(172, 365);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(322, 23);
            txtPesquisa.TabIndex = 29;
            // 
            // btnPesquisar
            // 
            btnPesquisar.Location = new Point(515, 364);
            btnPesquisar.Name = "btnPesquisar";
            btnPesquisar.Size = new Size(124, 25);
            btnPesquisar.TabIndex = 30;
            btnPesquisar.Text = "&Pesquisar";
            btnPesquisar.UseVisualStyleBackColor = true;
            btnPesquisar.Click += btnPesquisar_Click;
            // 
            // FrmUsuario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(789, 585);
            Controls.Add(btnEditar);
            Controls.Add(btnSalvar);
            Controls.Add(btnPesquisar);
            Controls.Add(txtPesquisa);
            Controls.Add(label6);
            Controls.Add(btnCancelar);
            Controls.Add(btnExcluir);
            Controls.Add(txtSenha);
            Controls.Add(txtNome);
            Controls.Add(txtEmail);
            Controls.Add(label1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(dgvUsers);
            Controls.Add(txtStatus);
            Controls.Add(label5);
            Controls.Add(cmbPerfil);
            Controls.Add(label4);
            Name = "FrmUsuario";
            Text = "FrmUsuario";
            Load += FrmUsuario_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label4;
        private Label label5;
        private ComboBox txtStatus;
        private DataGridView dgvUsers;
        private TextBox txtSenha;
        private TextBox txtEmail;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox txtNome;
        private ComboBox cmbPerfil;
        private Button btnEditar;
        private Button btnSalvar;
        private Button btnCancelar;
        private Button btnExcluir;
        private Label label6;
        private TextBox txtPesquisa;
        private Button btnPesquisar;
    }
}
