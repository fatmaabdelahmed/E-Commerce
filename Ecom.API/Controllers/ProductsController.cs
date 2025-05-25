using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTOs;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        public ProductsController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {

        }
        [HttpGet("get-all")]
        public async Task<IActionResult> get()
        {
            try
            {
               var products = await work.ProductRepository
                    .GetAllAsync(x=>x.Category,x=>x.Photos);
                var result = mapper.Map<List<ProductDTO>>(products); 
                if(products == null)
                {
                    return BadRequest(new ResponseApi(400));
                }
                return Ok(result);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-by-id/{id}")]
        public async Task <IActionResult> getById(int id)
        {
            try
            {
                var product = await work.ProductRepository.GetByIdAsync(id,
                    x => x.Category, x => x.Photos);
                var result = mapper.Map<ProductDTO>(product);
                if (product is null)
                {
                    return BadRequest(new ResponseApi(400));
                }
                return Ok(result); 

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> add(AddProductDTO productDTO)
        {
            try
            {
                await work.ProductRepository.AddAsync(productDTO);
                return Ok(new ResponseApi(200));

            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseApi(400, ex.Message));
            }
        }
        [Authorize(Roles = "Admin")]

        [HttpPut("update")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> update(UpdateProductDTO updateProductDTO)
        {
            try
            {
                await work.ProductRepository.UpdateAsync(updateProductDTO);
                return Ok(new ResponseApi(200));

            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseApi(400, ex.Message));
            }
        }
        [Authorize(Roles = "Admin")]

        [HttpDelete("delet/{id}")]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                var product = await work.ProductRepository.GetByIdAsync(id,x=>x.Photos,x=>x.Category);
                await work.ProductRepository.DeleteAsync(product);
                return Ok(new ResponseApi(200));

            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseApi(400, ex.Message));
            }
        }
    }
}
