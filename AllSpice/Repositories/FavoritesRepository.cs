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

    internal List<Favorite> GetAccountFavorites(string accountId)
    {
        string sql = @"
        SELECT
        accounts.*,
        favorites.*
        FROM favorites
        JOIN accounts ON accounts.id = favorites.accountId
        WHERE accounts.id = @accountId
        ;";
        List<Favorite> favorites = _db.Query<Account, Favorite, Favorite>(sql, (creator, favorite) =>
        {
            favorite.accountId = creator.Id;
            return favorite;
        }, new { accountId }).ToList();
        return favorites;
    }
}