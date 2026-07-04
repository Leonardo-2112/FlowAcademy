<?php

require_once __DIR__ . '/../includes/funcoes.php';
exigirPerfil(['admin', 'coordenacao', 'administrativo'], '../');

$base = '../';
$titulo = 'Ficha do Aluno';
$ativo = 'alunos';

$idAluno = (int) getValor('id');
$aluno = buscarUm(
    'SELECT a.*, u.nome, u.email, u.status AS acesso
     FROM alunos a JOIN usuarios u ON u.id_usuario = a.id_usuario
     WHERE a.id_aluno = :id',
    [':id' => $idAluno]
);

if (!$aluno) {
    flash('danger', 'Aluno nao encontrado.');
    redirecionar('alunos.php');
}

$matriculas = buscarTodos(
    'SELECT m.*, t.codigo_turma, c.nome AS curso
     FROM matriculas m
     JOIN turmas t ON t.id_turma = m.id_turma
     JOIN cursos c ON c.id_curso = t.id_curso
     WHERE m.id_aluno = :id
     ORDER BY m.id_matricula DESC',
    [':id' => $idAluno]
);

$notas = buscarTodos(
    'SELECT d.nome AS disciplina, n.prova_1, n.prova_2, n.trabalho, n.comportamental, n.media_uc, n.status
     FROM notas n
     JOIN matriculas m ON m.id_matricula = n.id_matricula
     WHERE m.id_aluno = :id
     ORDER BY d.nome',
    [':id' => $idAluno]
);

$pagamentos = buscarTodos(
    'SELECT * FROM pagamentos WHERE id_aluno = :id ORDER BY vencimento DESC',
    [':id' => $idAluno]
);

require __DIR__ . '/../includes/header.php';
?>

<section class="page-heading"><div><span class="eyebrow">Alunos</span><h1><?= e($aluno['nome']) ?></h1><p>Ficha individual, historico de notas e financeiro do aluno.</p></div></section>

<div class="grid two">
  <section class="panel">
    <div class="panel-header"><h2>Dados pessoais</h2></div>
    <div class="panel-body info-list">
      <div><span>Nome</span><strong><?= e($aluno['nome']) ?></strong></div>
      <div><span>E-mail</span><strong><?= e($aluno['email']) ?></strong></div>
      <div><span>Matricula</span><strong><?= e($aluno['matricula']) ?></strong></div>
      <div><span>CPF</span><strong><?= e($aluno['cpf']) ?></strong></div>
      <div><span>Telefone</span><strong><?= e($aluno['telefone']) ?></strong></div>
      <div><span>Status</span><?= badge($aluno['status_academico']) ?></div>
    </div>
  </section>

  <section class="panel">
    <div class="panel-header"><h2>Matriculas</h2></div>
    <div class="table-wrap">
      <table><thead><tr><th>Turma</th><th>Curso</th><th>Status</th></tr></thead><tbody>
        <?php if (empty($matriculas)): ?><tr><td colspan="3">Nenhuma matricula encontrada.</td></tr><?php endif; ?>
        <?php foreach ($matriculas as $matricula): ?>
          <tr><td><?= e($matricula['codigo_turma']) ?></td><td><?= e($matricula['curso']) ?></td><td><?= badge($matricula['status']) ?></td></tr>
        <?php endforeach; ?>
      </tbody></table>
    </div>
  </section>
</div>

<section class="panel">
  <div class="panel-header"><h2>Boletim</h2></div>
  <div class="table-wrap">
    <table><thead><tr><th>UC</th><th>P1</th><th>P2</th><th>Trabalho</th><th>Comp.</th><th>Media</th><th>Status</th></tr></thead><tbody>
      <?php if (empty($notas)): ?><tr><td colspan="7">Nenhuma nota encontrada.</td></tr><?php endif; ?>
      <?php foreach ($notas as $nota): ?>
        <tr><td><?= e($nota['disciplina']) ?></td><td><?= e($nota['prova_1']) ?></td><td><?= e($nota['prova_2']) ?></td><td><?= e($nota['trabalho']) ?></td><td><?= e($nota['comportamental']) ?></td><td><?= e($nota['media_uc']) ?></td><td><?= badge($nota['status']) ?></td></tr>
      <?php endforeach; ?>
    </tbody></table>
  </div>
</section>

<section class="panel">
  <div class="panel-header"><h2>Pagamentos</h2></div>
  <div class="table-wrap">
    <table><thead><tr><th>Valor</th><th>Vencimento</th><th>Status</th></tr></thead><tbody>
      <?php if (empty($pagamentos)): ?><tr><td colspan="3">Nenhum pagamento encontrado.</td></tr><?php endif; ?>
      <?php foreach ($pagamentos as $pagamento): ?>
        <tr><td><?= e(dinheiro($pagamento['valor'])) ?></td><td><?= e(dataBr($pagamento['vencimento'])) ?></td><td><?= badge($pagamento['status']) ?></td></tr>
      <?php endforeach; ?>
    </tbody></table>
  </div>
</section>

<div class="actions"><a class="btn ghost" href="alunos.php">Voltar</a></div>

<?php require __DIR__ . '/../includes/footer.php'; ?>
