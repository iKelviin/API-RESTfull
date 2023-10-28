using API_Restfull.Context;
using API_Restfull.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Restfull.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _context.Produtos.Take(100).AsNoTracking().ToList();

            if (produtos is null) return NotFound("Produtos não encontrados...");

            return Ok(produtos);
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _context.Produtos.Find(id);

            if (produto is null) return NotFound("Produto não encontrado...");

            return Ok(produto);
        }
        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null) return BadRequest();

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return CreatedAtRoute("ObterProduto", new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id,Produto produto)
        {
            if (produto.ProdutoId != id) return BadRequest();

            _context.Produtos.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);

        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.Find(id);

            if (produto is null) return NotFound();

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
