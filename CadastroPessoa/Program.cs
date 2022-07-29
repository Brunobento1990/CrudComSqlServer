using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace CadastroPessoa
{
    class Program
    {
        enum Opcao
        {
            Sair,
            Cadastrar,
            Listar,
            Atualizar,
            Excluir,
            AtualizarNovo
        }
        
        static void Main(string[] args)
        {



            Opcao opcao;
            do
            {
                Console.WriteLine("- 1 -> Cadastrar pessoa:");
                Console.WriteLine("- 2 -> Listar pessoas:");
                Console.WriteLine("- 3 -> Atualizar cadastro de pessoa:");
                Console.WriteLine("- 4 -> Excluir cadastro de pessoa:");
                Console.WriteLine("- 5 -> Atualização personalizada:");
                Console.WriteLine("- 0 -> Sair:");
                opcao = (Opcao)Convert.ToInt32(Console.ReadLine());

                if (opcao == Opcao.Cadastrar)
                {
                    
                    var pessoa = CadastrarPessoa();
                    if (pessoa != null)
                    {
                        pessoa.Gravar();
                    }
                    else
                    {
                        Console.WriteLine("-> Não foi possível finalizar o cadastro!");
                    }
                }
                if (opcao == Opcao.Listar)
                {
                    Listar();

                }
                if (opcao == Opcao.Atualizar)
                {
                    Console.WriteLine("-> Informe o Id da pessoa a ser atualizada: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Pessoa pessoa = new Pessoa();
                    pessoa = pessoa.Confirmar(id);
                    if (pessoa  != null && pessoa.Id == id)
                    {
                        Console.WriteLine("-> Informe o novo nome para " + pessoa.Nome );
                        string nome = Console.ReadLine();
                        Console.WriteLine("-> Informe o novo CPF:");
                        string cpf = Console.ReadLine();
                        Console.WriteLine("-> Informe o novo RG:");
                        string rg = Console.ReadLine();
                        pessoa.Atualizar(id, nome, cpf, rg);
                    }
                    else
                    {
                        Console.WriteLine("-> Cadastro não encontrado!");
                    }

                }
                if (opcao == Opcao.Excluir)
                {
                    Console.WriteLine("-> Informe o ID da pessoa a ser excluida:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Pessoa pessoa = new Pessoa();
                    pessoa = pessoa.Confirmar(id);
                    if (pessoa != null && pessoa.Id == id)
                    {
                        Console.WriteLine("Nome: " + pessoa.Nome);
                        Console.WriteLine("CPF: " + pessoa.Cpf);
                        Console.WriteLine("----------------------------");
                        Console.WriteLine("-> Sr usuário, digite 1 para confirmar a exclusão:");
                        int confirmacao = Convert.ToInt32(Console.ReadLine());
                        if (confirmacao == 1)
                        {

                            Excluir(id);
                        }
                        else
                        {
                            Console.WriteLine("-> Exclusão cancelada!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("-> Cadastro não encontrado!");
                    }
                }
                if (opcao == Opcao.AtualizarNovo)
                {
                    Console.WriteLine("-> Informe a campo a ser atualizado:");
                    string campo = Console.ReadLine();
                    campo = campo.ToUpper();
                    int coluna = 0;
                    if (campo == "CPF")
                    {
                        coluna = 1;
                    }
                    if (campo == "NOME")
                    {
                        coluna = 2;
                    }
                    if (campo == "RG")
                    {
                        coluna = 3;
                    }
                    else
                    {
                        Console.WriteLine("-> Campo não encontrado!");
                        return;
                    }
                    Console.WriteLine("-> Informe o valor:");
                    string valor = Console.ReadLine();
                    Console.WriteLine("-> Informe o Id do cadastro:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Pessoa pessoa = new Pessoa();
                    pessoa.AtualizaNovo(coluna, valor,id);
                }
            } while (opcao != Opcao.Sair);
        }

        static Pessoa CadastrarPessoa()
        {
            var pessoa = new Pessoa();
            Console.WriteLine("-> Informe o nome da pessoa:");
            pessoa.Nome = Console.ReadLine();
            Console.WriteLine("-> Informe o CPF:");
            pessoa.Cpf = Console.ReadLine();
            Console.WriteLine("-> Informe o RG:");
            pessoa.Rg = Console.ReadLine();
            Console.WriteLine("-> Informe a data de nascimento:");
            pessoa.Data_Nascimento = Console.ReadLine();
            Console.WriteLine("-> Informe a naturalidade:");
            pessoa.Naturalidade = Console.ReadLine();

            return pessoa;
        }

        static void Listar()
        {
            try
            {
                string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Bruno;Data Source=ITELABD03\SQLEXPRESS01";
                using (var cnn = new SqlConnection(Coneccao))
                {

                    string sql = "select Id,Nome,Cpf,Rg,Data_Nascimento,Naturalidade from Pessoa ORDER BY id";
                    SqlCommand sqlComm = new SqlCommand(sql,cnn);;
                    sqlComm.Connection.Open();
                    SqlDataReader rdr = sqlComm.ExecuteReader();


                    foreach (DbDataRecord s in rdr)
                    {
                        
                        int id = s.GetInt32(0);
                        string nome = s.GetString(1);
                        string cpf = s.GetString(2);
                        string rg = s.GetString(3);
                        string datanascimento = s.GetString(4);
                        string naturalidade = s.GetString(5);
                        Console.WriteLine("Código: "+id);
                        Console.WriteLine("Nome: "+nome);
                        Console.WriteLine("CPF: "+cpf);
                        Console.WriteLine("RG: "+rg);
                        Console.WriteLine("Data de nascimento: "+datanascimento);
                        Console.WriteLine("Naturalidade: "+naturalidade);
                        Console.WriteLine("----------------------------");
                    }


                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Errors);
            }
        }
        static void Excluir(int id)
        {

            try
            {
                string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Bruno;Data Source=ITELABD03\SQLEXPRESS01";
                using (var cnn = new SqlConnection(Coneccao))
                {

                    string sql = "delete from Pessoa where id = @id";
                    SqlCommand sqlComm = new SqlCommand(sql, cnn); 
                    sqlComm.Parameters.AddWithValue("@id", id);
                    sqlComm.Connection.Open();
                    sqlComm.ExecuteNonQuery();
                    Console.WriteLine("-> Cadastro excluido!");

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Errors);
            }
        }
    }
}
