using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjBiblioteca
{
    public class Livro
    {
        private int isbn;
        private String titulo;
        private String autor;
        private String editora;
        private List<Exemplar> exemplares;

        public Livro(int isbn, String titulo, String autor, String editora) 
        {
            this.isbn = isbn;
            this.titulo = titulo;
            this.autor = autor;
            this.editora = editora;
            exemplares = new List<Exemplar>();
        }

        public Livro(int isbn) : this(isbn, "", "", "") { }

        public int Isbn => isbn;
        public string Titulo => titulo;
        public string Autor => autor;
        public string Editora => editora;
        public List<Exemplar> Exemplares => exemplares;

        public void AdicionarExemplar(Exemplar exemplar)
        {
            exemplares.Add(exemplar);
        }

        public int QtdeExemplares() => exemplares.Count;
        public int QtdeDisponiveis() => exemplares.Count(e => e.Disponivel());
        public int QtdeEmprestimos() => exemplares.Sum(e => e.QtdeEmprestimos());

        public double PercDisponibilidade()
        {
            if (exemplares.Count == 0)
                return 0;

            return (double)QtdeDisponiveis() / QtdeExemplares() * 100;
        }
    }
}
