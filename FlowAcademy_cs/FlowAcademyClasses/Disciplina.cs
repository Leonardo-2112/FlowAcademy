using System;
using System.Collections.Generic;
using System.Data;

namespace FlowAcademyClasses
{
    public class Disciplina
    {
        // ==========================
        // PROPRIEDADES
        // ==========================
        public int IdDisciplina { get; set; }
        public int IdCurso { get; set; }
        public string? Nome { get; set; }
        public int CargaHoraria { get; set; }
        public string? NomeCurso { get; set; }

        // ==========================
        // CONSTRUTOR VAZIO
        // ==========================
        public Disciplina()
        {
            IdDisciplina = 0;
            IdCurso = 0;
            Nome = "";
            CargaHoraria = 0;
        }

        // ==========================
        // CONSTRUTOR COMPLETO
        // ==========================
        public Disciplina(int idDisciplina, int idCurso, string? nome, int cargaHoraria)
        {
            IdDisciplina = idDisciplina;
            IdCurso = idCurso;
            Nome = nome;
            CargaHoraria = cargaHoraria;
        }

        // ==========================
        // INSERIR
        // ==========================
        public bool Inserir()
        {
            if (IdCurso < 1) return false;
            if (string.IsNullOrEmpty(Nome)) return false;
            if (CargaHoraria <= 0) return false;

            bool inserido = false;
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {

                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_disciplina_insert";

                cmd.Parameters.AddWithValue("p_id_curso", IdCurso);
                cmd.Parameters.AddWithValue("p_nome", Nome);
                cmd.Parameters.AddWithValue("p_carga_horaria", CargaHoraria);

                IdDisciplina = Convert.ToInt32(cmd.ExecuteScalar());
                inserido = IdDisciplina > 0;

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

            if (IdDisciplina < 1) return atualizado;

            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {

                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_disciplina_update";

                cmd.Parameters.AddWithValue("p_id", IdDisciplina);
                cmd.Parameters.AddWithValue("p_id_curso", IdCurso);
                cmd.Parameters.AddWithValue("p_nome", Nome);
                cmd.Parameters.AddWithValue("p_carga_horaria", CargaHoraria);

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

            if (IdDisciplina < 1) return excluido;

            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {

                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_disciplina_delete";

                cmd.Parameters.AddWithValue("p_id", IdDisciplina);

                if (cmd.ExecuteNonQuery() > 0)
                    excluido = true;

                cmd.Connection.Close();
            }

            return excluido;
        }

        // ==========================
        // OBTER POR ID
        // ==========================
        public static Disciplina ObterPorId(int id)
        {
            Disciplina disciplina = new();
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"
                SELECT id_disciplina, id_curso, nome, carga_horaria
                FROM disciplinas
                WHERE id_disciplina = @id";

                cmd.Parameters.AddWithValue("@id", id);

                var dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    disciplina = MontarObjeto(dr);
                }

                dr.Close();
                cmd.Connection.Close();
            }

            return disciplina;
        }

        // ==========================
        // LISTAR
        // ==========================
        public static List<Disciplina> ObterLista(string busca = "")
        {
            List<Disciplina> disciplinas = new();
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"
                SELECT d.id_disciplina, d.id_curso, d.nome, d.carga_horaria, c.nome
                FROM disciplinas d
                INNER JOIN cursos c ON c.id_curso = d.id_curso
                WHERE d.nome LIKE @busca
                   OR c.nome LIKE @busca
                ORDER BY d.nome";

                cmd.Parameters.AddWithValue("@busca", "%" + busca + "%");

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    disciplinas.Add(MontarObjeto(dr));
                }

                dr.Close();
                cmd.Connection.Close();
            }

            return disciplinas;
        }

        public static List<Disciplina> ObterListaPorCurso(int idCurso)
        {
            List<Disciplina> disciplinas = new();
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"
                SELECT d.id_disciplina, d.id_curso, d.nome, d.carga_horaria, c.nome
                FROM disciplinas d
                INNER JOIN cursos c ON c.id_curso = d.id_curso
                WHERE d.id_curso = @id_curso
                ORDER BY d.nome";

                cmd.Parameters.AddWithValue("@id_curso", idCurso);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    disciplinas.Add(MontarObjeto(dr));
                }

                dr.Close();
                cmd.Connection.Close();
            }

            return disciplinas;
        }

        public static Disciplina MontarObjeto(IDataRecord dr)
        {
            Disciplina disciplina = new Disciplina(
                dr.GetInt32(0),
                dr.GetInt32(1),
                dr.IsDBNull(2) ? null : dr.GetString(2),
                dr.GetInt32(3)
            );

            if (dr.FieldCount > 4 && !dr.IsDBNull(4))
                disciplina.NomeCurso = dr.GetString(4);

            return disciplina;
        }
    }
}
