<?php

require_once __DIR__ . '/../includes/funcoes.php';
exigirPerfil(['admin', 'coordenacao', 'professor', 'aluno'], '../');

$base = '../';
$ativo = 'frequencias';
$erro = '';
$usuario = usuarioLogado();
$souAluno = $usuario['perfil'] === 'aluno';
$podeEditar = !$souAluno;
$titulo = $souAluno ? 'Frequencia' : 'Registrar Frequencia';

if ($_SERVER['REQUEST_METHOD'] === 'POST' && $podeEditar) {
    try {
        executar(
            'INSERT INTO frequencia (id_matricula, id_disciplina, total_aulas, presencas)
             VALUES (:id_matricula, :id_disciplina, :total_aulas, :presencas)
             ON DUPLICATE KEY UPDATE
                total_aulas = VALUES(total_aulas),
                presencas = VALUES(presencas)',
            [
                ':id_matricula' => (int) post('id_matricula'),
                ':id_disciplina' => (int) post('id_disciplina'),
                ':total_aulas' => (int) post('total_aulas'),
                ':presencas' => (int) post('presencas'),
            ]
        );
        registrarLog('Registrou frequencia');
        flash('success', 'Frequencia salva com sucesso.');
        redirecionar('frequencias.php');
    } catch (Throwable $erroSql) {
        $erro = $erroSql->getMessage();
    }
}

$temMatricula = true;
$parametros = [];
$whereAluno = '';

if ($souAluno) {
    $aluno = buscarUm('SELECT id_aluno FROM alunos WHERE id_usuario = :id', [':id' => $usuario['id_usuario']]);
    $temMatricula = (bool) buscarUm('SELECT id_matricula FROM matriculas WHERE id_aluno = :id AND status = "ativa"', [':id' => $aluno['id_aluno'] ?? 0]);
    $whereAluno = 'WHERE a.id_usuario = :id_usuario';
    $parametros[':id_usuario'] = $usuario['id_usuario'];
}

if ($podeEditar) {
    $turmas = buscarTodos(
        'SELECT t.id_turma, t.id_curso, t.codigo_turma, c.nome AS curso
         FROM turmas t JOIN cursos c ON c.id_curso = t.id_curso
         WHERE t.status = "ativa"
         ORDER BY t.codigo_turma'
    );
    $matriculas = buscarTodos(
        'SELECT m.id_matricula, m.id_turma, u.nome AS aluno, a.matricula
         FROM matriculas m
         JOIN alunos a ON a.id_aluno = m.id_aluno
         JOIN usuarios u ON u.id_usuario = a.id_usuario
         WHERE m.status = "ativa"
         ORDER BY u.nome'
    );
    $disciplinasPorCurso = buscarTodos('SELECT id_disciplina, id_curso, nome FROM disciplinas ORDER BY nome');
    $disciplinasFiltro = buscarTodos('SELECT id_disciplina, nome FROM disciplinas ORDER BY nome');
}

$filtroUc = getValor('uc');
if ($filtroUc !== '') {
    $whereAluno = ($whereAluno === '' ? 'WHERE' : $whereAluno . ' AND') . ' f.id_disciplina = :id_disciplina';
    $parametros[':id_disciplina'] = (int) $filtroUc;
}

$frequencias = buscarTodos(
    'SELECT f.*, u.nome AS aluno, a.matricula, t.codigo_turma, d.nome AS disciplina
     FROM frequencia f
     JOIN matriculas m ON m.id_matricula = f.id_matricula
     JOIN alunos a ON a.id_aluno = m.id_aluno
     JOIN usuarios u ON u.id_usuario = a.id_usuario
     JOIN turmas t ON t.id_turma = m.id_turma
     JOIN disciplinas d ON d.id_disciplina = f.id_disciplina
     ' . $whereAluno . '
     ORDER BY u.nome, d.nome',
    $parametros
);

require __DIR__ . '/../includes/header.php';
?>

<?php if ($souAluno): ?>
  <section class="page-heading"><div><span class="eyebrow">Presenca</span><h1>Frequencia</h1><p>Acompanhamento por unidade curricular.</p></div></section>

  <section class="panel">
    <div class="panel-header"><h2>Resumo de frequencia</h2></div>
    <div class="panel-body">
      <?php if (!$temMatricula): ?>
        <div class="alert warning">
          <span class="alert-marker"></span>
          <div><strong>Sem matricula</strong><span class="muted">Nenhuma matricula foi encontrada para este aluno.</span></div>
        </div>
      <?php endif; ?>
    </div>
    <div class="table-wrap">
      <table id="tabela-principal">
        <thead><tr><th>UC</th><th>Total de aulas</th><th>Presencas</th><th>Percentual</th><th>Situacao</th></tr></thead>
        <tbody>
          <?php if (empty($frequencias)): ?><tr><td colspan="5">Nenhum registro de frequencia encontrado.</td></tr><?php endif; ?>
          <?php foreach ($frequencias as $frequencia): ?>
            <tr>
              <td><?= e($frequencia['disciplina']) ?></td><td><?= e($frequencia['total_aulas']) ?></td><td><?= e($frequencia['presencas']) ?></td>
              <td><?= e(number_format((float) $frequencia['percentual'], 0)) ?>%</td>
              <td><?= badge((float) $frequencia['percentual'] >= 75 ? 'regular' : 'atrasado') ?></td>
            </tr>
          <?php endforeach; ?>
        </tbody>
      </table>
    </div>
  </section>

<?php else: ?>
  <section class="page-heading"><div><span class="eyebrow">Chamada</span><h1>Registrar Frequencia</h1><p>Atualize total de aulas e presencas com validacao simples.</p></div></section>
  <?php if ($erro): ?><div class="alert danger"><span class="alert-marker"></span><div><strong>Erro</strong><span class="muted"><?= e($erro) ?></span></div></div><?php endif; ?>

  <section class="panel">
    <div class="panel-header"><h2>Novo registro</h2></div>
    <div class="panel-body">
      <form class="form-grid" method="post">
        <label class="field span-2">
          <span>Turma</span>
          <select class="select" id="freq-turma" data-turma-filter data-filter-students="#freq-aluno" data-filter-ucs="#freq-uc" required>
            <option value="">Selecione a turma</option>
            <?php foreach ($turmas as $turma): ?>
              <option value="<?= e($turma['id_turma']) ?>"><?= e($turma['codigo_turma']) ?> - <?= e($turma['curso']) ?></option>
            <?php endforeach; ?>
          </select>
        </label>

        <label class="field span-2">
          <span>Aluno matriculado</span>
          <select class="select" id="freq-aluno" name="id_matricula" required>
            <option value="">Selecione a turma primeiro</option>
            <?php foreach ($matriculas as $matricula): ?>
              <option value="<?= e($matricula['id_matricula']) ?>" data-turma="<?= e($matricula['id_turma']) ?>" hidden disabled><?= e($matricula['aluno']) ?> - <?= e($matricula['matricula']) ?></option>
            <?php endforeach; ?>
          </select>
        </label>

        <label class="field span-2">
          <span>Unidade curricular</span>
          <select class="select" id="freq-uc" name="id_disciplina" required>
            <option value="">Selecione a turma primeiro</option>
            <?php foreach ($turmas as $turma): ?>
              <?php foreach ($disciplinasPorCurso as $disciplina): ?>
                <?php if ((int) $disciplina['id_curso'] === (int) $turma['id_curso']): ?>
                  <option value="<?= e($disciplina['id_disciplina']) ?>" data-turma="<?= e($turma['id_turma']) ?>" hidden disabled><?= e($disciplina['nome']) ?></option>
                <?php endif; ?>
              <?php endforeach; ?>
            <?php endforeach; ?>
          </select>
        </label>

        <label class="field"><span>Total de aulas</span><input class="control" type="number" name="total_aulas" min="1" required></label>
        <label class="field"><span>Presencas</span><input class="control" type="number" name="presencas" min="0" required></label>

        <div class="actions span-2"><button class="btn primary" type="submit">Salvar frequencia</button></div>
      </form>
    </div>
  </section>

  <section class="panel">
    <div class="panel-header"><h2>Frequencias por UC</h2></div>
    <div class="panel-body">
      <form class="form-grid" method="get">
        <label class="field span-2">
          <span>Filtrar por UC</span>
          <select class="select" name="uc" onchange="this.form.submit()">
            <option value="">Todas as unidades curriculares</option>
            <?php foreach ($disciplinasFiltro as $disciplina): ?>
              <option value="<?= e($disciplina['id_disciplina']) ?>" <?= $filtroUc === (string) $disciplina['id_disciplina'] ? 'selected' : '' ?>><?= e($disciplina['nome']) ?></option>
            <?php endforeach; ?>
          </select>
        </label>
      </form>
    </div>
    <div class="table-wrap">
      <table id="tabela-principal">
        <thead><tr><th>Aluno</th><th>Turma</th><th>Aulas</th><th>Presencas</th><th>Percentual</th><th>Situacao</th></tr></thead>
        <tbody>
          <?php if (empty($frequencias)): ?><tr><td colspan="6">Nenhum registro de frequencia encontrado.</td></tr><?php endif; ?>
          <?php foreach ($frequencias as $frequencia): ?>
            <tr>
              <td><strong><?= e($frequencia['aluno']) ?></strong><br><span class="muted"><?= e($frequencia['matricula']) ?></span></td>
              <td><?= e($frequencia['codigo_turma']) ?></td>
              <td><?= e($frequencia['total_aulas']) ?></td><td><?= e($frequencia['presencas']) ?></td>
              <td><?= e(number_format((float) $frequencia['percentual'], 0)) ?>%</td>
              <td><?= badge((float) $frequencia['percentual'] >= 75 ? 'regular' : 'atrasado') ?></td>
            </tr>
          <?php endforeach; ?>
        </tbody>
      </table>
    </div>
  </section>
<?php endif; ?>

<?php require __DIR__ . '/../includes/footer.php'; ?>
