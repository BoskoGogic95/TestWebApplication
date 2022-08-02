using ApiLayer.Models.CandidateModels;
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
    public class CandidateController : BaseController
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService, IMapper mapper) : base(mapper)
        {
            _candidateService = candidateService;
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var modelList = await _candidateService.GetAll();
            var responseList = new List<CandidateResponse>();

            foreach (var model in modelList)
            {
                responseList.Add(_mapper.Map<CandidateResponse>(model));
            }
            return Ok(responseList); 
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var model = await _candidateService.GetById(id);
            var candidateResponse = _mapper.Map<CandidateResponse>(model);
            return model == null ? NotFound() : Ok(candidateResponse);
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("getbyname/{name}")]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            var model = await _candidateService.GetByName(name);
            var candidateResponse = _mapper.Map<CandidateResponse>(model);
            return model == null ? NotFound() : Ok(candidateResponse);
        }

        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CandidateCreateRequest createReqBody)
        {
            var candidateModel = _mapper.Map<CandidateModel>(createReqBody);
            var createdId = await _candidateService.Create(candidateModel);

            return createdId == null ? BadRequest() : CreatedAtAction(nameof(GetById), new { id = "Guid" }, createdId);
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _candidateService.Delete(id);

            return result ? Ok() : NotFound();
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAccount([FromRoute] Guid id, [FromBody] CandidateUpdateRequest candidateUpdateRequest)
        {
            var currentCandidate = await _candidateService.GetById(id);
            
            if(currentCandidate == null) return NotFound();

            return Ok(await _candidateService.Update(id, _mapper.Map<CandidateModel> (candidateUpdateRequest)));
            //Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;
        }


    }
}
