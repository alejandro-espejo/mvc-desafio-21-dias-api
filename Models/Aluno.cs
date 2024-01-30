using System;
using System.Data.SqlClient;
using System.Xml;

namespace mvc.Models
{
    public partial class Aluno
    {
        #region Propriedades
        public int Id { get; set; }
    
        public string Nome { get; set; }

        public string Matricula { get; set; }

        private List<double> notas;
        public List<double> Notas { 
            get 
            {
                if (this.notas == null) 
                {
                    this.notas = new List<double>();
                }
                return this.notas;
            }
            set 
            {
                notas = value;
            }
        }

        #endregion

        #region Metodos de instância

        public double CalcularMedia()
        {
            var somaNotas = 0.0;
            foreach(var nota in this.Notas) {
                somaNotas += nota;
            }
            return somaNotas / this.Notas.Count;
        }

        public string Situacao() 
        {
            return this.CalcularMedia() >= 7 ? "APROVADO" : "REPROVADO";
        }

        public void Apagar()
        {
            Aluno.ApagarPorId(this.Id);
        }

        public void Salvar() 
        {
            if (this.Id > 0) 
            {
                Aluno.Atualizar(this);
            }
            else
            {
                Aluno.Incluir(this);
            }
        }

        #endregion

        public string StrNotas()
        {
            return string.Join(",", this.Notas.ToArray());
        }

    }
}
