<?php

require_once __DIR__ . '/includes/funcoes.php';

exigirLogin('');

if (!empty(usuarioLogado()['trocar_senha'])) {
    redirecionar('alterar_senha.php');
}

$titulo = 'Dashboard';
$ativo = 'dashboard';
$base = '';
$usuario = usuarioLogado();
$perfil = perfilAtivo();

atualizarPagamentosAtrasados();

require __DIR__ . '/includes/header.php';
?>

<?php if ($perfil === 'aluno'): ?>
  <?php
  $aluno = buscarUm('SELECT * FROM alunos WHERE id_usuario = :id', [':id' => $usuario['id_usuario']]);
  $idAluno = $aluno['id_aluno'] ?? 0;

  $mediaGeral = buscarUm(
      'SELECT AVG(n.media_uc) AS media FROM notas n JOIN matriculas m ON m.id_matricula = n.id_matricula WHERE m.id_aluno = :id',
      [':id' => $idAluno]
  );
  $frequenciaMedia = buscarUm(
      'SELECT AVG(f.percentual) AS percentual FROM frequencia f JOIN matriculas m ON m.id_matricula = f.id_matricula WHERE m.id_aluno = :id',
      [':id' => $idAluno]
  );
  $pendencias = contar('SELECT COUNT(*) AS total FROM pagamentos WHERE id_aluno = :id AND status IN ("pendente", "atrasado")', [':id' => $idAluno]);
  $matricula = buscarUm(
      'SELECT m.*, t.codigo_turma, c.nome AS curso
       FROM matriculas m
       JOIN turmas t ON t.id_turma = m.id_turma
       JOIN cursos c ON c.id_curso = t.id_curso
       WHERE m.id_aluno = :id AND m.status = "ativa"
       ORDER BY m.id_matricula DESC LIMIT 1',
      [':id' => $idAluno]
  );
  ?>
  <section class="page-heading"><div><span class="eyebrow">Aluno</span><h1>Dashboard Aluno</h1><p>Resumo academico conectado ao banco de dados.</p></div></section>

  <div class="grid four">
    <article class="card metric-card"><span class="metric-label">Media geral</span><div class="metric-value"><?= e(number_format((float) ($mediaGeral['media'] ?? 0), 1, ',', '.')) ?></div><p class="metric-meta warning">Criterio minimo: 6,0</p></article>
    <article class="card metric-card"><span class="metric-label">Frequencia</span><div class="metric-value"><?= e(number_format((float) ($frequenciaMedia['percentual'] ?? 0), 0)) ?>%</div><p class="metric-meta warning">Minimo recomendado: 75%</p></article>
    <article class="card metric-card"><span class="metric-label">Pendencias</span><div class="metric-value"><?= e($pendencias) ?></div><p class="metric-meta">Pagamentos em aberto</p></article>
    <article class="card metric-card"><span class="metric-label">Status</span><div class="metric-value"><?= e(textoStatus($aluno['status_academico'] ?? 'regular')) ?></div><p class="metric-meta positive">Matricula <?= e($aluno['matricula'] ?? '-') ?></p></article>
  </div>

  <div class="grid two">
    <section class="panel">
      <div class="panel-header"><h2>Dados do aluno</h2></div>
      <div class="panel-body info-list">
        <div><span>Nome</span><strong><?= e($usuario['nome']) ?></strong></div>
        <div><span>E-mail</span><strong><?= e($usuario['email']) ?></strong></div>
        <div><span>CPF</span><strong><?= e($aluno['cpf'] ?? '-') ?></strong></div>
        <div><span>Telefone</span><strong><?= e($aluno['telefone'] ?? '-') ?></strong></div>
      </div>
    </section>

    <section class="panel">
      <div class="panel-header"><h2>Matricula atual</h2></div>
      <div class="panel-body">
        <?php if ($matricula): ?>
          <div class="info-list">
            <div><span>Turma</span><strong><?= e($matricula['codigo_turma']) ?></strong></div>
            <div><span>Curso</span><strong><?= e($matricula['curso']) ?></strong></div>
            <div><span>Status</span><?= badge($matricula['status']) ?></div>
          </div>
        <?php else: ?>
          <div class="alert warning">
            <span class="alert-marker"></span>
            <div><strong>Sem matricula</strong><span class="muted">Nenhuma matricula foi encontrada para este aluno.</span></div>
          </div>
        <?php endif; ?>
      </div>
    </section>
  </div>

<?php elseif ($perfil === 'professor'): ?>
  <?php
  $professor = buscarUm('SELECT * FROM professores WHERE id_usuario = :id', [':id' => $usuario['id_usuario']]);
  $idProfessor = $professor['id_professor'] ?? 0;

  $totalTurmas = contar('SELECT COUNT(*) AS total FROM turmas WHERE id_professor = :id', [':id' => $idProfessor]);
  $totalAlunos = contar(
      'SELECT COUNT(*) AS total FROM matriculas m JOIN turmas t ON t.id_turma = m.id_turma WHERE t.id_professor = :id AND m.status = "ativa"',
      [':id' => $idProfessor]
  );
  $totalNotas = contar(
      'SELECT COUNT(*) AS total FROM notas n JOIN matriculas m ON m.id_matricula = n.id_matricula JOIN turmas t ON t.id_turma = m.id_turma WHERE t.id_professor = :id',
      [':id' => $idProfessor]
  );
  $mediaTurmas = buscarUm(
      'SELECT AVG(n.media_uc) AS media FROM notas n JOIN matriculas m ON m.id_matricula = n.id_matricula JOIN turmas t ON t.id_turma = m.id_turma WHERE t.id_professor = :id',
      [':id' => $idProfessor]
  );
  $turmas = buscarTodos(
      'SELECT t.*, c.nome AS curso, COUNT(m.id_matricula) AS alunos_matriculados
       FROM turmas t
       JOIN cursos c ON c.id_curso = t.id_curso
       LEFT JOIN matriculas m ON m.id_turma = t.id_turma AND m.status = "ativa"
       WHERE t.id_professor = :id
       GROUP BY t.id_turma
       ORDER BY t.codigo_turma',
      [':id' => $idProfessor]
  );
  ?>
  <section class="page-heading"><div><span class="eyebrow">Professor</span><h1>Dashboard Professor</h1><p>Turmas, alunos e notas vinculados ao professor logado.</p></div></section>

  <div class="grid four">
    <article class="card metric-card"><span class="metric-label">Turmas</span><div class="metric-value"><?= e($totalTurmas) ?></div><p class="metric-meta positive">Turmas atribuidas</p></article>
    <article class="card metric-card"><span class="metric-label">Alunos</span><div class="metric-value"><?= e($totalAlunos) ?></div><p class="metric-meta">Matriculas ativas</p></article>
    <article class="card metric-card"><span class="metric-label">Notas lancadas</span><div class="metric-value"><?= e($totalNotas) ?></div><p class="metric-meta">Registros em notas</p></article>
    <article class="card metric-card"><span class="metric-label">Media das turmas</span><div class="metric-value"><?= e(number_format((float) ($mediaTurmas['media'] ?? 0), 1, ',', '.')) ?></div><p class="metric-meta warning">Base: medias lancadas</p></article>
  </div>

  <section class="panel">
    <div class="panel-header"><h2>Minhas turmas</h2></div>
    <div class="table-wrap">
      <table id="tabela-principal">
        <thead><tr><th>Turma</th><th>Curso</th><th>Turno</th><th>Periodo</th><th>Alunos</th><th>Status</th></tr></thead>
        <tbody>
          <?php foreach ($turmas as $turma): ?>
            <tr>
              <td><?= e($turma['codigo_turma']) ?></td><td><?= e($turma['curso']) ?></td><td><?= e(textoStatus($turma['turno'])) ?></td><td><?= e($turma['periodo_letivo']) ?></td>
              <td><?= e($turma['alunos_matriculados']) ?>/<?= e($turma['capacidade_maxima']) ?></td><td><?= badge($turma['status']) ?></td>
            </tr>
          <?php endforeach; ?>
        </tbody>
      </table>
    </div>
  </section>

<?php elseif ($perfil === 'coordenacao'): ?>
  <?php
  $totalCursos = contar('SELECT COUNT(*) AS total FROM cursos');
  $totalTurmas = contar('SELECT COUNT(*) AS total FROM turmas');
  $totalUcs = contar('SELECT COUNT(*) AS total FROM disciplinas');
  $ultimasTurmas = buscarTodos(
      'SELECT t.*, c.nome AS curso, u.nome AS professor
       FROM turmas t
       JOIN cursos c ON c.id_curso = t.id_curso
       JOIN professores p ON p.id_professor = t.id_professor
       JOIN usuarios u ON u.id_usuario = p.id_usuario
       ORDER BY t.id_turma DESC LIMIT 6'
  );
  ?>
  <section class="page-heading"><div><span class="eyebrow">Coordenacao</span><h1>Dashboard Coordenacao</h1><p>Indicadores de cursos, turmas e alertas academicos.</p></div></section>

  <div class="grid four">
    <article class="card metric-card"><span class="metric-label">Cursos</span><div class="metric-value"><?= e($totalCursos) ?></div><p class="metric-meta positive">Cadastrados</p></article>
    <article class="card metric-card"><span class="metric-label">Turmas</span><div class="metric-value"><?= e($totalTurmas) ?></div><p class="metric-meta">No banco</p></article>
    <article class="card metric-card"><span class="metric-label">UCs</span><div class="metric-value"><?= e($totalUcs) ?></div><p class="metric-meta">Unidades curriculares</p></article>
    <article class="card metric-card"><span class="metric-label">Alertas</span><div class="metric-value">0</div><p class="metric-meta warning">Pendentes</p></article>
  </div>

  <section class="panel">
    <div class="panel-header"><h2>Ultimas turmas</h2></div>
    <div class="table-wrap">
      <table id="tabela-principal">
        <thead><tr><th>Turma</th><th>Curso</th><th>Professor</th><th>Turno</th><th>Periodo</th><th>Status</th></tr></thead>
        <tbody>
          <?php foreach ($ultimasTurmas as $turma): ?>
            <tr>
              <td><?= e($turma['codigo_turma']) ?></td><td><?= e($turma['curso']) ?></td><td><?= e($turma['professor']) ?></td>
              <td><?= e(textoStatus($turma['turno'])) ?></td><td><?= e($turma['periodo_letivo']) ?></td><td><?= badge($turma['status']) ?></td>
            </tr>
          <?php endforeach; ?>
        </tbody>
      </table>
    </div>
  </section>

<?php elseif ($perfil === 'admin'): ?>
  <?php
  $totalUsuarios = contar('SELECT COUNT(*) AS total FROM usuarios');
  $totalAtivos = contar('SELECT COUNT(*) AS total FROM usuarios WHERE status = "ativo"');
  $totalLogs = contar('SELECT COUNT(*) AS total FROM logs');
  $ultimosLogs = buscarTodos(
      'SELECT l.*, u.nome, u.perfil
       FROM logs l
       JOIN usuarios u ON u.id_usuario = l.id_usuario
       ORDER BY l.data_evento DESC LIMIT 8'
  );
  ?>
  <section class="page-heading">
    <div><span class="eyebrow">Admin</span><h1>Dashboard Admin</h1><p>Visao de usuarios, logs e alertas do sistema.</p></div>
    <div class="actions">
      <a class="btn ghost" href="pages/cadastro_coordenacao.php">Cadastrar coordenacao</a>
      <a class="btn primary" href="pages/cadastro_administrativo.php">Cadastrar administrativo</a>
    </div>
  </section>

  <div class="grid four">
    <article class="card metric-card"><span class="metric-label">Usuarios</span><div class="metric-value"><?= e($totalUsuarios) ?></div><p class="metric-meta">Total cadastrado</p></article>
    <article class="card metric-card"><span class="metric-label">Ativos</span><div class="metric-value"><?= e($totalAtivos) ?></div><p class="metric-meta positive">Podem acessar</p></article>
    <article class="card metric-card"><span class="metric-label">Logs</span><div class="metric-value"><?= e($totalLogs) ?></div><p class="metric-meta">Eventos registrados</p></article>
    <article class="card metric-card"><span class="metric-label">Alertas</span><div class="metric-value">0</div><p class="metric-meta warning">Risco academico</p></article>
  </div>

  <section class="panel">
    <div class="panel-header"><h2>Ultimos logs</h2></div>
    <div class="table-wrap">
      <table id="tabela-principal">
        <thead><tr><th>Usuario</th><th>Perfil</th><th>Acao</th><th>IP</th><th>Data</th></tr></thead>
        <tbody>
          <?php foreach ($ultimosLogs as $log): ?>
            <tr>
              <td><?= e($log['nome']) ?></td><td><?= e(textoStatus($log['perfil'])) ?></td><td><?= e($log['acao']) ?></td>
              <td><?= e($log['ip']) ?></td><td><?= e(dataBr($log['data_evento'])) ?></td>
            </tr>
          <?php endforeach; ?>
        </tbody>
      </table>
    </div>
  </section>

<?php else: /* administrativo */ ?>
  <?php
  $totalAlunos = contar('SELECT COUNT(*) AS total FROM alunos');
  $totalMatriculas = contar('SELECT COUNT(*) AS total FROM matriculas WHERE status = "ativa"');
  $totalRegulares = contar('SELECT COUNT(*) AS total FROM alunos WHERE status_academico = "regular"');
  $resumoPagamentos = buscarUm(
      'SELECT
          COALESCE(SUM(CASE WHEN status IN ("pendente", "atrasado") THEN valor END), 0) AS a_receber,
          COALESCE(SUM(CASE WHEN status = "pago" THEN valor END), 0) AS recebido,
          COUNT(CASE WHEN status = "pendente" THEN 1 END) AS pendentes,
          COUNT(CASE WHEN status = "atrasado" THEN 1 END) AS atrasados
       FROM pagamentos'
  );
  $ultimosAlunos = buscarTodos(
      'SELECT a.id_aluno, a.matricula, a.status_academico, u.nome, u.email
       FROM alunos a
       JOIN usuarios u ON u.id_usuario = a.id_usuario
       ORDER BY a.id_aluno DESC LIMIT 6'
  );
  ?>
  <section class="page-heading">
    <div><span class="eyebrow">Administrativo</span><h1>Dashboard Administrativo</h1><p>Cadastros, matriculas e financeiro em um unico painel.</p></div>
    <div class="actions"><a class="btn primary" href="pages/novo_pagamento.php">Novo pagamento</a></div>
  </section>

  <div class="grid four">
    <article class="card metric-card"><span class="metric-label">Alunos</span><div class="metric-value"><?= e($totalAlunos) ?></div><p class="metric-meta positive">Cadastrados</p></article>
    <article class="card metric-card"><span class="metric-label">Matriculas</span><div class="metric-value"><?= e($totalMatriculas) ?></div><p class="metric-meta">Ativas</p></article>
    <article class="card metric-card"><span class="metric-label">Regulares</span><div class="metric-value"><?= e($totalRegulares) ?></div><p class="metric-meta">Status academico</p></article>
    <article class="card metric-card"><span class="metric-label">A receber</span><div class="metric-value"><?= e(dinheiro($resumoPagamentos['a_receber'] ?? 0)) ?></div><p class="metric-meta warning">Pendentes e atrasados</p></article>
  </div>
  <div class="grid four">
    <article class="card metric-card"><span class="metric-label">Recebido</span><div class="metric-value"><?= e(dinheiro($resumoPagamentos['recebido'] ?? 0)) ?></div><p class="metric-meta positive">Pagos</p></article>
    <article class="card metric-card"><span class="metric-label">Pendentes</span><div class="metric-value"><?= e($resumoPagamentos['pendentes'] ?? 0) ?></div><p class="metric-meta positive">Aguardando pagamento</p></article>
    <article class="card metric-card"><span class="metric-label">Atrasados</span><div class="metric-value"><?= e($resumoPagamentos['atrasados'] ?? 0) ?></div><p class="metric-meta warning">Necessitam contato</p></article>
    <article class="card metric-card"><span class="metric-label">Painel unico</span><div class="metric-value">ADM</div><p class="metric-meta">Financeiro integrado</p></article>
  </div>

  <section class="panel">
    <div class="panel-header"><h2>Ultimos alunos</h2></div>
    <div class="table-wrap">
      <table id="tabela-principal">
        <thead><tr><th>Aluno</th><th>Matricula</th><th>E-mail</th><th>Status</th><th>Acoes</th></tr></thead>
        <tbody>
          <?php foreach ($ultimosAlunos as $aluno): ?>
            <tr>
              <td><?= e($aluno['nome']) ?></td><td><?= e($aluno['matricula']) ?></td><td><?= e($aluno['email']) ?></td><td><?= badge($aluno['status_academico']) ?></td>
              <td class="table-actions">
                <a class="btn ghost" href="pages/ver_aluno.php?id=<?= e($aluno['id_aluno']) ?>">Ver</a>
                <a class="btn ghost" href="pages/cadastro_aluno.php?editar=<?= e($aluno['id_aluno']) ?>">Editar</a>
              </td>
            </tr>
          <?php endforeach; ?>
        </tbody>
      </table>
    </div>
  </section>
<?php endif; ?>

<?php require __DIR__ . '/includes/footer.php'; ?>
