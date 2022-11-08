using DEVCoursesAPI.Data.DTOs;
using DEVCoursesAPI.Data.Models;
using DEVCoursesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DEVCoursesAPI.Controllers;

[Route("users")]
[ApiController]
public class UsersController : ControllerBase
{
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersService _usersService;
        private readonly IOptions<TokenSettings> _tokenSettings;


        public UsersController(IUsersService usersService, IOptions<TokenSettings> tokenSettings, ILogger<UsersController> logger)
        {
            _usersService = usersService;
            _tokenSettings = tokenSettings;
            _logger = logger;
        }


        /// <summary>
        /// Inserir usuário
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Retorna Id do usuário inserido</returns>
        /// <response code = "201">Usuário inserido com sucesso</response>
        /// <response code = "400">Inserção não realizada</response>
        /// <response code = "500">Erro execução</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([FromBody] CreateUser user)
        {
            try
            {
                return StatusCode(201); ;

            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Controller:{nameof(UsersController)} - Method:{nameof(Post)}");
                return StatusCode(500, e.Message);
            }

        }
        
        
    
}