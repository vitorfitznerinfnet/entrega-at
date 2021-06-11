using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorTurma.Models.Home
{
    public class IndexModel
    {
        public List<Aluno> Alunos { get; set; } = new List<Aluno>();
        public int QuantidadeDeAlunos { get; set; }
    }
}
