using FlowAcademyClasses;
using FlowAcademyF;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FlowAcademy
{
    public partial class FrmPrincipal : Form
    {
        private Panel pnlLateral = null!;
        private Panel pnlTopo = null!;
        private FlowLayoutPanel flpMenu = null!;
        private Label lblTitulo = null!;
        private Label lblSubtitulo = null!;
        private Label lblUsuario = null!;
        private Label lblPerfil = null!;
        private ToolStripStatusLabel statusUsuario = null!;
        private ToolStripStatusLabel statusPerfil = null!;
        private ToolStripStatusLabel statusData = null!;
        private Button btnInicio = null!;
        private Button btnSair = null!;
        private Button? botaoAtivo;
        private readonly List<Button> botoesMenu = new();

        public FrmPrincipal()
        {
            InitializeComponent();
            ConfigurarLayout();
            ConfigurarEventos();
        }

        // ==========================
        // LOAD
        // ==========================
        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            AplicarPermissoes();
            MontarTelaInicial();
        }

        // ==========================
        // LAYOUT
        // ==========================
        private void ConfigurarLayout()
        {
            SuspendLayout();

            Text = "Flow Academy - Dashboard";
            MinimumSize = new Size(1100, 700);

            Controls.Remove(menuStrip1);
            Controls.Remove(statusStrip1);
            Controls.Remove(panel1);

            ConfigurarStatus();

            TableLayoutPanel estrutura = new TableLayoutPanel();
            estrutura.Dock = DockStyle.Fill;
            estrutura.ColumnCount = 2;
            estrutura.RowCount = 2;
            estrutura.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 240F));
            estrutura.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            estrutura.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            estrutura.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            CriarMenuLateral();
            CriarTopo();

            panel1.Dock = DockStyle.Fill;
            panel1.Padding = new Padding(24);
            panel1.AutoScroll = true;

            estrutura.Controls.Add(pnlLateral, 0, 0);
            estrutura.SetRowSpan(pnlLateral, 2);
            estrutura.Controls.Add(pnlTopo, 1, 0);
            estrutura.Controls.Add(panel1, 1, 1);

            Controls.Add(estrutura);
            Controls.Add(statusStrip1);
            statusStrip1.BringToFront();

            ResumeLayout(false);
        }

        private void CriarMenuLateral()
        {
            pnlLateral = new Panel();
            pnlLateral.Dock = DockStyle.Fill;

            Panel pnlMarca = new Panel();
            pnlMarca.Dock = DockStyle.Top;
            pnlMarca.Height = 112;
            pnlMarca.Padding = new Padding(18, 20, 18, 8);

            Label lblMarca = new Label();
            lblMarca.Text = "Flow Academy";
            lblMarca.Dock = DockStyle.Top;
            lblMarca.Height = 34;

            Label lblModulo = new Label();
            lblModulo.Text = "Dashboard Desktop";
            lblModulo.Dock = DockStyle.Top;
            lblModulo.Height = 24;

            pnlMarca.Controls.Add(lblModulo);
            pnlMarca.Controls.Add(lblMarca);

            flpMenu = new FlowLayoutPanel();
            flpMenu.Dock = DockStyle.Fill;
            flpMenu.FlowDirection = FlowDirection.TopDown;
            flpMenu.WrapContents = false;
            flpMenu.AutoScroll = true;
            flpMenu.Padding = new Padding(18, 6, 18, 6);

            btnInicio = CriarBotaoMenu("Inicio");
            flpMenu.Controls.Add(btnInicio);

            AdicionarBotaoExistente(btnBoletimAluno, "Boletim / Notas");
            AdicionarBotaoExistente(btnFrequenciaAluno, "Frequência");
            AdicionarBotaoExistente(btnUsuarios, "Usuarios");
            AdicionarBotaoExistente(btnAlunos, "Alunos");
            AdicionarBotaoExistente(btnProfessores, "Professores");
            AdicionarBotaoExistente(btnFuncionarios, "Funcionarios");
            AdicionarBotaoExistente(btnCursos, "Cursos");
            AdicionarBotaoExistente(btnDisciplinas, "Disciplinas");
            AdicionarBotaoExistente(btnTurmas, "Turmas");
            AdicionarBotaoExistente(btnMatriculas, "Matriculas");
            AdicionarBotaoExistente(btnNotas, "Notas");
            AdicionarBotaoExistente(btnFrequencia, "Frequencia");
            AdicionarBotaoExistente(btnPagamentos, "Pagamentos");
            Panel pnlRodape = new Panel();
            pnlRodape.Dock = DockStyle.Bottom;
            pnlRodape.Height = 118;
            pnlRodape.Padding = new Padding(18, 10, 18, 16);

            lblUsuario = new Label();
            lblUsuario.Dock = DockStyle.Top;
            lblUsuario.Height = 22;

            lblPerfil = new Label();
            lblPerfil.Dock = DockStyle.Top;
            lblPerfil.Height = 22;

            btnSair = new Button();
            btnSair.Text = "Sair";
            btnSair.Dock = DockStyle.Bottom;
            btnSair.Height = 34;

            pnlRodape.Controls.Add(btnSair);
            pnlRodape.Controls.Add(lblPerfil);
            pnlRodape.Controls.Add(lblUsuario);

            pnlLateral.Controls.Add(flpMenu);
            pnlLateral.Controls.Add(pnlRodape);
            pnlLateral.Controls.Add(pnlMarca);
        }

        private void CriarTopo()
        {
            pnlTopo = new Panel();
            pnlTopo.Dock = DockStyle.Fill;
            pnlTopo.Padding = new Padding(24, 12, 24, 8);

            lblTitulo = new Label();
            lblTitulo.Text = "Dashboard";
            lblTitulo.Dock = DockStyle.Top;
            lblTitulo.Height = 32;

            lblSubtitulo = new Label();
            lblSubtitulo.Text = "Selecione uma opcao no menu lateral.";
            lblSubtitulo.Dock = DockStyle.Top;
            lblSubtitulo.Height = 24;

            pnlTopo.Controls.Add(lblSubtitulo);
            pnlTopo.Controls.Add(lblTitulo);
        }

        private Button CriarBotaoMenu(string texto)
        {
            Button botao = new Button();
            ConfigurarBotaoMenu(botao, texto);
            return botao;
        }

        private void AdicionarBotaoExistente(Button botao, string texto)
        {
            Controls.Remove(botao);
            ConfigurarBotaoMenu(botao, texto);
            flpMenu.Controls.Add(botao);
            botoesMenu.Add(botao);
        }

        private void ConfigurarBotaoMenu(Button botao, string texto)
        {
            botao.Text = texto;
            botao.Width = 198;
            botao.Height = 40;
            botao.Margin = new Padding(0, 0, 0, 8);
            botao.Padding = new Padding(14, 0, 0, 0);
            botao.TextAlign = ContentAlignment.MiddleLeft;
        }

        private void ConfigurarStatus()
        {
            statusStrip1.Items.Clear();
            statusStrip1.SizingGrip = false;

            statusUsuario = new ToolStripStatusLabel();
            statusPerfil = new ToolStripStatusLabel();
            statusData = new ToolStripStatusLabel();

            statusUsuario.Text = "Usuario: -";
            statusPerfil.Text = "Perfil: -";
            statusData.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            statusData.Spring = true;
            statusData.TextAlign = ContentAlignment.MiddleRight;

            statusStrip1.Items.Add(statusUsuario);
            statusStrip1.Items.Add(new ToolStripStatusLabel(" | "));
            statusStrip1.Items.Add(statusPerfil);
            statusStrip1.Items.Add(statusData);
        }

        // ==========================
        // PERMISSOES
        // ==========================
        private void AplicarPermissoes()
        {
            OcultarTodosMenus();

            string perfil = NormalizarPerfil(Sessao.NivelAcesso);

            Text = "Flow Academy - " + NomeUsuario() + " (" + NomePerfil(perfil) + ")";
            lblUsuario.Text = NomeUsuario();
            lblPerfil.Text = NomePerfil(perfil);
            statusUsuario.Text = "Usuario: " + NomeUsuario();
            statusPerfil.Text = "Perfil: " + NomePerfil(perfil);

            if (perfil == "aluno")
            {
                MostrarMenus(btnBoletimAluno, btnFrequenciaAluno);
                return;
            }

            if (perfil == "professor")
            {
                MostrarMenus(btnNotas, btnFrequencia);
                return;
            }

            if (perfil == "coordenacao")
            {
                MostrarMenus(btnAlunos, btnCursos, btnDisciplinas, btnTurmas, btnMatriculas);
                return;
            }

            if (perfil == "administrativo")
            {
                MostrarMenus(btnAlunos, btnMatriculas, btnPagamentos);
                return;
            }

            if (perfil == "admin")
            {
                MostrarMenus(btnAlunos, btnProfessores, btnFuncionarios, btnCursos,
                    btnDisciplinas, btnTurmas, btnMatriculas, btnNotas,
                    btnFrequencia, btnPagamentos);
            }
        }

        private string NormalizarPerfil(string? perfil)
        {
            string perfilTratado = (perfil ?? "").Trim().ToLower();

            // Mesmo comportamento do PHP: financeiro antigo entra como administrativo.
            if (perfilTratado == "financeiro")
            {
                return "administrativo";
            }

            return perfilTratado;
        }

        private string NomePerfil(string perfil)
        {
            if (perfil == "aluno") return "Aluno";
            if (perfil == "professor") return "Professor";
            if (perfil == "coordenacao") return "Coordenacao";
            if (perfil == "administrativo") return "Administrativo";
            if (perfil == "admin") return "Admin";

            return "Sem perfil";
        }

        private string NomeUsuario()
        {
            if (string.IsNullOrWhiteSpace(Sessao.Nome))
            {
                return "Usuario";
            }

            return Sessao.Nome;
        }

        private string TextoBoasVindas(string perfil)
        {
            if (perfil == "aluno") return "Consulte seu boletim e sua frequencia.";
            if (perfil == "professor") return "Acesse lancamento de notas e frequencia.";
            if (perfil == "coordenacao") return "Gerencie cursos, turmas, disciplinas e matriculas.";
            if (perfil == "administrativo") return "Gerencie alunos, matriculas e pagamentos.";
            if (perfil == "admin") return "Acompanhe usuarios e cadastros principais.";

            return "Perfil nao identificado.";
        }

        private void OcultarTodosMenus()
        {
            foreach (Button botao in botoesMenu)
            {
                botao.Visible = false;
            }

        }

        private void MostrarMenus(params Button[] botoes)
        {
            foreach (Button botao in botoes)
            {
                botao.Visible = true;
            }
        }

        private bool PodeAcessar(Button botao)
        {
            return botao.Visible;
        }

        private void AvisarSemPermissao()
        {
            MessageBox.Show("Seu usuario nao tem permissao para acessar esta tela.",
                "Permissao",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        private void ConfigurarEventos()
        {
            btnInicio.Click += btnInicio_Click;
            btnSair.Click += btnSair_Click;

            btnUsuarios.Click += btnUsuarios_Click;
            btnAlunos.Click += btnAlunos_Click;
            btnProfessores.Click += btnProfessores_Click;
            btnFuncionarios.Click += btnFuncionarios_Click;
            btnCursos.Click += btnCursos_Click;
            btnDisciplinas.Click += btnDisciplinas_Click;
            btnTurmas.Click += btnTurmas_Click;
            btnMatriculas.Click += btnMatriculas_Click;
            btnNotas.Click += btnNotas_Click;
            btnFrequencia.Click += btnFrequencia_Click;
            btnBoletimAluno.Click += btnBoletimAluno_Click;
            btnFrequenciaAluno.Click += btnFrequenciaAluno_Click;
            btnPagamentos.Click += btnPagamentos_Click;
        }

        private void MarcarBotaoAtivo(Button? botao)
        {

            foreach (Button item in botoesMenu)
            {
            }

            if (botao != null)
            {
            }

            botaoAtivo = botao;
        }

        // ==========================
        // TELA INICIAL
        // ==========================
        private void MontarTelaInicial()
        {
            LimparPainel();
            panel1.Padding = new Padding(24);

            string perfil = NormalizarPerfil(Sessao.NivelAcesso);
            AtualizarTopo("Dashboard", TextoBoasVindas(perfil));
            MarcarBotaoAtivo(btnInicio);

            FlowLayoutPanel area = new FlowLayoutPanel();
            area.Dock = DockStyle.Fill;
            area.FlowDirection = FlowDirection.TopDown;
            area.WrapContents = false;
            area.AutoScroll = true;

            Label lblBoasVindas = new Label();
            lblBoasVindas.Text = "Bem-vindo, " + NomeUsuario();
            lblBoasVindas.Height = 42;

            Label lblResumo = new Label();
            lblResumo.Text = TextoBoasVindas(perfil);
            lblResumo.Height = 30;

            TableLayoutPanel cards = CriarGridCards(perfil);
            Panel pnlAcessos = CriarPainelAcessos();

            area.Controls.Add(lblBoasVindas);
            area.Controls.Add(lblResumo);
            area.Controls.Add(cards);
            area.Controls.Add(pnlAcessos);

            area.Resize += (s, e) =>
            {
                int largura = area.ClientSize.Width - 26;
                lblBoasVindas.Width = largura;
                lblResumo.Width = largura;
                cards.Width = largura;
                pnlAcessos.Width = largura;
            };

            panel1.Controls.Add(area);
        }

        private TableLayoutPanel CriarGridCards(string perfil)
        {
            TableLayoutPanel cards = new TableLayoutPanel();
            cards.Height = 140;
            cards.Margin = new Padding(0, 16, 0, 18);
            cards.ColumnCount = 4;
            cards.RowCount = 1;
            cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            int acessos = botoesMenu.Count(b => b.Visible);

            cards.Controls.Add(CriarCard("Perfil", NomePerfil(perfil), "Acesso carregado pela sessao"), 0, 0);
            cards.Controls.Add(CriarCard("Acessos", acessos.ToString(), "Menus liberados"), 1, 0);
            cards.Controls.Add(CriarCard("Sessao", "Online", "Usuario autenticado"), 2, 0);
            cards.Controls.Add(CriarCard("Modulo", "Desktop", "Windows Forms"), 3, 0);

            return cards;
        }

        private Panel CriarCard(string titulo, string valor, string detalhe)
        {
            Panel card = new Panel();
            card.Dock = DockStyle.Fill;
            card.Margin = new Padding(0, 0, 12, 0);
            card.Padding = new Padding(16);

            Label lblTituloCard = new Label();
            lblTituloCard.Text = titulo;
            lblTituloCard.Dock = DockStyle.Top;
            lblTituloCard.Height = 26;

            Label lblValorCard = new Label();
            lblValorCard.Text = valor;
            lblValorCard.Dock = DockStyle.Top;
            lblValorCard.Height = 44;

            Label lblDetalheCard = new Label();
            lblDetalheCard.Text = detalhe;
            lblDetalheCard.Dock = DockStyle.Top;
            lblDetalheCard.Height = 28;

            card.Controls.Add(lblDetalheCard);
            card.Controls.Add(lblValorCard);
            card.Controls.Add(lblTituloCard);

            return card;
        }

        private Panel CriarPainelAcessos()
        {
            Panel painel = new Panel();
            painel.Height = 220;
            painel.Padding = new Padding(18);

            Label titulo = new Label();
            titulo.Text = "Funcionalidades liberadas";
            titulo.Dock = DockStyle.Top;
            titulo.Height = 34;

            Label texto = new Label();
            texto.Text = "Os botoes abaixo seguem a mesma divisao de perfis usada no modulo PHP.";
            texto.Dock = DockStyle.Top;
            texto.Height = 28;

            FlowLayoutPanel acoes = new FlowLayoutPanel();
            acoes.Dock = DockStyle.Fill;
            acoes.FlowDirection = FlowDirection.LeftToRight;
            acoes.WrapContents = true;
            acoes.Padding = new Padding(0, 12, 0, 0);

            foreach (Button botaoMenu in botoesMenu.Where(b => b.Visible))
            {
                Button botaoAcao = new Button();
                botaoAcao.Text = botaoMenu.Text;
                botaoAcao.Width = 150;
                botaoAcao.Height = 38;
                botaoAcao.Margin = new Padding(0, 0, 10, 10);
                botaoAcao.Tag = botaoMenu;
                botaoAcao.Click += (s, e) =>
                {
                    if (s is Button botaoClicado && botaoClicado.Tag is Button origem)
                    {
                        origem.PerformClick();
                    }
                };

                acoes.Controls.Add(botaoAcao);
            }

            if (acoes.Controls.Count == 0)
            {
                Label vazio = new Label();
                vazio.Text = "Nenhum menu liberado para este perfil.";
                vazio.AutoSize = true;
                acoes.Controls.Add(vazio);
            }

            painel.Controls.Add(acoes);
            painel.Controls.Add(texto);
            painel.Controls.Add(titulo);

            return painel;
        }

        // ==========================
        // ABRIR FORM NO PAINEL
        // ==========================
        private void AbrirFormulario(Form form, Button botao, string titulo)
        {
            try
            {
                LimparPainel();

                panel1.Padding = new Padding(0);
                AtualizarTopo(titulo, "Formulario aberto no painel principal.");
                MarcarBotaoAtivo(botao);

                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                panel1.Controls.Add(form);
                form.Show();
            }
            catch (Exception ex)
            {
                form.Dispose();

                MessageBox.Show("Erro ao abrir tela: " + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LimparPainel()
        {
            while (panel1.Controls.Count > 0)
            {
                Control controle = panel1.Controls[0];
                panel1.Controls.RemoveAt(0);
                controle.Dispose();
            }
        }

        private void AtualizarTopo(string titulo, string subtitulo)
        {
            lblTitulo.Text = titulo;
            lblSubtitulo.Text = subtitulo;
            statusData.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        // ==========================
        // BOTOES
        // ==========================
        private void btnInicio_Click(object? sender, EventArgs e)
        {
            MontarTelaInicial();
        }

        private void btnSair_Click(object? sender, EventArgs e)
        {
            DialogResult resposta = MessageBox.Show(
                "Deseja sair do sistema?",
                "Sair",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resposta != DialogResult.Yes)
            {
                return;
            }

            Sessao.IdUsuario = 0;
            Sessao.Nome = "";
            Sessao.NivelAcesso = "";

            Close();
        }

        private void btnUsuarios_Click(object? sender, EventArgs e)
        {
            if (!PodeAcessar(btnUsuarios)) { AvisarSemPermissao(); return; }
            AbrirFormulario(new FrmUsuario(), btnUsuarios, "Usuarios");
        }

        private void btnAlunos_Click(object? sender, EventArgs e)
        {
            if (!PodeAcessar(btnAlunos)) { AvisarSemPermissao(); return; }
            AbrirFormulario(new FrmAluno(), btnAlunos, "Alunos");
        }

        private void btnBoletimAluno_Click(object? sender, EventArgs e)
        {
            if (!PodeAcessar(btnBoletimAluno)) { AvisarSemPermissao(); return; }
            AbrirFormulario(new FrmBoletimAluno(), btnBoletimAluno, "Boletim / Notas");
        }

        private void btnFrequenciaAluno_Click(object? sender, EventArgs e)
        {
            if (!PodeAcessar(btnFrequenciaAluno)) { AvisarSemPermissao(); return; }
            AbrirFormulario(new FrmFrequenciaAluno(), btnFrequenciaAluno, "Frequência");
        }

        private void btnProfessores_Click(object? sender, EventArgs e)
        {
            if (!PodeAcessar(btnProfessores)) { AvisarSemPermissao(); return; }
            AbrirFormulario(new FrmProfessor(), btnProfessores, "Professores");
        }

        private void btnFuncionarios_Click(object? sender, EventArgs e)
        {
            if (!PodeAcessar(btnFuncionarios)) { AvisarSemPermissao(); return; }
            AbrirFormulario(new FrmFuncionarios(), btnFuncionarios, "Funcionarios");
        }

        private void btnCursos_Click(object? sender, EventArgs e)
        {
            if (!PodeAcessar(btnCursos)) { AvisarSemPermissao(); return; }
            AbrirFormulario(new FrmCurso(), btnCursos, "Cursos");
        }

        private void btnDisciplinas_Click(object? sender, EventArgs e)
        {
            if (!PodeAcessar(btnDisciplinas)) { AvisarSemPermissao(); return; }
            AbrirFormulario(new FrmDisciplina(), btnDisciplinas, "Disciplinas");
        }

        private void btnTurmas_Click(object? sender, EventArgs e)
        {
            if (!PodeAcessar(btnTurmas)) { AvisarSemPermissao(); return; }
            AbrirFormulario(new FrmTurma(), btnTurmas, "Turmas");
        }

        private void btnMatriculas_Click(object? sender, EventArgs e)
        {
            if (!PodeAcessar(btnMatriculas)) { AvisarSemPermissao(); return; }
            AbrirFormulario(new FrmMatricula(), btnMatriculas, "Matriculas");
        }

        private void btnNotas_Click(object? sender, EventArgs e)
        {
            if (!PodeAcessar(btnNotas)) { AvisarSemPermissao(); return; }
            AbrirFormulario(new FrmNota(), btnNotas, "Notas");
        }

        private void btnFrequencia_Click(object? sender, EventArgs e)
        {
            if (!PodeAcessar(btnFrequencia)) { AvisarSemPermissao(); return; }
            AbrirFormulario(new FrmFrequencia(), btnFrequencia, "Frequencia");
        }

        private void btnPagamentos_Click(object? sender, EventArgs e)
        {
            if (!PodeAcessar(btnPagamentos)) { AvisarSemPermissao(); return; }
            AbrirFormulario(new FrmPagamento(), btnPagamentos, "Pagamentos");
        }

    }
}
