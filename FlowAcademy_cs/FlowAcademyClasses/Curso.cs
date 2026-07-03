using System;
using System.Collections.Generic;
using System.Data;

namespace FlowAcademyClasses
{
    public class Curso
    {
        // Propriedades
        public int IdCurso { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public int CargaHoraria { get; set; }
        public string? Status { get; set; }

        // Construtor vazio
        public Curso()
        {
            IdCurso = 0;
            Nome = "";
            Descricao = "";
            CargaHoraria = 0;
            Status = "ativo";
        }

        // Construtor com ID
        public Curso(int idCurso)
        {
            IdCurso = idCurso;
        }

        // Construtor completo
        public Curso(int idCurso, string? nome, string? descricao, int cargaHoraria, string? status)
        {
            IdCurso = idCurso;
            Nome = nome;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            Status = status;
        }

        // ==========================
        // INSERIR
        // ==========================
        public bool Inserir()
        {
            if (string.IsNullOrEmpty(Nome)) return false;
            if (CargaHoraria <= 0) return false;

            bool inserido = false;
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_curso_insert";

                cmd.Parameters.AddWithValue("p_nome", Nome);
                cmd.Parameters.AddWithValue("p_descricao", Descricao);
                cmd.Parameters.AddWithValue("p_carga_horaria", CargaHoraria);
                cmd.Parameters.AddWithValue("p_status", Status);

                IdCurso = Convert.ToInt32(cmd.ExecuteScalar());
                inserido = IdCurso > 0;

                cmd.Connection.Close();
            }

            return inserido;
        }

        // ==========================
        // ATUALIZAR
        // ==========================
        public bool Atualizar()
        {
            bool atualizado = false;
            if (IdCurso < 1) return atualizado;

            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_curso_update";

                cmd.Parameters.AddWithValue("p_id", IdCurso);
                cmd.Parameters.AddWithValue("p_nome", Nome);
                cmd.Parameters.AddWithValue("p_descricao", Descricao);
                cmd.Parameters.AddWithValue("p_carga_horaria", CargaHoraria);
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
            bool excluido = false;
            if (IdCurso < 1) return excluido;

            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_curso_delete";

                cmd.Parameters.AddWithValue("p_id", IdCurso);

                if (cmd.ExecuteNonQuery() > 0)
                    excluido = true;

                cmd.Connection.Close();
            }

            return excluido;
        }

        // ==========================
        // OBTER POR ID
        // ==========================
        public static Curso ObterPorId(int id)
        {
            Curso curso= new();
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"
                    SELECT id_curso, nome, descricao, carga_horaria, status
                    FROM cursos
                    WHERE id_curso = @id";

                cmd.Parameters.AddWithValue("@id", id);

                var dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    curso = MontarObjeto(dr);
                }

                dr.Close();
                cmd.Connection.Close();
            }

            return curso;
        }




        // ==========================
        // LISTAR (Retorna List<Curso>)
        // ==========================
        public static List<Curso> ObterLista(string busca = "")
        {
            List<Curso> cursos = new();
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"
                    SELECT id_curso, nome, descricao, carga_horaria, status
                    FROM cursos
                    WHERE nome LIKE @busca
                       OR status LIKE @busca
                    ORDER BY nome";

                cmd.Parameters.AddWithValue("@busca", "%" + busca + "%");

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    cursos.Add(MontarObjeto(dr));
                }

                dr.Close();
                cmd.Connection.Close();
            }

            return cursos;
        }

        public static Curso MontarObjeto(IDataRecord dr)
        {
            return new Curso(
                dr.GetInt32(0),
                dr.IsDBNull(1) ? null : dr.GetString(1),
                dr.IsDBNull(2) ? null : dr.GetString(2),
                dr.GetInt32(3),
                dr.IsDBNull(4) ? null : dr.GetString(4)
            );
        }
    }
}
