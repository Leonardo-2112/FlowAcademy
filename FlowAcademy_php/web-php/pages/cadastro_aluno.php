<?php

require_once __DIR__ . '/../includes/funcoes.php';
exigirPerfil(['admin', 'coordenacao', 'administrativo'], '../');

$base = '../';
$titulo = 'Cadastro de Aluno';
$ativo = 'cadastro_aluno';
$erro = '';
$editando = null;

if ($_SERVER['REQUEST_METHOD'] === 'POST' && post('acao') === 'salvar') {
    try {
        $idAluno = (int) post('id_aluno');
        $dados = [
            ':nome' => post('nome'),
            ':email' => post('email'),
            ':matricula' => post('matricula'),
            ':cpf' => post('cpf'),
            ':telefone' => post('telefone'),
            ':data_nascimento' => post('data_nascimento') ?: null,
            ':endereco' => post('endereco'),
            ':status_academico' => post('status_academico', 'regular'),
        ];

        if ($idAluno > 0) {
            $alunoAtual = buscarUm('SELECT id_usuario FROM alunos WHERE id_aluno = :id', [':id' => $idAluno]);
            executar(
                'UPDATE usuarios SET nome = :nome, email = :email, status = :acesso WHERE id_usuario = :id_usuario',
                [':nome' => $dados[':nome'], ':email' => $dados[':email'], ':acesso' => post('acesso', 'ativo'), ':id_usuario' => $alunoAtual['id_usuario']]
            );
            executar(
                'UPDATE alunos
                 SET matricula = :matricula, cpf = :cpf, telefone = :telefone, data_nascimento = :data_nascimento,
                     endereco = :endereco, status_academico = :status_academico
                 WHERE id_aluno = :id_aluno',
                [
                    ':matricula' => $dados[':matricula'],
                    ':cpf' => $dados[':cpf'],
                    ':telefone' => $dados[':telefone'],
                    ':data_nascimento' => $dados[':data_nascimento'],
                    ':endereco' => $dados[':endereco'],
                    ':status_academico' => $dados[':status_academico'],
                    ':id_aluno' => $idAluno,
                ]
            );
            flash('success', 'Aluno atualizado com sucesso.');
        } else {
            $matriculaGerada = $dados[':matricula'] !== '' ? $dados[':matricula'] : (date('Y') . '-' . str_pad((string) random_int(0, 9999), 4, '0', STR_PAD_LEFT));

            executar(
                'INSERT INTO usuarios (nome, email, senha_hash, perfil, status) VALUES (:nome, :email, :senha_hash, "aluno", :acesso)',
                [':nome' => $dados[':nome'], ':email' => $dados[':email'], ':senha_hash' => hashSenha(post('senha', '123456')), ':acesso' => post('acesso', 'ativo')]
            );
            $idUsuario = $pdo->lastInsertId();
            executar(
                'INSERT INTO alunos (id_usuario, matricula, cpf, telefone, data_nascimento, endereco, status_academico)
                 VALUES (:id_usuario, :matricula, :cpf, :telefone, :data_nascimento, :endereco, :status_academico)',
                [
                    ':id_usuario' => $idUsuario,
                    ':matricula' => $matriculaGerada,
                    ':cpf' => $dados[':cpf'],
                    ':telefone' => $dados[':telefone'],
                    ':data_nascimento' => $dados[':data_nascimento'],
                    ':endereco' => $dados[':endereco'],
                    ':status_academico' => $dados[':status_academico'],
                ]
            );
            flash('success', 'Aluno cadastrado com sucesso.');
        }

        registrarLog('Salvou aluno');
        redirecionar('alunos.php');
    } catch (Throwable $erroSql) {
        $erro = $erroSql->getMessage();
    }
}

if (getValor('editar') !== '') {
    $editando = buscarUm(
        'SELECT a.*, u.nome, u.email, u.status AS acesso
         FROM alunos a
         JOIN usuarios u ON u.id_usuario = a.id_usuario
         WHERE a.id_aluno = :id',
        [':id' => (int) getValor('editar')]
    );
}

require __DIR__ . '/../includes/header.php';
?>

<section class="page-heading"><div><h1><?= $editando ? 'Editar Aluno' : 'Cadastro de Aluno' ?></h1><p>Cria o usuario de login e o cadastro academico do aluno.</p></div></section>
<?php if ($erro): ?><div class="alert danger"><span class="alert-marker"></span><div><strong>Erro</strong><span class="muted"><?= e($erro) ?></span></div></div><?php endif; ?>

<section class="panel">
  <div class="panel-header"><h2>Dados pessoais</h2></div>
  <div class="panel-body">
    <form class="form-grid" method="post">
      <input type="hidden" name="acao" value="salvar">
      <input type="hidden" name="id_aluno" value="<?= e($editando['id_aluno'] ?? '') ?>">

      <label class="field"><span>Nome completo</span><input class="control" name="nome" value="<?= e($editando['nome'] ?? '') ?>" required></label>
      <label class="field"><span>E-mail</span><input class="control" type="email" name="email" value="<?= e($editando['email'] ?? '') ?>" required></label>

      <?php if (!$editando): ?><label class="field"><span>Senha inicial</span><input class="control" name="senha" value="123456"></label><?php endif; ?>
      <label class="field"><span>Matricula</span><input class="control" name="matricula" placeholder="Gerada automaticamente se vazio" value="<?= e($editando['matricula'] ?? '') ?>"></label>

      <label class="field"><span>CPF</span><input class="control" name="cpf" data-mask="cpf" value="<?= e($editando['cpf'] ?? '') ?>"></label>
      <label class="field"><span>Telefone</span><input class="control" name="telefone" data-mask="telefone" value="<?= e($editando['telefone'] ?? '') ?>"></label>

      <label class="field"><span>Data de nascimento</span><input class="control" type="date" name="data_nascimento" value="<?= e($editando['data_nascimento'] ?? '') ?>"></label>
      <label class="field"><span>Status academico</span>
        <select class="select" name="status_academico">
          <?php foreach (['regular', 'trancado', 'jubilado', 'evadido'] as $status): ?>
            <option value="<?= e($status) ?>" <?= ($editando['status_academico'] ?? 'regular') === $status ? 'selected' : '' ?>><?= e(textoStatus($status)) ?></option>
          <?php endforeach; ?>
        </select>
      </label>

      <label class="field"><span>Acesso</span>
        <select class="select" name="acesso">
          <option value="ativo" <?= ($editando['acesso'] ?? 'ativo') === 'ativo' ? 'selected' : '' ?>>Ativo</option>
          <option value="inativo" <?= ($editando['acesso'] ?? '') === 'inativo' ? 'selected' : '' ?>>Inativo</option>
        </select>
      </label>

      <label class="field span-2"><span>Endereco</span><input class="control" name="endereco" value="<?= e($editando['endereco'] ?? '') ?>"></label>

      <div class="actions span-2"><button class="btn primary" type="submit">Salvar aluno</button><a class="btn ghost" href="alunos.php">Voltar</a></div>
    </form>
  </div>
</section>

<?php require __DIR__ . '/../includes/footer.php'; ?>
