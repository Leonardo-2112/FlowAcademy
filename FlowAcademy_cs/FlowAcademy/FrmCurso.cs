using System;
using System.Windows.Forms;
using FlowAcademyClasses;

namespace FlowAcademyF
{
    public partial class FrmCurso : Form
    {
        int idSelecionado = 0;

        public FrmCurso()
        {
            InitializeComponent();
        }

        private void FrmCurso_Load(object sender, EventArgs e)
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
            txtStatus.Items.Clear();
            txtStatus.Items.Add("ativo");
            txtStatus.Items.Add("inativo");

            if (txtStatus.Items.Count > 0)
                txtStatus.SelectedIndex = 0;
        }

        private void CarregarGrid()
        {
            dgvCurso.DataSource = Curso.ObterLista(txtPesquisa.Text.Trim());
        }

        private void LimparFormulario()
        {
            txtNome.Clear();
            txtDescricao.Clear();
            txtPesquisa.Clear();
            nudCargaHoraria.Value = 0;

            if (txtStatus.Items.Count > 0)
                txtStatus.SelectedIndex = 0;

            lstDisciplinas.DataSource = null;
            idSelecionado = 0;
            dgvCurso.ClearSelection();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                MessageBox.Show("Informe o nome do curso.");
                return false;
            }

            if (nudCargaHoraria.Value <= 0)
            {
                MessageBox.Show("Informe uma carga horária válida.");
                return false;
            }

            return true;
        }

        private void PreencherFormulario(Curso curso)
        {
            txtNome.Text = curso.Nome;
            txtDescricao.Text = curso.Descricao;
            nudCargaHoraria.Value = curso.CargaHoraria;
            txtStatus.Text = curso.Status;
            CarregarDisciplinasDoCurso(curso.IdCurso);
        }

        private void CarregarDisciplinasDoCurso(int idCurso)
        {
            lstDisciplinas.DataSource = null;

            if (idCurso > 0)
                lstDisciplinas.DataSource = Disciplina.ObterListaPorCurso(idCurso);
        }

        // ==========================
        // SALVAR (INSERIR / ATUALIZAR)
        // ==========================
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            Curso curso = new Curso();

            curso.Nome = txtNome.Text;
            curso.Descricao = txtDescricao.Text;
            curso.CargaHoraria = (int)nudCargaHoraria.Value;

            curso.Status = txtStatus.Text;

            bool sucesso;

            if (idSelecionado == 0)
            {
                sucesso = curso.Inserir();
            }
            else
            {
                curso.IdCurso = idSelecionado;
                sucesso = curso.Atualizar();
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

        // ==========================
        // EDITAR
        // ==========================
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvCurso.CurrentRow == null)
            {
                MessageBox.Show("Selecione um registro.");
                return;
            }

            idSelecionado = Convert.ToInt32(
                dgvCurso.CurrentRow.Cells[0].Value);

            Curso curso = Curso.ObterPorId(idSelecionado);

            PreencherFormulario(curso);
        }

        // ==========================
        // EXCLUIR
        // ==========================
        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            if (idSelecionado == 0 && dgvCurso.CurrentRow != null)
            {
                idSelecionado = Convert.ToInt32(dgvCurso.CurrentRow.Cells[0].Value);
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
                Curso curso = new Curso();
                curso.IdCurso = idSelecionado;

                if (curso.Excluir())
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

        private void lstDisciplinas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
