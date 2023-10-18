using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpice.Repositories
{
    public class IngredientsRepository
    {
        private readonly IDbConnection _db;

        public IngredientsRepository(IDbConnection db)
        {
            _db = db;
        }
        internal Ingredient CreateIngredient(Ingredient ingredientData)
        {
            string sql = @"
            INSERT INTO ingredients
            (name, quantity, recipeId)
            VALUES
            (@name, @quantity, @recipeId);
            SELECT * FROM ingredients WHERE id = LAST_INSERT_ID();
            ";
            Ingredient ingredient = _db.Query<Ingredient>(sql, ingredientData).FirstOrDefault();
            return ingredient;
        }


        internal Ingredient GetIngredientById(int ingredientId)
        {
            string sql = @"
            SELECT * FROM ingredients WHERE id = @ingredientId
            ;";
            Ingredient ingredient = _db.QueryFirstOrDefault<Ingredient>(sql, new { ingredientId });
            return ingredient;
        }
        internal void DeleteIngredient(int ingredientId)
        {
            string sql = @"DELETE FROM ingredients WHERE id = @ingredientId;";
            int rowsAffected = _db.Execute(sql, new { ingredientId });
            if (rowsAffected > 1) throw new Exception("Get the Senior dev, i just deleted it all.");
            if (rowsAffected < 1) throw new Exception("somehow nothing was deleted?");
        }

        internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
        {
            string sql = @"
            SELECT * FROM ingredients 
            WHERE recipeId = @recipeId
            ;";
            List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();
            return ingredients;
        }
    }
}