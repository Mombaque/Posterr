using Api.Core;
using Microsoft.AspNetCore.Mvc;
using StriderBackend.Domain.Repositories;

namespace StriderBackend.Api.Controllers.V1
{
    [Route("[controller]")]
    public class UserController : ApiController
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("get-user-with-posts/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = _userRepository.GetUserWithPosts(id);
            return await Task.FromResult(Response(user));
        }
    }
}
