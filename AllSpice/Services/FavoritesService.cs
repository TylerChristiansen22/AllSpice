using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpice.Services;

public class FavoritesService
{
    public readonly FavoritesRepository _repo;

    public FavoritesService(FavoritesRepository repo)
    {
        _repo = repo;
    }
    internal Favorite CreateFavorite(Favorite favoriteData)
    {
        Favorite favorite = _repo.CreateFavorite(favoriteData);
        return favorite;
    }


    internal List<RecipeFavorite> GetAccountFavorites(string accountId)
    {
        List<RecipeFavorite> favorites = _repo.GetAccountFavorites(accountId);
        return favorites;
    }
    internal string DeleteFavorite(int favoriteId, string id)
    {
        throw new NotImplementedException();
    }
}