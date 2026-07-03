using System;
using System.Linq;
using System.Windows.Forms;
using FlowAcademyClasses;

namespace FlowAcademyF
{
    public partial class FrmFrequencia : Form
    {
        private int idSelecionado = 0;

        public FrmFrequencia()
        {
            InitializeComponent();
        }

        private void FrmFrequencia_Load(object sender, EventArgs e)
        {
            if ((Sessao.NivelAcesso ?? "").Trim().ToLower() == "aluno")
            {
                MessageBox.Show("Acesso nao permitido.");
                Close();
                return;
            }

            CarregarCombos();
            CarregarGrid();
            LimparFormulario();

            txtPercentual.ReadOnly = true;
        }

        private void CarregarGrid()
        {
            var matriculasPermitidas = MatriculasPermitidas().Select(m => m.IdMatricula).ToList();

            dgvFrequencia.DataSource = null;
            dgvFrequencia.DataSource = Frequencia.ObterLista(txtPesquisa.Text.Trim())
                .Where(f => matriculasPermitidas.Contains(f.IdMatricula))
                .ToList();

            AjustarGrid();
            dgvFrequencia.ClearSelection();
        }

        private void AjustarGrid()
        {
            if (dgvFrequencia.Columns["IdMatricula"] != null) dgvFrequencia.Columns["IdMatricula"].Visible = false;
            if (dgvFrequencia.Columns["IdDisciplina"] != null) dgvFrequencia.Columns["IdDisciplina"].Visible = false;
            if (dgvFrequencia.Columns["Matricula"] != null) dgvFrequencia.Columns["Matricula"].Visible = false;
            if (dgvFrequencia.Columns["Disciplina"] != null) dgvFrequencia.Columns["Disciplina"].Visible = false;

            if (dgvFrequencia.Columns["IdFrequencia"] != null) dgvFrequencia.Columns["IdFrequencia"].HeaderText = "ID";
            if (dgvFrequencia.Columns["CodigoMatricula"] != null) dgvFrequencia.Columns["CodigoMatricula"].HeaderText = "Matricula";
            if (dgvFrequencia.Columns["NomeAluno"] != null) dgvFrequencia.Columns["NomeAluno"].HeaderText = "Aluno";
            if (dgvFrequencia.Columns["NomeDisciplina"] != null) dgvFrequencia.Columns["NomeDisciplina"].HeaderText = "Disciplina";
            if (dgvFrequencia.Columns["CodigoTurma"] != null) dgvFrequencia.Columns["CodigoTurma"].HeaderText = "Turma";
        }

        private void CarregarCombos()
        {
            cmbTurma.DataSource = TurmasPermitidas();
            cmbTurma.DisplayMember = "CodigoTurma";
            cmbTurma.ValueMember = "IdTurma";
            cmbTurma.SelectedIndex = -1;

            CarregarAlunosEDisciplinas();
        }

        private void CarregarAlunosEDisciplinas()
        {
            cmbMatricula.DataSource = null;
            cmbDisciplina.DataSource = null;

            int idTurma = ObterIdTurmaSelecionada();
            if (idTurma <= 0) return;

            Turma turma = Turma.ObterPorId(idTurma);

            var alunos = Matricula.ObterListaPorTurma(idTurma)
                .Where(m => m.IdTurma == idTurma && (m.Status ?? "").ToLower() == "ativa")
                .Select(m => new AlunoMatriculaItem(m.IdMatricula, m.NomeAluno ?? NomeAluno(m.IdAluno)))
                .OrderBy(a => a.Nome)
                .ToList();

            cmbMatricula.DataSource = alunos;
            cmbMatricula.DisplayMember = "Nome";
            cmbMatricula.ValueMember = "IdMatricula";
            cmbMatricula.SelectedIndex = -1;

            var disciplinas = Disciplina.ObterLista()
                .Where(d => d.IdCurso == turma.IdCurso)
                .OrderBy(d => d.Nome)
                .ToList();

            cmbDisciplina.DataSource = disciplinas;
            cmbDisciplina.DisplayMember = "Nome";
            cmbDisciplina.ValueMember = "IdDisciplina";
            cmbDisciplina.SelectedIndex = -1;
        }

        private void cmbTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarAlunosEDisciplinas();
        }

        private void LimparFormulario()
        {
            cmbTurma.SelectedIndex = -1;
            cmbMatricula.DataSource = null;
            cmbDisciplina.DataSource = null;

            txtTotalAulas.Clear();
            txtPresencas.Clear();
            txtPercentual.Clear();
            txtPesquisa.Clear();

            dgvFrequencia.ClearSelection();
            idSelecionado = 0;
        }

        private bool ValidarCampos()
        {
            if (cmbTurma.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione uma turma.");
                return false;
            }

            if (cmbMatricula.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione um aluno.");
                return false;
            }

            if (cmbDisciplina.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione uma disciplina.");
                return false;
            }

            if (!int.TryParse(txtTotalAulas.Text, out int total) || total <= 0)
            {
                MessageBox.Show("Total de aulas inválido.");
                return false;
            }

            if (!int.TryParse(txtPresencas.Text, out int presencas) || presencas < 0)
            {
                MessageBox.Show("Presenças inválidas.");
                return false;
            }

            if (presencas > total)
            {
                MessageBox.Show("Presenças não podem ser maiores que o total.");
                return false;
            }

            return true;
        }

        private void CalcularPercentualTela()
        {
            int.TryParse(txtTotalAulas.Text, out int total);
            int.TryParse(txtPresencas.Text, out int presencas);

            decimal percentual = total > 0 ? (presencas * 100m) / total : 0;
            txtPercentual.Text = percentual.ToString("0.00");
        }

        private void PreencherFormulario(Frequencia f)
        {
            if (f == null) return;

            idSelecionado = f.IdFrequencia;

            Matricula matricula = Matricula.ObterPorId(f.IdMatricula);
            cmbTurma.SelectedValue = matricula.IdTurma;
            CarregarAlunosEDisciplinas();
            cmbMatricula.SelectedValue = f.IdMatricula;
            cmbDisciplina.SelectedValue = f.IdDisciplina;

            txtTotalAulas.Text = f.TotalAulas.ToString();
            txtPresencas.Text = f.Presencas.ToString();
            txtPercentual.Text = f.Percentual.ToString("0.00");
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvFrequencia.CurrentRow == null)
            {
                MessageBox.Show("Selecione um registro.");
                return;
            }

            int id = Convert.ToInt32(dgvFrequencia.CurrentRow.Cells[0].Value);
            PreencherFormulario(Frequencia.ObterPorId(id));
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (idSelecionado == 0 && dgvFrequencia.CurrentRow != null)
                idSelecionado = Convert.ToInt32(dgvFrequencia.CurrentRow.Cells[0].Value);

            if (dgvFrequencia.CurrentRow == null)
            {
                MessageBox.Show("Selecione um registro.");
                return;
            }

            if (MessageBox.Show("Deseja excluir?", "Confirmação", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            Frequencia f = new() { IdFrequencia = idSelecionado };

            if (f.Excluir())
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

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            if (idSelecionado > 0)
            {
                AtualizarRegistro();
                return;
            }

            if (!ValidarCampos()) return;

            Frequencia f = MontarFrequenciaDaTela();

            if (f.Inserir())
            {
                MessageBox.Show("Cadastrado com sucesso!");
                CarregarGrid();
                LimparFormulario();
            }
            else
            {
                MessageBox.Show("Erro ao cadastrar.");
            }
        }

        private void AtualizarRegistro()
        {
            if (idSelecionado <= 0) return;
            if (!ValidarCampos()) return;

            Frequencia f = MontarFrequenciaDaTela();
            f.IdFrequencia = idSelecionado;

            if (f.Atualizar())
            {
                MessageBox.Show("Atualizado com sucesso!");
                CarregarGrid();
                LimparFormulario();
            }
            else
            {
                MessageBox.Show("Erro ao atualizar.");
            }
        }

        private Frequencia MontarFrequenciaDaTela()
        {
            Frequencia f = new()
            {
                IdMatricula = Convert.ToInt32(cmbMatricula.SelectedValue),
                IdDisciplina = Convert.ToInt32(cmbDisciplina.SelectedValue)
            };

            if (int.TryParse(txtTotalAulas.Text, out int valor)) f.TotalAulas = valor;
            if (int.TryParse(txtPresencas.Text, out valor)) f.Presencas = valor;

            return f;
        }

        private void txtTotalAulas_TextChanged_1(object sender, EventArgs e) => CalcularPercentualTela();
        private void txtPresencas_TextChanged(object sender, EventArgs e) => CalcularPercentualTela();
        private void btnCalcular_Click(object sender, EventArgs e) => CalcularPercentualTela();

        private void dgvFrequencia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFrequencia.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvFrequencia.CurrentRow.Cells[0].Value);
            PreencherFormulario(Frequencia.ObterPorId(id));
        }

        private void btnCancelar_Click(object sender, EventArgs e) => LimparFormulario();
        private void btnPesquisar_Click(object sender, EventArgs e) => CarregarGrid();

        private int IdProfessorLogado()
        {
            return Professor.ObterLista()
                .FirstOrDefault(p => p.IdUsuario == Sessao.IdUsuario)?.IdProfessor ?? 0;
        }

        private int ObterIdTurmaSelecionada()
        {
            if (cmbTurma.SelectedValue is int id)
                return id;

            if (cmbTurma.SelectedItem is Turma turma)
                return turma.IdTurma;

            if (int.TryParse(Convert.ToString(cmbTurma.SelectedValue), out int idConvertido))
                return idConvertido;

            return 0;
        }

        private bool UsuarioProfessor()
        {
            return (Sessao.NivelAcesso ?? "").Trim().ToLower() == "professor";
        }

        private System.Collections.Generic.List<Turma> TurmasPermitidas()
        {
            if (!UsuarioProfessor())
                return Turma.ObterLista().OrderBy(t => t.CodigoTurma).ToList();

            int idProfessor = IdProfessorLogado();
            return Turma.ObterLista()
                .Where(t => t.IdProfessor == idProfessor)
                .OrderBy(t => t.CodigoTurma)
                .ToList();
        }

        private System.Collections.Generic.List<Matricula> MatriculasPermitidas()
        {
            var turmas = TurmasPermitidas().Select(t => t.IdTurma).ToList();
            return Matricula.ObterLista()
                .Where(m => turmas.Contains(m.IdTurma))
                .ToList();
        }

        private string NomeAluno(int idAluno)
        {
            Aluno aluno = Aluno.ObterPorId(idAluno);
            Usuario usuario = Usuario.ObterPorId(aluno.IdUsuario);
            return string.IsNullOrWhiteSpace(usuario.Nome) ? aluno.Matricula ?? "" : usuario.Nome;
        }

        private class AlunoMatriculaItem
        {
            public int IdMatricula { get; set; }
            public string Nome { get; set; }

            public AlunoMatriculaItem(int idMatricula, string nome)
            {
                IdMatricula = idMatricula;
                Nome = nome;
            }
        }
    }
}
