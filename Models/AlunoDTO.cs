using System;
using System.Data.SqlClient;
using System.Xml;

namespace mvc.Models
{
    public partial class Aluno
    {

        #region Metodos de classe ou estaticos

        public static List<Aluno> Todos() 
        {
            var alunos = new List<Aluno>();
            SqlConnection sqlConnection = new SqlConnection(connectionString());
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM aluno", sqlConnection);
            var reader = sqlCommand.ExecuteReader();
            while(reader.Read()) 
            {
                var notas = new List<double>();
                string strNotas = reader["notas"].ToString();
                //List<double> valores = Array.ConvertAll(strNotas.Split(','), s => double.Parse(s)).ToList();
                foreach(var nota in strNotas.Split(','))
                {
                    notas.Add(Convert.ToDouble(nota));
                }
                var aluno = new Aluno()
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Nome =  reader["nome"].ToString(),
                    Matricula = reader["matricula"].ToString(),
                    Notas = notas,
                };
                alunos.Add(aluno);
            }

            sqlConnection.Close();
            sqlConnection.Dispose();
            return alunos;
        }
        
        public static void ApagarPorId(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString());
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand($"DELETE FROM aluno WHERE id={id}", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            
            sqlConnection.Close();
            sqlConnection.Dispose();
        }

        public static void Incluir(Aluno aluno) 
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString())) 
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand($"INSERT INTO aluno(nome, matricula, notas) VALUES (@nome, @matricula, @nota)", sqlConnection);
                sqlCommand.Parameters.Add("@nome", System.Data.SqlDbType.VarChar);
                sqlCommand.Parameters["@nome"].Value = aluno.Nome;
                sqlCommand.Parameters.AddWithValue("@matricula", aluno.Matricula);
                sqlCommand.Parameters.Add("@nota", System.Data.SqlDbType.VarChar);
                sqlCommand.Parameters["@nota"].Value = string.Join(",", aluno.Notas.ToArray());
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public static void Atualizar(Aluno aluno)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString());
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand($"UPDATE aluno SET nome = @nome, matricula = @matricula, notas = @nota WHERE id = @id", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", aluno.Id);
            sqlCommand.Parameters.AddWithValue("@nome", aluno.Nome);
            sqlCommand.Parameters.AddWithValue("@matricula", aluno.Matricula);
            sqlCommand.Parameters.AddWithValue("@nota", string.Join(",", aluno.Notas.ToArray()));
            sqlCommand.ExecuteNonQuery();
            
            sqlConnection.Close();
            sqlConnection.Dispose();
        }

        private static string connectionString() 
        {
            return "server=home\\SQLEXPRESS;database=desafio21diasapi;user=sa;password=;Integrated Security=True;";
        }

        #endregion
    }
}
