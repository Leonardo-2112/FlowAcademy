<?php

require_once __DIR__ . '/../includes/funcoes.php';
exigirPerfil(['admin', 'coordenacao'], '../');

$base = '../';
$titulo = 'Cursos';
$ativo = 'cursos';
$erro = '';

if ($_SERVER['REQUEST_METHOD'] === 'POST' && post('acao') === 'excluir') {
    $idCurso = (int) post('id_curso');
    $turmasVinculadas = contar('SELECT COUNT(*) AS total FROM turmas WHERE id_curso = :id', [':id' => $idCurso]);

    if ($turmasVinculadas > 0) {
        $erro = 'Nao e possivel excluir este curso porque existem turmas vinculadas a ele.';
    } else {
        try {
            executar('DELETE FROM cursos WHERE id_curso = :id_curso', [':id_curso' => $idCurso]);
            registrarLog('Excluiu curso');
            flash('success', 'Curso excluido com sucesso.');
            redirecionar('cursos.php');
        } catch (PDOException $erroSql) {
            $erro = $erroSql->getCode() === '23000'
                ? 'Nao e possivel excluir este curso porque existem registros vinculados a ele.'
                : 'Nao foi possivel excluir o curso. Tente novamente.';
        }
    }
}

$cursos = buscarTodos(
    'SELECT c.*, COUNT(d.id_disciplina) AS total_ucs, COALESCE(SUM(d.carga_horaria), 0) AS carga_ucs
     FROM cursos c
     LEFT JOIN disciplinas d ON d.id_curso = c.id_curso
     GROUP BY c.id_curso
     ORDER BY c.nome'
);

require __DIR__ . '/../includes/header.php';
?>

<section class="page-heading">
  <div><span class="eyebrow">Cursos</span><h1>Cursos</h1><p>Lista de cursos cadastrados no banco.</p></div>
  <div class="actions"><a class="btn primary" href="novo_curso.php">Novo curso</a></div>
</section>
<?php if ($erro): ?><div class="alert danger"><span class="alert-marker"></span><div><strong>Erro</strong><span class="muted"><?= e($erro) ?></span></div></div><?php endif; ?>

<section class="panel">
  <div class="panel-header"><h2>Cursos cadastrados</h2></div>
  <div class="table-wrap">
    <table id="tabela-principal">
      <thead><tr><th>Curso</th><th>Carga do curso</th><th>UCs</th><th>Horas nas UCs</th><th>Status</th><th>Descricao</th><th>Acoes</th></tr></thead>
      <tbody>
        <?php if (empty($cursos)): ?><tr><td colspan="7">Nenhum curso cadastrado.</td></tr><?php endif; ?>
        <?php foreach ($cursos as $curso): ?>
          <tr>
            <td><?= e($curso['nome']) ?></td>
            <td><?= e($curso['carga_horaria']) ?>h</td>
            <td><?= e($curso['total_ucs']) ?></td>
            <td><?= e($curso['carga_ucs']) ?>h</td>
            <td><?= badge($curso['status']) ?></td>
            <td><?= e($curso['descricao']) ?></td>
            <td class="table-actions">
              <a class="btn ghost" href="novo_curso.php?editar=<?= e($curso['id_curso']) ?>">Editar</a>
              <form method="post" onsubmit="return confirm('Excluir curso?')"><input type="hidden" name="acao" value="excluir"><input type="hidden" name="id_curso" value="<?= e($curso['id_curso']) ?>"><button class="btn danger" type="submit">Excluir</button></form>
            </td>
          </tr>
        <?php endforeach; ?>
      </tbody>
    </table>
  </div>
</section>

<?php require __DIR__ . '/../includes/footer.php'; ?>
