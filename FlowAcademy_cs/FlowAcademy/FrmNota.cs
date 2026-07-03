using System;
using System.Linq;
using System.Windows.Forms;
using FlowAcademyClasses;

namespace FlowAcademyF
{
    public partial class FrmNota : Form
    {
        private int idSelecionado = 0;

        public FrmNota()
        {
            InitializeComponent();
        }

        private void FrmNota_Load(object sender, EventArgs e)
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

            txtMedia.ReadOnly = true;
            txtStatus.ReadOnly = true;
        }

        private void CarregarGrid()
        {
            var matriculasPermitidas = MatriculasPermitidas().Select(m => m.IdMatricula).ToList();

            dgvNota.DataSource = null;
            dgvNota.DataSource = Nota.ObterLista(txtPesquisa.Text.Trim())
                .Where(n => matriculasPermitidas.Contains(n.IdMatricula))
                .ToList();
            AjustarGrid();
        }

        private void AjustarGrid()
        {
            if (dgvNota.Columns["IdMatricula"] != null) dgvNota.Columns["IdMatricula"].Visible = false;
            if (dgvNota.Columns["IdDisciplina"] != null) dgvNota.Columns["IdDisciplina"].Visible = false;
            if (dgvNota.Columns["Matricula"] != null) dgvNota.Columns["Matricula"].Visible = false;
            if (dgvNota.Columns["Disciplina"] != null) dgvNota.Columns["Disciplina"].Visible = false;

            if (dgvNota.Columns["IdNota"] != null) dgvNota.Columns["IdNota"].HeaderText = "ID";
            if (dgvNota.Columns["CodigoMatricula"] != null) dgvNota.Columns["CodigoMatricula"].HeaderText = "Matricula";
            if (dgvNota.Columns["NomeAluno"] != null) dgvNota.Columns["NomeAluno"].HeaderText = "Aluno";
            if (dgvNota.Columns["NomeDisciplina"] != null) dgvNota.Columns["NomeDisciplina"].HeaderText = "Disciplina";
            if (dgvNota.Columns["CodigoTurma"] != null) dgvNota.Columns["CodigoTurma"].HeaderText = "Turma";
            if (dgvNota.Columns["MediaUc"] != null) dgvNota.Columns["MediaUc"].HeaderText = "Media";
            if (dgvNota.Columns["DataLancamento"] != null) dgvNota.Columns["DataLancamento"].HeaderText = "Data";
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

            txtNota1.Clear();
            txtNota2.Clear();
            txtTrabalho.Clear();
            txtComportamento.Clear();
            txtMedia.Clear();
            txtStatus.Clear();
            txtPesquisa.Clear();

            dtpData.Value = DateTime.Now;
            dgvNota.ClearSelection();
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

            if (!NotaValida(txtNota1.Text)) return false;
            if (!NotaValida(txtNota2.Text)) return false;
            if (!NotaValida(txtTrabalho.Text)) return false;
            if (!NotaValida(txtComportamento.Text)) return false;

            return true;
        }

        private bool NotaValida(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return true;

            if (!decimal.TryParse(texto, out decimal valor))
            {
                MessageBox.Show("Informe notas válidas.");
                return false;
            }

            if (valor < 0 || valor > 10)
            {
                MessageBox.Show("As notas devem estar entre 0 e 10.");
                return false;
            }

            return true;
        }

        private void CalcularMediaTela()
        {
            decimal.TryParse(txtNota1.Text, out decimal p1);
            decimal.TryParse(txtNota2.Text, out decimal p2);
            decimal.TryParse(txtTrabalho.Text, out decimal t);
            decimal.TryParse(txtComportamento.Text, out decimal c);

            decimal media = (p1 * 0.3m) + (p2 * 0.3m) + (t * 0.3m) + (c * 0.1m);
            txtMedia.Text = media.ToString("0.00");

            txtStatus.Text =
                !string.IsNullOrWhiteSpace(txtNota1.Text) &&
                !string.IsNullOrWhiteSpace(txtNota2.Text) &&
                !string.IsNullOrWhiteSpace(txtTrabalho.Text) &&
                !string.IsNullOrWhiteSpace(txtComportamento.Text)
                    ? media >= 6 ? "aprovado" : "reprovado"
                    : "em_andamento";
        }

        private void txtNota1_TextChanged(object sender, EventArgs e) => CalcularMediaTela();
        private void txtNota2_TextChanged(object sender, EventArgs e) => CalcularMediaTela();
        private void txtTrabalho_TextChanged(object sender, EventArgs e) => CalcularMediaTela();
        private void txtComportamento_TextChanged(object sender, EventArgs e) => CalcularMediaTela();

        private void PreencherFormulario(Nota nota)
        {
            if (nota == null) return;

            idSelecionado = nota.IdNota;

            Matricula matricula = Matricula.ObterPorId(nota.IdMatricula);
            cmbTurma.SelectedValue = matricula.IdTurma;
            CarregarAlunosEDisciplinas();
            cmbMatricula.SelectedValue = nota.IdMatricula;
            cmbDisciplina.SelectedValue = nota.IdDisciplina;

            txtNota1.Text = nota.Prova1?.ToString();
            txtNota2.Text = nota.Prova2?.ToString();
            txtTrabalho.Text = nota.Trabalho?.ToString();
            txtComportamento.Text = nota.Comportamental?.ToString();
            txtMedia.Text = nota.MediaUc?.ToString();
            txtStatus.Text = nota.Status;
            dtpData.Value = nota.DataLancamento;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (idSelecionado > 0)
            {
                AtualizarRegistro();
                return;
            }

            if (!ValidarCampos()) return;

            Nota nota = MontarNotaDaTela();

            if (nota.Inserir())
            {
                MessageBox.Show("Nota cadastrada com sucesso!");
                CarregarGrid();
                LimparFormulario();
            }
            else
            {
                MessageBox.Show("Erro ao cadastrar nota.");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvNota.CurrentRow == null)
            {
                MessageBox.Show("Selecione um registro.");
                return;
            }

            int id = Convert.ToInt32(dgvNota.CurrentRow.Cells[0].Value);
            PreencherFormulario(Nota.ObterPorId(id));
        }

        private void AtualizarRegistro()
        {
            if (idSelecionado <= 0) return;
            if (!ValidarCampos()) return;

            Nota nota = MontarNotaDaTela();
            nota.IdNota = idSelecionado;

            if (nota.Atualizar())
            {
                MessageBox.Show("Nota atualizada com sucesso!");
                CarregarGrid();
                LimparFormulario();
            }
            else
            {
                MessageBox.Show("Erro ao atualizar nota.");
            }
        }

        private Nota MontarNotaDaTela()
        {
            Nota nota = new()
            {
                IdMatricula = Convert.ToInt32(cmbMatricula.SelectedValue),
                IdDisciplina = Convert.ToInt32(cmbDisciplina.SelectedValue),
                DataLancamento = dtpData.Value
            };

            if (decimal.TryParse(txtNota1.Text, out decimal valor)) nota.Prova1 = valor;
            if (decimal.TryParse(txtNota2.Text, out valor)) nota.Prova2 = valor;
            if (decimal.TryParse(txtTrabalho.Text, out valor)) nota.Trabalho = valor;
            if (decimal.TryParse(txtComportamento.Text, out valor)) nota.Comportamental = valor;

            return nota;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (idSelecionado == 0 && dgvNota.CurrentRow != null)
                idSelecionado = Convert.ToInt32(dgvNota.CurrentRow.Cells[0].Value);

            if (dgvNota.CurrentRow == null)
            {
                MessageBox.Show("Selecione um registro.");
                return;
            }

            if (MessageBox.Show("Deseja realmente excluir esta nota?", "Confirmação", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            Nota nota = new() { IdNota = idSelecionado };

            if (nota.Excluir())
            {
                MessageBox.Show("Nota excluída com sucesso!");
                CarregarGrid();
                LimparFormulario();
            }
            else
            {
                MessageBox.Show("Erro ao excluir nota.");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e) => LimparFormulario();
        private void dgvNota_CellDoubleClick(object sender, DataGridViewCellEventArgs e) => btnEditar_Click(sender, e);
        private void btnCalcular_Click(object sender, EventArgs e) => CalcularMediaTela();
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
