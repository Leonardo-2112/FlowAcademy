<?php

require_once __DIR__ . '/../includes/funcoes.php';
exigirPerfil(['admin'], '../');

$base = '../';
$titulo = 'Logs';
$ativo = 'logs';

$logs = buscarTodos(
    'SELECT l.*, u.nome, u.email, u.perfil
     FROM logs l
     JOIN usuarios u ON u.id_usuario = l.id_usuario
     ORDER BY l.data_evento DESC
     LIMIT 200'
);

require __DIR__ . '/../includes/header.php';
?>

<section class="page-heading"><div><span class="eyebrow">Logs</span><h1>Logs</h1><p>Historico das principais acoes realizadas no sistema.</p></div></section>

<section class="panel">
  <div class="panel-header"><h2>Eventos registrados</h2></div>
  <div class="table-wrap">
    <table id="tabela-principal">
      <thead><tr><th>Data</th><th>Usuario</th><th>E-mail</th><th>Perfil</th><th>Acao</th><th>IP</th></tr></thead>
      <tbody>
        <?php if (empty($logs)): ?><tr><td colspan="6">Nenhum evento registrado.</td></tr><?php endif; ?>
        <?php foreach ($logs as $log): ?>
          <tr>
            <td><?= e(dataBr($log['data_evento'])) ?></td><td><strong><?= e($log['nome']) ?></strong></td><td><?= e($log['email']) ?></td>
            <td><?= e(textoStatus($log['perfil'])) ?></td><td><?= e($log['acao']) ?></td><td><?= e($log['ip']) ?></td>
          </tr>
        <?php endforeach; ?>
      </tbody>
    </table>
  </div>
</section>

<?php require __DIR__ . '/../includes/footer.php'; ?>
