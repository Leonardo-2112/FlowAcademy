using System;
using System.Windows.Forms;
using FlowAcademyClasses;

namespace FlowAcademy
{
    public partial class FrmAluno : Form
    {
        int idSelecionado = 0;

        public FrmAluno()
        {
            InitializeComponent();
        }

        // ==========================
        // LOAD
        // ==========================
        private void FrmAluno_Load(object sender, EventArgs e)
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
            dgvAluno.DataSource = null;
            dgvAluno.DataSource = Aluno.ObterLista(txtPesquisa.Text.Trim());
            AjustarGrid();
        }

        private void AjustarGrid()
        {
            if (dgvAluno.Columns["IdUsuario"] != null) dgvAluno.Columns["IdUsuario"].Visible = false;
            if (dgvAluno.Columns["Usuario"] != null) dgvAluno.Columns["Usuario"].Visible = false;
            if (dgvAluno.Columns["NomeAluno"] != null) dgvAluno.Columns["NomeAluno"].Visible = false;

            if (dgvAluno.Columns["IdAluno"] != null) dgvAluno.Columns["IdAluno"].HeaderText = "ID";
            if (dgvAluno.Columns["NomeUsuario"] != null) dgvAluno.Columns["NomeUsuario"].HeaderText = "Aluno";
            if (dgvAluno.Columns["StatusAcademico"] != null) dgvAluno.Columns["StatusAcademico"].HeaderText = "Status Academico";
            if (dgvAluno.Columns["DataNascimento"] != null) dgvAluno.Columns["DataNascimento"].HeaderText = "Data Nascimento";
        }

        // ==========================
        // COMBOS
        // ==========================
        private void CarregarCombos()
        {
            cmbStatusUsuario.Items.Clear();
            cmbStatusUsuario.Items.Add("ativo");
            cmbStatusUsuario.Items.Add("inativo");
            cmbStatusUsuario.SelectedIndex = 0;

            cmbStatusAcademico.Items.Clear();
            cmbStatusAcademico.Items.Add("regular");
            cmbStatusAcademico.Items.Add("trancado");
            cmbStatusAcademico.Items.Add("concluido");
            cmbStatusAcademico.Items.Add("cancelado");
            cmbStatusAcademico.SelectedIndex = 0;
        }

        // ==========================
        // LIMPAR
        // ==========================
        private void LimparFormulario()
        {
            txtMatricula.Clear();
            txtNomeUsuario.Clear();
            txtEmailUsuario.Clear();
            txtSenhaUsuario.Clear();
            mtbCpf.Clear();
            mtbTelefone.Clear();
            txtEndereco.Clear();
            txtPesquisa.Clear();
            dtpDataNascimento.Value = DateTime.Today;

            if (cmbStatusUsuario.Items.Count > 0)
                cmbStatusUsuario.SelectedIndex = 0;

            if (cmbStatusAcademico.Items.Count > 0)
                cmbStatusAcademico.SelectedIndex = 0;

            dgvAluno.ClearSelection();

            idSelecionado = 0;
        }

        // ==========================
        // VALIDAR
        // ==========================
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNomeUsuario.Text))
            {
                MessageBox.Show("Informe o nome do aluno.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmailUsuario.Text))
            {
                MessageBox.Show("Informe o e-mail do aluno.");
                return false;
            }

            if (idSelecionado == 0 && string.IsNullOrWhiteSpace(txtSenhaUsuario.Text))
            {
                MessageBox.Show("Informe a senha do aluno.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMatricula.Text))
            {
                MessageBox.Show("Informe a matrícula.");
                return false;
            }

            if (!mtbCpf.MaskCompleted)
            {
                MessageBox.Show("Informe um CPF válido.");
                return false;
            }

            // Telefone opcional, mas se preenchido deve ser válido
            if (!string.IsNullOrWhiteSpace(mtbTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Trim())
                && !mtbTelefone.MaskCompleted)
            {
                MessageBox.Show("Informe um telefone válido.");
                return false;
            }

            return true;
        }

        // ==========================
        // PREENCHER
        // ==========================
        private void PreencherFormulario(Aluno aluno)
        {
            if (aluno == null) return;

            Usuario usuario = Usuario.ObterPorId(aluno.IdUsuario);
            txtNomeUsuario.Text = usuario.Nome;
            txtEmailUsuario.Text = usuario.Email;
            txtSenhaUsuario.Clear();
            cmbStatusUsuario.Text = string.IsNullOrEmpty(usuario.Status) ? "ativo" : usuario.Status;

            txtMatricula.Text = aluno.Matricula;
            mtbCpf.Text = aluno.Cpf;
            mtbTelefone.Text = aluno.Telefone;
            txtEndereco.Text = aluno.Endereco;
            dtpDataNascimento.Value = aluno.DataNascimento ?? DateTime.Today;
            cmbStatusAcademico.Text = string.IsNullOrEmpty(aluno.StatusAcademico) ? "regular" : aluno.StatusAcademico;
        }

        // ==========================
        // EDITAR
        // ==========================
        private void dgvAluno_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = Convert.ToInt32(dgvAluno.Rows[e.RowIndex].Cells[0].Value);

            Aluno aluno = Aluno.ObterPorId(id);

            if (aluno != null && aluno.IdAluno > 0)
            {
                idSelecionado = aluno.IdAluno;
                PreencherFormulario(aluno);
            }
        }

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            Usuario usuario = new Usuario();

            if (idSelecionado > 0)
            {
                Aluno alunoAtual = Aluno.ObterPorId(idSelecionado);
                usuario.IdUsuario = alunoAtual.IdUsuario;
            }

            usuario.Nome = txtNomeUsuario.Text.Trim();
            usuario.Email = txtEmailUsuario.Text.Trim();
            usuario.Senha = txtSenhaUsuario.Text.Trim();
            usuario.NivelAcesso = "aluno";
            usuario.Status = cmbStatusUsuario.Text;

            bool usuarioSalvo = idSelecionado == 0 ? usuario.Inserir() : usuario.Atualizar();

            if (!usuarioSalvo)
            {
                MessageBox.Show("Erro ao salvar usuário do aluno.");
                return;
            }

            Aluno aluno = new Aluno();

            aluno.IdUsuario = usuario.IdUsuario;
            aluno.Matricula = txtMatricula.Text;
            aluno.Cpf = mtbCpf.Text;
            aluno.Telefone = mtbTelefone.Text;
            aluno.Endereco = txtEndereco.Text;
            aluno.DataNascimento = dtpDataNascimento.Value.Date;
            aluno.StatusAcademico = cmbStatusAcademico.Text;

            // INSERIR
            if (idSelecionado == 0)
            {
                if (aluno.Inserir())
                {
                    MessageBox.Show("Aluno cadastrado com sucesso!");
                    CarregarGrid();
                    LimparFormulario();
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar aluno.");
                }
            }
            else
            {
                // ATUALIZAR
                aluno.IdAluno = idSelecionado;

                if (aluno.Atualizar())
                {
                    MessageBox.Show("Aluno atualizado com sucesso!");
                    CarregarGrid();
                    LimparFormulario();
                }
                else
                {
                    MessageBox.Show("Erro ao atualizar aluno.");
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvAluno.CurrentRow == null)
            {
                MessageBox.Show("Selecione um registro para editar.");
                return;
            }

            int id = Convert.ToInt32(dgvAluno.CurrentRow.Cells[0].Value);
            Aluno aluno = Aluno.ObterPorId(id);

            if (aluno.IdAluno > 0)
            {
                idSelecionado = aluno.IdAluno;
                PreencherFormulario(aluno);
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            LimparFormulario();
        }

        private void btnExluir_Click(object sender, EventArgs e)
        {
            if (idSelecionado == 0 && dgvAluno.CurrentRow != null)
            {
                idSelecionado = Convert.ToInt32(dgvAluno.CurrentRow.Cells[0].Value);
            }

            if (idSelecionado <= 0)
            {
                MessageBox.Show("Selecione um registro para excluir.");
                return;
            }

            var confirm = MessageBox.Show(
                "Deseja realmente excluir este aluno?",
                "Confirmação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                Aluno aluno = new Aluno();
                aluno.IdAluno = idSelecionado;

                if (aluno.Excluir())
                {
                    MessageBox.Show("Aluno excluído com sucesso!");
                    CarregarGrid();
                    LimparFormulario();
                }
                else
                {
                    MessageBox.Show("Erro ao excluir aluno.");
                }
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            CarregarGrid();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
