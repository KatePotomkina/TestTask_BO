using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTask_BO.Models;

namespace TestTask_BO.Controllers;

public class EmployeesController : Controller
{
    private readonly EmployeeDbContext _context;
    private IWebHostEnvironment _environment;

    public EmployeesController(EmployeeDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    // GET: Employees
    public async Task<IActionResult> Index1()
    {
        return View(await _context.Employees.ToListAsync());
    }

    // GET: Employees/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Employees == null) return NotFound();

        var employee = await _context.Employees
            .FirstOrDefaultAsync(m => m.Id == id);
        if (employee == null) return NotFound();

        return View(employee);
    }

    // GET: Employees/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Employees/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,DateOfBirth,Married,Phone,Salary")] Employee employee)
    {
        if (ModelState.IsValid)
        {
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index1));
        }

        return View(employee);
    }

    // GET: Employees/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Employees == null) return NotFound();

        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return NotFound();

        return View(employee);
    }

    // POST: Employees/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,Name,DateOfBirth,Married,Phone,Salary")]
        Employee employee)
    {
        if (id != employee.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index1));
        }

        return View(employee);
    }

    // GET: Employees/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Employees == null) return NotFound();

        var employee = await _context.Employees
            .FirstOrDefaultAsync(m => m.Id == id);
        if (employee == null) return NotFound();

        return View(employee);
    }

    // POST: Employees/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Employees == null) return Problem("Entity set 'EmployeeDbContext.Employees'  is null.");

        var employee = await _context.Employees.FindAsync(id);
        if (employee != null) _context.Employees.Remove(employee);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index1));
    }

    private bool EmployeeExists(int id)
    {
        return _context.Employees.Any(e => e.Id == id);
    }

    public bool ConvertBoolToString(string married)
    {
        if (string.IsNullOrEmpty(married) || married is "0" or "false") return false;
        return true;
    }

    [HttpPost]
    public async Task<IActionResult> Index(IFormFile file, [FromServices] IWebHostEnvironment hostEnvironment)
    {
        if (file == null || file.Length == 0) return await Index1();
        var fileName = $"{hostEnvironment.WebRootPath}\\files\\{file.FileName}";
        using (var fileStream = System.IO.File.Create(fileName))
        {
            file.CopyTo(fileStream);
            fileStream.Flush();
        }

        var employees = GetEmployeeList(file.FileName);
        foreach (var employee in employees)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        return RedirectToAction("Index1");
    }

    private List<Employee> GetEmployeeList(string fileName)
    {
        var employees = new List<Employee>();
        //var csvDataList = new List<Employee>();
        var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fileName;
        var lines = System.IO.File.ReadAllLines(path);
        foreach (var line in lines)
            if (!string.IsNullOrEmpty(line))
            {
                var columns = line.Split(',');
                var employee = new Employee
                {
                    Name = columns[0],
                    DateOfBirth = DateTime.Parse(columns[1]),
                    Married = ConvertBoolToString(columns[2]),
                    Phone = columns[3],
                    Salary = Convert.ToDecimal(columns[4])
                };
                employees.Add(employee);
            }

        return employees;
    }
}