<?php

$usuarioMenu = usuarioLogado();
$perfilReal = $usuarioMenu['perfil'] ?? '';
$perfilMenu = perfilAtivo();

// Cada perfil enxerga apenas os grupos e links definidos para ele no manual.
$gruposPorPerfil = [
    'aluno' => [
        'ALUNO' => [
            ['dashboard', 'Dashboard', 'dashboard.php'],
            ['boletim', 'Boletim', 'pages/boletim.php'],
            ['frequencias', 'Frequencia', 'pages/frequencias.php'],
        ],
    ],
    'professor' => [
        'PROFESSOR' => [
            ['dashboard', 'Dashboard', 'dashboard.php'],
            ['notas', 'Lancar notas', 'pages/notas.php'],
            ['frequencias', 'Registrar frequencia', 'pages/frequencias.php'],
        ],
    ],
    'coordenacao' => [
        'COORDENACAO' => [
            ['dashboard', 'Dashboard', 'dashboard.php'],
            ['cursos', 'Cursos', 'pages/cursos.php'],
            ['novo_curso', 'Novo curso', 'pages/novo_curso.php'],
            ['turmas', 'Turmas', 'pages/turmas.php'],
            ['nova_turma', 'Nova turma', 'pages/nova_turma.php'],
        ],
    ],
    'admin' => [
        'ADMIN' => [
            ['dashboard', 'Dashboard', 'dashboard.php'],
            ['cadastro_coordenacao', 'Cadastrar coordenacao', 'pages/cadastro_coordenacao.php'],
            ['cadastro_administrativo', 'Cadastrar administrativo', 'pages/cadastro_administrativo.php'],
            ['logs', 'Logs', 'pages/logs.php'],
        ],
    ],
    'administrativo' => [
        'ADMINISTRATIVO' => [
            ['dashboard', 'Dashboard', 'dashboard.php'],
            ['alunos', 'Alunos', 'pages/alunos.php'],
            ['cadastro_aluno', 'Cadastro de aluno', 'pages/cadastro_aluno.php'],
            ['matriculas', 'Matricula', 'pages/matriculas.php'],
            ['pagamentos', 'Pagamentos', 'pages/pagamentos.php'],
            ['novo_pagamento', 'Novo pagamento', 'pages/novo_pagamento.php'],
        ],
    ],
];

// O grupo exibido segue o PERFIL ATIVO (modo de visualizacao), nao o perfil real.
// Assim, quando o Admin "assume" a visao de Coordenacao ou Administrativo, o menu
// lateral fica identico ao daquele perfil, com os mesmos links funcionando.
$grupos = $gruposPorPerfil[$perfilMenu] ?? [];

// O atalho PERFIS so existe para quem realmente esta logado como admin, e fica
// sempre disponivel (mesmo dentro do modo de visualizacao) para poder alternar
// ou voltar para a propria area do admin.
if ($perfilReal === 'admin') {
    $grupos['PERFIS'] = [
        ['perfil_admin', 'Admin', 'dashboard.php?ver=admin'],
        ['perfil_coordenacao', 'Coordenacao', 'dashboard.php?ver=coordenacao'],
        ['perfil_administrativo', 'Administrativo', 'dashboard.php?ver=administrativo'],
    ];
}
?>

<?php foreach ($grupos as $tituloGrupo => $links): ?>
  <nav class="nav-section nav-group">
    <p class="nav-title"><?= e($tituloGrupo) ?></p>
    <ul class="nav-list">
      <?php foreach ($links as $link): ?>
        <?php
          $ehAtalhoPerfil = strpos($link[0], 'perfil_') === 0;
          $estaAtivo = $ehAtalhoPerfil
              ? $link[0] === 'perfil_' . $perfilMenu
              : ($ativo ?? '') === $link[0];
        ?>
        <li>
          <a class="nav-link <?= $estaAtivo ? 'active' : '' ?>" href="<?= e(($base ?? '') . $link[2]) ?>">
            <span class="nav-dot"></span><?= e($link[1]) ?>
          </a>
        </li>
      <?php endforeach; ?>
    </ul>
  </nav>
<?php endforeach; ?>
