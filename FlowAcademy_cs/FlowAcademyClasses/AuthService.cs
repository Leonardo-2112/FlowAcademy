using System;
using System.Text;
using System.Security.Cryptography;
using System.Data;

namespace FlowAcademyClasses
{
    public class AuthService
    {
        // ==========================
        // LOGIN
        // ==========================
        public static Usuario Login(string email, string senha)
        {
            return Usuario.EfetuarLogin(email, senha);
        }

        // ==========================
        // LOGOUT
        // ==========================
        public static void Logout()
        {
            // Simples, pode ser expandido depois
            // Ex: limpar sessão ou variável global
        }

        // ==========================
        // HASH DE SENHA
        // ==========================
        public static string GerarHash(string senha)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(senha);
                byte[] hash = sha.ComputeHash(bytes);

                StringBuilder builder = new StringBuilder();

                foreach (byte b in hash)
                    builder.Append(b.ToString("x2"));

                return builder.ToString();
            }
        }

        // ==========================
        // VERIFICAR SENHA
        // ==========================
        public static bool VerificarSenha(string senhaDigitada, string senhaHashBanco)
        {
            string hashDigitado = GerarHash(senhaDigitada);
            return hashDigitado == senhaHashBanco;
        }

        // ==========================
        // ATUALIZAR ÚLTIMO LOGIN
        // ==========================
        public static void AtualizarUltimoLogin(int idUsuario)
        {
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"
                UPDATE usuarios
                SET ultimo_login = NOW()
                WHERE id_usuario = @id";

                cmd.Parameters.AddWithValue("@id", idUsuario);

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();
            }
        }
    }
}
