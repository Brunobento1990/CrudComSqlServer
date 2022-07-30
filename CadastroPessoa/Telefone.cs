using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CadastroPessoa
{
    public class Telefone
    {
        public int Id { get; set; }
        public string DDD { get; set; }
        public string Numero { get; set; }
        public int IdPessoa { get; set; }
        public Telefone() { }
        public void Gravar()
        {

            try
            {
                string Coneccao = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Bruno;Data Source=ITELABD03\SQLEXPRESS01";
                using (var cnn = new SqlConnection(Coneccao))
                {


                    string sql = "INSERT INTO Telefone (DDD,Numero,IdPessoa) " +
                        "VALUES(@ddd,@numero,@idpessoa)";

                    SqlCommand sqlComm = new SqlCommand(sql, cnn);
                    sqlComm.Parameters.AddWithValue("@ddd", this.DDD);
                    sqlComm.Parameters.AddWithValue("@numero", this.Numero);
                    sqlComm.Parameters.AddWithValue("@idpessoa", this.IdPessoa);
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


                    string sql = "update Telefone set DDD = @ddd , Numero = @numero where id = @id";
                    SqlCommand sqlComm = new SqlCommand(sql, cnn);
                    sqlComm.Parameters.AddWithValue("@id", id);
                    sqlComm.Parameters.AddWithValue("@ddd", this.DDD);
                    sqlComm.Parameters.AddWithValue("@numero", this.Numero);
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
    }
}
