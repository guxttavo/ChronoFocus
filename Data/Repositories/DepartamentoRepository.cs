using Core.Dto.Departamento;

namespace Data.Repositories
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartamentoRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CadastrarAsync(DepartamentoCadastroDto departamentoCadastroDto)
        {
            await _dbContext.Departamentos.AddAsync(new Departamento{
                Nome = departamentoCadastroDto.Nome
            });
            await _dbContext.SaveChangesAsync();
        }
    }
}