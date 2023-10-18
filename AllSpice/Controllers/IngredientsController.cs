using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AllSpice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly IngredientsService _ingredientsService;
        private readonly Auth0Provider _auth0;

        public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0Provider)
        {
            _ingredientsService = ingredientsService;
            _auth0 = auth0Provider;
        }
        [Authorize]
        [HttpPost]
        public ActionResult<Ingredient> CreateIngredient([FromBody] Ingredient ingredientData)
        {
            try
            {
                Ingredient ingredient = _ingredientsService.CreateIngredient(ingredientData);
                return Ok(ingredient);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{ingredientId}")]
        public ActionResult<Ingredient> GetIngredientById(int ingredientId)
        {
            try
            {
                Ingredient ingredient = _ingredientsService.GetIngredientById(ingredientId);
                return Ok(ingredient);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{ingredientId}")]
        public async Task<ActionResult<string>> DeleteIngredient(int ingredientId)
        {
            try
            {
                Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
                string message = _ingredientsService.DeleteIngredient(ingredientId, userInfo.Id);
                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}