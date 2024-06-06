using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RatingRepository : IRatingRepository
    {
        public IConfiguration _configuration { get; }

        public RatingRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> addRating(Rating rating)
        {

            string query = "INSERT INTO RATING(HOST,METHOD,PATH,REFERER,USER_AGENT,Record_Date)" + "VALUES(@host,@method,@path,@referer,@user_agent,@record_date)";
            using (SqlConnection cn = new SqlConnection(_configuration["ConnectionStrings"]))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.Parameters.AddWithValue("@HOST", rating.Host);
                cmd.Parameters.AddWithValue("@METHOD", rating.Method);
                cmd.Parameters.AddWithValue("@PATH", rating.Path);
                cmd.Parameters.AddWithValue("@REFERER", rating.Referer);
                cmd.Parameters.AddWithValue("@USER_AGENT", rating.UserAgent);
                cmd.Parameters.AddWithValue("@Record_Date", DateTime.Now);

                cn.Open();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                cn.Close();

                return rowsAffected;
            }

        }
    }
}
