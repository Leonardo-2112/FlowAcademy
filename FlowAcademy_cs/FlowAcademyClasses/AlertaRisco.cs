using System;
using System.Collections.Generic;
using System.Data;

namespace FlowAcademyClasses
{
    public class AlertaRisco
    {
        // ==========================
        // PROPRIEDADES
        // ==========================
        public int IdAlerta { get; set; }
        public int IdMatricula { get; set; }
        public string? TipoRisco { get; set; }
        public decimal Score { get; set; }
        public string? Status { get; set; }

        public Matricula? Matricula { get; set; }

        // ==========================
        // CONSTRUTORES
        // ==========================
        public AlertaRisco()
        {
            IdAlerta = 0;
            IdMatricula = 0;
            TipoRisco = "";
            Score = 0;
            Status = "pendente";
        }

        public AlertaRisco(int idAlerta, int idMatricula,
                           string? tipoRisco, decimal score, string? status)
        {
            IdAlerta = idAlerta;
            IdMatricula = idMatricula;
            TipoRisco = tipoRisco;
            Score = score;
            Status = status;
        }

        // ==========================
        // INSERIR
        // ==========================
        public bool Inserir()
        {
            if (IdMatricula <= 0) return false;

            bool inserido = false;
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_inserir_alerta_risco";

                cmd.Parameters.AddWithValue("p_id_matricula", IdMatricula);
                cmd.Parameters.AddWithValue("p_tipo_risco", TipoRisco);
                cmd.Parameters.AddWithValue("p_score", Score);
                cmd.Parameters.AddWithValue("p_status",
                    string.IsNullOrEmpty(Status) ? "pendente" : Status);

                IdAlerta = Convert.ToInt32(cmd.ExecuteScalar());
                inserido = IdAlerta > 0;

                cmd.Connection.Close();
            }

            return inserido;
        }

        // ==========================
        // ATUALIZAR
        // ==========================
        public bool Atualizar()
        {
            if (IdAlerta < 1) return false;

            bool atualizado = false;
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_atualizar_alerta_risco";

                cmd.Parameters.AddWithValue("p_id", IdAlerta);
                cmd.Parameters.AddWithValue("p_id_matricula", IdMatricula);
                cmd.Parameters.AddWithValue("p_tipo_risco", TipoRisco);
                cmd.Parameters.AddWithValue("p_score", Score);
                cmd.Parameters.AddWithValue("p_status", Status);

                atualizado = cmd.ExecuteNonQuery() > 0;

                cmd.Connection.Close();
            }

            return atualizado;
        }

        // ==========================
        // EXCLUIR
        // ==========================
        public bool Excluir()
        {
            if (IdAlerta < 1) return false;

            bool excluido = false;
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_excluir_alerta_risco";

                cmd.Parameters.AddWithValue("p_id", IdAlerta);

                excluido = cmd.ExecuteNonQuery() > 0;

                cmd.Connection.Close();
            }

            return excluido;
        }

        // ==========================
        // OBTER POR ID (SEM PROCEDURE)
        // ==========================
        public static AlertaRisco ObterPorId(int id)
        {
            AlertaRisco alerta = new();
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"
                SELECT id_alerta, id_matricula, tipo_risco, score, status
                FROM alerta_risco
                WHERE id_alerta = @id";

                cmd.Parameters.AddWithValue("@id", id);

                var dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    alerta = new AlertaRisco(
                        dr.GetInt32(0),
                        dr.GetInt32(1),
                        dr.IsDBNull(2) ? null : dr.GetString(2),
                        dr.GetDecimal(3),
                        dr.IsDBNull(4) ? null : dr.GetString(4)
                    );

                    alerta.Matricula = Matricula.ObterPorId(alerta.IdMatricula);
                }

                dr.Close();
                cmd.Connection.Close();
            }

            return alerta;
        }

        // ==========================
        // LISTAR (SEM PROCEDURE)
        // ==========================
        public static List<AlertaRisco> ObterLista()
        {
            List<AlertaRisco> alertas = new();
            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"
                SELECT id_alerta, id_matricula, tipo_risco, score, status
                FROM alerta_risco";

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var alerta = new AlertaRisco(
                        dr.GetInt32(0),
                        dr.GetInt32(1),
                        dr.IsDBNull(2) ? null : dr.GetString(2),
                        dr.GetDecimal(3),
                        dr.IsDBNull(4) ? null : dr.GetString(4)
                    );

                    alerta.Matricula = Matricula.ObterPorId(alerta.IdMatricula);

                    alertas.Add(alerta);
                }

                dr.Close();
                cmd.Connection.Close();
            }

            return alertas;
        }
    }
}
