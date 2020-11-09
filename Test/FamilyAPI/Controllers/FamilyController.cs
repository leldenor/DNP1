using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FamilyAPI.Data;
using FamilyAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FamilyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FamilyController:ControllerBase
    {
        private IFamilyService familyService;

        public FamilyController(IFamilyService familyService)
        {
            this.familyService = familyService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Family>>> GetFamily()
        {
            try
            {
                IList<Family> families = await familyService.GetFamilies();
                return Ok(families);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Family>> AddFamily([FromBody] Family family)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Family added = await familyService.AddFamily(family);
                return Created($"/{added.StreetName}&{added.HouseNumber}", added);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("{streetName}&{houseNumber:int}")]
        public async Task<ActionResult> DeleteFamily([FromBody] string streetName, int houseNumber)
        {
            try
            {
                await familyService.RemoveFamily(houseNumber, streetName);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("{streetName}&{houseNumber:int}")]
        public async Task<ActionResult> UpdateFamily([FromBody] Family family)
        {
            try
            {
                await familyService.UpdateAsync(family);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}