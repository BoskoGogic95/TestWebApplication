using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiLayer.Controllers
{
    [Route("api/[ontroller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMapper _mapper;

        protected BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        
    }
}
