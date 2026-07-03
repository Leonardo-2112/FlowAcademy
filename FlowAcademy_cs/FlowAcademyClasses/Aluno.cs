using System;
using System.Collections.Generic;
using System.Data;

namespace FlowAcademyClasses
{
    public class Aluno
    {
        public int IdAluno { get; set; }
        public int IdUsuario { get; set; }
        public string? Matricula { get; set; }
        public string? Cpf { get; set; }
        public string? Telefone { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Endereco { get; set; }
        public string? StatusAcademico { get; set; }
        public string? NomeUsuario { get; set; }
        public string? NomeAluno { get { return NomeUsuario; } }
        public Usuario? Usuario { get; set; }

        public Aluno()
        {
            IdAluno = 0;
            IdUsuario = 0;
            Matricula = "";
            Cpf = "";
            Telefone = "";
            DataNascimento = null;
            Endereco = "";
            StatusAcademico = "regular";
        }

        public Aluno(int idAluno)
        {
            IdAluno = idAluno;
        }

        public Aluno(int idAluno, int idUsuario, string? matricula, string? cpf,
                     string? telefone, DateTime? dataNascimento,
                     string? endereco, string? statusAcademico)
        {
            IdAluno = idAluno;
            IdUsuario = idUsuario;
            Matricula = matricula;
            Cpf = cpf;
            Telefone = telefone;
            DataNascimento = dataNascimento;
            Endereco = endereco;
            StatusAcademico = statusAcademico;
        }

        public bool Inserir()
        {
            if (IdUsuario < 1) return false;
            if (string.IsNullOrEmpty(Matricula)) return false;
            if (string.IsNullOrEmpty(Cpf)) return false;

            bool inserido = false;
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_aluno_insert";

                cmd.Parameters.AddWithValue("p_id_usuario", IdUsuario);
                cmd.Parameters.AddWithValue("p_matricula", Matricula);
                cmd.Parameters.AddWithValue("p_cpf", Cpf);
                cmd.Parameters.AddWithValue("p_telefone", Telefone);
                cmd.Parameters.AddWithValue("p_data_nascimento", DataNascimento);
                cmd.Parameters.AddWithValue("p_endereco", Endereco);
                cmd.Parameters.AddWithValue("p_status_academico", StatusAcademico);

                IdAluno = Convert.ToInt32(cmd.ExecuteScalar());
                inserido = IdAluno > 0;

                cmd.Connection.Close();
            }

            return inserido;
        }

        public bool Atualizar()
        {
            if (IdAluno < 1) return false;
            if (IdUsuario < 1) return false;

            bool atualizado = false;
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_aluno_update";

                cmd.Parameters.AddWithValue("p_id", IdAluno);
                cmd.Parameters.AddWithValue("p_id_usuario", IdUsuario);
                cmd.Parameters.AddWithValue("p_matricula", Matricula);
                cmd.Parameters.AddWithValue("p_cpf", Cpf);
                cmd.Parameters.AddWithValue("p_telefone", Telefone);
                cmd.Parameters.AddWithValue("p_data_nascimento", DataNascimento);
                cmd.Parameters.AddWithValue("p_endereco", Endereco);
                cmd.Parameters.AddWithValue("p_status_academico", StatusAcademico);

                atualizado = cmd.ExecuteNonQuery() >= 0;

                cmd.Connection.Close();
            }

            return atualizado;
        }

        public bool Excluir()
        {
            if (IdAluno < 1) return false;

            bool excluido = false;
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_aluno_delete";

                cmd.Parameters.AddWithValue("p_id", IdAluno);

                excluido = cmd.ExecuteNonQuery() > 0;

                cmd.Connection.Close();
            }

            return excluido;
        }

        public static List<Aluno> ObterLista(string busca = "")
        {
            List<Aluno> alunos = new();
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"
                    SELECT a.id_aluno, a.id_usuario, a.matricula, a.cpf,
                           a.telefone, a.data_nascimento, a.endereco,
                           a.status_academico, u.nome
                    FROM alunos a
                    INNER JOIN usuarios u ON u.id_usuario = a.id_usuario
                    WHERE u.perfil = 'aluno'
                      AND (u.nome LIKE @busca
                       OR a.matricula LIKE @busca
                       OR a.cpf LIKE @busca)
                    ORDER BY u.nome";

                cmd.Parameters.AddWithValue("@busca", "%" + busca + "%");

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    alunos.Add(MontarObjeto(dr));
                }

                dr.Close();
                cmd.Connection.Close();
            }

            return alunos;
        }

        public static Aluno ObterPorId(int id)
        {
            Aluno aluno = new();
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"
                    SELECT a.id_aluno, a.id_usuario, a.matricula, a.cpf, a.telefone,
                           a.data_nascimento, a.endereco, a.status_academico, u.nome
                    FROM alunos a
                    INNER JOIN usuarios u ON u.id_usuario = a.id_usuario
                    WHERE a.id_aluno = @id";

                cmd.Parameters.AddWithValue("@id", id);

                var dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    aluno = MontarObjeto(dr);
                }

                dr.Close();
                cmd.Connection.Close();
            }

            if (aluno.IdUsuario > 0)
            {
                aluno.Usuario = Usuario.ObterPorId(aluno.IdUsuario);
            }

            return aluno;
        }

        public static Aluno MontarObjeto(IDataRecord dr)
        {
            Aluno aluno = new Aluno(
                dr.GetInt32(0),
                dr.GetInt32(1),
                dr.IsDBNull(2) ? null : dr.GetString(2),
                dr.IsDBNull(3) ? null : dr.GetString(3),
                dr.IsDBNull(4) ? null : dr.GetString(4),
                dr.IsDBNull(5) ? null : dr.GetDateTime(5),
                dr.IsDBNull(6) ? null : dr.GetString(6),
                dr.IsDBNull(7) ? null : dr.GetString(7)
            );

            if (dr.FieldCount > 8 && !dr.IsDBNull(8))
                aluno.NomeUsuario = dr.GetString(8);

            return aluno;
        }
    }
}
