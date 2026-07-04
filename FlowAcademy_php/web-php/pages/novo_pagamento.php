<?php

require_once __DIR__ . '/../includes/funcoes.php';
exigirPerfil(['admin', 'administrativo'], '../');

$base = '../';
$titulo = 'Novo Pagamento';
$ativo = 'novo_pagamento';
$erro = '';
$editando = null;

if ($_SERVER['REQUEST_METHOD'] === 'POST' && post('acao') === 'salvar') {
    try {
        $idPagamento = (int) post('id_pagamento');
        $dadosPagamento = [
            ':id_aluno' => (int) post('id_aluno'),
            ':valor' => (float) str_replace(',', '.', post('valor')),
            ':vencimento' => post('vencimento'),
            ':status' => post('status', 'pendente'),
        ];

        if ($idPagamento > 0) {
            executar(
                'UPDATE pagamentos SET id_aluno = :id_aluno, valor = :valor, vencimento = :vencimento, status = :status WHERE id_pagamento = :id_pagamento',
                $dadosPagamento + [':id_pagamento' => $idPagamento]
            );
            flash('success', 'Pagamento atualizado com sucesso.');
        } else {
            executar(
                'INSERT INTO pagamentos (id_aluno, valor, vencimento, status) VALUES (:id_aluno, :valor, :vencimento, :status)',
                $dadosPagamento
            );
            flash('success', 'Pagamento cadastrado com sucesso.');
        }
        registrarLog('Salvou pagamento');
        redirecionar('pagamentos.php');
    } catch (Throwable $erroSql) {
        $erro = $erroSql->getMessage();
    }
}

if (getValor('editar') !== '') {
    $editando = buscarUm('SELECT * FROM pagamentos WHERE id_pagamento = :id', [':id' => (int) getValor('editar')]);
}

$alunos = buscarTodos(
    'SELECT a.id_aluno, a.matricula, u.nome
     FROM alunos a
     JOIN usuarios u ON u.id_usuario = a.id_usuario
     ORDER BY u.nome'
);

require __DIR__ . '/../includes/header.php';
?>

<section class="page-heading"><div><span class="eyebrow">Administrativo</span><h1><?= $editando ? 'Editar Pagamento' : 'Novo Pagamento' ?></h1><p>Cadastre cobrancas vinculadas ao aluno.</p></div></section>
<?php if ($erro): ?><div class="alert danger"><span class="alert-marker"></span><div><strong>Erro</strong><span class="muted"><?= e($erro) ?></span></div></div><?php endif; ?>

<section class="panel">
  <div class="panel-header"><h2>Dados do pagamento</h2></div>
  <div class="panel-body">
    <form class="form-grid" method="post">
      <input type="hidden" name="acao" value="salvar">
      <input type="hidden" name="id_pagamento" value="<?= e($editando['id_pagamento'] ?? '') ?>">
      <label class="field span-2"><span>Aluno</span>
        <select class="select" name="id_aluno" required>
          <option value="">Selecione</option>
          <?php foreach ($alunos as $aluno): ?><option value="<?= e($aluno['id_aluno']) ?>" <?= (int) ($editando['id_aluno'] ?? 0) === (int) $aluno['id_aluno'] ? 'selected' : '' ?>><?= e($aluno['nome']) ?> - <?= e($aluno['matricula']) ?></option><?php endforeach; ?>
        </select>
      </label>
      <label class="field"><span>Valor</span><input class="control" name="valor" value="<?= e($editando['valor'] ?? '') ?>" required></label>
      <label class="field"><span>Vencimento</span><input class="control" type="date" name="vencimento" value="<?= e($editando['vencimento'] ?? '') ?>" required></label>
      <label class="field"><span>Status</span>
        <select class="select" name="status">
          <?php foreach (['pendente', 'pago', 'atrasado', 'cancelado'] as $status): ?>
            <option value="<?= e($status) ?>" <?= ($editando['status'] ?? 'pendente') === $status ? 'selected' : '' ?>><?= e(textoStatus($status)) ?></option>
          <?php endforeach; ?>
        </select>
      </label>
      <div class="actions span-2"><button class="btn primary" type="submit">Salvar pagamento</button><a class="btn ghost" href="pagamentos.php">Voltar</a></div>
    </form>
  </div>
</section>

<?php require __DIR__ . '/../includes/footer.php'; ?>
