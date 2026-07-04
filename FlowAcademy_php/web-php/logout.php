<?php

require_once __DIR__ . '/includes/funcoes.php';

registrarLog('Logout realizado');
$_SESSION = [];
session_destroy();

redirecionar('login.php');
