using FlowAcademyClasses;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace FlowAcademyF
{
    public partial class FrmFrequenciaAluno : Form
    {
        public FrmFrequenciaAluno()
        {
            InitializeComponent();
        }

        private void FrmFrequenciaAluno_Load(object sender, EventArgs e)
        {
            CarregarFrequencia();
        }

        private void CarregarFrequencia()
        {
            try
            {
                DataTable tabela = CriarTabelaFrequencia();

                Aluno? alunoLogado = Aluno.ObterLista()
                    .FirstOrDefault(a => a.IdUsuario == Sessao.IdUsuario);

                if (alunoLogado == null || alunoLogado.IdAluno == 0)
                {
                    dgvFrequencia.DataSource = tabela;
                    lblResumo.Text = "Nenhum aluno vinculado ao usuario logado.";
                    return;
                }

                var matriculasAluno = Matricula.ObterLista()
                    .Where(m => m.IdAluno == alunoLogado.IdAluno)
                    .Select(m => m.IdMatricula)
                    .ToList();

                var frequencias = Frequencia.ObterLista()
                    .Where(f => matriculasAluno.Contains(f.IdMatricula))
                    .OrderBy(f => f.NomeDisciplina);

                foreach (Frequencia frequencia in frequencias)
                {
                    tabela.Rows.Add(
                        frequencia.NomeDisciplina,
                        frequencia.TotalAulas,
                        frequencia.Presencas,
                        frequencia.Percentual.ToString("0.00") + "%",
                        frequencia.Percentual >= 75 ? "Regular" : "Atenção");
                }

                dgvFrequencia.DataSource = tabela;
                lblResumo.Text = tabela.Rows.Count == 0
                    ? "Nenhum registro de frequencia encontrado para o aluno logado."
                    : "Registros encontrados: " + tabela.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar frequencia: " + ex.Message);
            }
        }

        private DataTable CriarTabelaFrequencia()
        {
            DataTable tabela = new();
            tabela.Columns.Add("Disciplina / UC");
            tabela.Columns.Add("Total de Aulas");
            tabela.Columns.Add("Presenças");
            tabela.Columns.Add("Percentual");
            tabela.Columns.Add("Situação");
            return tabela;
        }
    }
}
