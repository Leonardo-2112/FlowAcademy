<?php

require_once __DIR__ . '/includes/funcoes.php';

if (estaLogado()) {
    redirecionar('dashboard.php');
}

$erro = '';
$email = '';

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $email = post('email');
    $senha = post('senha');

    $usuario = buscarUm(
        'SELECT * FROM usuarios WHERE email = :email AND status = "ativo" LIMIT 1',
        [':email' => $email]
    );

    if (!$usuario || !senhaCorreta($senha, $usuario['senha_hash'])) {
        $erro = 'E-mail ou senha incorretos.';
    } else {
        $_SESSION['usuario'] = [
            'id_usuario' => (int) $usuario['id_usuario'],
            'nome' => $usuario['nome'],
            'email' => $usuario['email'],
            'perfil' => $usuario['perfil'],
            'trocar_senha' => $usuario['ultimo_login'] === null,
        ];

        if ($usuario['ultimo_login'] === null) {
            registrarLog('Login realizado');
            redirecionar('alterar_senha.php');
        }

        executar('UPDATE usuarios SET ultimo_login = NOW() WHERE id_usuario = :id', [
            ':id' => $usuario['id_usuario'],
        ]);

        registrarLog('Login realizado');
        redirecionar('dashboard.php');
    }
}
?>
<!doctype html>
<html lang="pt-BR">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>Login | Flow Academy</title>
  <link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet">
  <link rel="stylesheet" href="assets/css/main.css">
</head>
<body class="auth-shell">
  <main class="auth-card">
    <a class="brand" href="index.php">
      <img class="brand-logo" src="assets/img/logos/logo-flow-academy-gold.jpg" alt="Logo Flow Academy">
    </a>
    <h1 class="auth-title">Acesso institucional</h1>
    <p class="auth-subtitle">Entre com um usuario cadastrado na tabela usuarios do banco flow_academy.</p>

    <?php if ($erro !== ''): ?>
      <div class="alert danger">
        <span class="alert-marker"></span>
        <div><strong>Erro de autenticacao</strong><span class="muted"><?= e($erro) ?></span></div>
      </div>
    <?php endif; ?>

    <form class="stack" method="post">
      <label class="field">
        <span>E-mail</span>
        <input class="control" type="email" name="email" placeholder="seu.email@flowacademy.com" value="<?= e($email) ?>" required>
      </label>
      <label class="field">
        <span>Senha</span>
        <span class="password-wrap">
          <input class="control" id="login-password" type="password" name="senha" placeholder="Digite sua senha" required>
          <button class="btn ghost password-action" type="button" data-password-toggle="#login-password">Mostrar</button>
        </span>
      </label>
      <button class="btn primary" type="submit">Entrar</button>
    </form>
  </main>

  <div class="toast" data-toast-root></div>
  <script src="assets/bootstrap/js/bootstrap.bundle.min.js"></script>
  <script src="assets/js/app.js"></script>
</body>
</html>
