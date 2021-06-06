using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinochet
{
    public class RepositorioMySQLPatente
    {    ConexaoDb conexao= new Conexao();
         string sql;
          MySqlCommand comando;
         
       
        public void Inserir(Patente patente)
        {
            try
            {
                conexao.AbirConexao();
                sql="INSERT INTO patente (NomePatente) VALUES (@NomePatente)";
                comando = new MySqlCommand(sql, conexao.con);
                comando.Parameters.AddWithValue("@NomePatente", patente.NomePatente);
                //comando.Parameters.AddWithValue("@IdPatente", patente.IdPatente);
                comando.ExecuteNonQuery();
                conexao.Fechar();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                conexao.Close();
            }
        }
        public void Alterar(Patente patente)
        {
            try
            {
                conexao.AbirConexao();
                sql = "UPDATE patente SET NomePatente = @NomePatente WHERE id_Patente = @Id";
                MySqlCommand comando = new MySqlCommand(sql, conexao.con);
                comando.Parameters.AddWithValue("@Id", patente.IdPatente);
                comando.Parameters.AddWithValue("@NomePatente", patente.NomePatente);
                comando.ExecuteNonQuery();
                conexao.Fechar();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                conexao.FecharConexao();
            }
        }

        public void Deletar(int id)
        {
            try
            {
                conexao.AbirConexao();
                sql = "DELETE FROM patente WHERE id_Patente = @Id";
                 MySqlCommand comando = new MySqlCommand(sql, conexao.con);
                comando.Parameters.AddWithValue("@Id", id);
                comando.ExecuteNonQuery();
                conexao.Fechar();
                Console.WriteLine("Registro excluido com sucesso");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }

            finally
            {
                conexao.Close();
            }
        }

        public void Listar(string nome = null)
        {

        
            try
            {
                conexao.Open();
               
                
                if (nome == null)
                {
                    // Comando retornando todos os clientes ser "WHERE"
                    sql = "SELECT * FROM patente order by NomePatente";
                    comando = new MySqlCommand(sql, conexao.con);
                }
               
                else
                {
                    sql="SELECT * FROM patente WHERE NomePatente LIKE @NomePatente";
                    comando = new MySqlCommand(sql, conexao);
                    comando.Parameters.AddWithValue("@NomePatente", string.Format($"%{nome}%"));



                }
               
                MySqlDataReader reader = comando.ExecuteReader();
                
                if (reader.HasRows){
                    while (reader.Read())
                    {
                       Console.WriteLine(reader.GetInt32("Id")); 
                       Console.WriteLine(reader.GetString("Nome"));
                       
                    }
                  }
                  else{
                      Console.WriteLine("Registro Nao encontrados");

                  }
             
                
                    
                    
            }
            // Tratando possíveis erros ...
            catch (Exception ex)
            {
                // Escalando o erro para a classe que chama este método
                throw ex;
            }
            // Conseguindo ou não realizar as opções, executar o Finally (Fechando a conexão)
            finally
            {
                // Fechando a conexão com o banco de dados (Tem que fechar sempre);
                conexao.Close();
            }
            // Retornando a lista de clientes (agora totalmente preenchida)
            return patentes;
        }

        public long ObterQuantidadeDePatentes()
        {
            long quantidade = 0;
            conexao.AbirConexao();
            sql = "SELECT COUNT(*) FROM patente";
          
            try
            {
                // Abrindo a conexão com o banco de dados;
                conexao.Open();
                // Instanciando um "Command" com o código SQL e a Conexão estabelecida anteriormente
                MySqlCommand comando = new MySqlCommand(sql, conexao.con);
                // Executando (Efetivando) o comando criando anteriormente e salvando os dados inteiro "quantidade"
                quantidade = (long)comando.ExecuteScalar();
            }
            // Tratando possíveis erros ...
            catch (Exception ex)
            {
                // Escalando o erro para a classe que chama este método
                throw ex;
            }
            // Conseguindo ou não realizar as opções, executar o Finally (Fechando a conexão)
            finally
            {
                // Fechando a conexão com o banco de dados (Tem que fechar sempre);
                conexao.Close();
            }
            // Retornando a quantidade de patentes
            return quantidade;
        }
    }
}
