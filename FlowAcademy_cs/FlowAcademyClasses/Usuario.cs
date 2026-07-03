using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FlowAcademyClasses
{
    public class Usuario
    {
        // ==========================
        // PROPRIEDADES
        // ==========================
        public int IdUsuario { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? NivelAcesso { get; set; }
        public string? Status { get; set; }
        public DateTime? UltimoLogin { get; set; }
        public DateTime DataCriacao { get; set; }

        // ==========================
        // CONSTRUTOR
        // ==========================
        public Usuario()
        {
            IdUsuario = 0;
            Nome = "";
            Email = "";
            Senha = "";
            NivelAcesso = "aluno";
            Status = "ativo";
            UltimoLogin = null;
            DataCriacao = DateTime.Now;
        }

        public Usuario(int idUsuario, string? nome, string? email,
                       string? senha, string? nivelAcesso,
                       string? status, DateTime? ultimoLogin, DateTime dataCriacao)
        {
            IdUsuario = idUsuario;
            Nome = nome;
            Email = email;
            Senha = senha;
            NivelAcesso = nivelAcesso;
            Status = status;
            UltimoLogin = ultimoLogin;
            DataCriacao = dataCriacao;
        }

        // ==========================
        // INSERIR
        // ==========================
        public bool Inserir()
        {
            if (string.IsNullOrEmpty(Nome)) return false;
            if (string.IsNullOrEmpty(Email)) return false;
            if (string.IsNullOrEmpty(Senha)) return false;

            bool inserido = false;
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_usuario_insert";

                cmd.Parameters.AddWithValue("p_nome", Nome);
                cmd.Parameters.AddWithValue("p_email", Email);
                cmd.Parameters.AddWithValue("p_senha_hash", GerarHash(Senha)); 
                cmd.Parameters.AddWithValue("p_perfil", NivelAcesso);
                cmd.Parameters.AddWithValue("p_status", Status);

                IdUsuario = Convert.ToInt32(cmd.ExecuteScalar());

                inserido = IdUsuario > 0;

                cmd.Connection.Close();
            }

            return inserido;
        }

        // ==========================
        // ATUALIZAR
        // ==========================
        public bool Atualizar()
        {
            if (IdUsuario < 1) return false;

            bool atualizado = false;
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_usuario_update";

                cmd.Parameters.AddWithValue("p_id", IdUsuario);
                cmd.Parameters.AddWithValue("p_nome", Nome);
                cmd.Parameters.AddWithValue("p_email", Email);
                cmd.Parameters.AddWithValue("p_senha_hash", ObterSenhaParaSalvar());
                cmd.Parameters.AddWithValue("p_perfil", NivelAcesso);
                cmd.Parameters.AddWithValue("p_status", Status);

                atualizado = cmd.ExecuteNonQuery() >= 0;

                cmd.Connection.Close();
            }

            return atualizado;
        }

        // ==========================
        // EXCLUIR
        // ==========================
        public bool Excluir()
        {
            if (IdUsuario < 1) return false;

            bool excluido = false;
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_usuario_delete";

                cmd.Parameters.AddWithValue("p_id", IdUsuario);

                excluido = cmd.ExecuteNonQuery() > 0;

                cmd.Connection.Close();
            }

            return excluido;
        }

        // ==========================
        // OBTER POR ID
        // ==========================
        public static Usuario ObterPorId(int id)
        {
            Usuario usuario = new();
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"
                SELECT id_usuario, nome, email, senha_hash,
                       perfil, status, ultimo_login, created_at
                FROM usuarios
                WHERE id_usuario = @id";

                cmd.Parameters.AddWithValue("@id", id);

                var dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    usuario = MontarObjeto(dr);
                }

                dr.Close();
                cmd.Connection.Close();
            }

            return usuario;
        }

        // ==========================
        // LISTAR
        // ==========================
        public static List<Usuario> ObterLista(string busca = "")
        {
            List<Usuario> usuarios = new();
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"
                SELECT id_usuario, nome, email, senha_hash,
                       perfil, status, ultimo_login, created_at
                FROM usuarios
                WHERE nome LIKE @busca
                   OR email LIKE @busca
                   OR perfil LIKE @busca
                ORDER BY nome";

                cmd.Parameters.AddWithValue("@busca", "%" + busca + "%");

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    usuarios.Add(MontarObjeto(dr));
                }

                dr.Close();
                cmd.Connection.Close();
            }

            return usuarios;
        }

        public static List<Usuario> ObterListaPorPerfil(string perfil)
        {
            List<Usuario> usuarios = new();
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"
                SELECT id_usuario, nome, email, senha_hash,
                       perfil, status, ultimo_login, created_at
                FROM usuarios
                WHERE perfil = @perfil
                ORDER BY nome";

                cmd.Parameters.AddWithValue("@perfil", perfil);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    usuarios.Add(MontarObjeto(dr));
                }

                dr.Close();
                cmd.Connection.Close();
            }

            return usuarios;
        }

        // ==========================
        // LOGIN
        // ==========================
        public static Usuario EfetuarLogin(string email, string senha)
        {
            Usuario usuario = new();
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_usuario_autenticar";

                cmd.Parameters.AddWithValue("p_email", email);
                cmd.Parameters.AddWithValue("p_senha_hash", GerarHash(senha));

                var dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    usuario = MontarObjeto(dr);
                }

                dr.Close();
                cmd.Connection.Close();
            }

            return usuario;
        }

        public static string GerarHash(string senha)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));

                StringBuilder builder = new StringBuilder();

                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }

        }

        public static Usuario MontarObjeto(IDataRecord dr)
        {
            return new Usuario(
                dr.GetInt32(0),
                dr.IsDBNull(1) ? null : dr.GetString(1),
                dr.IsDBNull(2) ? null : dr.GetString(2),
                dr.IsDBNull(3) ? null : dr.GetString(3),
                dr.IsDBNull(4) ? null : dr.GetString(4),
                dr.IsDBNull(5) ? null : dr.GetString(5),
                dr.FieldCount > 7 && !dr.IsDBNull(6) ? dr.GetDateTime(6) : null,
                dr.GetDateTime(dr.FieldCount > 7 ? 7 : 6)
            );
        }

        private string ObterSenhaParaSalvar()
        {
            if (string.IsNullOrEmpty(Senha))
            {
                Usuario usuario = ObterPorId(IdUsuario);
                return usuario.Senha ?? "";
            }

            if (Senha.Length == 64 && Senha.All(c => Uri.IsHexDigit(c)))
            {
                return Senha;
            }

            return GerarHash(Senha);
        }
    }
}

