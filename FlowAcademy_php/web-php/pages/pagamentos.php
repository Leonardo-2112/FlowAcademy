<?php

require_once __DIR__ . '/../includes/funcoes.php';
exigirPerfil(['admin', 'administrativo'], '../');

$base = '../';
$titulo = 'Pagamentos';
$ativo = 'pagamentos';

atualizarPagamentosAtrasados();

$idAlunoFiltro = (int) getValor('id_aluno');
$idTurmaFiltro = (int) getValor('id_turma');

$alunos = buscarTodos(
    'SELECT a.id_aluno, a.matricula, u.nome, m.id_turma
     FROM alunos a
     JOIN usuarios u ON u.id_usuario = a.id_usuario
     LEFT JOIN matriculas m ON m.id_aluno = a.id_aluno AND m.status = "ativa"
     ORDER BY u.nome'
);
$turmas = buscarTodos(
    'SELECT t.id_turma, t.codigo_turma, c.nome AS curso
     FROM turmas t JOIN cursos c ON c.id_curso = t.id_curso
     ORDER BY t.codigo_turma'
);

$pagamentos = [];
$alunoSelecionado = null;
if ($idAlunoFiltro > 0) {
    $alunoSelecionado = buscarUm(
        'SELECT a.*, u.nome FROM alunos a JOIN usuarios u ON u.id_usuario = a.id_usuario WHERE a.id_aluno = :id',
        [':id' => $idAlunoFiltro]
    );
    $pagamentos = buscarTodos(
        'SELECT * FROM pagamentos WHERE id_aluno = :id ORDER BY vencimento DESC',
        [':id' => $idAlunoFiltro]
    );
}

require __DIR__ . '/../includes/header.php';
?>

<section class="page-heading">
  <div><span class="eyebrow">Administrativo</span><h1>Pagamentos</h1><p>Consulta de pagamentos por turma e aluno.</p></div>
  <div class="actions"><a class="btn primary" href="novo_pagamento.php">Novo pagamento</a></div>
</section>

<section class="panel">
  <div class="panel-header"><h2>Filtrar aluno</h2></div>
  <div class="panel-body">
    <form class="form-grid" method="get">
      <label class="field span-2">
        <span>Turma</span>
        <select class="select" id="pag-turma" name="id_turma" data-turma-filter data-filter-students="#pag-aluno">
          <option value="">Selecione a turma</option>
          <?php foreach ($turmas as $turma): ?><option value="<?= e($turma['id_turma']) ?>" <?= $idTurmaFiltro === (int) $turma['id_turma'] ? 'selected' : '' ?>><?= e($turma['codigo_turma']) ?> - <?= e($turma['curso']) ?></option><?php endforeach; ?>
        </select>
      </label>
      <label class="field">
        <span>Buscar aluno</span>
        <input class="control" type="text" placeholder="Nome ou matricula" data-student-search="#pag-aluno" data-turma-source="#pag-turma">
      </label>
      <label class="field">
        <span>Aluno</span>
        <select class="select" id="pag-aluno" name="id_aluno">
          <option value="">Selecione a turma primeiro</option>
          <?php foreach ($alunos as $aluno): ?>
            <option value="<?= e($aluno['id_aluno']) ?>" data-turma="<?= e($aluno['id_turma']) ?>" data-search="<?= e($aluno['nome'] . ' ' . $aluno['matricula']) ?>" hidden disabled <?= $idAlunoFiltro === (int) $aluno['id_aluno'] ? 'selected' : '' ?>><?= e($aluno['nome']) ?> - <?= e($aluno['matricula']) ?></option>
          <?php endforeach; ?>
        </select>
      </label>
      <div class="actions">
        <button class="btn primary" type="submit">Buscar pagamentos</button>
        <a class="btn ghost" href="pagamentos.php">Limpar filtro</a>
      </div>
    </form>
  </div>
</section>

<section class="panel">
  <div class="panel-header"><h2>Pagamentos do aluno</h2></div>
  <div class="panel-body">
    <?php if (!$alunoSelecionado): ?>
      <div class="alert warning">
        <span class="alert-marker"></span>
        <div><strong>Selecione um aluno</strong><span class="muted">Escolha uma turma e um aluno para visualizar os pagamentos.</span></div>
      </div>
    <?php endif; ?>
  </div>
  <?php if ($alunoSelecionado): ?>
    <div class="table-wrap">
      <table id="tabela-principal">
        <thead><tr><th>Aluno</th><th>Valor</th><th>Vencimento</th><th>Status</th><th>Acoes</th></tr></thead>
        <tbody>
          <?php if (empty($pagamentos)): ?><tr><td colspan="5">Nenhum pagamento encontrado para este aluno.</td></tr><?php endif; ?>
          <?php foreach ($pagamentos as $pagamento): ?>
            <tr>
              <td><?= e($alunoSelecionado['nome']) ?></td><td><?= e(dinheiro($pagamento['valor'])) ?></td><td><?= e(dataBr($pagamento['vencimento'])) ?></td><td><?= badge($pagamento['status']) ?></td>
              <td class="table-actions"><a class="btn ghost" href="novo_pagamento.php?editar=<?= e($pagamento['id_pagamento']) ?>">Editar</a></td>
            </tr>
          <?php endforeach; ?>
        </tbody>
      </table>
    </div>
  <?php endif; ?>
</section>

<?php require __DIR__ . '/../includes/footer.php'; ?>
