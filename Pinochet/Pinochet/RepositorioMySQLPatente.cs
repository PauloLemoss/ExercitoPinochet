using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinochet
{
    public class RepositorioMySQLPatente
    {
        MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=exercito;Uid=root;Pwd=kabuterimon12;");

        public void Inserir(Patente patente)
        {
            try
            {
                conexao.Open();
                MySqlCommand comando = new MySqlCommand(string.Format($"INSERT INTO patente (NomePatente) VALUES (@NomePatente)"), conexao);
                comando.Parameters.AddWithValue("@NomePatente", patente.NomePatente);
                comando.Parameters.AddWithValue("@IdPatente", patente.IdPatente);
                comando.ExecuteNonQuery();
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
                conexao.Open();
                MySqlCommand comando = new MySqlCommand(string.Format($"UPDATE patente SET NomePatente = @NomePatente WHERE id_Patente = @Id"), conexao);
                comando.Parameters.AddWithValue("@Id", patente.IdPatente);
                comando.Parameters.AddWithValue("@Id", patente.NomePatente);
                comando.ExecuteNonQuery();
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

        public void Deletar(int id)
        {
            try
            {
                conexao.Open();
                MySqlCommand comando = new MySqlCommand("DELETE FROM patente WHERE id_Patente = @Id", conexao);
                comando.Parameters.AddWithValue("@Id", id);
                comando.ExecuteNonQuery();
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

        public List<Patente> Listar(string nome = null)
        {

            List<Patente> patentes = new List<Patente>();

            try
            {
                // Abrindo a conexão com o banco de dados;
                conexao.Open();
                // Instanciando um "Command" com o código SQL e a Conexão estabelecida anteriormente
                MySqlCommand comando = null;
                // Comando SQL em caso do parâmetro "Nome" ser NULL
                if (nome == null)
                {
                    // Comando retornando todos os clientes ser "WHERE"
                    comando = new MySqlCommand("SELECT * FROM patente", conexao);
                }
                // Comando SQL em caso do parâmetro "Nome" não ser NULL
                else
                {

                    // Nome é igual ao informado pelo parâmetro
                    comando = new MySqlCommand("SELECT * FROM patente WHERE NomePatente LIKE @NomePatente", conexao);
                    // Adicionando parâmetro SQL para o Nome
                    comando.Parameters.AddWithValue("@NomePatente", string.Format($"%{nome}%"));
                }
                // Executando (Efetivando) o comando criando anteriormente e salvando os dados no Data Reader
                MySqlDataReader reader = comando.ExecuteReader();
                // Em posse do Data Reader, vou ler os dados, sempre do primeiro até o último e "pra frente"
                while (reader.Read())
                {
                    
                    Patente patente = new Patente();
                    // Buscando a informação ID do Banco de dados e salvando no atributo correspondente
                    patente.IdPatente = reader.GetInt32("Id");
                    // Buscando a informação Nome do Banco de dados e salvando no atributo correspondente
                    patente.NomePatente = reader.GetString("Nome");
                    Console.WriteLine(patente.IdPatente);
                    Console.WriteLine(patente.NomePatente);

                }
                Console.WriteLine(patentes.ToString());
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
            // Bloco do Try: Tentando abrir conexão e realizar comando no Banco de Dados
            try
            {
                // Abrindo a conexão com o banco de dados;
                conexao.Open();
                // Instanciando um "Command" com o código SQL e a Conexão estabelecida anteriormente
                MySqlCommand comando = new MySqlCommand("SELECT COUNT(*) FROM patente", conexao);
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
