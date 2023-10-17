using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpice.Services
{
    public class RecipesService
    {
        private readonly RecipesRepository _repo;

        public RecipesService(RecipesRepository repo)
        {
            _repo = repo;
        }

        internal Recipe CreateRecipe(Recipe recipeData)
        {
            Recipe recipe = _repo.CreateRecipe(recipeData);
            return recipe;
        }


        internal List<Recipe> GetRecipes()
        {
            List<Recipe> recipes = _repo.GetRecipes();
            return recipes;
        }
        internal Recipe GetRecipeById(int recipeId)
        {
            Recipe recipe = _repo.GetRecipeById(recipeId);
            if (recipe == null) throw new Exception($"No recipe at id: {recipeId}");
            return recipe;
        }

        internal Recipe EditRecipe(Recipe recipeData, String userId)
        {
            Recipe originalRecipe = this.GetRecipeById(recipeData.Id);
            if (originalRecipe.creatorId != userId) throw new Exception("You didn't make this recipe");
            originalRecipe.Title = recipeData.Title != null ? recipeData.Title : originalRecipe.Title;
            originalRecipe.Instructions = recipeData.Instructions != null ? recipeData.Instructions : originalRecipe.Instructions;
            originalRecipe.img = recipeData.img != null ? recipeData.img : originalRecipe.img;
            originalRecipe.Category = recipeData.Category != null ? recipeData.Category : originalRecipe.Category;
            Recipe recipe = _repo.EditRecipe(originalRecipe);
            return recipe;
        }

        internal string DeleteRecipe(int recipeId, string userId)
        {
            Recipe recipe = this.GetRecipeById(recipeId);
            if (recipe.creatorId != userId) throw new Exception("This is not your recipe to delete!");
            _repo.DeleteRecipe(recipeId);
            return $"{recipe.Title} was deleted from the database";
        }
    }
}