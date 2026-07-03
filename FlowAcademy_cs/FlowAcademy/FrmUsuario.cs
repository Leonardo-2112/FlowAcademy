using FlowAcademyClasses;
using System;
using System.Windows.Forms;

namespace FlowAcademyF
{
    public partial class FrmUsuario : Form
    {
        int idSelecionado = 0;

        public FrmUsuario()
        {
            InitializeComponent();
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            CarregarCombos();
            CarregarGrid();
            LimparFormulario();
        }

        // ==========================
        // MÉTODOS AUXILIARES
        // ==========================
        private void CarregarCombos()
        {
            cmbPerfil.Items.Clear();
            cmbPerfil.Items.Add("aluno");
            cmbPerfil.Items.Add("professor");
            cmbPerfil.Items.Add("coordenacao");
            cmbPerfil.Items.Add("administrativo");
            cmbPerfil.Items.Add("admin");

            txtStatus.Items.Clear();
            txtStatus.Items.Add("ativo");
            txtStatus.Items.Add("inativo");

            if (cmbPerfil.Items.Count > 0) cmbPerfil.SelectedIndex = 0;
            if (txtStatus.Items.Count > 0) txtStatus.SelectedIndex = 0;
        }

        private void CarregarGrid()
        {
            dgvUsers.DataSource = Usuario.ObterLista(txtPesquisa.Text.Trim());
        }

        private void LimparFormulario()
        {
            txtNome.Clear();
            txtEmail.Clear();
            txtSenha.Clear();
            txtPesquisa.Clear();

            if (cmbPerfil.Items.Count > 0) cmbPerfil.SelectedIndex = 0;
            if (txtStatus.Items.Count > 0) txtStatus.SelectedIndex = 0;

            idSelecionado = 0;
            dgvUsers.ClearSelection();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                MessageBox.Show("Informe o nome.");
                return false;
            }

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Informe o e-mail.");
                return false;
            }

            if (idSelecionado == 0 && string.IsNullOrEmpty(txtSenha.Text))
            {
                MessageBox.Show("Informe a senha.");
                return false;
            }

            return true;
        }

        private void PreencherFormulario(Usuario usuario)
        {
            txtNome.Text = usuario.Nome;
            txtEmail.Text = usuario.Email;
            txtSenha.Text = "";

            cmbPerfil.Text = usuario.NivelAcesso;
            txtStatus.Text = usuario.Status;
        }

        // ==========================
        // SALVAR
        // ==========================


        // ==========================
        // EDITAR
        // ==========================
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null)
            {
                MessageBox.Show("Selecione um registro.");
                return;
            }

            idSelecionado = Convert.ToInt32(
                dgvUsers.CurrentRow.Cells[0].Value);

            Usuario usuario = Usuario.ObterPorId(idSelecionado);

            PreencherFormulario(usuario);
        }

        // ==========================
        // EXCLUIR
        // ==========================
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (idSelecionado == 0 && dgvUsers.CurrentRow != null)
            {
                idSelecionado = Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value);
            }

            if (idSelecionado == 0)
            {
                MessageBox.Show("Selecione um registro.");
                return;
            }

            var confirmacao = MessageBox.Show(
                "Deseja realmente excluir?",
                "Confirmação",
                MessageBoxButtons.YesNo);

            if (confirmacao == DialogResult.Yes)
            {
                Usuario usuario = new Usuario();
                usuario.IdUsuario = idSelecionado;

                if (usuario.Excluir())
                {
                    MessageBox.Show("Excluído com sucesso!");
                    CarregarGrid();
                    LimparFormulario();
                }
                else
                {
                    MessageBox.Show("Erro ao excluir.");
                }
            }
        }

        // ==========================
        // CANCELAR
        // ==========================
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparFormulario();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            CarregarGrid();
        }

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            Usuario usuario = new Usuario();

            usuario.Nome = txtNome.Text;
            usuario.Email = txtEmail.Text;

            if (!string.IsNullOrEmpty(txtSenha.Text))
                usuario.Senha = txtSenha.Text;

            usuario.NivelAcesso = cmbPerfil.Text;
            usuario.Status = txtStatus.Text;

            bool sucesso;

            if (idSelecionado == 0)
            {
                sucesso = usuario.Inserir();
            }
            else
            {
                usuario.IdUsuario = idSelecionado;
                sucesso = usuario.Atualizar();
            }

            if (sucesso)
            {
                MessageBox.Show("Operação realizada com sucesso!");
                CarregarGrid();
                LimparFormulario();
            }
            else
            {
                MessageBox.Show("Erro ao salvar.");
            }
        }
    }
}
