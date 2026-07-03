using System;
using System.Linq;
using System.Windows.Forms;
using FlowAcademyClasses;

namespace FlowAcademyF
{
    public partial class FrmTurma : Form
    {
        private int idSelecionado = 0;

        public FrmTurma()
        {
            InitializeComponent();
        }

        private void FrmTurma_Load(object sender, EventArgs e)
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
            dgvTurma.DataSource = null;
            dgvTurma.DataSource = Turma.ObterLista(txtPesquisa.Text.Trim());
            AjustarGrid();
        }

        private void AjustarGrid()
        {
            if (dgvTurma.Columns["IdCurso"] != null) dgvTurma.Columns["IdCurso"].Visible = false;
            if (dgvTurma.Columns["IdProfessor"] != null) dgvTurma.Columns["IdProfessor"].Visible = false;
            if (dgvTurma.Columns["Curso"] != null) dgvTurma.Columns["Curso"].Visible = false;
            if (dgvTurma.Columns["Professor"] != null) dgvTurma.Columns["Professor"].Visible = false;

            if (dgvTurma.Columns["IdTurma"] != null) dgvTurma.Columns["IdTurma"].HeaderText = "ID";
            if (dgvTurma.Columns["CodigoTurma"] != null) dgvTurma.Columns["CodigoTurma"].HeaderText = "Codigo";
            if (dgvTurma.Columns["NomeCurso"] != null) dgvTurma.Columns["NomeCurso"].HeaderText = "Curso";
            if (dgvTurma.Columns["NomeProfessor"] != null) dgvTurma.Columns["NomeProfessor"].HeaderText = "Professor";
            if (dgvTurma.Columns["PeriodoLetivo"] != null) dgvTurma.Columns["PeriodoLetivo"].HeaderText = "Periodo Letivo";
            if (dgvTurma.Columns["CapacidadeMaxima"] != null) dgvTurma.Columns["CapacidadeMaxima"].HeaderText = "Capacidade";
        }

        // ==========================
        // CARREGAR COMBOS
        // ==========================
        private void CarregarCombos()
        {
            cmbCurso.DataSource = Curso.ObterLista();
            cmbCurso.DisplayMember = "Nome";
            cmbCurso.ValueMember = "IdCurso";
            cmbCurso.SelectedIndex = -1;

            cmbProfessor.DataSource = Professor.ObterLista()
                .Select(p => new ProfessorComboItem(p.IdProfessor, NomeProfessor(p.IdUsuario)))
                .OrderBy(p => p.Nome)
                .ToList();
            cmbProfessor.DisplayMember = "Nome";
            cmbProfessor.ValueMember = "IdProfessor";
            cmbProfessor.SelectedIndex = -1;

            cmbTurno.Items.Clear();
            cmbTurno.Items.Add("manha");
            cmbTurno.Items.Add("tarde");
            cmbTurno.Items.Add("noite");

            txtStatus.Items.Clear();
            txtStatus.Items.Add("ativa");
            txtStatus.Items.Add("inativa");
        }

        // ==========================
        // LIMPAR FORMULÁRIO
        // ==========================
        private void LimparFormulario()
        {
            cmbCurso.SelectedIndex = -1;
            cmbProfessor.SelectedIndex = -1;
            cmbTurno.SelectedIndex = -1;
            txtStatus.SelectedIndex = -1;

            txtCodTurma.Clear();
            txtPeriodoLetivo.Clear();
            txtPesquisa.Clear();

            nudCapacidade.Minimum = 1;
            nudCapacidade.Value = 1;

            dgvTurma.ClearSelection();

            idSelecionado = 0;
        }

        // ==========================
        // VALIDAR CAMPOS
        // ==========================
        private bool ValidarCampos()
        {
            if (cmbCurso.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione um curso.");
                return false;
            }

            if (cmbProfessor.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione um professor.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCodTurma.Text))
            {
                MessageBox.Show("Informe o código da turma.");
                return false;
            }

            if (cmbTurno.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione o turno.");
                return false;
            }

            if (nudCapacidade.Value <= 0)
            {
                MessageBox.Show("Informe uma capacidade válida.");
                return false;
            }

            if (txtStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione o status.");
                return false;
            }

            return true;
        }

        // ==========================
        // PREENCHER FORMULÁRIO
        // ==========================
        private void PreencherFormulario(Turma turma)
        {
            if (turma == null) return;

            idSelecionado = turma.IdTurma;

            cmbCurso.SelectedValue = turma.IdCurso;
            cmbProfessor.SelectedValue = turma.IdProfessor;

            txtCodTurma.Text = turma.CodigoTurma;

            cmbTurno.Text = turma.Turno;
            txtPeriodoLetivo.Text = turma.PeriodoLetivo;

            nudCapacidade.Value = turma.CapacidadeMaxima;

            txtStatus.Text = turma.Status;
        }

        // ==========================
        // SALVAR (INSERIR / ATUALIZAR)
        // ==========================
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (idSelecionado > 0)
            {
                AtualizarRegistro();
                return;
            }

            if (!ValidarCampos()) return;

            Turma turma = new Turma();

            turma.IdCurso = Convert.ToInt32(cmbCurso.SelectedValue);
            turma.IdProfessor = Convert.ToInt32(cmbProfessor.SelectedValue);
            turma.CodigoTurma = txtCodTurma.Text;
            turma.Turno = cmbTurno.Text;
            turma.PeriodoLetivo = txtPeriodoLetivo.Text;
            turma.CapacidadeMaxima = Convert.ToInt32(nudCapacidade.Value);
            turma.Status = txtStatus.Text;

            if (turma.Inserir())
            {
                MessageBox.Show("Turma cadastrada com sucesso!");
                CarregarGrid();
                LimparFormulario();
            }
            else
            {
                MessageBox.Show("Erro ao cadastrar turma.");
            }
        }

        // ==========================
        // EDITAR
        // ==========================
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvTurma.CurrentRow == null)
            {
                MessageBox.Show("Selecione um registro.");
                return;
            }

            int id = Convert.ToInt32(dgvTurma.CurrentRow.Cells[0].Value);

            Turma turma = Turma.ObterPorId(id);

            PreencherFormulario(turma);
        }

        // ==========================
        // ATUALIZAR
        // ==========================
        private void AtualizarRegistro()
        {
            if (idSelecionado <= 0)
                return;

            if (!ValidarCampos())
                return;

            Turma turma = new Turma();

            turma.IdTurma = idSelecionado;
            turma.IdCurso = Convert.ToInt32(cmbCurso.SelectedValue);
            turma.IdProfessor = Convert.ToInt32(cmbProfessor.SelectedValue);
            turma.CodigoTurma = txtCodTurma.Text;
            turma.Turno = cmbTurno.Text;
            turma.PeriodoLetivo = txtPeriodoLetivo.Text;
            turma.CapacidadeMaxima = Convert.ToInt32(nudCapacidade.Value);
            turma.Status = txtStatus.Text;

            if (turma.Atualizar())
            {
                MessageBox.Show("Turma atualizada com sucesso!");
                CarregarGrid();
                LimparFormulario();
            }
            else
            {
                MessageBox.Show("Erro ao atualizar turma.");
            }
        }

        // ==========================
        // EXCLUIR
        // ==========================
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (idSelecionado == 0 && dgvTurma.CurrentRow != null)
            {
                idSelecionado = Convert.ToInt32(dgvTurma.CurrentRow.Cells[0].Value);
            }

            if (dgvTurma.CurrentRow == null)
            {
                MessageBox.Show("Selecione um registro.");
                return;
            }

            int id = Convert.ToInt32(dgvTurma.CurrentRow.Cells[0].Value);

            var confirmacao = MessageBox.Show(
                "Deseja realmente excluir esta turma?",
                "Confirmação",
                MessageBoxButtons.YesNo
            );

            if (confirmacao != DialogResult.Yes)
                return;

            Turma turma = new Turma();
            turma.IdTurma = id;

            if (turma.Excluir())
            {
                MessageBox.Show("Turma excluída com sucesso!");
                CarregarGrid();
                LimparFormulario();
            }
            else
            {
                MessageBox.Show("Erro ao excluir turma.");
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
        // DUPLO CLIQUE NO GRID
        // ==========================
        private void dgvTurma_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar_Click(sender, e);
        }

        private string NomeProfessor(int idUsuario)
        {
            Usuario usuario = Usuario.ObterPorId(idUsuario);
            return string.IsNullOrWhiteSpace(usuario.Nome) ? "Professor " + idUsuario : usuario.Nome;
        }

        private class ProfessorComboItem
        {
            public int IdProfessor { get; set; }
            public string Nome { get; set; }

            public ProfessorComboItem(int idProfessor, string nome)
            {
                IdProfessor = idProfessor;
                Nome = nome;
            }
        }

    }
}
