<?php

require_once __DIR__ . '/../includes/funcoes.php';
exigirPerfil(['admin'], '../');

$base = '../';
$titulo = 'Professores';
$ativo = 'professores';
$erro = '';
$editando = null;

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    try {
        if (post('acao') === 'salvar') {
            $idProfessor = (int) post('id_professor');

            if ($idProfessor > 0) {
                $professorAtual = buscarUm('SELECT id_usuario FROM professores WHERE id_professor = :id', [':id' => $idProfessor]);
                executar(
                    'UPDATE usuarios SET nome = :nome, email = :email, status = :status WHERE id_usuario = :id_usuario',
                    [
                        ':nome' => post('nome'),
                        ':email' => post('email'),
                        ':status' => post('status', 'ativo'),
                        ':id_usuario' => $professorAtual['id_usuario'],
                    ]
                );
                executar(
                    'UPDATE professores SET cpf = :cpf, especialidade = :especialidade WHERE id_professor = :id_professor',
                    [
                        ':cpf' => post('cpf'),
                        ':especialidade' => post('especialidade'),
                        ':id_professor' => $idProfessor,
                    ]
                );
                flash('success', 'Professor atualizado com sucesso.');
            } else {
                executar(
                    'INSERT INTO usuarios (nome, email, senha_hash, perfil, status) VALUES (:nome, :email, :senha_hash, "professor", "ativo")',
                    [':nome' => post('nome'), ':email' => post('email'), ':senha_hash' => hashSenha(post('senha', '123456'))]
                );
                $idUsuario = $pdo->lastInsertId();
                executar(
                    'INSERT INTO professores (id_usuario, cpf, especialidade) VALUES (:id_usuario, :cpf, :especialidade)',
                    [':id_usuario' => $idUsuario, ':cpf' => post('cpf'), ':especialidade' => post('especialidade')]
                );
                flash('success', 'Professor cadastrado com sucesso.');
            }

            registrarLog('Salvou professor');
            redirecionar('professores.php');
        }

        if (post('acao') === 'excluir') {
            $professor = buscarUm('SELECT id_usuario FROM professores WHERE id_professor = :id', [':id' => (int) post('id_professor')]);
            if ($professor) {
                executar('DELETE FROM usuarios WHERE id_usuario = :id_usuario', [':id_usuario' => $professor['id_usuario']]);
                registrarLog('Excluiu professor');
                flash('success', 'Professor excluido com sucesso.');
            }
            redirecionar('professores.php');
        }
    } catch (Throwable $erroSql) {
        $erro = $erroSql->getMessage();
    }
}

if (getValor('editar') !== '') {
    $editando = buscarUm(
        'SELECT p.*, u.nome, u.email, u.status
         FROM professores p
         JOIN usuarios u ON u.id_usuario = p.id_usuario
         WHERE p.id_professor = :id',
        [':id' => (int) getValor('editar')]
    );
}

$professores = buscarTodos(
    'SELECT p.*, u.nome, u.email, u.status
     FROM professores p
     JOIN usuarios u ON u.id_usuario = p.id_usuario
     ORDER BY u.nome'
);

require __DIR__ . '/../includes/header.php';
?>

<section class="page-heading">
  <div><span class="eyebrow">Admin</span><h1>Professores</h1><p>Cadastro direto em usuarios e professores.</p></div>
</section>

<?php if ($erro): ?><div class="alert danger"><span class="alert-marker"></span><div><strong>Erro</strong><span class="muted"><?= e($erro) ?></span></div></div><?php endif; ?>

<section class="panel">
  <div class="panel-header"><h2><?= $editando ? 'Editar professor' : 'Cadastrar professor' ?></h2></div>
  <div class="panel-body">
    <form class="form-grid" method="post">
      <input type="hidden" name="acao" value="salvar">
      <input type="hidden" name="id_professor" value="<?= e($editando['id_professor'] ?? '') ?>">
      <label class="field"><span>Nome</span><input class="control" name="nome" value="<?= e($editando['nome'] ?? '') ?>" required></label>
      <label class="field"><span>E-mail</span><input class="control" type="email" name="email" value="<?= e($editando['email'] ?? '') ?>" required></label>
      <label class="field"><span>CPF</span><input class="control" name="cpf" value="<?= e($editando['cpf'] ?? '') ?>" required></label>
      <label class="field"><span>Especialidade</span><input class="control" name="especialidade" value="<?= e($editando['especialidade'] ?? '') ?>"></label>
      <label class="field"><span>Status</span><select class="select" name="status"><option value="ativo">Ativo</option><option value="inativo" <?= ($editando['status'] ?? '') === 'inativo' ? 'selected' : '' ?>>Inativo</option></select></label>
      <?php if (!$editando): ?><label class="field"><span>Senha inicial</span><input class="control" name="senha" value="123456"></label><?php endif; ?>
      <div class="actions span-2"><button class="btn primary" type="submit">Salvar</button><a class="btn ghost" href="professores.php">Limpar</a></div>
    </form>
  </div>
</section>

<section class="panel">
  <div class="panel-header"><h2>Lista de professores</h2></div>
  <div class="table-wrap">
    <table id="tabela-principal">
      <thead><tr><th>Nome</th><th>E-mail</th><th>CPF</th><th>Especialidade</th><th>Status</th><th>Acoes</th></tr></thead>
      <tbody>
        <?php foreach ($professores as $professor): ?>
          <tr>
            <td><?= e($professor['nome']) ?></td><td><?= e($professor['email']) ?></td><td><?= e($professor['cpf']) ?></td><td><?= e($professor['especialidade']) ?></td><td><?= badge($professor['status']) ?></td>
            <td class="table-actions">
              <a class="btn ghost" href="professores.php?editar=<?= e($professor['id_professor']) ?>">Editar</a>
              <form method="post" onsubmit="return confirm('Excluir este professor?')"><input type="hidden" name="acao" value="excluir"><input type="hidden" name="id_professor" value="<?= e($professor['id_professor']) ?>"><button class="btn danger" type="submit">Excluir</button></form>
            </td>
          </tr>
        <?php endforeach; ?>
      </tbody>
    </table>
  </div>
</section>

<?php require __DIR__ . '/../includes/footer.php'; ?>
