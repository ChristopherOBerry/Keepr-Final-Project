using System.Collections.Generic;
using System.Data;
using keepr.Models;
using Dapper;


namespace keepr.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;
        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        public IEnumerable<Vault> GetAll(string UserId)
        {
            string query = "SELECT * FROM vaults WHERE userId = @UserId";
            return _db.Query<Vault>(query, new { UserId });
        }

        public Vault GetById(string Id)
        {
            string query = "SELECT * FROM vaults WHERE id = @Id";
            return _db.QueryFirstOrDefault<Vault>(query, new { Id });
        }
        public Vault CreateVault(Vault vault)
        {
            _db.Execute("INSERT INTO vaults (id, name, description, userId) VALUES (@Id, @Name, @Description, @UserId)", vault);
            return vault;
        }
        public bool DeleteVault(string vaultId)
        {
            int success = _db.Execute("DELETE FROM vaults WHERE id = @vaultId", new { vaultId });
            return success > 0;
        }
    }

}