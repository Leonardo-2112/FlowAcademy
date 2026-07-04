<?php

require_once __DIR__ . '/funcoes.php';

$base = $base ?? '';
$titulo = $titulo ?? 'Flow Academy';
$ativo = $ativo ?? '';
$usuario = usuarioLogado();
$flash = pegarFlash();
?>
<!doctype html>
<html lang="pt-BR">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title><?= e($titulo) ?> | Flow Academy</title>
  <link href="<?= e($base) ?>assets/bootstrap/css/bootstrap.min.css" rel="stylesheet">
  <link rel="stylesheet" href="<?= e($base) ?>assets/css/main.css">
</head>
<body class="app-shell">
  <aside class="sidebar" aria-label="Navegacao principal">
    <div class="sidebar-header">
      <a class="brand" href="<?= e($base) ?>dashboard.php">
        <img class="brand-logo" src="<?= e($base) ?>assets/img/logos/logo-flow-academy-gold.jpg" alt="Logo Flow Academy">
      </a>
    </div>

    <div class="sidebar-content">
      <?php require __DIR__ . '/menu.php'; ?>
    </div>

    <div class="sidebar-footer">
      <a class="role-pill" href="<?= e($base) ?>logout.php">
        <span><span>Sessao ativa</span><strong><?= e($usuario['nome'] ?? 'Visitante') ?></strong></span>
        <span class="badge success">Online</span>
      </a>
    </div>
  </aside>

  <button class="sidebar-backdrop js-sidebar-close" aria-label="Fechar menu"></button>

  <div class="app-frame">
    <header class="topbar">
      <div class="topbar-left">
        <button class="icon-btn mobile-only js-sidebar-toggle" aria-label="Abrir menu">
          <span class="hamburger"><span></span><span></span><span></span></span>
        </button>
        <div class="top-path"><strong><?= e($titulo) ?></strong></div>
      </div>
      <div class="topbar-right">
        <input class="control top-search" type="search" placeholder="Buscar na tabela" data-table-filter="#tabela-principal">
        <span class="icon-btn" aria-hidden="true">
          <svg class="icon" viewBox="0 0 24 24"><path d="M6 8a6 6 0 0 1 12 0c0 4 1.5 5.5 2 6H4c.5-.5 2-2 2-6Z"/><path d="M9.5 17a2.5 2.5 0 0 0 5 0"/></svg>
        </span>
        <a class="avatar" href="<?= e($base) ?>logout.php" title="Sair"><?= e(iniciais($usuario['nome'] ?? 'FA')) ?></a>
      </div>
    </header>

    <main class="main-content">
      <?php if ($flash): ?>
        <div class="alert <?= e($flash['tipo']) ?>">
          <span class="alert-marker"></span>
          <div><strong>Aviso</strong><span class="muted"><?= e($flash['mensagem']) ?></span></div>
        </div>
      <?php endif; ?>
