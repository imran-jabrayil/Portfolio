using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWeb.Data;

namespace PortfolioWeb.Controllers;

public class WorkExperienceController : Controller {
    private readonly ILogger<WorkExperienceController> _logger;
    private readonly PortfolioDbContext _context;
    
    
    public WorkExperienceController(PortfolioDbContext context, ILogger<WorkExperienceController> logger) {
        _context = context;
        _logger = logger;
    }

    public async Task<IActionResult> Index() {
        var workExperiences = await _context.WorkExperiences
            .Include(we => we.Company)
            .Include(we => we.City)
            .ThenInclude(c => c.Country)
            .OrderByDescending(we => we.StartDate)
            .ToListAsync();
        return View(workExperiences);
    }
}