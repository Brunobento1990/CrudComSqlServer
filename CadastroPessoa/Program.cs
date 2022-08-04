using System;
using System.Collections.Generic;
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
            AtualizarNovo,
            CadastrarTelefone,
            AtualizarTelefone,
            ExcluirTelefone,
            ListarTelefone,
            ListarTelefonePorPessoa,
            MostrarQuantidadeDeTelefone,
            MostraQuantidadeDeTelefonePorPessoa
        }
        List<Pessoa> pessoas = new List<Pessoa>(); 

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
                Console.WriteLine("- 6 -> Cadastrar telefone:");
                Console.WriteLine("- 7 -> Atualizar telefone:");
                Console.WriteLine("- 8 -> Excluir telefone:");
                Console.WriteLine("- 9 -> Listar todos os telefones:");
                Console.WriteLine("- 10 -> Listar todos os telefones por pessoa:");
                Console.WriteLine("- 11 -> Listar quantidade de telefones cadastrados:");
                Console.WriteLine("- 12 -> Listar quantidade de telefones cadastrados por pessoa:");
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
                    var pessoa = AcharPessoaPeloId(id);
                    if (pessoa != null && pessoa.Id == id)
                    {
                        Console.WriteLine("-> Informe o novo nome para " + pessoa.Nome);
                        pessoa.Nome = Console.ReadLine();
                        Console.WriteLine("-> Informe o novo CPF:");
                        pessoa.Cpf = Console.ReadLine();
                        Console.WriteLine("-> Informe o novo RG:");
                        pessoa.Rg = Console.ReadLine();
                        pessoa.Atualizar(pessoa.Id);
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
                    var pessoa = AcharPessoaPeloId(id);
                    if (pessoa != null && pessoa.Id == id)
                    {
                        Console.WriteLine("Nome: " + pessoa.Nome);
                        Console.WriteLine("CPF: " + pessoa.Cpf);
                        Console.WriteLine("----------------------------");
                        Console.WriteLine("-> Sr usuário, digite 1 para confirmar a exclusão:");
                        int confirmacao = Convert.ToInt32(Console.ReadLine());
                        if (confirmacao == 1)
                        {
                            var telefone = AcharTelefonePeloIdPessoa(id);
                            if (telefone != null)
                            {
                                ExcluirTelefoneIdPessoa(telefone.IdPessoa);
                                ExcluirPessoa(id);
                            }
                            
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
                    if (campo != "RG" && campo != "CPF" && campo != "NOME")
                    {
                        Console.WriteLine("-> Campo não encontrado!");
                        return;
                    }
                    Console.WriteLine("-> Informe o valor:");
                    string valor = Console.ReadLine();
                    Console.WriteLine("-> Informe o Id do cadastro:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Pessoa pessoa = new Pessoa();
                    pessoa.AtualizarPersonalizado(coluna, valor, id);
                }
                if (opcao == Opcao.CadastrarTelefone)
                {
                    var telefone = new Telefone();
                    Console.WriteLine("-> Informe o nome da pessoa para vincular ao telefone:");
                    string nome = Console.ReadLine();
                    var pessoa = AcharPessoaPeloNome(nome);
                    if (pessoa == null)
                    {
                        Console.WriteLine("-> Informe um nome mais expecífico!");
                        nome = Console.ReadLine();
                        pessoa = AcharPessoaPeloNome(nome);
                    }
                    else
                    {

                        Console.WriteLine("-> Informe o DDD:");
                        telefone.DDD = Console.ReadLine();
                        Console.WriteLine("-> Informe o telefone:");
                        telefone.Numero = Console.ReadLine();
                        telefone.IdPessoa = pessoa.Id;
                        telefone.Gravar();
                    }

                }
                if (opcao == Opcao.AtualizarTelefone)
                {
                    Console.WriteLine("-> Informe o nome da pessoa para atualizar o telefone:");
                    string nome = Console.ReadLine();
                    var pessoa = AcharPessoaPeloNome(nome);
                    if (pessoa != null)
                    {
                        Console.WriteLine("-> Informe o id do telefone:");
                        int idtel = Convert.ToInt32(Console.ReadLine());

                        var telefone = AcharTelefonePeloId(idtel);
                        if (telefone != null)
                        {
                            Console.WriteLine("-> Informe o novo DDD:");
                            telefone.DDD = Console.ReadLine();
                            Console.WriteLine("-> Informe o novo número:");
                            telefone.Numero = Console.ReadLine();
                            telefone.Atualizar(telefone.Id);
                        }
                        else
                        {
                            Console.WriteLine("-> Cadastro não encontrado!");
                        }

                    }
                    else
                    {
                        Console.WriteLine("-> Cadastro não encontrado!");
                    }
                }
                if (opcao == Opcao.ExcluirTelefone)
                {
                    Console.WriteLine("-> Informe o Id do telefone:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var telefone = AcharTelefonePeloId(id);
                    if (telefone != null)
                    {
                        ExcluirTelefone(id);
                    }
                    else
                    {
                        Console.WriteLine("Cadastro não encontrado!");
                    }
                }
                if (opcao == Opcao.ListarTelefone)
                {
                    ListarTelefones();
                }
                if (opcao == Opcao.ListarTelefonePorPessoa)
                {
                    Console.WriteLine("-> Informe o Id da pessoa:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var pessoa = AcharPessoaPeloId(id);
                    if (pessoa != null)
                    {
                        ListarTelefonePorPessoa(id,pessoa.Nome);
                    }
                    else
                    {
                        Console.WriteLine("Cadastro não encontrado!");
                    }
                }
                if (opcao == Opcao.MostrarQuantidadeDeTelefone)
                {
                    int quantidade = QuantidadeDeTelefones();
                    if (quantidade > 0)
                    {
                        Console.WriteLine("-> Quantidade de telefones cadastrados: "+ quantidade);
                    }
                    else
                    {
                        Console.WriteLine("-> Não há cadastros de telefones");
                    }
                }
                if (opcao == Opcao.MostraQuantidadeDeTelefonePorPessoa)
                {
                    Console.WriteLine("-> Informe o Id da pessoa:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var pessoa = AcharPessoaPeloId(id);
                    if (pessoa != null)
                    {
                        int quantidade = QuantidadeDeTelefonePorPessoa(id);
                        if (quantidade > 0)
                        {
                            Console.WriteLine("-> Quantidade de telefones cadastrados para a pessoa : " + pessoa.Nome +" : "+ quantidade);
                        }
                    }
                    else
                    {
                        Console.WriteLine("-> Cadastro não encontrado:");
                    }
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
                    SqlCommand sqlComm = new SqlCommand(sql,cnn);
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
        static void ExcluirPessoa(int id)
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
        static Pessoa AcharPessoaPeloNome(string nome)
        {
            var pessoa = new Pessoa();

            try
            {
                string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Bruno;Data Source=ITELABD03\SQLEXPRESS01";
                using (var cnn = new SqlConnection(Coneccao))
                {

                    string sql = "select * from Pessoa where Nome = @nome";
                    SqlCommand sqlComm = new SqlCommand(sql, cnn);
                    sqlComm.Parameters.AddWithValue("@nome", nome);
                    sqlComm.Connection.Open();
                    sqlComm.ExecuteNonQuery();
                    SqlDataReader rdr = sqlComm.ExecuteReader();
                    rdr.Read();
                    pessoa.Id = rdr.GetInt32(0);
                    pessoa.Nome = rdr.GetString(1);
                    pessoa.Cpf = rdr.GetString(2);
                    pessoa.Rg = rdr.GetString(3);
                    pessoa.Data_Nascimento = rdr.GetString(4);
                    pessoa.Naturalidade = rdr.GetString(5);

                }
                return pessoa;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        static Pessoa AcharPessoaPeloId(int id)
        {
            var pessoa = new Pessoa();
            try
            {
                string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Bruno;Data Source=ITELABD03\SQLEXPRESS01";
                using (var cnn = new SqlConnection(Coneccao))
                {

                    string sql = "select * from Pessoa where id = @id";
                    SqlCommand sqlComm = new SqlCommand(sql, cnn);
                    sqlComm.Parameters.AddWithValue("@id", id);
                    sqlComm.Connection.Open();
                    sqlComm.ExecuteNonQuery();
                    SqlDataReader rdr = sqlComm.ExecuteReader();
                    rdr.Read();
                    pessoa.Id = rdr.GetInt32(0);
                    pessoa.Nome = rdr.GetString(1);
                    pessoa.Cpf = rdr.GetString(2);
                    pessoa.Rg = rdr.GetString(3);
                    pessoa.Data_Nascimento = rdr.GetString(4);
                    pessoa.Naturalidade = rdr.GetString(5);

                }
                return pessoa;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }


        }
        static Telefone AcharTelefonePeloId(int id)
        {
            var telefone = new Telefone();

            try
            {
                string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Bruno;Data Source=ITELABD03\SQLEXPRESS01";
                using (var cnn = new SqlConnection(Coneccao))
                {

                    string sql = "select * from Telefone where Id = @id";
                    SqlCommand sqlComm = new SqlCommand(sql, cnn);
                    sqlComm.Parameters.AddWithValue("@id", id);
                    sqlComm.Connection.Open();
                    sqlComm.ExecuteNonQuery();
                    SqlDataReader rdr = sqlComm.ExecuteReader();
                    rdr.Read();
                    telefone.Id = rdr.GetInt32(0);
                    telefone.DDD = rdr.GetString(1);
                    telefone.Numero = rdr.GetString(2);
                    telefone.IdPessoa = rdr.GetInt32(3);
                }
                return telefone;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }
        static void ExcluirTelefone(int id)
        {
            try
            {
                string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Bruno;Data Source=ITELABD03\SQLEXPRESS01";
                using (var cnn = new SqlConnection(Coneccao))
                {

                    string sql = "delete from Telefone where id = @id";
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
        static void ListarTelefones()
        {
            try
            {
                string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Bruno;Data Source=ITELABD03\SQLEXPRESS01";
                using (var cnn = new SqlConnection(Coneccao))
                {

                    string sql = "select t.Id,t.DDD,t.Numero,t.IdPessoa,p.Nome from Telefone t , Pessoa p where t.IdPessoa = p.Id";
                    SqlCommand sqlComm = new SqlCommand(sql, cnn);
                    sqlComm.Connection.Open();
                    SqlDataReader rdr = sqlComm.ExecuteReader();

                    foreach (DbDataRecord s in rdr)
                    {

                        int id = s.GetInt32(0);
                        string ddd = s.GetString(1);
                        string numero = s.GetString(2);
                        int IdPessoa = s.GetInt32(3);
                        string nome = s.GetString(4);
                        Console.WriteLine("Nome: " + nome);
                        Console.WriteLine("DDD: " + ddd);
                        Console.WriteLine("Número : " + numero);
                        Console.WriteLine("----------------------------");
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Errors);
            }
        }
        static void ListarTelefonePorPessoa(int idPessoa,string nome)
        {
            try
            {
                string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Bruno;Data Source=ITELABD03\SQLEXPRESS01";
                using (var cnn = new SqlConnection(Coneccao))
                {

                    string sql = "select t.Id,t.DDD,t.Numero,t.IdPessoa,p.Nome from Telefone t , Pessoa p where t.IdPessoa = @idPessoa and p.Nome = @nome";
                    SqlCommand sqlComm = new SqlCommand(sql, cnn);
                    sqlComm.Parameters.AddWithValue("@idPessoa",idPessoa);
                    sqlComm.Parameters.AddWithValue("@nome",nome);
                    sqlComm.Connection.Open();
                    SqlDataReader rdr = sqlComm.ExecuteReader();

                    foreach (DbDataRecord s in rdr)
                    {

                        int id = s.GetInt32(0);
                        string ddd = s.GetString(1);
                        string numero = s.GetString(2);
                        int IdPessoa = s.GetInt32(3);
                        string nome1 = s.GetString(4);
                        Console.WriteLine("Nome: " + nome1);
                        Console.WriteLine("DDD: " + ddd);
                        Console.WriteLine("Número : " + numero);
                        Console.WriteLine("----------------------------");
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Cadastro não encontrado!" + e.Errors);
            }
        }
        static Telefone AcharTelefonePeloIdPessoa(int id)
        {
            var telefone = new Telefone();

            try
            {
                string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Bruno;Data Source=ITELABD03\SQLEXPRESS01";
                using (var cnn = new SqlConnection(Coneccao))
                {

                    string sql = "select * from Telefone where IdPessoa = @id";
                    SqlCommand sqlComm = new SqlCommand(sql, cnn);
                    sqlComm.Parameters.AddWithValue("@id", id);
                    sqlComm.Connection.Open();
                    sqlComm.ExecuteNonQuery();
                    SqlDataReader rdr = sqlComm.ExecuteReader();
                    rdr.Read();
                    telefone.Id = rdr.GetInt32(0);
                    telefone.DDD = rdr.GetString(1);
                    telefone.Numero = rdr.GetString(2);
                    telefone.IdPessoa = rdr.GetInt32(3);
                }
                return telefone;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }
        static void ExcluirTelefoneIdPessoa(int id)
        {
            try
            {
                string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Bruno;Data Source=ITELABD03\SQLEXPRESS01";
                using (var cnn = new SqlConnection(Coneccao))
                {

                    string sql = "delete from Telefone where IdPessoa = @id";

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
        static int QuantidadeDeTelefones()
        {
            int quantidade = 0;

            try
            {
                string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Bruno;Data Source=ITELABD03\SQLEXPRESS01";
                using (var cnn = new SqlConnection(Coneccao))
                {

                    string sql = "SELECT COUNT(id)FROM Telefone";
                    SqlCommand sqlComm = new SqlCommand(sql, cnn);
                    sqlComm.Connection.Open();
                    sqlComm.ExecuteNonQuery();
                    SqlDataReader rdr = sqlComm.ExecuteReader();
                    rdr.Read();
                    quantidade = rdr.GetInt32(0);

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Errors);
            }

            return quantidade;
        }
        static int QuantidadeDeTelefonePorPessoa(int id)
        {
            int quantidade = 0;

            try
            {
                string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Bruno;Data Source=ITELABD03\SQLEXPRESS01";
                using (var cnn = new SqlConnection(Coneccao))
                {

                    string sql = "SELECT COUNT(IdPessoa)FROM Telefone where IdPessoa = @id";
                    SqlCommand sqlComm = new SqlCommand(sql, cnn);
                    sqlComm.Parameters.AddWithValue("@id",id);
                    sqlComm.Connection.Open();
                    sqlComm.ExecuteNonQuery();
                    SqlDataReader rdr = sqlComm.ExecuteReader();
                    rdr.Read();
                    quantidade = rdr.GetInt32(0);

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Errors);
            }

            return quantidade;
        }
    }
}
