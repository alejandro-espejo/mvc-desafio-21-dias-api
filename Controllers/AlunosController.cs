using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;

namespace mvc.Controllers;

public class AlunosController : Controller
{
    private readonly ILogger<AlunosController> _logger;

    public AlunosController(ILogger<AlunosController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        // Forma de enviar a informação para a view
        //ViewBag.Aluno = new {
        //    Mensagem = "Testesss"
        //};
        // <h1>@ViewBag.Aluno.Mensagem</h1>
        //ViewData["Teste"] = "ssss"; // <h1>@ViewData["Teste"]</h1>
        return View(Aluno.Todos());
        
        // return View(new {  Mensagem = "Testesss" }); // testado na View com @Model.Mensagem
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
