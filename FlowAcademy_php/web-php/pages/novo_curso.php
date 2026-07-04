<?php

require_once __DIR__ . '/../includes/funcoes.php';
exigirPerfil(['admin', 'coordenacao'], '../');

$base = '../';
$titulo = 'Novo Curso';
$ativo = 'novo_curso';
$erro = '';
$editando = null;

if ($_SERVER['REQUEST_METHOD'] === 'POST' && post('acao') === 'salvar') {
    try {
        $idCurso = (int) post('id_curso');
        if ($idCurso > 0) {
            executar(
                'UPDATE cursos SET nome = :nome, descricao = :descricao, carga_horaria = :carga_horaria, status = :status WHERE id_curso = :id_curso',
                [':nome' => post('nome'), ':descricao' => post('descricao'), ':carga_horaria' => (int) post('carga_horaria'), ':status' => post('status'), ':id_curso' => $idCurso]
            );
            flash('success', 'Curso atualizado com sucesso.');
        } else {
            executar(
                'INSERT INTO cursos (nome, descricao, carga_horaria, status) VALUES (:nome, :descricao, :carga_horaria, :status)',
                [':nome' => post('nome'), ':descricao' => post('descricao'), ':carga_horaria' => (int) post('carga_horaria'), ':status' => post('status')]
            );
            $idCurso = $pdo->lastInsertId();
            $nomesUc = $_POST['uc_nome'] ?? [];
            $cargasUc = $_POST['uc_carga'] ?? [];
            foreach ($nomesUc as $i => $nomeUc) {
                $nomeUc = trim((string) $nomeUc);
                if ($nomeUc === '') { continue; }
                executar(
                    'INSERT INTO disciplinas (id_curso, nome, carga_horaria) VALUES (:id_curso, :nome, :carga_horaria)',
                    [':id_curso' => $idCurso, ':nome' => $nomeUc, ':carga_horaria' => (int) ($cargasUc[$i] ?? 0)]
                );
            }
            flash('success', 'Curso cadastrado com sucesso.');
        }
        registrarLog('Salvou curso');
        redirecionar('cursos.php');
    } catch (Throwable $erroSql) {
        $erro = $erroSql->getMessage();
    }
}

if (getValor('editar') !== '') {
    $editando = buscarUm('SELECT * FROM cursos WHERE id_curso = :id', [':id' => (int) getValor('editar')]);
}

$ucsExistentes = [];
if ($editando) {
    $ucsExistentes = buscarTodos('SELECT nome, carga_horaria FROM disciplinas WHERE id_curso = :id ORDER BY nome', [':id' => $editando['id_curso']]);
}

require __DIR__ . '/../includes/header.php';
?>

<section class="page-heading"><div><span class="eyebrow">Cadastro</span><h1><?= $editando ? 'Editar Curso' : 'Novo Curso' ?></h1><p>Cadastre o curso e informe a carga horaria de cada unidade curricular.</p></div></section>
<?php if ($erro): ?><div class="alert danger"><span class="alert-marker"></span><div><strong>Erro</strong><span class="muted"><?= e($erro) ?></span></div></div><?php endif; ?>

<section class="panel">
  <div class="panel-header"><h2>Dados do curso</h2></div>
  <div class="panel-body">
    <form class="form-grid" method="post">
      <input type="hidden" name="acao" value="salvar">
      <input type="hidden" name="id_curso" value="<?= e($editando['id_curso'] ?? '') ?>">

      <label class="field span-2"><span>Nome</span><input class="control" name="nome" value="<?= e($editando['nome'] ?? '') ?>" required></label>
      <label class="field"><span>Carga horaria</span><input class="control" type="number" name="carga_horaria" value="<?= e($editando['carga_horaria'] ?? '') ?>" required></label>
      <label class="field"><span>Status</span>
        <select class="select" name="status">
          <option value="ativo" <?= ($editando['status'] ?? 'ativo') === 'ativo' ? 'selected' : '' ?>>Ativo</option>
          <option value="inativo" <?= ($editando['status'] ?? '') === 'inativo' ? 'selected' : '' ?>>Inativo</option>
        </select>
      </label>
      <label class="field span-2"><span>Descricao</span><textarea class="textarea" name="descricao"><?= e($editando['descricao'] ?? '') ?></textarea></label>

      <?php if (!$editando): ?>
        <div class="span-2 stack uc-editor" data-uc-editor data-uc-template="#uc-template">
          <span class="field-label"><strong>Unidades curriculares</strong></span>
          <div data-uc-list>
            <div class="uc-row" data-uc-row>
              <label class="field"><span>Nome da UC</span><input class="control" name="uc_nome[]" placeholder="Ex.: Banco de dados"></label>
              <label class="field"><span>Carga horaria</span><input class="control" type="number" name="uc_carga[]" placeholder="Horas"></label>
              <button class="btn ghost" type="button" data-remove-uc>Remover</button>
            </div>
          </div>
          <button class="btn ghost" type="button" data-add-uc>Adicionar UC</button>
        </div>

        <template id="uc-template">
          <div class="uc-row" data-uc-row>
            <label class="field"><span>Nome da UC</span><input class="control" name="uc_nome[]" placeholder="Ex.: Banco de dados"></label>
            <label class="field"><span>Carga horaria</span><input class="control" type="number" name="uc_carga[]" placeholder="Horas"></label>
            <button class="btn ghost" type="button" data-remove-uc>Remover</button>
          </div>
        </template>
      <?php else: ?>
        <div class="span-2 panel">
          <div class="panel-header"><h2>Unidades curriculares cadastradas</h2></div>
          <div class="table-wrap">
            <table><thead><tr><th>UC</th><th>Carga</th></tr></thead><tbody>
              <?php foreach ($ucsExistentes as $uc): ?><tr><td><?= e($uc['nome']) ?></td><td><?= e($uc['carga_horaria']) ?>h</td></tr><?php endforeach; ?>
            </tbody></table>
          </div>
        </div>
      <?php endif; ?>

      <div class="actions span-2"><button class="btn primary" type="submit">Salvar curso</button><a class="btn ghost" href="cursos.php">Voltar</a></div>
    </form>
  </div>
</section>

<?php require __DIR__ . '/../includes/footer.php'; ?>
