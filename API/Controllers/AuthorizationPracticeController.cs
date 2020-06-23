using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Npgsql;
using System.Net.Http;
using Microsoft.Net.Http.Headers;

namespace API.Controllers
{
    [ApiController]
    [Route("api/authpractice")]

    public class AuthorizationPracticeController : ControllerBase
    {


        [HttpPost]
        public async System.Threading.Tasks.Task<List<SessionModel>> PostAsync([FromForm] SessionModel sessionModel)
        {
            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";
       
            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            using (var cmd = new NpgsqlCommand("INSERT INTO sessions (user_id) VALUES (1) RETURNING id", conn))
            {

                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    List<SessionModel> sessionList = new List<SessionModel>();

                    while (await reader.ReadAsync())
                    {
                        var session = new SessionModel();                                          
                        session.Id = (int)reader[0];
                        sessionList.Add(session);

                    }
                    return sessionList;
                }

      
            }

        }

      

    }
}
