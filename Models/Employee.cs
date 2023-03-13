using System;
using System.Collections.Generic;

namespace TestTask_BO.Models;

public partial class Employee
{
   

    public int Id { get; set; }

    public string? Name { get; set; } 

    public DateTime? DateOfBirth { get; set; }

    public bool  Married { get; set; }

    public string? Phone { get; set; } 

    public decimal? Salary { get; set; }

    /*public Employee(int id, string name,DateTime dateOfBirth,bool married, string phone, decimal salary)
    {
        Id = id;
        Name = name;
        DateOfBirth = dateOfBirth;
        Married = married;
        Phone = phone;
        Salary = salary;
    }*/
}
