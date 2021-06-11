using Microsoft.EntityFrameworkCore;
using GerenciadorTurma.Models;

namespace GerenciadorTurma.Data
{
    public class BancoDeDados : DbContext
    {
        public BancoDeDados(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }
    }
}
