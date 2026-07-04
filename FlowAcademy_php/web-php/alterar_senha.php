<?php

require_once __DIR__ . '/includes/funcoes.php';

exigirLogin('');

$erro = '';
$usuario = usuarioLogado();

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $senha = post('senha');
    $confirmacao = post('confirmacao');

    if (strlen($senha) < 4) {
        $erro = 'A senha deve ter pelo menos 4 caracteres.';
    } elseif ($senha !== $confirmacao) {
        $erro = 'A confirmacao da senha nao confere.';
    } else {
        executar(
            'UPDATE usuarios SET senha_hash = :senha_hash, ultimo_login = NOW() WHERE id_usuario = :id_usuario',
            [
                ':senha_hash' => hashSenha($senha),
                ':id_usuario' => $usuario['id_usuario'],
            ]
        );

        $_SESSION['usuario']['trocar_senha'] = false;
        registrarLog('Alterou senha');
        flash('success', 'Senha alterada com sucesso.');
        redirecionar('dashboard.php');
    }
}
?>
<!doctype html>
<html lang="pt-BR">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>Alterar senha | Flow Academy</title>
  <link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet">
  <link rel="stylesheet" href="assets/css/main.css">
</head>
<body class="auth-shell">
  <main class="auth-card">
    <a class="brand" href="dashboard.php">
      <img class="brand-logo" src="assets/img/logos/logo-flow-academy-gold.jpg" alt="Logo Flow Academy">
    </a>
    <h1 class="auth-title">Alterar senha</h1>
    <p class="auth-subtitle">No primeiro acesso, cadastre uma nova senha.</p>

    <?php if ($erro !== ''): ?>
      <div class="alert danger"><span class="alert-marker"></span><div><strong>Aviso</strong><span class="muted"><?= e($erro) ?></span></div></div>
    <?php endif; ?>

    <form class="stack" method="post">
      <label class="field">
        <span>Nova senha</span>
        <input class="control" type="password" name="senha" required>
      </label>
      <label class="field">
        <span>Confirmar senha</span>
        <input class="control" type="password" name="confirmacao" required>
      </label>
      <button class="btn primary" type="submit">Salvar senha</button>
    </form>
  </main>
</body>
</html>
