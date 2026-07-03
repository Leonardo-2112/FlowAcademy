using System;
using System.Collections.Generic;
using System.Data;

namespace FlowAcademyClasses
{
    public class Professor
    {
        public int IdProfessor { get; set; }
        public int IdUsuario { get; set; }
        public string? Cpf { get; set; }
        public string? Especialidade { get; set; }
        public string? NomeUsuario { get; set; }

        public Usuario? Usuario { get; set; }

        public Professor()
        {
            IdProfessor = 0;
            IdUsuario = 0;
            Cpf = "";
            Especialidade = "";
        }

        public Professor(int idProfessor, int idUsuario, string? cpf, string? especialidade)
        {
            IdProfessor = idProfessor;
            IdUsuario = idUsuario;
            Cpf = cpf;
            Especialidade = especialidade;
        }

        // ==========================
        // INSERIR
        // ==========================
        public bool Inserir()
        {
            if (IdUsuario <= 0) return false;

            bool inserido = false;
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_professor_insert";

                cmd.Parameters.AddWithValue("p_id_usuario", IdUsuario);
                cmd.Parameters.AddWithValue("p_cpf", Cpf);
                cmd.Parameters.AddWithValue("p_especialidade", Especialidade);

                IdProfessor = Convert.ToInt32(cmd.ExecuteScalar());
                inserido = IdProfessor > 0;

                cmd.Connection.Close();
            }

            return inserido;
        }

        // ==========================
        // ATUALIZAR
        // ==========================
        public bool Atualizar()
        {
            if (IdProfessor < 1) return false;
            if (IdUsuario < 1) return false;

            var cmd = Banco.Abrir();
            bool atualizado = false;

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_professor_update";

                cmd.Parameters.AddWithValue("p_id", IdProfessor);
                cmd.Parameters.AddWithValue("p_id_usuario", IdUsuario);
                cmd.Parameters.AddWithValue("p_cpf", Cpf);
                cmd.Parameters.AddWithValue("p_especialidade", Especialidade);

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
            if (IdProfessor < 1) return false;

            var cmd = Banco.Abrir();
            bool excluido = false;

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_professor_delete";

                cmd.Parameters.AddWithValue("p_id", IdProfessor);

                excluido = cmd.ExecuteNonQuery() > 0;

                cmd.Connection.Close();
            }

            return excluido;
        }

        // ==========================
        // OBTER POR ID
        // ==========================
        public static Professor ObterPorId(int id)
        {
            Professor professor = new();
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"
            SELECT p.id_professor, p.id_usuario, p.cpf, p.especialidade, u.nome
            FROM professores p
            INNER JOIN usuarios u ON u.id_usuario = p.id_usuario
            WHERE p.id_professor = @id";

                cmd.Parameters.AddWithValue("@id", id);

                var dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    professor = MontarObjeto(dr);
                }

                dr.Close();
                cmd.Connection.Close();
            }

            return professor;
        }

        // ==========================
        // LISTAR
        // ==========================
        public static List<Professor> ObterLista(string busca = "")
        {
            List<Professor> professores = new();
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"
                SELECT p.id_professor, p.id_usuario, p.cpf, p.especialidade, u.nome
                FROM professores p
                INNER JOIN usuarios u ON u.id_usuario = p.id_usuario
                WHERE u.perfil = 'professor'
                  AND (u.nome LIKE @busca
                   OR p.cpf LIKE @busca
                   OR p.especialidade LIKE @busca)
                ORDER BY u.nome";

                cmd.Parameters.AddWithValue("@busca", "%" + busca + "%");

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    professores.Add(MontarObjeto(dr));
                }

                dr.Close();
                cmd.Connection.Close();
            }

            return professores;
        }

        public static Professor ObterPorUsuario(int idUsuario)
        {
            Professor professor = new();
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"
                SELECT p.id_professor, p.id_usuario, p.cpf, p.especialidade, u.nome
                FROM professores p
                INNER JOIN usuarios u ON u.id_usuario = p.id_usuario
                WHERE p.id_usuario = @id_usuario";

                cmd.Parameters.AddWithValue("@id_usuario", idUsuario);

                var dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    professor = MontarObjeto(dr);
                }

                dr.Close();
                cmd.Connection.Close();
            }

            return professor;
        }

        public static Professor MontarObjeto(IDataRecord dr)
        {
            Professor professor = new Professor(
                dr.GetInt32(0),
                dr.GetInt32(1),
                dr.IsDBNull(2) ? null : dr.GetString(2),
                dr.IsDBNull(3) ? null : dr.GetString(3)
            );

            if (dr.FieldCount > 4 && !dr.IsDBNull(4))
                professor.NomeUsuario = dr.GetString(4);

            return professor;
        }
    }
}
