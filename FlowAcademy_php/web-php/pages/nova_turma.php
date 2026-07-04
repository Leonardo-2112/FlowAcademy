<?php

require_once __DIR__ . '/../includes/funcoes.php';
exigirPerfil(['admin', 'coordenacao'], '../');

$base = '../';
$titulo = 'Nova Turma';
$ativo = 'nova_turma';
$erro = '';
$editando = null;

if ($_SERVER['REQUEST_METHOD'] === 'POST' && post('acao') === 'salvar') {
    try {
        $idTurma = (int) post('id_turma');
        $sqlDados = [
            ':id_curso' => (int) post('id_curso'),
            ':id_professor' => (int) post('id_professor'),
            ':codigo_turma' => post('codigo_turma'),
            ':turno' => post('turno'),
            ':periodo_letivo' => post('periodo_letivo'),
            ':capacidade_maxima' => (int) post('capacidade_maxima'),
            ':status' => post('status'),
        ];

        if ($idTurma > 0) {
            executar(
                'UPDATE turmas
                 SET id_curso = :id_curso, id_professor = :id_professor, codigo_turma = :codigo_turma,
                     turno = :turno, periodo_letivo = :periodo_letivo, capacidade_maxima = :capacidade_maxima,
                     status = :status
                 WHERE id_turma = :id_turma',
                $sqlDados + [':id_turma' => $idTurma]
            );
            flash('success', 'Turma atualizada com sucesso.');
        } else {
            executar(
                'INSERT INTO turmas (id_curso, id_professor, codigo_turma, turno, periodo_letivo, capacidade_maxima, status)
                 VALUES (:id_curso, :id_professor, :codigo_turma, :turno, :periodo_letivo, :capacidade_maxima, :status)',
                $sqlDados
            );
            flash('success', 'Turma cadastrada com sucesso.');
        }
        registrarLog('Salvou turma');
        redirecionar('turmas.php');
    } catch (Throwable $erroSql) {
        $erro = $erroSql->getMessage();
    }
}

if (getValor('editar') !== '') {
    $editando = buscarUm('SELECT * FROM turmas WHERE id_turma = :id', [':id' => (int) getValor('editar')]);
}

$cursos = buscarTodos('SELECT id_curso, nome FROM cursos ORDER BY nome');
$professores = buscarTodos(
    'SELECT p.id_professor, u.nome
     FROM professores p
     JOIN usuarios u ON u.id_usuario = p.id_usuario
     ORDER BY u.nome'
);

require __DIR__ . '/../includes/header.php';
?>

<section class="page-heading"><div><span class="eyebrow">Cadastro</span><h1><?= $editando ? 'Editar Turma' : 'Nova Turma' ?></h1><p>Crie uma turma vinculando curso e professor.</p></div></section>
<?php if ($erro): ?><div class="alert danger"><span class="alert-marker"></span><div><strong>Erro</strong><span class="muted"><?= e($erro) ?></span></div></div><?php endif; ?>

<section class="panel">
  <div class="panel-header"><h2>Dados da turma</h2></div>
  <div class="panel-body">
    <form class="form-grid" method="post">
      <input type="hidden" name="acao" value="salvar"><input type="hidden" name="id_turma" value="<?= e($editando['id_turma'] ?? '') ?>">

      <label class="field"><span>Curso</span>
        <select class="select" name="id_curso" required>
          <option value="">Selecione</option>
          <?php foreach ($cursos as $curso): ?><option value="<?= e($curso['id_curso']) ?>" <?= (int) ($editando['id_curso'] ?? 0) === (int) $curso['id_curso'] ? 'selected' : '' ?>><?= e($curso['nome']) ?></option><?php endforeach; ?>
        </select>
      </label>
      <label class="field"><span>Professor</span>
        <select class="select" name="id_professor" required>
          <option value="">Selecione</option>
          <?php foreach ($professores as $professor): ?><option value="<?= e($professor['id_professor']) ?>" <?= (int) ($editando['id_professor'] ?? 0) === (int) $professor['id_professor'] ? 'selected' : '' ?>><?= e($professor['nome']) ?></option><?php endforeach; ?>
        </select>
      </label>
      <label class="field"><span>Codigo da turma</span><input class="control" name="codigo_turma" placeholder="TI-1A" value="<?= e($editando['codigo_turma'] ?? '') ?>" required></label>
      <label class="field"><span>Turno</span>
        <select class="select" name="turno">
          <?php foreach (['noite', 'manha', 'tarde'] as $turno): ?><option value="<?= e($turno) ?>" <?= ($editando['turno'] ?? 'noite') === $turno ? 'selected' : '' ?>><?= e(textoStatus($turno)) ?></option><?php endforeach; ?>
        </select>
      </label>
      <label class="field"><span>Periodo letivo</span><input class="control" name="periodo_letivo" placeholder="2026.1" value="<?= e($editando['periodo_letivo'] ?? '') ?>" required></label>
      <label class="field"><span>Capacidade</span><input class="control" type="number" name="capacidade_maxima" value="<?= e($editando['capacidade_maxima'] ?? 35) ?>"></label>
      <label class="field"><span>Status</span>
        <select class="select" name="status">
          <option value="ativa" <?= ($editando['status'] ?? 'ativa') === 'ativa' ? 'selected' : '' ?>>Ativa</option>
          <option value="encerrada" <?= ($editando['status'] ?? '') === 'encerrada' ? 'selected' : '' ?>>Encerrada</option>
        </select>
      </label>

      <div class="actions"><button class="btn primary" type="submit">Salvar turma</button><a class="btn ghost" href="turmas.php">Voltar</a></div>
    </form>
  </div>
</section>

<?php require __DIR__ . '/../includes/footer.php'; ?>
