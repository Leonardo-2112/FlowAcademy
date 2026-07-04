<?php

require_once __DIR__ . '/../includes/funcoes.php';
exigirPerfil(['admin', 'coordenacao', 'administrativo'], '../');

$base = '../';
$titulo = 'Alunos';
$ativo = 'alunos';

$alunos = buscarTodos(
    'SELECT a.*, u.nome, u.email, u.status AS status_usuario
     FROM alunos a
     JOIN usuarios u ON u.id_usuario = a.id_usuario
     ORDER BY u.nome'
);

require __DIR__ . '/../includes/header.php';
?>

<section class="page-heading">
  <div><span class="eyebrow">Alunos</span><h1>Alunos</h1><p>Consulta de alunos cadastrados.</p></div>
  <div class="actions"><a class="btn primary" href="cadastro_aluno.php">Novo aluno</a></div>
</section>

<section class="panel">
  <div class="panel-header"><h2>Lista de alunos</h2></div>
  <div class="table-wrap">
    <table id="tabela-principal">
      <thead><tr><th>Nome</th><th>Matricula</th><th>CPF</th><th>Telefone</th><th>Status</th><th>Acoes</th></tr></thead>
      <tbody>
        <?php if (empty($alunos)): ?><tr><td colspan="6">Nenhum aluno cadastrado.</td></tr><?php endif; ?>
        <?php foreach ($alunos as $aluno): ?>
          <tr>
            <td><strong><?= e($aluno['nome']) ?></strong><br><span class="muted"><?= e($aluno['email']) ?></span></td>
            <td><?= e($aluno['matricula']) ?></td>
            <td><?= e($aluno['cpf']) ?></td>
            <td><?= e($aluno['telefone']) ?></td>
            <td><?= badge($aluno['status_academico']) ?></td>
            <td class="table-actions">
              <a class="btn ghost" href="ver_aluno.php?id=<?= e($aluno['id_aluno']) ?>">Ver</a>
              <a class="btn ghost" href="cadastro_aluno.php?editar=<?= e($aluno['id_aluno']) ?>">Editar</a>
            </td>
          </tr>
        <?php endforeach; ?>
      </tbody>
    </table>
  </div>
</section>

<?php require __DIR__ . '/../includes/footer.php'; ?>
