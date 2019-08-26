using System.Collections.Generic;
using System.Data;
using keepr.Models;
using Dapper;


namespace keepr.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;
        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }
        public IEnumerable<Keep> GetAll()
        {
            return _db.Query<Keep>("SELECT * ALL FROM keeps");
        }
        public Keep GetById(string Id)
        {
            string query = "SELECT * FROM keeps WHERE id = @Id";
            return _db.QueryFirstOrDefault<Keep>(query, new { Id });
        }
        public IEnumerable<Keep> GetKeepsByUserId(string UserId)
        {
            string query = "SELECT * FROM keeps WHERE userId = @UserId";
            return _db.Query<Keep>(query);
        }

        public Keep CreateKeep(Keep keep)
        {
            _db.Execute("INSERT INTO keeps (id, name, description, img, isPrivate) VALUES (@Id, @Name,@Description, @Img, @IsPrivate)", keep);
            return keep;
        }
        public bool DeleteKeep(string keepId)
        {
            int success = _db.Execute("DELETE FROM keeps WHERE id = @keepId", new { keepId });
            return success > 0;
        }
    }
}