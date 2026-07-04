<?php

require_once __DIR__ . '/conexao.php';

if (session_status() === PHP_SESSION_NONE) {
    session_start();
}

function e($valor)
{
    return htmlspecialchars((string) ($valor ?? ''), ENT_QUOTES, 'UTF-8');
}

function post($campo, $padrao = '')
{
    return trim((string) ($_POST[$campo] ?? $padrao));
}

function getValor($campo, $padrao = '')
{
    return trim((string) ($_GET[$campo] ?? $padrao));
}

function buscarUm($sql, $parametros = [])
{
    global $pdo;
    $stmt = $pdo->prepare($sql);
    $stmt->execute($parametros);
    $linha = $stmt->fetch();
    return $linha ?: null;
}

function buscarTodos($sql, $parametros = [])
{
    global $pdo;
    $stmt = $pdo->prepare($sql);
    $stmt->execute($parametros);
    return $stmt->fetchAll();
}

function executar($sql, $parametros = [])
{
    global $pdo;
    $stmt = $pdo->prepare($sql);
    $stmt->execute($parametros);
    return $stmt;
}

function redirecionar($caminho)
{
    header('Location: ' . $caminho);
    exit;
}

function flash($tipo, $mensagem)
{
    $_SESSION['flash'] = ['tipo' => $tipo, 'mensagem' => $mensagem];
}

function pegarFlash()
{
    $flash = $_SESSION['flash'] ?? null;
    unset($_SESSION['flash']);
    return $flash;
}

function usuarioLogado()
{
    return $_SESSION['usuario'] ?? null;
}

function estaLogado()
{
    return usuarioLogado() !== null;
}

function exigirLogin($base = '')
{
    if (!estaLogado()) {
        flash('danger', 'Faca login para acessar o sistema.');
        redirecionar($base . 'login.php');
    }

    $arquivoAtual = basename((string) ($_SERVER['SCRIPT_NAME'] ?? ''));
    if (!empty($_SESSION['usuario']['trocar_senha']) && $arquivoAtual !== 'alterar_senha.php') {
        redirecionar($base . 'alterar_senha.php');
    }
}

function exigirPerfil($perfisPermitidos, $base = '')
{
    exigirLogin($base);
    $usuario = usuarioLogado();

    if (!in_array($usuario['perfil'], $perfisPermitidos, true)) {
        flash('danger', 'Seu usuario nao tem permissao para acessar esta pagina.');
        redirecionar($base . 'dashboard.php');
    }
}

// Permite que o Admin "assuma" a visualizacao de outro perfil (Coordenacao ou
// Administrativo) apenas para fins de menu/dashboard, sem alterar a sessao real
// nem as permissoes de acesso. Para qualquer outro perfil, retorna o proprio perfil.
function perfilAtivo()
{
    $usuario = usuarioLogado();
    if (!$usuario) {
        return null;
    }

    if ($usuario['perfil'] !== 'admin') {
        return $usuario['perfil'];
    }

    $permitidos = ['admin', 'coordenacao', 'administrativo'];
    $solicitado = getValor('ver');
    if (in_array($solicitado, $permitidos, true)) {
        $_SESSION['ver_perfil'] = $solicitado;
    }

    return $_SESSION['ver_perfil'] ?? 'admin';
}

function paginaInicialPorPerfil($perfil)
{
    return 'dashboard.php';
}

function hashSenha($senha)
{
    return hash('sha256', (string) $senha);
}

function senhaCorreta($senhaDigitada, $hashBanco)
{
    return hash_equals((string) $hashBanco, hashSenha($senhaDigitada));
}

function registrarLog($acao)
{
    $usuario = usuarioLogado();

    if (!$usuario) {
        return;
    }

    try {
        executar(
            'INSERT INTO logs (id_usuario, acao, ip) VALUES (:id_usuario, :acao, :ip)',
            [
                ':id_usuario' => $usuario['id_usuario'],
                ':acao' => $acao,
                ':ip' => $_SERVER['REMOTE_ADDR'] ?? null,
            ]
        );
    } catch (Throwable $erro) {
        // O log nao deve impedir a acao principal.
    }
}

function textoStatus($status)
{
    $textos = [
        'ativo' => 'Ativo',
        'inativo' => 'Inativo',
        'regular' => 'Regular',
        'trancado' => 'Trancado',
        'jubilado' => 'Jubilado',
        'evadido' => 'Evadido',
        'ativa' => 'Ativa',
        'cancelada' => 'Cancelada',
        'concluida' => 'Concluida',
        'encerrada' => 'Encerrada',
        'pendente' => 'Pendente',
        'pago' => 'Pago',
        'atrasado' => 'Atrasado',
        'cancelado' => 'Cancelado',
        'aprovado' => 'Aprovado',
        'reprovado' => 'Reprovado',
        'em_andamento' => 'Em andamento',
        'manha' => 'Manha',
        'tarde' => 'Tarde',
        'noite' => 'Noite',
    ];

    return $textos[$status] ?? ucfirst((string) $status);
}

function classeBadge($status)
{
    if (in_array($status, ['ativo', 'regular', 'ativa', 'pago', 'aprovado'], true)) {
        return 'success';
    }

    if (in_array($status, ['pendente', 'em_andamento'], true)) {
        return 'warning';
    }

    if (in_array($status, ['inativo', 'cancelada', 'cancelado', 'trancado', 'jubilado', 'evadido', 'reprovado', 'atrasado'], true)) {
        return 'danger';
    }

    return 'info';
}

function badge($status)
{
    return '<span class="badge ' . classeBadge($status) . '">' . e(textoStatus($status)) . '</span>';
}

function dataBr($data)
{
    if (!$data) {
        return '-';
    }

    return date('d/m/Y', strtotime($data));
}

function dinheiro($valor)
{
    return 'R$ ' . number_format((float) $valor, 2, ',', '.');
}

function contar($sql, $parametros = [])
{
    $linha = buscarUm($sql, $parametros);
    return (int) ($linha['total'] ?? 0);
}

function atualizarPagamentosAtrasados()
{
    executar(
        'UPDATE pagamentos
         SET status = "atrasado"
         WHERE vencimento < CURDATE()
           AND status = "pendente"'
    );
}

function iniciais($nome)
{
    $partes = preg_split('/\s+/', trim((string) $nome));
    $primeira = $partes[0][0] ?? 'F';
    $segunda = $partes[1][0] ?? '';
    return strtoupper($primeira . $segunda);
}
