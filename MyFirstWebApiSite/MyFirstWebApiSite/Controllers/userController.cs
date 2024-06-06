

using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;
using Entities;
using AutoMapper;
using DTOs;
using System.ComponentModel.DataAnnotations;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyFirstWebApiSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {

        private IUsersServices _userService;
        private IMapper _mapper;
        private readonly ILogger<userController> _logger;

        public userController(IUsersServices usersServices,IMapper mapper, ILogger<userController> logger)
        {
            _userService = usersServices;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<userDTO>> GetById(int id)
        {
            User user = await _userService.GetById(id);
            userDTO userDto = _mapper.Map<User, userDTO>(user);
            if (user != null)
                return Ok(userDto);
            return NoContent();
        }

        [HttpPost]
        [Route("checkPassword")]
        public ActionResult<int> Register([FromBody] string password)
        {
            return _userService.CheckPassword(password);
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<userDTO>> Register([FromBody] userDTO userDto)
        {
            User user = _mapper.Map<userDTO, User>(userDto);
            User newUser = await _userService.Register(user);
            userDTO userToReturn = _mapper.Map<User, userDTO>(newUser);
            if (newUser != null)
                return Ok(userToReturn);
            return NoContent();
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<userDTO>> Login([FromBody] userLoginDTO userLoginDto)
        {
           
            UserLogin userToLogin = _mapper.Map<userLoginDTO, UserLogin>(userLoginDto);
           
            _logger.LogInformation($"Login attempted with User Name,{userToLogin.Email} and password {userToLogin.Password}");
            //_logger.LogError($"Login attempted with User Name,{userToLogin.Email} and password {userToLogin.Password}");
            User user = await _userService.Login(userToLogin);
            userDTO userToReturn = _mapper.Map<User, userDTO>(user);
            if (user != null)
                return Ok(userToReturn);
            return Unauthorized();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] userDTO userToUpdate)
        {
            User updateUser = _mapper.Map<userDTO, User>(userToUpdate);
            int strongPass = _userService.CheckPassword(updateUser.Password);
            if (strongPass < 2)
                return BadRequest();
            User user= await _userService.Update(id, updateUser);
            userDTO userToReturn = _mapper.Map<User, userDTO>(user);
            if (user != null)
                return Ok();
            return BadRequest();

        }


    }
}

