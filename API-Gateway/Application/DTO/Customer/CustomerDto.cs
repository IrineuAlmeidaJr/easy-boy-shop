﻿namespace Application.Dto.Customer;

public class CustomerDto
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Cpf { get; set; }
    public DateTime? BithDate { get; set; }
    public DateTime RegisterDate { get; set; }
}