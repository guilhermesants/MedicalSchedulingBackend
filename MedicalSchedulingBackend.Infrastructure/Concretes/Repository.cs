using MedicalSchedulingBackend.Domain.Interfaces.Repositories;
using MedicalSchedulingBackend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MedicalSchedulingBackend.Infrastructure.Concretes;

internal abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly MedicalSchedulingContext DbContext;
    protected DbSet<TEntity> DbSet { get; }

    protected Repository(MedicalSchedulingContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        DbContext = dbContext;
        DbSet = dbContext.Set<TEntity>();
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        => await DbSet.AddAsync(entity, cancellationToken);

    public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => await DbSet.Where(predicate).ToListAsync(cancellationToken);

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => await DbSet.ToListAsync(cancellationToken);

    public virtual void Remove(TEntity entity)
        => DbSet.Remove(entity);

    public virtual void Update(TEntity entity)
        => DbSet.Update(entity);
}

