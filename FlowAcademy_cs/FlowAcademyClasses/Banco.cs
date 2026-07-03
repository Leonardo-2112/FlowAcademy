using MySql.Data.MySqlClient;


namespace FlowAcademyClasses

{
    public class Banco
    {
        public static string? StrConn { get; set; }

        public static MySqlCommand Abrir(string strconn = "")
        {
            MySqlCommand cmd = new MySqlCommand();

            StrConn = strconn;

            if (strconn == string.Empty)
                StrConn = $@"host=10.91.47.67;database=flow_academy;user=root;password=P@ssw0rd";

            MySqlConnection cn = new(StrConn);
            try
            {
                cn.Open();
                cmd.Connection = cn;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return cmd;

        }
    }
}
