using System.Collections.Generic;
using System;
using System.Data;
using keepr.Models;
using Dapper;

namespace keepr.Repositories
{
    public class VaultKeepRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepRepository(IDbConnection db)
        {
            _db = db;
        }
        public IEnumerable<Vault> GetById(int vaultId, string userId)
        {
            string query = "SELECT * FROM vaultkeeps vk INNER JOIN keeps k ON k.id = vk.keepId WHERE(vaultId = @vaultId AND vk.userId = @userId)";
            return _db.Query<Vault>(query, new { vaultId, userId });
        }
        public VaultKeep CreateVaultKeep(VaultKeep vaultKeep)
        {
            _db.Execute("INSERT INTO vaultkeeps (vaultId, keepId, userId) VALUES (@VaultId, @KeepId, @UserId)", vaultKeep);
            return vaultKeep;
        }
        public bool DeleteVaultKeep(string vaultKeepId)
        {
            int success = _db.Execute("DELETE FROM vaultkeeps WHERE id = @vaultKeepId", new { vaultKeepId });
            return success > 0;

        }

    }
}