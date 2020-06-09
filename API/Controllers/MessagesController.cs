﻿using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Npgsql;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class MessagesController : ControllerBase
    {
        List<Messages> message = new List<Messages>();


        public MessagesController()
        {
            message.Add(new Messages { id = 1, username = "Reid", text = "Hey there." });
            message.Add(new Messages { id = 2, username = "Reid", text = "Hey there." });
            message.Add(new Messages { id = 3, username = "Reid", text = "Hey there." });
        }

        [HttpGet]
        public async System.Threading.Tasks.Task GetAsync()
        {
            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            // Retrieve all rows
            await using (var cmd = new NpgsqlCommand("SELECT * FROM messages", conn))
            {
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var id = reader[0]; 
                        var userId = reader[1];
                        var text = reader[2];
                        var createdDate = reader[3];

                    }
                }
            }
                           
        }

        [HttpPost]
        public async System.Threading.Tasks.Task PostAsync(UserInput userInput)
        {
            var connString = "Host=localhost;Username=reid;Password=Lucy07181985!;Database=chat_app";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            using (var cmd = new NpgsqlCommand("INSERT INTO messages (id, user_id, text, created_date) VALUES (@id, @user_id, @text, @created_date)", conn))
            {
                cmd.Parameters.AddWithValue("@id", userInput.Id);
                cmd.Parameters.AddWithValue("@user_id", userInput.UserId);
                cmd.Parameters.AddWithValue("@text", userInput.Text);
                cmd.Parameters.AddWithValue("@created_date", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
        }
      
    }
}
