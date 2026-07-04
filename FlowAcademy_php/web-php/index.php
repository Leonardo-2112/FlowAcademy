<?php

require_once __DIR__ . '/includes/funcoes.php';

if (estaLogado()) {
    redirecionar('dashboard.php');
}

try {
    $cursos = buscarTodos(
        'SELECT c.id_curso, c.nome, c.descricao, c.carga_horaria, c.status,
                COUNT(d.id_disciplina) AS total_ucs,
                COALESCE(SUM(d.carga_horaria), 0) AS carga_ucs
         FROM cursos c
         LEFT JOIN disciplinas d ON d.id_curso = c.id_curso
         WHERE c.status = "ativo"
         GROUP BY c.id_curso
         ORDER BY c.nome'
    );
} catch (Throwable $erro) {
    $cursos = [];
}
?>
<!doctype html>
<html lang="pt-BR">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>Flow Academy | Gestao Academica</title>
  <link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet">
  <link rel="stylesheet" href="assets/css/main.css">
</head>
<body class="landing-page">
  <nav class="navbar navbar-expand-lg landing-nav fixed-top">
    <div class="container">
      <a class="brand landing-brand" href="#sobre">
        <img src="assets/img/logos/logo-flow-academy-gold.jpg" alt="Logo Flow Academy">
        <span><strong>Flow Academy</strong><small>Gestao Academica Presencial</small></span>
      </a>
      <button class="landing-toggler navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#landingMenu">
        <span class="hamburger"><span></span><span></span><span></span></span>
      </button>
      <div class="collapse navbar-collapse justify-content-end" id="landingMenu">
        <ul class="nav landing-menu">
          <li><a class="nav-link" href="#sobre">Sobre</a></li>
          <li><a class="nav-link" href="#proposta">Proposta</a></li>
          <li><a class="nav-link" href="#cursos">Cursos</a></li>
          <li><a class="nav-link" href="#perfis">Perfis</a></li>
        </ul>
        <a class="btn primary" href="login.php">Entrar</a>
      </div>
    </div>
  </nav>

  <main>
    <section class="school-hero" id="sobre">
      <div class="container">
        <img class="school-hero-mark" src="assets/img/logos/logo-flow-academy-final.png" alt="Flow Academy">
        <div class="school-hero-content">
          <span class="eyebrow">Flow Academy</span>
          <h1>Gestao academica presencial em um unico ambiente.</h1>
          <p class="hero-text">Sistema simples para acompanhar alunos, cursos, turmas, notas, frequencia e pagamentos, com um ambiente dedicado para cada perfil de acesso.</p>
          <div class="actions landing-actions">
            <a class="btn primary" href="#cursos">Conhecer cursos</a>
            <a class="btn ghost" href="login.php">Area do aluno</a>
          </div>
        </div>
      </div>
    </section>

    <section class="landing-section soft-section" id="proposta">
      <div class="container">
        <div class="section-title">
          <span class="eyebrow">O que a Flow representa</span>
          <h2>Uma escola com rotina academica clara e acompanhamento proximo.</h2>
          <p>A Flow Academy organiza a jornada do aluno desde o cadastro ate o acompanhamento de notas, frequencia e situacao academica. A proposta e unir ensino tecnico presencial com uma gestao simples e objetiva.</p>
        </div>

        <div class="grid pillar-grid">
          <article class="card pillar-card">
            <span class="pillar-index">01</span>
            <h3>Formacao tecnica</h3>
            <p>Cursos voltados para desenvolvimento profissional, com unidades curriculares organizadas por carga horaria.</p>
          </article>
          <article class="card pillar-card">
            <span class="pillar-index">02</span>
            <h3>Acompanhamento real</h3>
            <p>Boletim, frequencia, matriculas e informacoes academicas ficam mais faceis de consultar e explicar.</p>
          </article>
          <article class="card pillar-card">
            <span class="pillar-index">03</span>
            <h3>Gestao integrada</h3>
            <p>Coordenacao, administrativo, professores e alunos trabalham com informacoes separadas por perfil.</p>
          </article>
        </div>
      </div>
    </section>

    <section class="landing-section" id="cursos">
      <div class="container">
        <div class="section-title courses-title">
          <span class="eyebrow">Cursos da escola</span>
          <h2>Conheca as formacoes da Flow Academy.</h2>
          <p>Os cursos abaixo sao uma porta de entrada para um mundo profissional com foco em tecnologia e inovacao.</p>
        </div>

        <?php if (empty($cursos)): ?>
          <div class="course-empty-box">
            <span class="course-empty-tag">Cursos em cadastro</span>
            <h3>Nenhum curso ativo foi encontrado.</h3>
            <p>Assim que a coordenacao cadastrar cursos ativos, esta secao mostrara nome, descricao, carga horaria e unidades curriculares.</p>
            <a class="btn primary" href="login.php">Entrar no sistema</a>
          </div>
        <?php else: ?>
          <div class="grid three">
            <?php foreach ($cursos as $curso): ?>
              <article class="card landing-card">
                <span class="badge warning">Curso tecnico</span>
                <h3><?= e($curso['nome']) ?></h3>
                <p><?= e($curso['descricao']) ?></p>
                <p class="metric-meta"><?= e($curso['carga_horaria']) ?>h - <?= e($curso['total_ucs']) ?> UCs</p>
              </article>
            <?php endforeach; ?>
          </div>
        <?php endif; ?>
      </div>
    </section>

    <section class="landing-section soft-section" id="perfis">
      <div class="container">
        <div class="section-title">
          <span class="eyebrow">Acesso por perfil</span>
          <h2>Cada perfil especifico tem seu proprio ambiente.</h2>
        </div>

        <div class="grid profile-grid">
          <article class="card profile-env-card">
            <span class="badge perfil-aluno">Aluno</span>
            <h3>Vida academica</h3>
            <p>Consulta de boletim, frequencia e informacoes do proprio curso.</p>
          </article>
          <article class="card profile-env-card">
            <span class="badge perfil-professor">Professor</span>
            <h3>Turmas e avaliacoes</h3>
            <p>Lancamento de notas, registro de frequencia e acompanhamento das turmas.</p>
          </article>
          <article class="card profile-env-card">
            <span class="badge perfil-coordenacao">Coordenacao</span>
            <h3>Cursos e turmas</h3>
            <p>Organizacao dos cursos, unidades curriculares, turmas e professores.</p>
          </article>
          <article class="card profile-env-card">
            <span class="badge perfil-admin">Admin</span>
            <h3>Controle do sistema</h3>
            <p>Apoio aos cadastros, permissoes, logs e operacao institucional.</p>
          </article>
        </div>
      </div>
    </section>

    <section class="landing-cta">
      <div class="container">
        <div class="landing-cta-box">
          <div>
            <span class="eyebrow">Acesso institucional</span>
            <h2>Acesse o ambiente academico da Flow.</h2>
            <p>Alunos, professores e equipe administrativa entram pela mesma tela de login. O sistema abre automaticamente o painel correto conforme o perfil do usuario.</p>
          </div>
          <a class="btn primary" href="login.php">Acessar login</a>
        </div>
      </div>
    </section>
  </main>

  <footer class="site-footer">
    <div class="container footer-grid">
      <div>
        <strong>Flow Academy</strong>
        <span>Flow Academy Platform</span>
      </div>
      <div>
        <strong>Endereco</strong>
        <span>Av. Itaquera, 8266 - Vila Carmosina - Sao Paulo/SP</span>
      </div>
      <div>
        <strong>Contato</strong>
        <span>(11) 91552-8586</span>
        <span>contato@flowacademy.com</span>
      </div>
      <div class="footer-copy">
        <span>&copy; Projeto Integrador UC16 - Curso Tecnico em Informatica.</span>
      </div>
    </div>
  </footer>

  <script src="assets/bootstrap/js/bootstrap.bundle.min.js"></script>
  <script src="assets/js/app.js"></script>
</body>
</html>
