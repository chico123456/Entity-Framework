using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace MeuProjetoPET
    {
        class Program
        {
            static void Main(string[] args)
            {
                //variável para conexão com o banco
                using (var banco = new Contexto())
                {
                    //Guardar os dados em variáveis
                    Console.Write("Informe o nome: ");
                    var nome = Console.ReadLine();
                    Console.Write("Informe o telefone: ");
                    var telefone = Console.ReadLine();
                    
                    //Colocar os dados na tabela Pessoas criada
                    var pessoa = new Pessoa { PessoaNome = nome, PessoaFone = telefone };
                    banco.Pessoas.Add(pessoa);
                    banco.SaveChanges();

                    //Variável de consulta para retornar as pessoas cadastradas
                    var query = from x in banco.Pessoas
                                orderby x.PessoaNome
                                select x;

                    Console.WriteLine("Tabela Pessoas");
                    //Lista as pessoas na tabela Pessoas
                    foreach (var item in query)
                    {
                        Console.WriteLine("Nome: " + item.PessoaNome+"\nTelefone: "+item.PessoaFone);
                    }

                    Console.WriteLine("Clique para sair");
                    Console.ReadKey();
                }
            }
            public class Pessoa
            {
                public int PessoaId { get; set; }
                public string PessoaNome { get; set; }
                public string PessoaFone { get; set; }
            }
            public class Contexto : DbContext
            {   
                //Define qual conexão será utilizada
                public Contexto() : base("name = MinhaConexao") { }
                //Interliga a classe Pessoa com o Contexto, gerando uma tabela Pessoas
                public DbSet<Pessoa> Pessoas { get; set; }
            }
        }
    }
