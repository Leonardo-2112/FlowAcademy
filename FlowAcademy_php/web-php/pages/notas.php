<?php

require_once __DIR__ . '/../includes/funcoes.php';
exigirPerfil(['admin', 'coordenacao', 'professor'], '../');

$base = '../';
$titulo = 'Lancar Notas';
$ativo = 'notas';
$erro = '';

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    try {
        $p1 = (float) str_replace(',', '.', post('prova_1'));
        $p2 = (float) str_replace(',', '.', post('prova_2'));
        $trabalho = (float) str_replace(',', '.', post('trabalho'));
        $comportamental = (float) str_replace(',', '.', post('comportamental'));
        $media = round(($p1 + $p2 + $trabalho + $comportamental) / 4, 2);
        $status = $media >= 6 ? 'aprovado' : 'reprovado';

        executar(
            'INSERT INTO notas (id_matricula, id_disciplina, prova_1, prova_2, trabalho, comportamental, media_uc, status)
             VALUES (:id_matricula, :id_disciplina, :prova_1, :prova_2, :trabalho, :comportamental, :media_uc, :status)
             ON DUPLICATE KEY UPDATE
                prova_1 = VALUES(prova_1),
                prova_2 = VALUES(prova_2),
                trabalho = VALUES(trabalho),
                comportamental = VALUES(comportamental),
                media_uc = VALUES(media_uc),
                status = VALUES(status),
                data_lancamento = NOW()',
            [
                ':id_matricula' => (int) post('id_matricula'),
                ':id_disciplina' => (int) post('id_disciplina'),
                ':prova_1' => $p1,
                ':prova_2' => $p2,
                ':trabalho' => $trabalho,
                ':comportamental' => $comportamental,
                ':media_uc' => $media,
                ':status' => $status,
            ]
        );
        registrarLog('Lancou nota');
        flash('success', 'Nota salva com sucesso.');
        redirecionar('notas.php');
    } catch (Throwable $erroSql) {
        $erro = $erroSql->getMessage();
    }
}

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

$filtroUc = getValor('uc');
$parametrosNotas = [];
$whereUc = '';
if ($filtroUc !== '') {
    $whereUc = 'WHERE n.id_disciplina = :id_disciplina';
    $parametrosNotas[':id_disciplina'] = (int) $filtroUc;
}

$notas = buscarTodos(
    'SELECT n.*, u.nome AS aluno, a.matricula, t.codigo_turma, d.nome AS disciplina
     FROM notas n
     JOIN matriculas m ON m.id_matricula = n.id_matricula
     JOIN alunos a ON a.id_aluno = m.id_aluno
     JOIN usuarios u ON u.id_usuario = a.id_usuario
     JOIN turmas t ON t.id_turma = m.id_turma
     JOIN disciplinas d ON d.id_disciplina = n.id_disciplina
     ' . $whereUc . '
     ORDER BY n.data_lancamento DESC',
    $parametrosNotas
);

require __DIR__ . '/../includes/header.php';
?>

<section class="page-heading"><div><span class="eyebrow">Avaliacao</span><h1>Lancar Notas</h1><p>A media e calculada no sistema: provas 60%, trabalho 30% e comportamental 10%.</p></div></section>
<?php if ($erro): ?><div class="alert danger"><span class="alert-marker"></span><div><strong>Erro</strong><span class="muted"><?= e($erro) ?></span></div></div><?php endif; ?>

<section class="panel">
  <div class="panel-header"><h2>Novo lancamento</h2></div>
  <div class="panel-body">
    <form class="form-grid" method="post">
      <label class="field span-2">
        <span>Turma</span>
        <select class="select" id="notas-turma" data-turma-filter data-filter-students="#notas-aluno" data-filter-ucs="#notas-uc" required>
          <option value="">Selecione a turma</option>
          <?php foreach ($turmas as $turma): ?>
            <option value="<?= e($turma['id_turma']) ?>"><?= e($turma['codigo_turma']) ?> - <?= e($turma['curso']) ?></option>
          <?php endforeach; ?>
        </select>
      </label>

      <label class="field span-2">
        <span>Aluno matriculado</span>
        <select class="select" id="notas-aluno" name="id_matricula" required>
          <option value="">Selecione a turma primeiro</option>
          <?php foreach ($matriculas as $matricula): ?>
            <option value="<?= e($matricula['id_matricula']) ?>" data-turma="<?= e($matricula['id_turma']) ?>" hidden disabled><?= e($matricula['aluno']) ?> - <?= e($matricula['matricula']) ?></option>
          <?php endforeach; ?>
        </select>
      </label>

      <label class="field span-2">
        <span>Unidade curricular</span>
        <select class="select" id="notas-uc" name="id_disciplina" required>
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

      <label class="field"><span>Prova 1</span><input class="control" name="prova_1" type="number" step="0.01" min="0" max="10" required></label>
      <label class="field"><span>Prova 2</span><input class="control" name="prova_2" type="number" step="0.01" min="0" max="10" required></label>
      <label class="field"><span>Trabalho</span><input class="control" name="trabalho" type="number" step="0.01" min="0" max="10" required></label>
      <label class="field"><span>Comportamental</span><input class="control" name="comportamental" type="number" step="0.01" min="0" max="10" required></label>

      <div class="actions span-2"><button class="btn primary" type="submit">Salvar notas</button></div>
    </form>
  </div>
</section>

<section class="panel">
  <div class="panel-header"><h2>Notas lancadas</h2></div>
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
      <thead><tr><th>Aluno</th><th>Turma</th><th>P1</th><th>P2</th><th>Trabalho</th><th>Comp.</th><th>Media</th><th>Status</th></tr></thead>
      <tbody>
        <?php if (empty($notas)): ?><tr><td colspan="8">Nenhuma nota encontrada.</td></tr><?php endif; ?>
        <?php foreach ($notas as $nota): ?>
          <tr>
            <td><strong><?= e($nota['aluno']) ?></strong><br><span class="muted"><?= e($nota['matricula']) ?></span></td>
            <td><?= e($nota['codigo_turma']) ?></td>
            <td><?= e($nota['prova_1']) ?></td><td><?= e($nota['prova_2']) ?></td><td><?= e($nota['trabalho']) ?></td><td><?= e($nota['comportamental']) ?></td><td><?= e($nota['media_uc']) ?></td><td><?= badge($nota['status']) ?></td>
          </tr>
        <?php endforeach; ?>
      </tbody>
    </table>
  </div>
</section>

<?php require __DIR__ . '/../includes/footer.php'; ?>
