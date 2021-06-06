using MySql.Data.MySqlClient;

namespace Pinochet
{
    class ConexaoDb
    {
        string conecta = "Server=localhost;Port=3306;Database=exercito;Uid=root;Pwd=kabuterimon12;";
        public MySqlConnection con = null;

        public void AbirConexao(){
            try{
                 con = new MySqlConnection(conecta);
                 con.Open();

            }
            catch(Exception ex){
                throw ex;
            }

        }

         public void FecharConexao(){
             try{
                 con = new MySqlConnection(conecta);
                 con.Close();

            }
            catch(Exception ex){
                throw ex;
            }
        }
    }
}