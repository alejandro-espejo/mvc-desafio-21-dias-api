using System.Data.SqlClient;
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
    // DESAFIO: CRIAR O BOTÃO PARA CRIAR E EXCLUIR
    [Route("/alunos")]
    public IActionResult Index()
    {
        // Forma de enviar a informação para a view
        ViewData["Itens"] = Aluno.Todos();
        //ViewBag.Aluno = new {
        //    Mensagem = "Testesss"
        //};
        // <h1>@ViewBag.Aluno.Mensagem</h1>
        //ViewData["Teste"] = "ssss"; // <h1>@ViewData["Teste"]</h1>
        //return View(Aluno.Todos()); // Na view é retornado como Model

        return View();
        // return View(new {  Mensagem = "Testesss" }); // testado na View com @Model.Mensagem
    }

    [Route("/alunos/remover")]
    public IActionResult Remover(int id)
    {
        Aluno.ApagarPorId(id);
        return RedirectToAction("Index");
    }

    [Route("/alunos/inserir")]
    [HttpPost]
    public IActionResult Inserir(Aluno aluno) 
    {
        foreach(var nota in aluno.NotasTexto.Split(','))
        {
            aluno.Notas.Add(Convert.ToDouble(nota));
        }
        aluno.Salvar();
        return RedirectToAction("Index");
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
