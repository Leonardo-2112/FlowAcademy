using System;
using System.Windows.Forms;
using FlowAcademyClasses;

namespace FlowAcademyF
{
    public partial class FrmMatricula : Form
    {
        int idSelecionado = 0;

        public FrmMatricula()
        {
            InitializeComponent();
        }

        // ==========================
        // LOAD
        // ==========================
        private void FrmMatricula_Load(object sender, EventArgs e)
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
            dgvMatricula.DataSource = null;
            dgvMatricula.DataSource = Matricula.ObterLista(txtPesquisa.Text.Trim());
            AjustarGrid();
        }

        private void AjustarGrid()
        {
            if (dgvMatricula.Columns["IdAluno"] != null) dgvMatricula.Columns["IdAluno"].Visible = false;
            if (dgvMatricula.Columns["IdTurma"] != null) dgvMatricula.Columns["IdTurma"].Visible = false;
            if (dgvMatricula.Columns["Aluno"] != null) dgvMatricula.Columns["Aluno"].Visible = false;
            if (dgvMatricula.Columns["Turma"] != null) dgvMatricula.Columns["Turma"].Visible = false;

            if (dgvMatricula.Columns["IdMatricula"] != null) dgvMatricula.Columns["IdMatricula"].HeaderText = "Matricula";
            if (dgvMatricula.Columns["NomeAluno"] != null) dgvMatricula.Columns["NomeAluno"].HeaderText = "Aluno";
            if (dgvMatricula.Columns["CodigoTurma"] != null) dgvMatricula.Columns["CodigoTurma"].HeaderText = "Turma";
            if (dgvMatricula.Columns["DataMatricula"] != null) dgvMatricula.Columns["DataMatricula"].HeaderText = "Data";
        }

        // ==========================
        // COMBOS
        // ==========================
        private void CarregarCombos()
        {
            cmbAluno.DataSource = Aluno.ObterLista();
            cmbAluno.DisplayMember = "NomeAluno";
            cmbAluno.ValueMember = "IdAluno";

            cmbTurma.DataSource = Turma.ObterLista();
            cmbTurma.DisplayMember = "CodigoTurma";
            cmbTurma.ValueMember = "IdTurma";

            // Status
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("ativa");
            cmbStatus.Items.Add("trancada");
            cmbStatus.Items.Add("cancelada");
        }

        // ==========================
        // LIMPAR
        // ==========================
        private void LimparFormulario()
        {
            if (cmbAluno.Items.Count > 0)
                cmbAluno.SelectedIndex = -1;

            if (cmbTurma.Items.Count > 0)
                cmbTurma.SelectedIndex = -1;

            dtpDataMatricula.Value = DateTime.Today;

            cmbStatus.SelectedIndex = -1;
            txtPesquisa.Clear();


            dgvMatricula.ClearSelection();

            idSelecionado = 0;
        }

        // ==========================
        // VALIDAR
        // ==========================
        private bool ValidarCampos()
        {
            if (cmbAluno.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione um aluno.");
                return false;
            }

            if (cmbTurma.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione uma turma.");
                return false;
            }

            if (cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione o status.");
                return false;
            }

            return true;
        }

        // ==========================
        // PREENCHER
        // ==========================
        private void PreencherFormulario(Matricula matricula)
        {
            if (matricula == null) return;

            cmbAluno.SelectedValue = matricula.IdAluno;
            cmbTurma.SelectedValue = matricula.IdTurma;
            dtpDataMatricula.Value = matricula.DataMatricula;
            cmbStatus.Text = matricula.Status;

        }

        // ==========================
        // SALVAR / ATUALIZAR
        // ==========================
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            Matricula matricula = new Matricula();

            matricula.IdAluno = Convert.ToInt32(cmbAluno.SelectedValue);
            matricula.IdTurma = Convert.ToInt32(cmbTurma.SelectedValue);
            matricula.DataMatricula = dtpDataMatricula.Value;
            matricula.Status = cmbStatus.Text;

            // INSERIR
            if (idSelecionado == 0)
            {
                if (matricula.Inserir())
                {
                    MessageBox.Show("Matrícula cadastrada com sucesso!");
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar matrícula.");
                }
            }
            else
            {
                // ATUALIZAR
                matricula.IdMatricula = idSelecionado;

                if (matricula.Atualizar())
                {
                    MessageBox.Show("Matrícula atualizada com sucesso!");
                }
                else
                {
                    MessageBox.Show("Erro ao atualizar matrícula.");
                }
            }

            CarregarGrid();
            LimparFormulario();
        }

        // ==========================
        // EDITAR
        // ==========================
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvMatricula.CurrentRow == null)
            {
                MessageBox.Show("Selecione um registro.");
                return;
            }

            int id = Convert.ToInt32(dgvMatricula.CurrentRow.Cells[0].Value);

            Matricula m = Matricula.ObterPorId(id);

            if (m != null && m.IdMatricula > 0)
            {
                idSelecionado = m.IdMatricula;
                PreencherFormulario(m);
            }
        }

        // ==========================
        // EXCLUIR
        // ==========================
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (idSelecionado == 0 && dgvMatricula.CurrentRow != null)
            {
                idSelecionado = Convert.ToInt32(dgvMatricula.CurrentRow.Cells[0].Value);
            }

            if (idSelecionado <= 0)
            {
                MessageBox.Show("Selecione um registro.");
                return;
            }

            var confirm = MessageBox.Show(
                "Deseja excluir esta matrícula?",
                "Confirmação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                Matricula m = new Matricula();
                m.IdMatricula = idSelecionado;

                if (m.Excluir())
                {
                    MessageBox.Show("Matrícula excluída!");
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
    }
}
