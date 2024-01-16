using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;

namespace Bulky.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        CategoryRepository = new CategoryRepository(_context);
    }
    
    public ICategoryRepository CategoryRepository { get; private set; }
    
    public void Save()
    {
        _context.SaveChanges();
    }
}
