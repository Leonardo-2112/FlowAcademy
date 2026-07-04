<?php

$host = '10.91.47.67';
$banco = 'flow_academy';
$usuario = 'root';
$senha = 'P@ssw0rd';

try {
    $pdo = new PDO(
        "mysql:host=$host;dbname=$banco;charset=utf8mb4",
        $usuario,
        $senha,
        [
            PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
            PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
        ]
    );
} catch (PDOException $erro) {
    die('Erro ao conectar no banco de dados: ' . $erro->getMessage());
}
