﻿namespace MedicalSchedulingBackend.Domain.Entities;

public class Patient
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public virtual User User { get; set; } = null!;
    public virtual ICollection<Appointment> Appointments { get; set; } = null!;
}

