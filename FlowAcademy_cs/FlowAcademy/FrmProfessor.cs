using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FlowAcademyClasses;

namespace FlowAcademyF
{
    public partial class FrmProfessor : Form
    {
        private int idSelecionado = 0;
        public FrmProfessor()
        {
            InitializeComponent();
        }

        private void FrmProfessor_Load(object sender, EventArgs e)
        {
            CarregarCombos();
            CarregarGrid();
            LimparFormulario();
        }

        // ==========================
        // CARREGAR GRID
        // ==========================
        private void CarregarGrid()
        {
            dgvProfessores.DataSource = null;
            dgvProfessores.DataSource = Professor.ObterLista(txtPesquisa.Text.Trim());
            AjustarGrid();
        }

        private void AjustarGrid()
        {
            if (dgvProfessores.Columns["IdUsuario"] != null) dgvProfessores.Columns["IdUsuario"].Visible = false;
            if (dgvProfessores.Columns["Usuario"] != null) dgvProfessores.Columns["Usuario"].Visible = false;

            if (dgvProfessores.Columns["IdProfessor"] != null) dgvProfessores.Columns["IdProfessor"].HeaderText = "ID";
            if (dgvProfessores.Columns["NomeUsuario"] != null) dgvProfessores.Columns["NomeUsuario"].HeaderText = "Professor";
        }

        // ==========================
        // CARREGAR COMBOS
        // ==========================
        private void CarregarCombos()
        {
            cmbStatusUsuario.Items.Clear();
            cmbStatusUsuario.Items.Add("ativo");
            cmbStatusUsuario.Items.Add("inativo");
            cmbStatusUsuario.SelectedIndex = 0;
        }

        // ==========================
        // LIMPAR FORMULÁRIO
        // ==========================
        private void LimparFormulario()
        {
            txtNomeUsuario.Clear();
            txtEmailUsuario.Clear();
            txtSenhaUsuario.Clear();
            txtCpf.Clear();
            txtEspecialidade.Clear();
            txtPesquisa.Clear();

            if (cmbStatusUsuario.Items.Count > 0)
                cmbStatusUsuario.SelectedIndex = 0;

            dgvProfessores.ClearSelection();

            idSelecionado = 0;
        }

        // ==========================
        // VALIDAR CAMPOS
        // ==========================
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNomeUsuario.Text))
            {
                MessageBox.Show("Informe o nome do professor.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmailUsuario.Text))
            {
                MessageBox.Show("Informe o e-mail do professor.");
                return false;
            }

            if (idSelecionado == 0 && string.IsNullOrWhiteSpace(txtSenhaUsuario.Text))
            {
                MessageBox.Show("Informe a senha do professor.");
                return false;
            }

            string cpfLimpo = SomenteNumeros(txtCpf.Text);

            if (string.IsNullOrWhiteSpace(cpfLimpo))
            {
                MessageBox.Show("Informe o CPF.");
                return false;
            }

            if (!ValidarCpf(cpfLimpo))
            {
                MessageBox.Show("CPF inválido.");
                return false;
            }

            return true;
        }


        // ==========================
        // PREENCHER FORMULÁRIO
        // ==========================
        private void PreencherFormulario(Professor prof)
        {
            if (prof == null) return;

            idSelecionado = prof.IdProfessor;

            Usuario usuario = Usuario.ObterPorId(prof.IdUsuario);
            txtNomeUsuario.Text = usuario.Nome;
            txtEmailUsuario.Text = usuario.Email;
            txtSenhaUsuario.Clear();
            cmbStatusUsuario.Text = string.IsNullOrEmpty(usuario.Status) ? "ativo" : usuario.Status;

            txtCpf.Text = prof.Cpf;
            txtEspecialidade.Text = prof.Especialidade;
        }

        // ==========================
        // SALVAR
        // ==========================
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            Usuario usuario = new Usuario();

            if (idSelecionado > 0)
            {
                Professor professorAtual = Professor.ObterPorId(idSelecionado);
                usuario.IdUsuario = professorAtual.IdUsuario;
            }

            usuario.Nome = txtNomeUsuario.Text.Trim();
            usuario.Email = txtEmailUsuario.Text.Trim();
            usuario.Senha = txtSenhaUsuario.Text.Trim();
            usuario.NivelAcesso = "professor";
            usuario.Status = cmbStatusUsuario.Text;

            bool usuarioSalvo = idSelecionado == 0 ? usuario.Inserir() : usuario.Atualizar();

            if (!usuarioSalvo)
            {
                MessageBox.Show("Erro ao salvar usuário do professor.");
                return;
            }

            Professor professor = new Professor();

            professor.IdProfessor = idSelecionado;
            professor.IdUsuario = usuario.IdUsuario;
            professor.Cpf = SomenteNumeros(txtCpf.Text);
            professor.Especialidade = txtEspecialidade.Text;

            bool sucesso;

            if (idSelecionado == 0)
            {
                sucesso = professor.Inserir();
            }
            else
            {
                sucesso = professor.Atualizar();
            }

            if (sucesso)
            {
                MessageBox.Show("Operação realizada com sucesso!");
                CarregarGrid();
                LimparFormulario();
            }
            else
            {
                MessageBox.Show("Erro ao salvar professor.");
            }
        }

        // ==========================
        // EDITAR
        // ==========================
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProfessores.CurrentRow == null)
            {
                MessageBox.Show("Selecione um registro.");
                return;
            }

            int id = Convert.ToInt32(dgvProfessores.CurrentRow.Cells[0].Value);

            Professor prof = Professor.ObterPorId(id);

            PreencherFormulario(prof);
        }

        // ==========================
        // EXCLUIR
        // ==========================
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvProfessores.CurrentRow == null)
            {
                MessageBox.Show("Selecione um registro.");
                return;
            }

            int id = Convert.ToInt32(dgvProfessores.CurrentRow.Cells[0].Value);

            var confirm = MessageBox.Show(
                "Deseja realmente excluir este professor?",
                "Confirmação",
                MessageBoxButtons.YesNo
            );

            if (confirm != DialogResult.Yes)
                return;

            Professor professor = new Professor();
            professor.IdProfessor = id;

            if (professor.Excluir())
            {
                MessageBox.Show("Professor excluído com sucesso!");
                CarregarGrid();
                LimparFormulario();
            }
            else
            {
                MessageBox.Show("Erro ao excluir professor.");
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

        // ==========================
        // DUPLO CLIQUE NO GRID (EDITAR)
        // ==========================
        private void dgvProfessores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar_Click(sender, e);
        }

        private string SomenteNumeros(string valor)
        {
            return new string(valor.Where(char.IsDigit).ToArray());
        }

        private bool ValidarCpf(string cpf)
        {
            cpf = SomenteNumeros(cpf);

            if (cpf.Length != 11)
                return false;

            return !cpf.All(c => c == cpf[0]);
        }

    }
}
