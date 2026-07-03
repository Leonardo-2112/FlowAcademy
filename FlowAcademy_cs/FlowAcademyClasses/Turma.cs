using System;
using System.Collections.Generic;
using System.Data;

namespace FlowAcademyClasses
{
    public class Turma
    {
        // Propriedades

        public int IdTurma { get; set; }

        public int IdCurso { get; set; }

        public int IdProfessor { get; set; }

        public string? CodigoTurma { get; set; }

        public string? Turno { get; set; }

        public string? PeriodoLetivo { get; set; }

        public int CapacidadeMaxima { get; set; }

        public string? Status { get; set; }
        public string? NomeCurso { get; set; }
        public string? NomeProfessor { get; set; }


        // Objetos de relacionamento

        public Curso? Curso { get; set; }

        public Professor? Professor { get; set; }


        // Construtor vazio

        public Turma()
        {
            IdTurma = 0;

            IdCurso = 0;

            IdProfessor = 0;

            CodigoTurma = "";

            Turno = "manha";

            PeriodoLetivo = "";

            CapacidadeMaxima = 0;

            Status = "ativa";
        }


        // Construtor com ID

        public Turma(int idTurma)
        {
            IdTurma = idTurma;
        }


        // Construtor completo

        public Turma(

            int idTurma,

            int idCurso,

            int idProfessor,

            string? codigoTurma,

            string? turno,

            string? periodoLetivo,

            int capacidadeMaxima,

            string? status

            )

        {
            IdTurma = idTurma;

            IdCurso = idCurso;

            IdProfessor = idProfessor;

            CodigoTurma = codigoTurma;

            Turno = turno;

            PeriodoLetivo = periodoLetivo;

            CapacidadeMaxima = capacidadeMaxima;

            Status = status;
        }


        // ==========================
        // INSERIR
        // ==========================

        public bool Inserir()
        {

            if (IdCurso <= 0) return false;
            if (IdProfessor <= 0) return false;
            if (CapacidadeMaxima <= 0) return false;

            bool inserido = false;

            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {

                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "sp_turma_insert";

                cmd.Parameters.AddWithValue("p_id_curso", IdCurso);

                cmd.Parameters.AddWithValue("p_id_professor", IdProfessor);

                cmd.Parameters.AddWithValue("p_codigo_turma", CodigoTurma);

                cmd.Parameters.AddWithValue("p_turno", Turno);

                cmd.Parameters.AddWithValue("p_periodo_letivo", PeriodoLetivo);

                cmd.Parameters.AddWithValue("p_capacidade_maxima", CapacidadeMaxima);

                cmd.Parameters.AddWithValue("p_status", Status);


                IdTurma = Convert.ToInt32(cmd.ExecuteScalar());

                inserido = IdTurma > 0;

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

            if (IdTurma < 1)

                return atualizado;


            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {

                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "sp_turma_update";

                cmd.Parameters.AddWithValue("p_id", IdTurma);

                cmd.Parameters.AddWithValue("p_id_curso", IdCurso);

                cmd.Parameters.AddWithValue("p_id_professor", IdProfessor);

                cmd.Parameters.AddWithValue("p_codigo_turma", CodigoTurma);

                cmd.Parameters.AddWithValue("p_turno", Turno);

                cmd.Parameters.AddWithValue("p_periodo_letivo", PeriodoLetivo);

                cmd.Parameters.AddWithValue("p_capacidade_maxima", CapacidadeMaxima);

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

            if (IdTurma < 1)

                return excluido;


            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {

                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "sp_turma_delete";

                cmd.Parameters.AddWithValue("p_id", IdTurma);

                if (cmd.ExecuteNonQuery() > 0)

                    excluido = true;

                cmd.Connection.Close();
            }

            return excluido;
        }



        // ==========================
        // OBTER POR ID
        // ==========================

        public static Turma ObterPorId(int idTurma)
        {
            Turma turma = new();

            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"

                SELECT

                id_turma,

                id_curso,

                id_professor,

                codigo_turma,

                turno,

                periodo_letivo,

                capacidade_maxima,

                status

                FROM turmas

                WHERE id_turma = @id";

                cmd.Parameters.AddWithValue("@id", idTurma);

                var dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    turma = MontarObjeto(dr);
                }

                dr.Close();

                cmd.Connection.Close();
            }

            return turma;
        }



        // ==========================
        // LISTAR
        // ==========================

        public static List<Turma> ObterLista(string busca = "")
        {
            List<Turma> turmas = new();

            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"

                SELECT
                t.id_turma,
                t.id_curso,
                t.id_professor,
                t.codigo_turma,
                t.turno,
                t.periodo_letivo,
                t.capacidade_maxima,
                t.status,
                c.nome,
                u.nome
                FROM turmas t
                INNER JOIN cursos c ON c.id_curso = t.id_curso
                INNER JOIN professores p ON p.id_professor = t.id_professor
                INNER JOIN usuarios u ON u.id_usuario = p.id_usuario
                WHERE t.codigo_turma LIKE @busca
                ORDER BY t.periodo_letivo, t.codigo_turma";

                cmd.Parameters.AddWithValue("@busca", "%" + busca + "%");

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    turmas.Add(MontarObjeto(dr));
                }

                dr.Close();

                cmd.Connection.Close();
            }

            return turmas;
        }

        public static List<Turma> ObterListaPorProfessor(int idProfessor)
        {
            List<Turma> turmas = new();

            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"
                SELECT
                t.id_turma,
                t.id_curso,
                t.id_professor,
                t.codigo_turma,
                t.turno,
                t.periodo_letivo,
                t.capacidade_maxima,
                t.status,
                c.nome,
                u.nome
                FROM turmas t
                INNER JOIN cursos c ON c.id_curso = t.id_curso
                INNER JOIN professores p ON p.id_professor = t.id_professor
                INNER JOIN usuarios u ON u.id_usuario = p.id_usuario
                WHERE t.id_professor = @id_professor
                ORDER BY t.periodo_letivo, t.codigo_turma";

                cmd.Parameters.AddWithValue("@id_professor", idProfessor);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    turmas.Add(MontarObjeto(dr));
                }

                dr.Close();
                cmd.Connection.Close();
            }

            return turmas;
        }

        public static Turma MontarObjeto(IDataRecord dr)
        {
            Turma turma = new Turma(
                dr.GetInt32(0),
                dr.GetInt32(1),
                dr.GetInt32(2),
                dr.IsDBNull(3) ? null : dr.GetString(3),
                dr.IsDBNull(4) ? null : dr.GetString(4),
                dr.IsDBNull(5) ? null : dr.GetString(5),
                dr.GetInt32(6),
                dr.IsDBNull(7) ? null : dr.GetString(7)
            );

            if (dr.FieldCount > 8 && !dr.IsDBNull(8))
                turma.NomeCurso = dr.GetString(8);

            if (dr.FieldCount > 9 && !dr.IsDBNull(9))
                turma.NomeProfessor = dr.GetString(9);

            return turma;
        }



        // ==========================
        // POSSUI VAGA
        // ==========================

        public bool PossuiVaga()
        {
            bool possui = false;

            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"

                SELECT

                (

                SELECT COUNT(*)

                FROM matriculas

                WHERE id_turma = @id

                AND status = 'ativa'

                ),

                capacidade_maxima

                FROM turmas

                WHERE id_turma = @id";

                cmd.Parameters.AddWithValue("@id", IdTurma);

                var dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int matriculados = dr.GetInt32(0);

                    int capacidade = dr.GetInt32(1);

                    possui = matriculados < capacidade;
                }

                dr.Close();

                cmd.Connection.Close();
            }

            return possui;
        }



    }
}
