using System;
using System.Windows.Forms;
using FlowAcademyClasses;

namespace FlowAcademy
{
    public partial class FrmPagamento : Form
    {
        int idSelecionado = 0;

        public FrmPagamento()
        {
            InitializeComponent();
            btnSalvar.Click += (sender, e) => btnSalvar_Click(sender ?? this, e);
            btnEditar.Click += (sender, e) => btnEditar_Click(sender ?? this, e);
            button4.Click += (sender, e) => btnCancelar_Click(sender ?? this, e);
            button5.Click += (sender, e) => btnExcluir_Click(sender ?? this, e);
            dgvPagamento.CellDoubleClick += (sender, e) => btnEditar_Click(sender ?? this, EventArgs.Empty);
        }

        // ==========================
        // LOAD
        // ==========================
        private void FrmPagamento_Load(object sender, EventArgs e)
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
            dgvPagamento.DataSource = null;
            dgvPagamento.DataSource = Pagamento.ObterLista(txtPesquisa.Text.Trim());
            AjustarGrid();
        }

        private void AjustarGrid()
        {
            if (dgvPagamento.Columns["IdAluno"] != null) dgvPagamento.Columns["IdAluno"].Visible = false;
            if (dgvPagamento.Columns["Aluno"] != null) dgvPagamento.Columns["Aluno"].Visible = false;

            if (dgvPagamento.Columns["IdPagamento"] != null) dgvPagamento.Columns["IdPagamento"].HeaderText = "ID";
            if (dgvPagamento.Columns["NomeAluno"] != null) dgvPagamento.Columns["NomeAluno"].HeaderText = "Aluno";
        }

        // ==========================
        // COMBOS
        // ==========================
        private void CarregarCombos()
        {
            cmbStatus.Items.Clear();

            cmbStatus.Items.Add("pendente");
            cmbStatus.Items.Add("pago");
            cmbStatus.Items.Add("atrasado");
            cmbStatus.Items.Add("cancelado");
        }

        // ==========================
        // LIMPAR
        // ==========================
        private void LimparFormulario()
        {
            txtIdPagamento.Clear();
            txtAluno.Clear();
            txtPesquisa.Clear();
            nudValor.Value = 0;
            dtpVencimento.Value = DateTime.Now;

            if (cmbStatus.Items.Count > 0)
                cmbStatus.SelectedIndex = 0;

            dgvPagamento.ClearSelection();

            idSelecionado = 0;
        }

        // ==========================
        // VALIDAR
        // ==========================
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtAluno.Text))
            {
                MessageBox.Show("Informe o ID do aluno.");
                return false;
            }

            if (!int.TryParse(txtAluno.Text, out int idAluno) || idAluno <= 0)
            {
                MessageBox.Show("ID do aluno inválido.");
                return false;
            }

            if (nudValor.Value <= 0)
            {
                MessageBox.Show("Informe um valor válido.");
                return false;
            }

            if (cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione o status.");
                return false;
            }

            return true;
        }

        private void btnPesquisar_Click(object? sender, EventArgs e)
        {
            CarregarGrid();
        }

        // ==========================
        // PREENCHER
        // ==========================
        private void PreencherFormulario(Pagamento pagamento)
        {
            if (pagamento == null) return;

            txtIdPagamento.Text = pagamento.IdPagamento.ToString();
            txtAluno.Text = pagamento.IdAluno.ToString();
            nudValor.Value = pagamento.Valor;
            dtpVencimento.Value = pagamento.Vencimento;
            cmbStatus.Text = pagamento.Status;
        }

        // ==========================
        // SALVAR / ATUALIZAR
        // ==========================
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            Pagamento pagamento = new Pagamento();

            pagamento.IdAluno = Convert.ToInt32(txtAluno.Text);
            pagamento.Valor = nudValor.Value;
            pagamento.Vencimento = dtpVencimento.Value;
            pagamento.Status = cmbStatus.Text;

            // INSERIR
            if (idSelecionado == 0)
            {
                if (pagamento.Inserir())
                {
                    MessageBox.Show("Pagamento cadastrado com sucesso!");
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar pagamento.");
                }
            }
            else
            {
                // ATUALIZAR
                pagamento.IdPagamento = idSelecionado;

                if (pagamento.Atualizar())
                {
                    MessageBox.Show("Pagamento atualizado com sucesso!");
                }
                else
                {
                    MessageBox.Show("Erro ao atualizar pagamento.");
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
            if (dgvPagamento.CurrentRow == null)
            {
                MessageBox.Show("Selecione um registro.");
                return;
            }

            int id = Convert.ToInt32(dgvPagamento.CurrentRow.Cells[0].Value);

            Pagamento pagamento = Pagamento.ObterPorId(id);

            if (pagamento != null && pagamento.IdPagamento > 0)
            {
                idSelecionado = pagamento.IdPagamento;
                PreencherFormulario(pagamento);
            }
        }

        // ==========================
        // EXCLUIR
        // ==========================
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (idSelecionado == 0 && dgvPagamento.CurrentRow != null)
            {
                idSelecionado = Convert.ToInt32(dgvPagamento.CurrentRow.Cells[0].Value);
            }

            if (idSelecionado <= 0)
            {
                MessageBox.Show("Selecione um registro para excluir.");
                return;
            }

            var confirm = MessageBox.Show(
                "Deseja excluir este pagamento?",
                "Confirmação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                Pagamento pagamento = new Pagamento();
                pagamento.IdPagamento = idSelecionado;

                if (pagamento.Excluir())
                {
                    MessageBox.Show("Pagamento excluído com sucesso!");
                    CarregarGrid();
                    LimparFormulario();
                }
                else
                {
                    MessageBox.Show("Erro ao excluir pagamento.");
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
    }
}
