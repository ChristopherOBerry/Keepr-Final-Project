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
        public VaultKeep GetById(string Id)
        {
            string query = "SELECT * FROM vaultkeeps vk INNER JOIN keeps k ON k.id = vk.keepId WHERE(vaultId = @vaultId AND vk.userId = @userId)";
            return _db.QueryFirstOrDefault<VaultKeep>(query, new { Id });
        }
        public VaultKeep CreateVaultKeep(VaultKeep vaultKeep)
        {
            _db.Execute("INSERT INTO vaultkeeps ", vaultKeep);
            return vaultKeep;
        }
        public bool DeleteVaultKeep(string vaultKeepId)
        {
            int success = _db.Execute("DELETE FROM vaultkeeps", new { vaultKeepId });
            return success > 0;

        }

    }
}