using System;
using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;

namespace TestTask_BO.Models;

public partial class Employee
{
//[Index(0)]
    public int Id { get; set; }
    [Index(0)] public string? Name { get; set; }
    [Index(1)] public DateTime? DateOfBirth { get; set;}
    [Index(2)] public bool Married { get; set; }
    [Index(3)] public string? Phone { get; set; }
    [Index(4)] public decimal? Salary { get; set; }

    public Employee(string name, DateTime? dateOfBirth, bool married, string phone, decimal? salary)
    {
        Name = name;
        DateOfBirth = dateOfBirth;
        Married = married;
        Phone = phone;
        Salary = salary;
    }

    public Employee()
    {
    }
}