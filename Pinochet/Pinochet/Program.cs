using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinochet
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                // Perguntar qual é a ação que o usuário deseja
                Console.WriteLine("Escolha uma opção:");
                // Mudando a cor de fundo do texto para Azul
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("_______________________ Patente __________________________________");
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("1 - Inserir Patente");
                Console.WriteLine("2 - Deletar Patente");
                Console.WriteLine("3 - Listar Patente");
                Console.WriteLine("4 - Consultar Patente");
                Console.WriteLine("5 - Alterar Patente");
                Console.WriteLine("6 - Contar Patente");
                Console.ResetColor();
                //Istanciando o meu repositório
                RepositorioMySQLPatente repositorio = new RepositorioMySQLPatente();
                try
                {
                    switch (Console.ReadKey().KeyChar)
                    {
                        case '1':
                            Console.Clear();

                            // Instanciar a patente
                            Patente patente = new Patente();
                            
                            Console.WriteLine("Informe o nome da patente:");
                           
                            patente.NomePatente = Console.ReadLine();


                            repositorio.Inserir(patente);
                            break;
                        case '2':
                            Console.Clear();
                            // Listando todos os clientes retornados pela lista de cliente através do método repositorio.Listar()
                            //ListarPatentes(repositorio);
                            // Perguntar ao usuário qual é o ID que ele deseja deletar
                            Console.WriteLine("Informe o ID do usuário que você deseja deletar:");
                            // Chamar o método deletar, passando o ID informado pelo usuário
                            repositorio.Deletar(int.Parse(Console.ReadLine()));
                            break;
                        case '3':
                            Console.Clear();
                            // Listando todos os clientes retornados pela lista de cliente através do método repositorio.Listar()
                            repositorio.Listar();
                            break;
                        case '4':
                            Console.Clear();
                            // Perguntar o nome que deseja consultar
                            Console.WriteLine("Informe o nome que deseja consultar:");
                            // Listando todos os clientes retornados pela lista de cliente através do método repositorio.Listar()
                            foreach (Patente patenteDaVez in repositorio.Listar(Console.ReadLine()))
                            {
                                // Imprimindo as informações de cada  na tela, utilizando o "ToString" que foi sobrescrito na classe
                                // Cliente ...
                                Console.WriteLine(patenteDaVez.ToString());
                            }
                            break;
                        case '5':
                            Console.Clear();
                            // Perguntar o nome que deseja consultar
                            Console.WriteLine("Informe o nome que deseja alterar:");
                            // Obtendo a lista de Clientes
                            List<Patente> listaDePatentes = repositorio.Listar(Console.ReadLine());
                            // Listando todos os clientes retornados pela lista de cliente através do método repositorio.Listar()
                            foreach (Patente patenteDaVez in listaDePatentes)
                            {
                                // Imprimindo as informações de cada cliente na tela, utilizando o "ToString" que foi sobrescrito na classe
                                // Cliente ...
                                Console.WriteLine(patenteDaVez.ToString());
                            }
                            if (listaDePatentes != null && listaDePatentes.Count > 0)
                            {
                                // Solicitar o ID de qual dos clientes listados deseja alterar:
                                Console.WriteLine("Informe, pelo ID, qual das patentes acima você deseja alterar:");
                                // Buscar, na lista de Clientes, pelo Id, o objeto cliente relacionado ...
                                Patente patenteAlterar = listaDePatentes.FindLast(c => c.IdPatente == int.Parse(Console.ReadLine()));
                                // Solicitar o novo nome
                                Console.WriteLine($"Informe o nome nome para {patenteAlterar.NomePatente}:");
                                // Alterando a Propriedade Nome do Cliente encontrado ...
                                patenteAlterar.NomePatente = Console.ReadLine();
                                // Efetivando a alteração no Banco de Dados
                                repositorio.Alterar(patenteAlterar);
                            }
                            else
                            {
                                // Caso a lista de Clientes não retorne nenhum registro, exibir mensagem abaixo.
                                Console.WriteLine("Nenhum registro encontrado ...");
                            }
                            break;
                        case '6':
                            Console.Clear();
                            // Informando a quantidade de Clientes registrados, através do método Obter Quantidade ...
                            Console.WriteLine($"Total de patentes registrados: {repositorio.ObterQuantidadeDePatentes()}");
                            break;
                        case '7':
                            Console.Clear();
                            
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Opção Indisponível!");
                            break;
                    }
                }
                catch (MySqlException ex)
                {
                    // Mudando a cor do texto para Vermelho (Informações Críticas)
                    Console.ForegroundColor = ConsoleColor.Red;
                    // Exibindo mensagens de erro de banco de dados
                    Console.WriteLine("Ocorreu um erro ao tentar realizar a Operação no Banco de Dados.");
                    Console.WriteLine("Contacte o suporte.");
                    // Voltando a cor do texto para a cor padrão
                    Console.ResetColor();
                }
                catch (FormatException ex)
                {
                    // Mudando a cor do texto para Amarelo (Informações Importantes)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    // Eixibindo mensagem de erro de Formato
                    Console.WriteLine("Alguma das informações não estava no formato correto.");
                    Console.WriteLine("Tente novamente, inserindo as informações corretas.");
                    // Voltando a cor do texto para a cor padrão
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    // Mudando a cor do texto para Vermelho (Informações Críticas)
                    Console.ForegroundColor = ConsoleColor.Red;
                    // Exibindo mensagens genéricas ...
                    Console.WriteLine("Ocorreu um erro.");
                    Console.WriteLine("Contacte o suporte.");
                    // Voltando a cor do texto para a cor padrão
                    Console.ResetColor();
                }
                Console.WriteLine("Você deseja continuar? s p/ sim ...");
            } while (Console.ReadKey().KeyChar == 's');
        }

        public static void ListarPatentes(RepositorioMySQLPatente repositorio)
        {
            foreach (Patente patenteDaVez in repositorio.Listar())
            {
                
                Console.WriteLine(patenteDaVez.ToString());
            }
        }
    }         
}
