<?php

require_once __DIR__ . '/../includes/funcoes.php';
exigirPerfil(['admin', 'coordenacao', 'administrativo'], '../');

$base = '../';
$titulo = 'Nova Matricula';
$ativo = 'matriculas';
$erro = '';

if ($_SERVER['REQUEST_METHOD'] === 'POST' && post('acao') === 'salvar') {
    try {
        $idTurma = (int) post('id_turma');
        $idAluno = (int) post('id_aluno');

        $turma = buscarUm(
            'SELECT t.capacidade_maxima, COUNT(m.id_matricula) AS total
             FROM turmas t
             LEFT JOIN matriculas m ON m.id_turma = t.id_turma AND m.status = "ativa"
             WHERE t.id_turma = :id_turma
             GROUP BY t.id_turma',
            [':id_turma' => $idTurma]
        );

        if ($turma && (int) $turma['total'] >= (int) $turma['capacidade_maxima']) {
            $erro = 'A turma selecionada ja esta lotada.';
        } else {
            executar(
                'INSERT INTO matriculas (id_aluno, id_turma, data_matricula, status)
                 VALUES (:id_aluno, :id_turma, :data_matricula, :status)',
                [
                    ':id_aluno' => $idAluno,
                    ':id_turma' => $idTurma,
                    ':data_matricula' => date('Y-m-d'),
                    ':status' => 'ativa',
                ]
            );
            registrarLog('Cadastrou matricula');
            flash('success', 'Matricula realizada com sucesso.');
            redirecionar('matriculas.php');
        }
    } catch (Throwable $erroSql) {
        $erro = $erroSql->getMessage();
    }
}

$alunos = buscarTodos(
    'SELECT a.id_aluno, a.matricula, u.nome
     FROM alunos a
     JOIN usuarios u ON u.id_usuario = a.id_usuario
     ORDER BY u.nome'
);
$turmas = buscarTodos(
    'SELECT t.id_turma, t.codigo_turma, c.nome AS curso
     FROM turmas t
     JOIN cursos c ON c.id_curso = t.id_curso
     WHERE t.status = "ativa"
     ORDER BY t.codigo_turma'
);

require __DIR__ . '/../includes/header.php';
?>

<section class="page-heading"><div><span class="eyebrow">Matricula</span><h1>Nova Matricula</h1><p>Matricula o aluno em uma turma ativa, respeitando duplicidade e capacidade.</p></div></section>
<?php if ($erro): ?><div class="alert danger"><span class="alert-marker"></span><div><strong>Erro</strong><span class="muted"><?= e($erro) ?></span></div></div><?php endif; ?>

<section class="panel">
  <div class="panel-header"><h2>Dados da matricula</h2></div>
  <div class="panel-body">
    <form class="form-grid" method="post">
      <input type="hidden" name="acao" value="salvar">
      <label class="field"><span>Aluno</span>
        <select class="select" name="id_aluno" required>
          <option value="">Selecione</option>
          <?php foreach ($alunos as $aluno): ?><option value="<?= e($aluno['id_aluno']) ?>"><?= e($aluno['nome']) ?> - <?= e($aluno['matricula']) ?></option><?php endforeach; ?>
        </select>
      </label>
      <label class="field"><span>Turma</span>
        <select class="select" name="id_turma" required>
          <option value="">Selecione</option>
          <?php foreach ($turmas as $turma): ?><option value="<?= e($turma['id_turma']) ?>"><?= e($turma['codigo_turma']) ?> - <?= e($turma['curso']) ?></option><?php endforeach; ?>
        </select>
      </label>
      <div class="actions span-2"><button class="btn primary" type="submit">Realizar matricula</button></div>
    </form>
  </div>
</section>

<?php require __DIR__ . '/../includes/footer.php'; ?>
