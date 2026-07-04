<?php

require_once __DIR__ . '/../includes/funcoes.php';
exigirPerfil(['admin'], '../');

$base = '../';
$titulo = 'Usuarios';
$ativo = 'usuarios';
$erro = '';

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    try {
        if (post('acao') === 'salvar') {
            executar(
                'INSERT INTO usuarios (nome, email, senha_hash, perfil, status) VALUES (:nome, :email, :senha_hash, :perfil, :status)',
                [
                    ':nome' => post('nome'),
                    ':email' => post('email'),
                    ':senha_hash' => hashSenha(post('senha', '123456')),
                    ':perfil' => post('perfil'),
                    ':status' => post('status', 'ativo'),
                ]
            );
            registrarLog('Cadastrou usuario');
            flash('success', 'Usuario cadastrado com sucesso.');
            redirecionar('usuarios.php');
        }

        if (post('acao') === 'status') {
            executar(
                'UPDATE usuarios SET status = :status WHERE id_usuario = :id_usuario',
                [':status' => post('status'), ':id_usuario' => (int) post('id_usuario')]
            );
            registrarLog('Atualizou status de usuario');
            flash('success', 'Usuario atualizado com sucesso.');
            redirecionar('usuarios.php');
        }
    } catch (Throwable $erroSql) {
        $erro = $erroSql->getMessage();
    }
}

$usuarios = buscarTodos('SELECT * FROM usuarios ORDER BY nome');

require __DIR__ . '/../includes/header.php';
?>

<section class="page-heading"><div><span class="eyebrow">Admin</span><h1>Usuarios</h1><p>Cadastro simples de usuarios administrativos, coordenacao e admin.</p></div></section>
<?php if ($erro): ?><div class="alert danger"><span class="alert-marker"></span><div><strong>Erro</strong><span class="muted"><?= e($erro) ?></span></div></div><?php endif; ?>

<section class="panel">
  <div class="panel-header"><h2>Novo usuario</h2></div>
  <div class="panel-body">
    <form class="form-grid" method="post">
      <input type="hidden" name="acao" value="salvar">
      <label class="field"><span>Nome</span><input class="control" name="nome" required></label>
      <label class="field"><span>E-mail</span><input class="control" type="email" name="email" required></label>
      <label class="field"><span>Senha inicial</span><input class="control" name="senha" value="123456"></label>
      <label class="field"><span>Perfil</span><select class="select" name="perfil"><option value="admin">Admin</option><option value="coordenacao">Coordenacao</option><option value="administrativo">Administrativo</option></select></label>
      <label class="field"><span>Status</span><select class="select" name="status"><option value="ativo">Ativo</option><option value="inativo">Inativo</option></select></label>
      <div class="actions"><button class="btn primary" type="submit">Salvar</button></div>
    </form>
  </div>
</section>

<section class="panel">
  <div class="panel-header"><h2>Lista de usuarios</h2></div>
  <div class="table-wrap"><table id="tabela-principal"><thead><tr><th>Nome</th><th>E-mail</th><th>Perfil</th><th>Status</th><th>Ultimo login</th><th>Alterar</th></tr></thead><tbody>
    <?php foreach ($usuarios as $u): ?>
      <tr>
        <td><?= e($u['nome']) ?></td><td><?= e($u['email']) ?></td><td><?= e(textoStatus($u['perfil'])) ?></td><td><?= badge($u['status']) ?></td><td><?= e($u['ultimo_login'] ?? '-') ?></td>
        <td><form method="post" class="actions"><input type="hidden" name="acao" value="status"><input type="hidden" name="id_usuario" value="<?= e($u['id_usuario']) ?>"><select class="select" name="status"><option value="ativo">Ativo</option><option value="inativo">Inativo</option></select><button class="btn ghost" type="submit">Salvar</button></form></td>
      </tr>
    <?php endforeach; ?>
  </tbody></table></div>
</section>

<?php require __DIR__ . '/../includes/footer.php'; ?>
