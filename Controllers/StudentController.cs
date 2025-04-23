using CoreDotNetToken.Models;
using CoreDotNetToken.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace CoreDotNetToken.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepo;

        public StudentController(IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _studentRepo.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var student = await _studentRepo.GetByIdAsync(id);
            return student == null ? NotFound() : Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Student student)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            await _studentRepo.CreateAsync(student);
            return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Student student)
        {
            var existing = await _studentRepo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            student.Id = id;
            await _studentRepo.UpdateAsync(id, student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _studentRepo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _studentRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}

