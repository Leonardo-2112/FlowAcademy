using System;
using System.Drawing;
using System.Windows.Forms;
using FlowAcademyClasses;

namespace FlowAcademy
{
    public class FrmPrimeiroAcesso : Form
    {
        private readonly Usuario usuario;
        private TextBox txtNovaSenha = new();
        private TextBox txtConfirmarSenha = new();

        public FrmPrimeiroAcesso(Usuario usuarioLogado)
        {
            usuario = usuarioLogado;
            MontarTela();
        }

        private void MontarTela()
        {
            Text = "Primeiro acesso";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(360, 210);
            Padding = new Padding(18);

            Label lblTitulo = new()
            {
                Text = "Altere sua senha para continuar",
                Location = new Point(18, 18),
                AutoSize = true
            };

            Label lblNova = new()
            {
                Text = "Nova senha",
                Location = new Point(18, 62),
                AutoSize = true
            };

            txtNovaSenha.Location = new Point(18, 82);
            txtNovaSenha.Width = 310;
            txtNovaSenha.PasswordChar = '*';
            txtNovaSenha.TabIndex = 0;

            Label lblConfirmar = new()
            {
                Text = "Confirmar senha",
                Location = new Point(18, 112),
                AutoSize = true
            };

            txtConfirmarSenha.Location = new Point(18, 132);
            txtConfirmarSenha.Width = 310;
            txtConfirmarSenha.PasswordChar = '*';
            txtConfirmarSenha.TabIndex = 1;

            Button btnSalvar = new()
            {
                Text = "Salvar",
                Location = new Point(172, 170),
                Width = 75,
                TabIndex = 2
            };
            btnSalvar.Click += (sender, e) => SalvarSenha();

            Button btnCancelar = new()
            {
                Text = "Cancelar",
                Location = new Point(253, 170),
                Width = 75,
                TabIndex = 3
            };
            btnCancelar.Click += (sender, e) => DialogResult = DialogResult.Cancel;

            AcceptButton = btnSalvar;
            CancelButton = btnCancelar;

            Controls.Add(lblTitulo);
            Controls.Add(lblNova);
            Controls.Add(txtNovaSenha);
            Controls.Add(lblConfirmar);
            Controls.Add(txtConfirmarSenha);
            Controls.Add(btnSalvar);
            Controls.Add(btnCancelar);
        }

        private void SalvarSenha()
        {
            if (string.IsNullOrWhiteSpace(txtNovaSenha.Text))
            {
                MessageBox.Show("Informe a nova senha.");
                return;
            }

            if (txtNovaSenha.Text.Length < 4)
            {
                MessageBox.Show("A senha deve ter pelo menos 4 caracteres.");
                return;
            }

            if (txtNovaSenha.Text != txtConfirmarSenha.Text)
            {
                MessageBox.Show("As senhas não conferem.");
                return;
            }

            usuario.Senha = txtNovaSenha.Text;

            if (!usuario.Atualizar())
            {
                MessageBox.Show("Não foi possível alterar a senha.");
                return;
            }

            MessageBox.Show("Senha alterada com sucesso.");
            DialogResult = DialogResult.OK;
        }
    }
}
