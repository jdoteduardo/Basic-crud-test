using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly APIContext _context;

        public ProdutosController(APIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produtos>>> ListarProdutos(string filter = "", string range = "", string sort = "")
        {
            var entityQuery = _context.Produtos.Where(d => d.Delete == false);

            if (!string.IsNullOrEmpty(filter))
            {
                var filterVal = (JObject)JsonConvert.DeserializeObject(filter);
                var t = new Produtos();
                foreach (var f in filterVal)
                {
                    entityQuery = entityQuery.Where(x => x.Nome.Contains(f.Value.ToString()));
                }
            }
            var count = entityQuery.Count();

            if (!string.IsNullOrEmpty(sort))
            {
                var sortVal = JsonConvert.DeserializeObject<List<string>>(sort);
                var condition = sortVal.First();
                var order = sortVal.Last() == "ASC" ? "" : "descending";
                entityQuery = entityQuery.OrderBy(x => x.ID);
            }

            var from = 0;
            var to = 0;
            if (!string.IsNullOrEmpty(range))
            {
                var rangeVal = JsonConvert.DeserializeObject<List<int>>(range);
                from = rangeVal.First();
                to = rangeVal.Last();
                entityQuery = entityQuery.Skip(from).Take(to - from + 1);
            }

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
            Response.Headers.Add("Content-Range", $"{typeof(Produtos).Name.ToLower()} {from}-{to}/{count}");
            return await entityQuery.ToListAsync();
        }

        // GET: api/produtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produtos>> ObterPorId(int id)
        {
            var entity = await _context.Produtos.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return entity;
        }

        // PUT: api/produtos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarProduto(int id, Produtos produto)
        {
            var entityId = produto.ID;
            if (id != entityId)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(await _context.Produtos.FindAsync(entityId));
        }

        // POST: api/produtos
        [HttpPost]
        public async Task<ActionResult<Produtos>> AdicionarProduto(Produtos produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            var id = produto.ID;
            return Ok(await _context.Produtos.FindAsync(id));
        }

        // DELETE: api/produtos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Produtos>> DeleteProdutos(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            produto.Delete = true;

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(await _context.Produtos.FindAsync(produto.ID));
        }

        private bool ProdutosExists(int id)
        {
            return _context.Produtos.Any(e => e.ID == id);
        }
    }
}
