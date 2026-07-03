using FlowAcademyClasses;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace FlowAcademyF
{
    public partial class FrmBoletimAluno : Form
    {
        public FrmBoletimAluno()
        {
            InitializeComponent();
        }

        private void FrmBoletimAluno_Load(object sender, EventArgs e)
        {
            CarregarBoletim();
        }

        private void CarregarBoletim()
        {
            try
            {
                DataTable tabela = CriarTabelaBoletim();

                Aluno? alunoLogado = Aluno.ObterLista()
                    .FirstOrDefault(a => a.IdUsuario == Sessao.IdUsuario);

                if (alunoLogado == null || alunoLogado.IdAluno == 0)
                {
                    dgvBoletim.DataSource = tabela;
                    lblResumo.Text = "Nenhum aluno vinculado ao usuario logado.";
                    return;
                }

                var matriculasAluno = Matricula.ObterLista()
                    .Where(m => m.IdAluno == alunoLogado.IdAluno)
                    .Select(m => m.IdMatricula)
                    .ToList();

                var notas = Nota.ObterLista()
                    .Where(n => matriculasAluno.Contains(n.IdMatricula))
                    .OrderBy(n => n.NomeDisciplina);

                foreach (Nota nota in notas)
                {
                    tabela.Rows.Add(
                        nota.NomeDisciplina,
                        FormatarDecimal(nota.Prova1),
                        FormatarDecimal(nota.Prova2),
                        FormatarDecimal(nota.Trabalho),
                        FormatarDecimal(nota.Comportamental),
                        FormatarDecimal(nota.MediaUc),
                        nota.Status);
                }

                dgvBoletim.DataSource = tabela;
                lblResumo.Text = tabela.Rows.Count == 0
                    ? "Nenhuma nota encontrada para o aluno logado."
                    : "Notas encontradas: " + tabela.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar boletim: " + ex.Message);
            }
        }

        private DataTable CriarTabelaBoletim()
        {
            DataTable tabela = new();
            tabela.Columns.Add("Disciplina / UC");
            tabela.Columns.Add("Prova 1");
            tabela.Columns.Add("Prova 2");
            tabela.Columns.Add("Trabalho");
            tabela.Columns.Add("Comportamental");
            tabela.Columns.Add("Média");
            tabela.Columns.Add("Status");
            return tabela;
        }

        private string FormatarDecimal(decimal? valor)
        {
            return valor.HasValue ? valor.Value.ToString("0.00") : "";
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
