using Api.Core;
using AutoMapper;
using Domain.Core.Notification;
using MediatR;
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
            IMapper mapper,
            IRequestHandler<DomainNotification, bool> notificationHandler) : base(notificationHandler)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("get-user-with-posts/{id}")]
        public async Task<IActionResult> GetPosts(int id)
        {
            var user = _userRepository.GetUserWithPosts(id);
            return await Task.FromResult(Response(user));
        }
    }
}
