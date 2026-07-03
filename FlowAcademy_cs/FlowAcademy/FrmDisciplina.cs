using System;
using System.Windows.Forms;
using FlowAcademyClasses;

namespace FlowAcademyF
{
    public partial class FrmDisciplina : Form
    {
        int idSelecionado = 0;

        public FrmDisciplina()
        {
            InitializeComponent();
        }

        // ==========================
        // LOAD
        // ==========================
        private void FrmDisciplina_Load(object sender, EventArgs e)
        {
            CarregarCombos();
            CarregarGrid();
            LimparFormulario();
        }

        // ==========================
        // GRID
        // ==========================
        private void CarregarGrid()
        {
            dgvDisciplina.DataSource = null;
            dgvDisciplina.DataSource = Disciplina.ObterLista(txtPesquisa.Text.Trim());
            AjustarGrid();
        }

        private void AjustarGrid()
        {
            if (dgvDisciplina.Columns["IdCurso"] != null) dgvDisciplina.Columns["IdCurso"].Visible = false;

            if (dgvDisciplina.Columns["IdDisciplina"] != null) dgvDisciplina.Columns["IdDisciplina"].HeaderText = "ID";
            if (dgvDisciplina.Columns["NomeCurso"] != null) dgvDisciplina.Columns["NomeCurso"].HeaderText = "Curso";
            if (dgvDisciplina.Columns["CargaHoraria"] != null) dgvDisciplina.Columns["CargaHoraria"].HeaderText = "Carga Horaria";
        }

        // ==========================
        // LIMPAR
        // ==========================
        private void LimparFormulario()
        {
            txtNome.Clear();
            txtPesquisa.Clear();
            nudCargaHoraria.Value = 0;

            if (cmbCurso.Items.Count > 0)
                cmbCurso.SelectedIndex = -1;

            dgvDisciplina.ClearSelection();
            idSelecionado = 0;
        }

        // ==========================
        // COMBO
        // ==========================
        private void CarregarCombos()
        {
            cmbCurso.DataSource = Curso.ObterLista();
            cmbCurso.DisplayMember = "Nome";
            cmbCurso.ValueMember = "IdCurso";
        }

        // ==========================
        // VALIDAÇÃO
        // ==========================
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Informe o nome da disciplina.");
                return false;
            }

            if (cmbCurso.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione um curso.");
                return false;
            }

            if (nudCargaHoraria.Value <= 0)
            {
                MessageBox.Show("Informe uma carga horária válida.");
                return false;
            }

            return true;
        }

        // ==========================
        // PREENCHER
        // ==========================
        private void PreencherFormulario(Disciplina disciplina)
        {
            if (disciplina == null) return;

            txtNome.Text = disciplina.Nome;
            nudCargaHoraria.Value = disciplina.CargaHoraria;
            cmbCurso.SelectedValue = disciplina.IdCurso;
        }

        // ==========================
        // SALVAR / ATUALIZAR
        // ==========================
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            Disciplina disciplina = new Disciplina();

            disciplina.IdCurso = Convert.ToInt32(cmbCurso.SelectedValue);
            disciplina.Nome = txtNome.Text;
            disciplina.CargaHoraria = (int)nudCargaHoraria.Value;

            // INSERIR
            if (idSelecionado == 0)
            {
                if (disciplina.Inserir())
                {
                    MessageBox.Show("Disciplina cadastrada com sucesso!");
                    CarregarGrid();
                    LimparFormulario();
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar disciplina.");
                }
            }
            else
            {
                // ATUALIZAR
                disciplina.IdDisciplina = idSelecionado;

                if (disciplina.Atualizar())
                {
                    MessageBox.Show("Disciplina atualizada com sucesso!");
                    CarregarGrid();
                    LimparFormulario();
                }
                else
                {
                    MessageBox.Show("Erro ao atualizar disciplina.");
                }
            }
        }

        // ==========================
        // EDITAR
        // ==========================
        private void dgvDisciplina_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = Convert.ToInt32(dgvDisciplina.Rows[e.RowIndex].Cells[0].Value);

            Disciplina disciplina = Disciplina.ObterPorId(id);

            if (disciplina != null && disciplina.IdDisciplina > 0)
            {
                idSelecionado = disciplina.IdDisciplina;
                PreencherFormulario(disciplina);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvDisciplina.CurrentRow == null)
            {
                MessageBox.Show("Selecione um registro para editar.");
                return;
            }

            int id = Convert.ToInt32(dgvDisciplina.CurrentRow.Cells[0].Value);
            Disciplina disciplina = Disciplina.ObterPorId(id);

            if (disciplina.IdDisciplina > 0)
            {
                idSelecionado = disciplina.IdDisciplina;
                PreencherFormulario(disciplina);
            }
        }

        // ==========================
        // EXCLUIR
        // ==========================
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (idSelecionado == 0 && dgvDisciplina.CurrentRow != null)
            {
                idSelecionado = Convert.ToInt32(dgvDisciplina.CurrentRow.Cells[0].Value);
            }

            if (idSelecionado <= 0)
            {
                MessageBox.Show("Selecione um registro para excluir.");
                return;
            }

            var confirm = MessageBox.Show(
                "Deseja realmente excluir esta disciplina?",
                "Confirmação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                Disciplina disciplina = new Disciplina();
                disciplina.IdDisciplina = idSelecionado;

                if (disciplina.Excluir())
                {
                    MessageBox.Show("Disciplina excluída com sucesso!");
                    CarregarGrid();
                    LimparFormulario();
                }
                else
                {
                    MessageBox.Show("Erro ao excluir disciplina.");
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
    }
}
