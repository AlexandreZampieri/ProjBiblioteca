using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjBiblioteca
{
    public class Exemplar
    {
        private int tombo;
        private List<Emprestimo> emprestimos;

        public Exemplar(int tombo)
        {
            this.tombo = tombo;
            emprestimos = new List<Emprestimo>();
        }
        public int Tombo => tombo;
        public List<Emprestimo> Emprestimos => emprestimos;
        public bool Emprestar()
        {
            if (!Disponivel())
                return false;

            emprestimos.Add(new Emprestimo());
            return true;
        }
        public bool Devolver()
        {
            if (Disponivel())
                return false;

            emprestimos[emprestimos.Count - 1].RegistrarDevolucao();
            return true;
        }
        public bool Disponivel()
        {
            if (emprestimos.Count == 0)
                return true;

            return !emprestimos[emprestimos.Count - 1].EmAberto();
        }
        public int QtdeEmprestimos() => emprestimos.Count;

    }
     

    }
