using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShiftTech.Models;
using ShiftTech.Models.Context;
using ShiftTech.Models.Database;
using System.Diagnostics;

namespace ShiftTech.Controllers
{
  public class HomeController : Controller
  {
    private readonly CardContext _context;

    public HomeController(CardContext context)
    {
      _context = context;//using one context for now as my use case only has a handful of calls. If i were to expand then i might concidder making a new controller and context.
    }

    public IActionResult ValidateCard()
    {
      return View();
    }

    public IActionResult Config() { return View(); }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    /// <summary>
    /// Handles the ajax request and calls the saveCard context.
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public async Task<string> SaveCard(string number, string company)
    {
      var response = await InMemoryFunctions.SaveCard(number, company, _context);
      return JsonConvert.SerializeObject(response, Formatting.Indented);
    }
    /// <summary>
    /// Returns a Array of companies used to validate cards.
    /// </summary>
    /// <returns></returns>
    public async Task<string> GetConfigList()
    {
      var response = await InMemoryFunctions.GetConfigList(_context);
      return JsonConvert.SerializeObject(response, Formatting.Indented);
    }

    /// <summary>
    /// Save Company to in memory database
    /// </summary>
    /// <returns></returns>
    public async Task<string> SaveCompany()
    {
      return "";
    }

    /// <summary>
    /// Delete Company in the in-memory database
    /// </summary>
    /// <returns></returns>
    public async Task<string> DeleteCompany(int id)
    {
      return "";
    }

    /// <summary>
    /// Gets a list of All currently saved cards.
    /// </summary>
    /// <returns></returns>
    public async Task<string> GetCardList()
    {
      return "";
    }

    /// <summary>
    /// Delete call for if you want to clean up a bit
    /// </summary>
    /// <returns></returns>
    public async Task<string> DeleteCard(int id)
    {
      return "";
    }
  }
}