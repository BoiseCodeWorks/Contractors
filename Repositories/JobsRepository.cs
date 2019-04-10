using System.Collections.Generic;
using System.Data;
using contracted.Models;
using Dapper;

namespace contracted.Repositories
{
    public class JobsRepository
    {
        private readonly IDbConnection _db;
        public JobsRepository(IDbConnection db)
        {
            _db = db;
        }
        public IEnumerable<Job> GetALL()
        {
            return _db.Query<Job>("SELECT * FROM Jobs");
        }

        public Job NewJob(Job job)
        {
            int id = _db.ExecuteScalar<int>("INSERT INTO jobs (title, location, budget)"
            + "VALUES (@Title, @Location, @Budget); SELECT LAST_INSERT_ID()", job);
            job.Id = id;
            return job;
        }
        //edit
        public Job EditJob(string id, Job job)
        {
            //WILL FAIL ON BAD ID
            string query = @"
                UPDATE jobs SET
                    title = @Title,
                    location = @Location,
                    budget = @Budget
                WHERE id = @Id;
                SELECT * FROM Jobs WHERE id = @Id
            ";
            return _db.QueryFirstOrDefault<Job>(query, job);
        }

        //delete
        public bool DeleteJob(string jobId)
        {
            int success = _db.Execute("DELETE FROM jobs WHERE id = @jobId", new { jobId });
            return success > 0;
        }



    }
}