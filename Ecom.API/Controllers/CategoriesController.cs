using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTOs;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
     
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> get()
        {
            try
            {
                var category = await work.CategoryRepository.GetAllAsync();
                if (category == null) { 
                return BadRequest(new ResponseApi(400));
                }
                return Ok(category);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-b-id/{id}")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                var category =await work.CategoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return BadRequest(new ResponseApi(400,$"not found Category id={id}"));
                }
                return Ok(category);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
                
            }

        }
        [Authorize(Roles = "Admin")]
        [HttpPost("add-category")]
        public async Task<IActionResult> add(CategoryDTO categoryDTO)
        {
            try 
            {
                Category category = mapper.Map<Category>(categoryDTO);

                await work.CategoryRepository.AddAsync(category);
                return Ok(new ResponseApi(200,"Item has been added"));
            }
            catch (Exception ex) 
            {

                return BadRequest(ex.Message);

            }
        }
        [Authorize(Roles = "Admin")]

        [HttpPut("update-category")]
        public async Task<IActionResult> update(UpdateCategoryDTO categoryDTO)
        {
            try
            {
                Category category = mapper.Map<Category>(categoryDTO);
                await work.CategoryRepository.UpdateAsync(category);
                return Ok(new ResponseApi(200, "Item has been updated"));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]

        [HttpDelete("delete-category")]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                await work.CategoryRepository.DeleteAsync(id);
                return Ok(new ResponseApi(200, "Item has been deleted"));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    
    }
}
