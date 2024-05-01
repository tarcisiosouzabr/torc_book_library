﻿using EasyNetQ;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TorcBooks.Data;
using TorcBooks.Integration;

namespace TorcBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksRepository _booksRepository;
        public BooksController(BooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] string searchBy, [FromQuery] string searchValue)
        {
            var booksResponse = await _booksRepository.GetAsync(searchBy, searchValue);
            return Ok(booksResponse.Select(x => new
            {
                BookTitle = x.Title,
                Authors = $"{x.LastName}, {x.FirstName}",
                x.Type,
                x.ISBN,
                x.Category,
                AvaliableCopies = $"{x.CopiesInUse}/{x.TotalCopies}"
            }).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync()
        {
            try
            {
                string? rabbitConnection = Environment.GetEnvironmentVariable("RABBITMQ");
                using (var bus = RabbitHutch.CreateBus(rabbitConnection))
                {
                    await bus.PubSub.PublishAsync(new IntegrationEvent { Message = "Sended through RabbitMQ" }, "BookCreate");
                }
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
