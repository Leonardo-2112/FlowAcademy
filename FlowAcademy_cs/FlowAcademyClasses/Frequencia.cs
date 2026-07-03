using System;
using System.Collections.Generic;
using System.Data;

namespace FlowAcademyClasses
{
    public class Frequencia
    {
        // Propriedades
        public int IdFrequencia { get; set; }
        public int IdMatricula { get; set; }
        public int IdDisciplina { get; set; }
        public int TotalAulas { get; set; }
        public int Presencas { get; set; }
        public decimal Percentual { get; set; }
        public string? CodigoMatricula { get; set; }
        public string? NomeAluno { get; set; }
        public string? NomeDisciplina { get; set; }
        public string? CodigoTurma { get; set; }

        // Objetos relacionados
        public Matricula? Matricula { get; set; }
        public Disciplina? Disciplina { get; set; }

        // Construtores
        public Frequencia()
        {
            IdFrequencia = 0;
            IdMatricula = 0;
            IdDisciplina = 0;
            TotalAulas = 0;
            Presencas = 0;
            Percentual = 0;
        }

        public Frequencia(int idFrequencia)
        {
            IdFrequencia = idFrequencia;
        }

        public Frequencia(int idFrequencia, int idMatricula, int idDisciplina,
                          int totalAulas, int presencas, decimal percentual)
        {
            IdFrequencia = idFrequencia;
            IdMatricula = idMatricula;
            IdDisciplina = idDisciplina;
            TotalAulas = totalAulas;
            Presencas = presencas;
            Percentual = percentual;
        }

        // Inserir
        public bool Inserir()
        {
            if (IdMatricula <= 0) return false;
            if (IdDisciplina <= 0) return false;
            if (TotalAulas <= 0) return false;
            if (Presencas < 0) return false;
            if (Presencas > TotalAulas) return false;

            bool inserido = false;
            CalcularPercentual();

            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_frequencia_insert";

                cmd.Parameters.AddWithValue("p_id_matricula", IdMatricula);
                cmd.Parameters.AddWithValue("p_id_disciplina", IdDisciplina);
                cmd.Parameters.AddWithValue("p_total_aulas", TotalAulas);
                cmd.Parameters.AddWithValue("p_presencas", Presencas);

                IdFrequencia = Convert.ToInt32(cmd.ExecuteScalar());
                inserido = IdFrequencia > 0;

                cmd.Connection.Close();
            }

            return inserido;
        }

        // Atualizar
        public bool Atualizar()
        {
            if (IdFrequencia < 1) return false;
            if (IdMatricula <= 0) return false;
            if (IdDisciplina <= 0) return false;
            if (TotalAulas <= 0) return false;
            if (Presencas < 0) return false;
            if (Presencas > TotalAulas) return false;

            bool atualizado = false;
            CalcularPercentual();

            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_frequencia_update";

                cmd.Parameters.AddWithValue("p_id", IdFrequencia);
                cmd.Parameters.AddWithValue("p_id_matricula", IdMatricula);
                cmd.Parameters.AddWithValue("p_id_disciplina", IdDisciplina);
                cmd.Parameters.AddWithValue("p_total_aulas", TotalAulas);
                cmd.Parameters.AddWithValue("p_presencas", Presencas);

                atualizado = cmd.ExecuteNonQuery() >= 0;

                cmd.Connection.Close();
            }

            return atualizado;
        }

        // Excluir
        public bool Excluir()
        {
            if (IdFrequencia < 1) return false;

            bool excluido = false;
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_frequencia_delete";

                cmd.Parameters.AddWithValue("p_id", IdFrequencia);

                excluido = cmd.ExecuteNonQuery() > 0;

                cmd.Connection.Close();
            }

            return excluido;
        }

        // Obter lista
        public static List<Frequencia> ObterLista(string busca = "")
        {
            List<Frequencia> frequencias = new();
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"
                    SELECT f.id_frequencia, f.id_matricula, f.id_disciplina,
                           f.total_aulas, f.presencas, f.percentual,
                           a.matricula, u.nome, d.nome, t.codigo_turma
                    FROM frequencia f
                    INNER JOIN matriculas m ON m.id_matricula = f.id_matricula
                    INNER JOIN alunos a ON a.id_aluno = m.id_aluno
                    INNER JOIN usuarios u ON u.id_usuario = a.id_usuario
                    INNER JOIN turmas t ON t.id_turma = m.id_turma
                    INNER JOIN disciplinas d ON d.id_disciplina = f.id_disciplina
                    WHERE u.nome LIKE @busca
                    ORDER BY u.nome, d.nome";

                cmd.Parameters.AddWithValue("@busca", "%" + busca + "%");

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    frequencias.Add(MontarObjeto(dr));
                }

                dr.Close();
                cmd.Connection.Close();
            }

            return frequencias;
        }

        // Obter por id
        public static Frequencia ObterPorId(int id)
        {
            Frequencia frequencia = new();
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"
                    SELECT id_frequencia, id_matricula, id_disciplina,
                           total_aulas, presencas, percentual
                    FROM frequencia
                    WHERE id_frequencia = @id";

                cmd.Parameters.AddWithValue("@id", id);

                var dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    frequencia = MontarObjeto(dr);
                }

                dr.Close();
                cmd.Connection.Close();
            }

            return frequencia;
        }

        // Montar objeto
        public static Frequencia MontarObjeto(IDataRecord dr)
        {
            Frequencia frequencia = new Frequencia(
                dr.GetInt32(0),
                dr.GetInt32(1),
                dr.GetInt32(2),
                dr.GetInt32(3),
                dr.GetInt32(4),
                dr.IsDBNull(5) ? 0 : dr.GetDecimal(5)
            );

            if (dr.FieldCount > 6 && !dr.IsDBNull(6))
                frequencia.CodigoMatricula = dr.GetString(6);

            if (dr.FieldCount > 7 && !dr.IsDBNull(7))
                frequencia.NomeAluno = dr.GetString(7);

            if (dr.FieldCount > 8 && !dr.IsDBNull(8))
                frequencia.NomeDisciplina = dr.GetString(8);

            if (dr.FieldCount > 9 && !dr.IsDBNull(9))
                frequencia.CodigoTurma = dr.GetString(9);

            return frequencia;
        }

        // Regra simples
        public void CalcularPercentual()
        {
            if (TotalAulas > 0)
                Percentual = (Presencas * 100m) / TotalAulas;
            else
                Percentual = 0;
        }
    }
}
