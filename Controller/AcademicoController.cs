using edu_connect_backend.DTO;
using edu_connect_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("academico")]
    [Authorize]
    public class AcademicoController : ControllerBase
    {
        private readonly AcademicoService academicoService;

        public AcademicoController(AcademicoService academicoService)
        {
            this.academicoService = academicoService;
        }

        
    }
}