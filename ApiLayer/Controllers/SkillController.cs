using ApiLayer.Models.SkillModels;
using AutoMapper;
using Business_Access_Layer.Models;
using Business_Access_Layer.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : BaseController
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService, IMapper mapper) : base(mapper)
        {
            _skillService = skillService;
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var modelList = await _skillService.GetAll();
            var responseList = new List<SkillResponse>();

            foreach (var model in modelList)
            {
                responseList.Add(_mapper.Map<SkillResponse>(model));
            }
            return Ok(responseList);
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var model = await _skillService.GetById(id);
            var skilLResponse = _mapper.Map<SkillResponse>(model);
            return model == null ? NotFound() : Ok(skilLResponse);
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("getbyname/{name}")]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            var model = await _skillService.GetByName(name);
            var skilLResponse = _mapper.Map<SkillResponse>(model);
            return model == null ? NotFound() : Ok(skilLResponse);
        }

        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SkillCreateRequest createReqBody)
        {
            var candidateModel = _mapper.Map<SkillModel>(createReqBody);
            var createdId = await _skillService.Create(candidateModel);

            return createdId == null ? BadRequest() : CreatedAtAction(nameof(GetById), new { id = "Guid" }, createdId);
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _skillService.Delete(id);

            return result ? Ok() : NotFound();
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAccount([FromRoute] Guid id, [FromBody] SkillCreateRequest skillCreateReq)
        {
            var currentCandidate = await _skillService.GetById(id);

            if (currentCandidate == null) return NotFound();

            return Ok(await _skillService.Update(id, _mapper.Map<SkillModel>(skillCreateReq)));

        }

    }
}
