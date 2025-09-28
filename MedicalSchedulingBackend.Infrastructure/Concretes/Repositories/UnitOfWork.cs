using MedicalSchedulingBackend.Domain.Interfaces.Repositories;
using MedicalSchedulingBackend.Infrastructure.Context;

namespace MedicalSchedulingBackend.Infrastructure.Concretes.Repositories;

internal class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly MedicalSchedulingContext _context;
    private bool _disposed = false;

    public UnitOfWork(MedicalSchedulingContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));

        AppointmentStatusRepository = new AppointmentStatusRepository(context);
        UserRepository = new UserRepository(context);
        DoctorRepository = new DoctorRepository(context);
    }

    public IAppointmentStatusRepository AppointmentStatusRepository { get; }
    public IUserRepository UserRepository { get; }
    public IDoctorRepository DoctorRepository { get; }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
            _context.Dispose();

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
