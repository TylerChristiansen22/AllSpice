using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpice.Repositories;

public class FavoritesRepository
{

    private readonly IDbConnection _db;

    public FavoritesRepository(IDbConnection db)
    {
        _db = db;
    }
    internal Favorite CreateFavorite(Favorite favoriteData)
    {
        string sql = @"
            INSERT INTO favorites
            (accountId, recipeId)
            VALUES
            (@accountId, @recipeId);

            SELECT * FROM favorites WHERE id = LAST_INSERT_ID()
            ;";
        Favorite favorite = _db.Query<Favorite>(sql, favoriteData).FirstOrDefault();
        return favorite;
    }

    internal List<RecipeFavorite> GetAccountFavorites(string accountId)
    {
        string sql = @"
        SELECT
        accounts.*,
        favorites.*,
        recipes.*
        FROM favorites
        JOIN accounts ON accounts.id = favorites.accountId
        JOIN recipes ON recipes.id = favorites.recipeId
        WHERE favorites.accountId = @accountId
        ;";
        List<RecipeFavorite> favorites = _db.Query<Account, Favorite, RecipeFavorite, RecipeFavorite>(sql, (creator, favorite, favoriteRecipe) =>
        {
            favoriteRecipe.FavoriteId = favorite.id;
            favoriteRecipe.accountId = creator.Id;
            favoriteRecipe.creator = creator;
            return favoriteRecipe;
        }, new { accountId }).ToList();
        return favorites;
    }
}