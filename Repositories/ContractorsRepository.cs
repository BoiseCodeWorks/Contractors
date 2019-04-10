using System;
using System.Collections.Generic;
using System.Data;
using contracted.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace contracted.Repositories
{
    public class ContractorsRepository
    {
        private readonly IDbConnection _db;
        public ContractorsRepository(IDbConnection db)
        {
            _db = db;
        }

        //returns all contractors from the database
        public IEnumerable<Contractor> GetALL()
        {
            return _db.Query<Contractor>("SELECT * FROM contractors");
        }

        public Contractor GetbyId(string Id)
        {
            //THIS MIGHT RETURN NULL
            string query = "SELECT * FROM contractors WHERE id = @Id";
            return _db.QueryFirstOrDefault<Contractor>(query, new { Id });
        }

        public Contractor CreateContractor(Contractor contractor)
        {
            //will crash if fails
            _db.Execute("INSERT INTO contractors (id, name, rate) VALUES (@Id, @Name, @Rate)", contractor);
            return contractor;
        }


        //edit
        public Contractor EditContractor(string id, Contractor con)
        {
            //WILL FAIL ON BAD ID
            string query = @"
                UPDATE contractors SET
                    name = @Name,
                    rate = @Rate
                WHERE id = @Id;
                SELECT * FROM contractors WHERE id = @Id
            ";
            return _db.QueryFirstOrDefault<Contractor>(query, con);
        }

        //delete
        public bool DeleteContractor(string conId)
        {
            int success = _db.Execute("DELETE FROM contractors WHERE id = @conId", new { conId });
            return success > 0;
        }

        public string AddConToJob(string conId, int jobId)
        {
            int success = _db.Execute(@"
            INSERT INTO contractorJobs (contractorId, jobId) 
                VALUES (@conId, @jobId)",
                new
                {
                    conId,
                    jobId
                });
            return success > 0 ? "success!" : "something has gone terribly wrong";
        }

        internal IEnumerable<Contractor> GetConsByJobId(int jobId)
        {
            string query = @"
            SELECT * FROM contractorjobs cj
            INNER JOIN contractors c ON c.id = cj.contractorId
            WHERE jobId = @jobId;
            ";
            return _db.Query<Contractor>(query, new { jobId });
        }
    }
}

