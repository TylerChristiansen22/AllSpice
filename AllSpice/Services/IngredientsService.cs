using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpice.Services
{
    public class IngredientsService
    {
        public readonly IngredientsRepository _repo;

        public IngredientsService(IngredientsRepository repo)
        {
            _repo = repo;
        }

        internal Ingredient CreateIngredient(Ingredient ingredientData)
        {
            Ingredient ingredient = _repo.CreateIngredient(ingredientData);
            return ingredient;
        }


        internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
        {
            List<Ingredient> ingredients = _repo.GetIngredientsByRecipeId(recipeId);
            // if (ingredients == null) throw new Exception($"No Recipe at id: {recipeId}");
            return ingredients;
        }
        internal Ingredient GetIngredientById(int ingredientId)
        {
            Ingredient ingredient = _repo.GetIngredientById(ingredientId);
            if (ingredient == null) throw new Exception($"No ingredient at id: {ingredientId}");
            return ingredient;
        }
        internal string DeleteIngredient(int ingredientId, string userId)
        {
            Ingredient ingredient = this.GetIngredientById(ingredientId);
            _repo.DeleteIngredient(ingredientId);
            return $"{ingredient.name} has been deleted";
        }

    }
}