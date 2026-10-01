using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjBiblioteca
{
    public class Emprestimo
    {
        private DateTime dtEmprestimo;
        private DateTime? dtDevolucao; 

        public Emprestimo()
        {
            dtEmprestimo = DateTime.Now;
            dtDevolucao = null;
        }

        public DateTime DtEmprestimo => dtEmprestimo;
        public DateTime? DtDevolucao => dtDevolucao;
        public bool EmAberto() => dtDevolucao == null;

        public void RegistrarDevolucao()
        {
            dtDevolucao = DateTime.Now;
        }
    }
}
