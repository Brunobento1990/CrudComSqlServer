using Microsoft.OData.Edm;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CadastroPessoa
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Data_Nascimento { get; set; }
        public string Naturalidade { get; set; }
        public Pessoa() { }
        public void Gravar()
        {
            try
            {
                string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Bruno;Data Source=ITELABD03\SQLEXPRESS01";
                using (var cnn = new SqlConnection(Coneccao))
                {


                    string sql = "INSERT INTO Pessoa (Nome,Cpf,Rg,Data_Nascimento,Naturalidade) " +
                        "VALUES(@nome,@cpf,@rg,@data_nascimento,@naturalidade)";

                    SqlCommand sqlComm = new SqlCommand(sql, cnn);
                    sqlComm.Parameters.AddWithValue("@nome", this.Nome);
                    sqlComm.Parameters.AddWithValue("@cpf", this.Cpf);
                    sqlComm.Parameters.AddWithValue("@rg", this.Rg);
                    sqlComm.Parameters.AddWithValue("@data_nascimento", this.Data_Nascimento);
                    sqlComm.Parameters.AddWithValue("@naturalidade", this.Naturalidade);
                    sqlComm.Connection.Open();
                    sqlComm.ExecuteNonQuery();
                    Console.WriteLine("-> Cadastro efetuado com sucesso!");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Errors);
            }

        }
        public void Atualizar(int id)
        {
            try
            {
                string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Bruno;Data Source=ITELABD03\SQLEXPRESS01";
                using (var cnn = new SqlConnection(Coneccao))
                {

                    string sql = "update Pessoa set Nome = @nome1 ,Cpf = @cpf, Rg = @rg where id = @id";
                    SqlCommand sqlComm = new SqlCommand(sql, cnn); ;
                    sqlComm.Parameters.AddWithValue("@nome", this.Nome);
                    sqlComm.Parameters.AddWithValue("@Cpf", this.Cpf);
                    sqlComm.Parameters.AddWithValue("@rg", this.Rg);
                    sqlComm.Parameters.AddWithValue("@id", id);
                    sqlComm.Connection.Open();
                    sqlComm.ExecuteNonQuery();
                    Console.WriteLine("-> Atualização concluída!");

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Errors);
            }

        }
        public void AtualizarPersonalizado(int campo,string valor,int id)
        {
            try
            {
                string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Bruno;Data Source=ITELABD03\SQLEXPRESS01";
                using (var cnn = new SqlConnection(Coneccao))
                {
                    if (campo == 1)
                    {
                        string sql = "update Pessoa set Cpf = @valor where id = @id";
                        SqlCommand sqlComm = new SqlCommand(sql, cnn); ;
                        sqlComm.Parameters.AddWithValue("@campo", campo);
                        sqlComm.Parameters.AddWithValue("@valor", valor);
                        sqlComm.Parameters.AddWithValue("@id", id);
                        sqlComm.Connection.Open();
                        sqlComm.ExecuteNonQuery();
                        Console.WriteLine("-> Atualização concluída!");
                    }
                    if (campo == 2)
                    {
                        string sql = "update Pessoa set Nome = @valor where id = @id";
                        SqlCommand sqlComm = new SqlCommand(sql, cnn); ;
                        sqlComm.Parameters.AddWithValue("@campo", campo);
                        sqlComm.Parameters.AddWithValue("@valor", valor);
                        sqlComm.Parameters.AddWithValue("@id", id);
                        sqlComm.Connection.Open();
                        sqlComm.ExecuteNonQuery();
                        Console.WriteLine("-> Atualização concluída!");
                    }
                    if (campo == 3)
                    {
                        string sql = "update Pessoa set Rg = @valor where id = @id";
                        SqlCommand sqlComm = new SqlCommand(sql, cnn); ;
                        sqlComm.Parameters.AddWithValue("@campo", campo);
                        sqlComm.Parameters.AddWithValue("@valor", valor);
                        sqlComm.Parameters.AddWithValue("@id", id);
                        sqlComm.Connection.Open();
                        sqlComm.ExecuteNonQuery();
                        Console.WriteLine("-> Atualização concluída!");
                    }



                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Errors);
            }
        }
        
    }
}