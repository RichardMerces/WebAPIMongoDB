using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIMongoDB.Models;
using WebAPIMongoDB.Services;

namespace WebAPIMongoDB.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProdutosController : ControllerBase
	{
		private readonly ProdutoServices _produtoServices;

		public ProdutosController(ProdutoServices produtoServices) =>
			_produtoServices = produtoServices;

		[HttpGet]
		public async Task<List<Produto>> GetProdutos() =>
			await _produtoServices.GetAsync();

		[HttpGet("{id:length(24)}")]
		public async Task<ActionResult<Produto>> Get(string id)
		{
			var produto = await _produtoServices.GetAsync(id);

			if (produto is null)
			{
				return NotFound();
			}

			return produto;
		}

		[HttpPost]
		public async Task<IActionResult> Post(Produto newBook)
		{
			await _produtoServices.CreateAsync(newBook);

			return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
		}

		[HttpPut("{id:length(24)}")]
		public async Task<IActionResult> Update(string id, Produto updatedBook)
		{
			var produto = await _produtoServices.GetAsync(id);

			if (produto is null)
			{
				return NotFound();
			}

			updatedBook.Id = produto.Id;

			await _produtoServices.UpdateAsync(id, updatedBook);

			return NoContent();
		}

		[HttpDelete("{id:length(24)}")]
		public async Task<IActionResult> Delete(string id)
		{
			var produto = await _produtoServices.GetAsync(id);

			if (produto is null)
			{
				return NotFound();
			}

			await _produtoServices.RemoveAsync(id);

			return NoContent();
		}
	}
}
