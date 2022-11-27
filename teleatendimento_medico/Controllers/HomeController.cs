using Microsoft.AspNetCore.Mvc;
using teleatendimento_medico.Models;
using teleatendimento_medico.DAO;

namespace teleatendimento_medico.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var pessoaListViewModel = PessoaDAO.getAll();
        return View(pessoaListViewModel);
        // return View();
    }

    [HttpGet]
    public JsonResult Get(int id)
    {
        var pessoa = PessoaDAO.get(id);
        return Json(pessoa);
    }

    [HttpPost]
    public JsonResult Delete(string id)
    {
        Console.WriteLine("ID: " + id);
        PessoaDAO.delete(id);

        return Json(new Object { });
    }

    [HttpPost]
    public RedirectResult Update(PessoaDTO p)
    {
        var pessoa = new Pessoa
        {
            id = p.id,
            nome = p.nome,
            cpf = p.cpf,
            endereco = new Endereco
            {
                id = p.endereco.id,
                logradouro = p.endereco.logradouro,
                numero = p.endereco.numero,
                bairro = p.endereco.bairro,
                cidade = p.endereco.cidade,
                estado = p.endereco.estado,
                cep = p.endereco.cep,
            }
        };
        PessoaDAO.update(pessoa);
        return Redirect("/");
    }

    [HttpPost]
    public RedirectResult Insert(PessoaDTO p)
    {
        var pessoa = new Pessoa
        {
            nome = p.nome,
            cpf = p.cpf,
            endereco = new Endereco
            {
                logradouro = p.endereco.logradouro,
                numero = p.endereco.numero,
                bairro = p.endereco.bairro,
                cidade = p.endereco.cidade,
                estado = p.endereco.estado,
                cep = p.endereco.cep,
            }
        };
        PessoaDAO.create(pessoa);
        return Redirect("/");
    }
}
