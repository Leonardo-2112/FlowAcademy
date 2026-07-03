using FlowAcademyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FlowAcademyF
{
    public partial class FrmFuncionarios : Form
    {
        private int idSelecionado;

        public FrmFuncionarios()
        {
            InitializeComponent();
        }

        private void FrmFuncionarios_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            CarregarPerfis();
            CarregarStatus();
            CarregarGrid();
            LimparFormulario();
        }

        private void CarregarPerfis()
        {
            cmbPerfilFuncionario.Items.Clear();
            cmbPerfilFuncionario.Items.Add("administrativo");
            cmbPerfilFuncionario.Items.Add("coordenacao");
            cmbPerfilFuncionario.SelectedIndex = 0;
        }

        private void CarregarStatus()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("ativo");
            cmbStatus.Items.Add("inativo");
            cmbStatus.SelectedIndex = 0;
        }

        private void CarregarGrid()
        {
            string busca = txtPesquisa.Text.Trim();
            List<Usuario> lista = Usuario.ObterLista(busca)
                .Where(UsuarioPertenceAoFormulario)
                .ToList();

            dgvUsuarios.DataSource = lista;

            if (dgvUsuarios.Columns.Contains("Senha"))
            {
                dgvUsuarios.Columns["Senha"].Visible = false;
            }

            if (dgvUsuarios.Columns.Contains("NivelAcesso"))
            {
                dgvUsuarios.Columns["NivelAcesso"].HeaderText = "Tipo / Perfil";
            }
        }

        private bool UsuarioPertenceAoFormulario(Usuario usuario)
        {
            string perfilUsuario = (usuario.NivelAcesso ?? "").Trim().ToLower();
            return perfilUsuario == "coordenacao" || perfilUsuario == "administrativo";
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Informe o nome.");
                txtNome.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Informe o e-mail.");
                txtEmail.Focus();
                return false;
            }

            if (idSelecionado == 0 && string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show("Informe a senha para o novo usuario.");
                txtSenha.Focus();
                return false;
            }

            if (cmbPerfilFuncionario.SelectedIndex < 0)
            {
                MessageBox.Show("Selecione o perfil do funcionario.");
                cmbPerfilFuncionario.Focus();
                return false;
            }

            return true;
        }

        private void Salvar()
        {
            if (!ValidarCampos()) return;

            Usuario usuario = new()
            {
                IdUsuario = idSelecionado,
                Nome = txtNome.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Senha = txtSenha.Text.Trim(),
                NivelAcesso = cmbPerfilFuncionario.Text,
                Status = cmbStatus.Text
            };

            bool sucesso = idSelecionado == 0 ? usuario.Inserir() : usuario.Atualizar();

            if (!sucesso)
            {
                MessageBox.Show("Nao foi possivel salvar o usuario.");
                return;
            }

            MessageBox.Show("Usuario salvo com sucesso.");
            CarregarGrid();
            LimparFormulario();
        }

        private void EditarSelecionado()
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Selecione um usuario na lista.");
                return;
            }

            idSelecionado = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["IdUsuario"].Value);
            Usuario usuario = Usuario.ObterPorId(idSelecionado);

            if (!UsuarioPertenceAoFormulario(usuario))
            {
                MessageBox.Show("Este usuario nao pertence a este formulario.");
                LimparFormulario();
                return;
            }

            txtNome.Text = usuario.Nome;
            txtEmail.Text = usuario.Email;
            txtSenha.Clear();
            cmbPerfilFuncionario.Text = usuario.NivelAcesso;
            cmbStatus.Text = usuario.Status;
        }

        private void LimparFormulario()
        {
            idSelecionado = 0;
            txtNome.Clear();
            txtEmail.Clear();
            txtSenha.Clear();

            if (cmbPerfilFuncionario.Items.Count > 0)
            {
                cmbPerfilFuncionario.SelectedIndex = 0;
            }

            if (cmbStatus.Items.Count > 0)
            {
                cmbStatus.SelectedIndex = 0;
            }

            dgvUsuarios.ClearSelection();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarSelecionado();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparFormulario();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            CarregarGrid();
        }

        private void dgvUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditarSelecionado();
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
