using GameMenagement.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagement.EntityFramework.Repository.Extension
{
    public static class PlayerRepositoryExtension
    {
        public static IQueryable<Player> SearchByAccount(this IQueryable<Player> players, string account)
        {
            if (string.IsNullOrWhiteSpace(account))
            {
                return players;
            }
            return players.Where(p => p.Account.ToLower().Contains(account.Trim().ToLower()));
        }
    }
}
