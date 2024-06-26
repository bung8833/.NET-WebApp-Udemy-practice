using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnet_rpg.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase // controller w/o View
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }


        [HttpGet]
        [Route("GetAll")] // More than one Get function
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get() 
        {
            // Return characters that belong to the current user
            return Ok(await _characterService.GetYourCharacters());
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
        {
            return Ok(await _characterService.GetYourCharacterById(id));
        }

        
        [HttpGet]
        [Route("Name/{name}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> 
            GetCharactersByName(string name)
        {
            return Ok(await _characterService.GetYourCharactersByName(name));
        }


        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> 
            AddCharacter(AddCharacterDto addDto)
        {
            return Ok(await _characterService.AddCharacter(addDto));
        }


        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> 
            UpdateCharacter(UpdateCharacterDto updateDto)
        {
            var response = await _characterService.UpdateYourCharacter(updateDto);

            if (response.Data is null) {
                return NotFound(response);
            }

            return Ok(response);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> 
            DeleteCharacter(int id)
        {
            var response = await _characterService.DeleteYourCharacter(id);

            // check if character not found
            if (response.Success == false) {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("Skill")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> 
            AddCharacterSkill(AddCharacterSkillDto addSkillDto)
        {
            return Ok(await _characterService.AddCharaterSkill(addSkillDto));
        }
    }
}