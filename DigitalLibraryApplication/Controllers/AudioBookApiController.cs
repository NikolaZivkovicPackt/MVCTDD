using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalLibraryApplication.Models;
using DigitalLibraryApplication.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DigitalLibraryApplication.Controllers
{
    [Route("api/[controller]")]
    public class AudioBookApiController : Controller
    {
        private IAudioBookService _audioBookService;

        public AudioBookApiController(IAudioBookService audioBookService)
        {
            _audioBookService = audioBookService;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<AudioBook> Get()
        {
            return _audioBookService.GetAll();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var item = _audioBookService.GetById(id);

            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Create([FromBody]AudioBook value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            try
            {
                _audioBookService.Add(value);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody]AudioBook value)
        {
            if (value == null || value.Id != id)
            {
                return BadRequest();
            }

            try
            {
                _audioBookService.Update(id, value);
            }
            catch (Exception e)
            {
                return NotFound();
            }

            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _audioBookService.Delete(id);
            }
            catch (Exception e)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
