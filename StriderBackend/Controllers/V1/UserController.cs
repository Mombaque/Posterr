using Api.Core;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StriderBackend.Domain.Repositories;

namespace StriderBackend.Api.Controllers.V1
{
    [Route("[controller]")]
    public class UserController : ApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("get-user-with-posts/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = _userRepository.GetUserWithPosts(id);
            return await Task.FromResult(Response(user));
        }
    }
}
