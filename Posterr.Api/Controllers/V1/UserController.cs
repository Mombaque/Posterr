using Api.Core;
using AutoMapper;
using Domain.Core.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Posterr.Api.Controllers.V1.ViewModels;
using Posterr.Domain.Repositories;

namespace Posterr.Api.Controllers.V1
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
            var viewModel = _mapper.Map<UserViewModel>(user);
            return await Task.FromResult(Response(viewModel));
        }

        [HttpGet("get-followers/{userId}")]
        public async Task<IActionResult> GetFollowers(int userId)
        {
            var userFollowers = _userRepository.GetUserFollowers(userId);
            var viewModel = _mapper.Map<IEnumerable<UserViewModel>>(userFollowers);
            return await Task.FromResult(Response(viewModel));
        }
    }
}
