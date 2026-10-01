using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjBiblioteca
{
    public class Livros
    {
        private List<Livro> acervo;

        public Livros()
        {
            acervo = new List<Livro>();
        }

        public void Adicionar(Livro livro)
        {
            acervo.Add(livro);
        }

        public Livro? Pesquisar(Livro livro)
        {
            return acervo.FirstOrDefault(l => l.Isbn == livro.Isbn);
        }

        public Exemplar? PesquisarExemplar(int tombo)
        {
            return acervo
                .SelectMany(l => l.Exemplares)
                .FirstOrDefault(e => e.Tombo == tombo);
        }
    }
}
