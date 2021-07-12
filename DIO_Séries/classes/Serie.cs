using System;

namespace DIO_Séries
{
    public class Serie : EntidadeBase
    {
        private Genero genero { get; set; }
        private String titulo { get; set; }
        private String descricao { get; set; }
        private int ano { get; set; }
        private bool excluido { get; set; }

        public Serie(int id, Genero genero, String titulo, String descricao, int ano)
        {
            this.Id = id;
            this.genero = genero;
            this.titulo = titulo;
            this.descricao = descricao;
            this.ano = ano;
            this.excluido = false;
        }

        public override string ToString()
        {
            String retorno = "";
            retorno += "Gênero: " + this.genero + Environment.NewLine;
            retorno += "Título: " + this.titulo + Environment.NewLine;
            retorno += "Descrição: " + this.descricao + Environment.NewLine;
            retorno += "Ano de Início: " + this.ano + Environment.NewLine;
            retorno += "Excluído: " + this.excluido;

            return retorno;
        }

        public String RetornaTitulo()
        {
            return this.titulo;
        }

        public int RetornaId()
        {
            return this.Id;
        }

        public bool RetornaExcluido()
        {
            return this.excluido;
        }

        public void Excluir()
        {
            this.excluido = true;
        }
        
    }
}
