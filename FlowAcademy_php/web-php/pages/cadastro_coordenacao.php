<?php

require_once __DIR__ . '/../includes/funcoes.php';
exigirPerfil(['admin'], '../');

$base = '../';
$titulo = 'Cadastro de Coordenacao';
$ativo = 'cadastro_coordenacao';
$erro = '';
$perfilAlvo = 'coordenacao';

if ($_SERVER['REQUEST_METHOD'] === 'POST' && post('acao') === 'salvar') {
    try {
        executar(
            'INSERT INTO usuarios (nome, email, senha_hash, perfil, status) VALUES (:nome, :email, :senha_hash, :perfil, :status)',
            [
                ':nome' => post('nome'),
                ':email' => post('email'),
                ':senha_hash' => hashSenha(post('senha', '123456')),
                ':perfil' => $perfilAlvo,
                ':status' => post('status', 'ativo'),
            ]
        );
        registrarLog('Cadastrou usuario de coordenacao');
        flash('success', 'Usuario cadastrado com sucesso.');
        redirecionar('cadastro_coordenacao.php');
    } catch (Throwable $erroSql) {
        $erro = $erroSql->getMessage();
    }
}

$usuarios = buscarTodos('SELECT * FROM usuarios WHERE perfil = :perfil ORDER BY nome', [':perfil' => $perfilAlvo]);

require __DIR__ . '/../includes/header.php';
?>

<section class="page-heading"><div><span class="eyebrow">Admin</span><h1>Cadastro de Coordenacao</h1><p>Cria o usuario de acesso para funcionarios da coordenacao.</p></div></section>
<?php if ($erro): ?><div class="alert danger"><span class="alert-marker"></span><div><strong>Erro</strong><span class="muted"><?= e($erro) ?></span></div></div><?php endif; ?>

<section class="panel">
  <div class="panel-header"><h2>Dados do usuario</h2></div>
  <div class="panel-body">
    <form class="form-grid" method="post">
      <input type="hidden" name="acao" value="salvar">
      <label class="field"><span>Nome completo</span><input class="control" name="nome" required></label>
      <label class="field"><span>E-mail</span><input class="control" type="email" name="email" required></label>
      <label class="field"><span>Senha inicial</span><input class="control" name="senha" value="123456"></label>
      <label class="field"><span>Status de acesso</span>
        <select class="select" name="status"><option value="ativo">Ativo</option><option value="inativo">Inativo</option></select>
      </label>
      <div class="actions span-2"><button class="btn primary" type="submit">Cadastrar usuario</button><a class="btn ghost" href="../dashboard.php">Voltar</a></div>
    </form>
  </div>
</section>

<section class="panel">
  <div class="panel-header"><h2>Funcionarios da coordenacao cadastrados</h2></div>
  <div class="table-wrap">
    <table id="tabela-principal">
      <thead><tr><th>Nome</th><th>E-mail</th><th>Status</th><th>Ultimo login</th><th>Cadastro</th></tr></thead>
      <tbody>
        <?php if (empty($usuarios)): ?><tr><td colspan="5">Nenhum funcionario cadastrado.</td></tr><?php endif; ?>
        <?php foreach ($usuarios as $u): ?>
          <tr>
            <td><?= e($u['nome']) ?></td><td><?= e($u['email']) ?></td><td><?= badge($u['status']) ?></td>
            <td><?= $u['ultimo_login'] ? e(dataBr($u['ultimo_login'])) : '-' ?></td>
            <td>-</td>
          </tr>
        <?php endforeach; ?>
      </tbody>
    </table>
  </div>
</section>

<?php require __DIR__ . '/../includes/footer.php'; ?>
