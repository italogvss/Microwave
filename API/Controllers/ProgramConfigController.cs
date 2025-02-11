using Data.Context;
using Microondas.API.Exeptions;
using Microondas.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.DTO.Request;
using Shared.Models.Model;

namespace Microondas.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProgramConfigController : ControllerBase
    {
        private readonly IProgramConfigService _programConfigService;

        public ProgramConfigController(IProgramConfigService programConfigService)
        {
            _programConfigService = programConfigService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var programConfigs = await _programConfigService.GetAllAsync();
            return Ok(programConfigs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var programs = await _programConfigService.GetAllAsync();
            var programConfig = programs.FirstOrDefault(p => p.Id == id);
            if (programConfig == null)
            {
                return NotFound();
            }
            return Ok(programConfig);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProgramConfigRequestDTO programConfig)
        {
            if (programConfig == null)
            {
                return BadRequest();
            }
            try
            {
                await _programConfigService.AddAsync(programConfig);
                return CreatedAtAction(nameof(GetById), new { id = programConfig.Id }, programConfig);
            }
            catch (Exception ex)
            {

                return Conflict(ex.Message);
            }            
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProgramConfigRequestDTO programConfig)
        {
            if (id != programConfig.Id)
            {
                return BadRequest();
            }
            try
            {
                await _programConfigService.UpdateAsync(programConfig);
                return NoContent();
            }
            catch (DuplicateEntryException ex)
            {
                return Conflict(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _programConfigService.DeleteAsync(id);
            return NoContent();
        }
    
}
}
