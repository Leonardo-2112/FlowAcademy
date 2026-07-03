using System;
using System.Collections.Generic;
using System.Data;

namespace FlowAcademyClasses
{
    public class Pagamento
    {
        // ==========================
        // PROPRIEDADES
        // ==========================
        public int IdPagamento { get; set; }
        public int IdAluno { get; set; }
        public decimal Valor { get; set; }
        public DateTime Vencimento { get; set; }
        public string? Status { get; set; }
        public string? NomeAluno { get; set; }

        public Aluno? Aluno { get; set; }

        // ==========================
        // CONSTRUTORES
        // ==========================
        public Pagamento()
        { 
            IdPagamento = 0;
            IdAluno = 0;
            Valor = 0;
            Vencimento = DateTime.Now;
            Status = "pendente";
        }

        public Pagamento(int idPagamento)
        {
            IdPagamento = idPagamento;
        }

        public Pagamento(int idPagamento, int idAluno, decimal valor, DateTime vencimento, string? status)
        {
            IdPagamento = idPagamento;
            IdAluno = idAluno;
            Valor = valor;
            Vencimento = vencimento;
            Status = status;
        }

        // ==========================
        // INSERIR
        // ==========================
        public bool Inserir()
        {
            bool inserido = false;

            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_inserir_pagamento";

                cmd.Parameters.AddWithValue("p_id_aluno", IdAluno);
                cmd.Parameters.AddWithValue("p_valor", Valor);
                cmd.Parameters.AddWithValue("p_vencimento", Vencimento.Date);
                cmd.Parameters.AddWithValue("p_status", Status ?? "pendente");

                IdPagamento = Convert.ToInt32(cmd.ExecuteScalar());
                inserido = IdPagamento > 0;

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

            if (IdPagamento < 1)
                return false;

            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_atualizar_pagamento";

                cmd.Parameters.AddWithValue("p_id_pagamento", IdPagamento);
                cmd.Parameters.AddWithValue("p_id_aluno", IdAluno);
                cmd.Parameters.AddWithValue("p_valor", Valor);
                cmd.Parameters.AddWithValue("p_vencimento", Vencimento.Date);
                cmd.Parameters.AddWithValue("p_status", Status ?? "pendente");

                if (cmd.ExecuteNonQuery() > 0)
                    atualizado = true;

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

            if (IdPagamento < 1)
                return false;

            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_excluir_pagamento";

                cmd.Parameters.AddWithValue("p_id_pagamento", IdPagamento);

                if (cmd.ExecuteNonQuery() > 0)
                    excluido = true;

                cmd.Connection.Close();
            }

            return excluido;
        }

        // ==========================
        // OBTER POR ID (SQL PADRÃO)
        // ==========================
        public static Pagamento ObterPorId(int idPagamento)
        {
            Pagamento pagamento = new();

            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"
                    SELECT p.id_pagamento,
                           p.id_aluno,
                           p.valor,
                           p.vencimento,
                           p.status
                    FROM pagamentos p
                    WHERE p.id_pagamento = @id";

                cmd.Parameters.AddWithValue("@id", idPagamento);

                var dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    pagamento = new Pagamento(
                        dr.GetInt32(0),
                        dr.GetInt32(1),
                        dr.GetDecimal(2),
                        dr.GetDateTime(3),
                        dr.IsDBNull(4) ? "pendente" : dr.GetString(4)
                    );
                }

                dr.Close();
                cmd.Connection.Close();
            }

            return pagamento;
        }

        // ==========================
        // OBTER LISTA (SQL PADRÃO)
        // ==========================
        public static List<Pagamento> ObterLista(string busca = "")
        {
            List<Pagamento> lista = new();

            var cmd = Banco.Abrir();

            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"
                    SELECT p.id_pagamento,
                           p.id_aluno,
                           p.valor,
                           p.vencimento,
                           p.status,
                           u.nome AS nome_aluno
                    FROM pagamentos p
                    INNER JOIN alunos a ON a.id_aluno = p.id_aluno
                    INNER JOIN usuarios u ON u.id_usuario = a.id_usuario
                    WHERE u.nome LIKE @busca
                    ORDER BY p.vencimento";

                cmd.Parameters.AddWithValue("@busca", "%" + busca + "%");

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Pagamento pagamento = new Pagamento(
                        dr.GetInt32(0),
                        dr.GetInt32(1),
                        dr.GetDecimal(2),
                        dr.GetDateTime(3),
                        dr.IsDBNull(4) ? "pendente" : dr.GetString(4)
                    );

                    if (!dr.IsDBNull(5))
                        pagamento.NomeAluno = dr.GetString(5);

                    lista.Add(pagamento);
                }

                dr.Close();
                cmd.Connection.Close();
            }

            return lista;
        }
    }
}
