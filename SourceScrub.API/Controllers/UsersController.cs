using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SourceScrub.API.Models;
using SourceScrub.Business.Services.Interfaces;
using SourceScrub.Entities;

namespace SourceScrub.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a specific question by ID.
        /// </summary>
        /// <param name="id">The ID of the question.</param>
        /// <returns>The question details.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            var user = await _userService.GetAsync(id);
            if (user == null) return NotFound();
            return Ok(_mapper.Map<UserModel>(user));
        }

        /// <summary>
        /// Adds a new question.
        /// </summary>
        /// <param name="userModel">The question to be added.</param>
        /// <returns>The newly created question.</returns>
        [HttpPost]
        public async Task<ActionResult<UserModel>> Post([FromBody]UserModel userModel)
        {
            if(userModel == null) return BadRequest();
            var user = _mapper.Map<User>(userModel);
            user = await _userService.AddAsync(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, _mapper.Map<UserModel>(user));
        }
    }
}
