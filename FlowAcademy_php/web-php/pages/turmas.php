<?php

require_once __DIR__ . '/../includes/funcoes.php';
exigirPerfil(['admin', 'coordenacao'], '../');

$base = '../';
$titulo = 'Turmas';
$ativo = 'turmas';

$turmas = buscarTodos(
    'SELECT t.*, c.nome AS curso, u.nome AS professor,
            COUNT(m.id_matricula) AS alunos_matriculados
     FROM turmas t
     JOIN cursos c ON c.id_curso = t.id_curso
     JOIN professores p ON p.id_professor = t.id_professor
     JOIN usuarios u ON u.id_usuario = p.id_usuario
     LEFT JOIN matriculas m ON m.id_turma = t.id_turma AND m.status = "ativa"
     GROUP BY t.id_turma
     ORDER BY t.codigo_turma'
);

require __DIR__ . '/../includes/header.php';
?>

<section class="page-heading">
  <div><span class="eyebrow">Turmas</span><h1>Turmas</h1><p>Controle de turmas, professores e capacidade.</p></div>
  <div class="actions"><a class="btn primary" href="nova_turma.php">Nova turma</a></div>
</section>

<section class="panel">
  <div class="panel-header"><h2>Turmas cadastradas</h2></div>
  <div class="table-wrap">
    <table id="tabela-principal">
      <thead><tr><th>Turma</th><th>Curso</th><th>Professor</th><th>Turno</th><th>Periodo</th><th>Vagas</th><th>Status</th><th>Acoes</th></tr></thead>
      <tbody>
        <?php if (empty($turmas)): ?><tr><td colspan="8">Nenhuma turma cadastrada.</td></tr><?php endif; ?>
        <?php foreach ($turmas as $turma): ?>
          <tr>
            <td><?= e($turma['codigo_turma']) ?></td><td><?= e($turma['curso']) ?></td><td><?= e($turma['professor']) ?></td>
            <td><?= e(textoStatus($turma['turno'])) ?></td><td><?= e($turma['periodo_letivo']) ?></td>
            <td><?= e($turma['alunos_matriculados']) ?>/<?= e($turma['capacidade_maxima']) ?></td><td><?= badge($turma['status']) ?></td>
            <td><a class="btn ghost" href="nova_turma.php?editar=<?= e($turma['id_turma']) ?>">Editar</a></td>
          </tr>
        <?php endforeach; ?>
      </tbody>
    </table>
  </div>
</section>

<?php require __DIR__ . '/../includes/footer.php'; ?>
