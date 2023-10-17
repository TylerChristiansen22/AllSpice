using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpice.Repositories
{
    public class RecipesRepository
    {
        private readonly IDbConnection _db;

        public RecipesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Recipe CreateRecipe(Recipe recipeData)
        {
            string sql = @"
            INSERT INTO recipes
            (title, instructions, img, category, creatorId)
            VALUES
            (@title, @instructions, @img, @category, @creatorId);

            SELECT
            accounts.*,
            recipes.*
            FROM recipes
            JOIN accounts ON accounts.id = recipes.creatorId
            WHERE recipes.id = LAST_INSERT_ID()
            ;";
            Recipe recipe = _db.Query<Account, Recipe, Recipe>(sql, (creator, recipe) =>
            {
                recipe.creator = creator;
                return recipe;
            }, recipeData).FirstOrDefault();
            return recipe;
        }


        internal List<Recipe> GetRecipes()
        {
            string sql = @"
            SELECT
            recipes.*,
            accounts.*
            FROM recipes
            JOIN accounts ON accounts.id = recipes.creatorId
            ;";

            List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, (recipe, creator) =>
            {
                recipe.creator = creator;
                return recipe;
            }).ToList();
            return recipes;
        }
        internal Recipe GetRecipeById(int recipeId)
        {
            string sql = @"
            SELECT
            accounts.*,
            recipes.*
            FROM recipes
            JOIN accounts ON accounts.id = recipes.creatorId
            WHERE recipes.id = @recipeId
            ;";

            Recipe recipe = _db.Query<Account, Recipe, Recipe>(sql, (creator, recipe) =>
            {
                recipe.creator = creator;
                return recipe;
            }, new { recipeId }).FirstOrDefault();
            return recipe;
        }

        internal Recipe EditRecipe(Recipe originalRecipe)
        {
            string sql = @"
            UPDATE recipes
            SET 
            Title = @Title,
            Instructions = @Instructions,
            img = @img,
            Category = @Category
            WHERE id = @id;
            SELECT * FROM recipes WHERE id = @id
            ;";
            Recipe recipe = _db.Query<Recipe>(sql, originalRecipe).FirstOrDefault();
            return recipe;
        }

        internal void DeleteRecipe(int recipeId)
        {
            string sql = "DELETE FROM recipes WHERE id = @recipeId";

            int rowsAffected = _db.Execute(sql, new { recipeId });
            if (rowsAffected > 1) throw new Exception("Get the Senior dev, i just deleted it all.");
            if (rowsAffected < 1) throw new Exception("somehow nothing was deleted?");
        }
    }
}