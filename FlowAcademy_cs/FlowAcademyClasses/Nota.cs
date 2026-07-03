using System;
using System.Collections.Generic;
using System.Data;

namespace FlowAcademyClasses
{
    public class Nota
    {
        // Propriedades
        public int IdNota { get; set; }
        public int IdMatricula { get; set; }
        public int IdDisciplina { get; set; }
        public decimal? Prova1 { get; set; }
        public decimal? Prova2 { get; set; }
        public decimal? Trabalho { get; set; }
        public decimal? Comportamental { get; set; }
        public decimal? MediaUc { get; set; }
        public string? Status { get; set; }
        public DateTime DataLancamento { get; set; }
        public string? CodigoMatricula { get; set; }
        public string? NomeAluno { get; set; }
        public string? NomeDisciplina { get; set; }
        public string? CodigoTurma { get; set; }

        // Objetos relacionados
        public Matricula? Matricula { get; set; }
        public Disciplina? Disciplina { get; set; }

        // Construtores
        public Nota()
        {
            IdNota = 0;
            IdMatricula = 0;
            IdDisciplina = 0;
            Status = "em_andamento";
            DataLancamento = DateTime.Now;
        }

        public Nota(int idNota, int idMatricula, int idDisciplina,
                    decimal? prova1, decimal? prova2, decimal? trabalho,
                    decimal? comportamental, decimal? mediaUc,
                    string? status, DateTime dataLancamento)
        {
            IdNota = idNota;
            IdMatricula = idMatricula;
            IdDisciplina = idDisciplina;
            Prova1 = prova1;
            Prova2 = prova2;
            Trabalho = trabalho;
            Comportamental = comportamental;
            MediaUc = mediaUc;
            Status = status;
            DataLancamento = dataLancamento;
        }

        // Inserir
        public bool Inserir()
        {
            if (IdMatricula <= 0) return false;
            if (IdDisciplina <= 0) return false;
            if (!NotasValidas()) return false;

            bool inserido = false;
            CalcularMedia();
            DataLancamento = DateTime.Now;

            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_nota_insert";

                cmd.Parameters.AddWithValue("p_id_matricula", IdMatricula);
                cmd.Parameters.AddWithValue("p_id_disciplina", IdDisciplina);
                cmd.Parameters.AddWithValue("p_prova_1", Prova1 ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("p_prova_2", Prova2 ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("p_trabalho", Trabalho ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("p_comportamental", Comportamental ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("p_media_uc", MediaUc ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("p_status", Status);
                cmd.Parameters.AddWithValue("p_data_lancamento", DataLancamento);

                IdNota = Convert.ToInt32(cmd.ExecuteScalar());
                inserido = IdNota > 0;

                cmd.Connection.Close();
            }

            return inserido;
        }

        // Atualizar
        public bool Atualizar()
        {
            if (IdNota < 1) return false;
            if (IdMatricula <= 0) return false;
            if (IdDisciplina <= 0) return false;
            if (!NotasValidas()) return false;

            bool atualizado = false;
            CalcularMedia();
            DataLancamento = DateTime.Now;

            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_nota_update";

                cmd.Parameters.AddWithValue("p_id", IdNota);
                cmd.Parameters.AddWithValue("p_id_matricula", IdMatricula);
                cmd.Parameters.AddWithValue("p_id_disciplina", IdDisciplina);
                cmd.Parameters.AddWithValue("p_prova_1", Prova1 ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("p_prova_2", Prova2 ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("p_trabalho", Trabalho ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("p_comportamental", Comportamental ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("p_media_uc", MediaUc ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("p_status", Status);
                cmd.Parameters.AddWithValue("p_data_lancamento", DataLancamento);

                atualizado = cmd.ExecuteNonQuery() >= 0;

                cmd.Connection.Close();
            }

            return atualizado;
        }

        // Excluir
        public bool Excluir()
        {
            if (IdNota < 1) return false;

            bool excluido = false;
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_nota_delete";

                cmd.Parameters.AddWithValue("p_id", IdNota);

                excluido = cmd.ExecuteNonQuery() > 0;

                cmd.Connection.Close();
            }

            return excluido;
        }

        // Obter lista
        public static List<Nota> ObterLista(string busca = "")
        {
            List<Nota> notas = new();
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"
                    SELECT n.id_nota, n.id_matricula, n.id_disciplina,
                           n.prova_1, n.prova_2, n.trabalho, n.comportamental,
                           n.media_uc, n.status, n.data_lancamento,
                           a.matricula, u.nome, d.nome, t.codigo_turma
                    FROM notas n
                    INNER JOIN matriculas m ON m.id_matricula = n.id_matricula
                    INNER JOIN alunos a ON a.id_aluno = m.id_aluno
                    INNER JOIN usuarios u ON u.id_usuario = a.id_usuario
                    INNER JOIN turmas t ON t.id_turma = m.id_turma
                    INNER JOIN disciplinas d ON d.id_disciplina = n.id_disciplina
                    WHERE u.nome LIKE @busca
                       ORDER BY n.data_lancamento DESC";

                cmd.Parameters.AddWithValue("@busca", "%" + busca + "%");

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    notas.Add(MontarObjeto(dr));
                }

                dr.Close();
                cmd.Connection.Close();
            }

            return notas;
        }

        // Obter por id
        public static Nota ObterPorId(int id)
        {
            Nota nota = new();
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"
                    SELECT id_nota, id_matricula, id_disciplina,
                           prova_1, prova_2, trabalho, comportamental,
                           media_uc, status, data_lancamento
                    FROM notas
                    WHERE id_nota = @id";

                cmd.Parameters.AddWithValue("@id", id);

                var dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    nota = MontarObjeto(dr);
                }

                dr.Close();
                cmd.Connection.Close();
            }

            return nota;
        }

        // Montar objeto
        public static Nota MontarObjeto(IDataRecord dr)
        {
            Nota nota = new Nota(
                dr.GetInt32(0),
                dr.GetInt32(1),
                dr.GetInt32(2),
                dr.IsDBNull(3) ? null : dr.GetDecimal(3),
                dr.IsDBNull(4) ? null : dr.GetDecimal(4),
                dr.IsDBNull(5) ? null : dr.GetDecimal(5),
                dr.IsDBNull(6) ? null : dr.GetDecimal(6),
                dr.IsDBNull(7) ? null : dr.GetDecimal(7),
                dr.IsDBNull(8) ? null : dr.GetString(8),
                dr.GetDateTime(9)
            );

            if (dr.FieldCount > 10 && !dr.IsDBNull(10))
                nota.CodigoMatricula = dr.GetString(10);

            if (dr.FieldCount > 11 && !dr.IsDBNull(11))
                nota.NomeAluno = dr.GetString(11);

            if (dr.FieldCount > 12 && !dr.IsDBNull(12))
                nota.NomeDisciplina = dr.GetString(12);

            if (dr.FieldCount > 13 && !dr.IsDBNull(13))
                nota.CodigoTurma = dr.GetString(13);

            return nota;
        }

        // Regras simples
        private void CalcularMedia()
        {
            decimal p1 = Prova1 ?? 0;
            decimal p2 = Prova2 ?? 0;
            decimal t = Trabalho ?? 0;
            decimal c = Comportamental ?? 0;

            MediaUc = (p1 * 0.3m) + (p2 * 0.3m) + (t * 0.3m) + (c * 0.1m);

            if (Prova1.HasValue && Prova2.HasValue && Trabalho.HasValue && Comportamental.HasValue)
                Status = MediaUc >= 6 ? "aprovado" : "reprovado";
            else
                Status = "em_andamento";
        }

        private bool NotasValidas()
        {
            if (!NotaValida(Prova1)) return false;
            if (!NotaValida(Prova2)) return false;
            if (!NotaValida(Trabalho)) return false;
            if (!NotaValida(Comportamental)) return false;

            return true;
        }

        private bool NotaValida(decimal? valor)
        {
            if (!valor.HasValue) return true;
            return valor.Value >= 0 && valor.Value <= 10;
        }
    }
}
