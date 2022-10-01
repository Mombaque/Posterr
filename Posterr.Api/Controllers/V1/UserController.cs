using Api.Core;
using AutoMapper;
using Domain.Core.Mediator;
using Domain.Core.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Posterr.Api.Controllers.V1.InputModels;
using Posterr.Api.Controllers.V1.ViewModels;
using Posterr.Domain.Commands.User;
using Posterr.Domain.Repositories;

namespace Posterr.Api.Controllers.V1
{
    [Route("[controller]")]
    public class UserController : ApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public UserController(
            IUserRepository userRepository,
            IMapper mapper,
            IRequestHandler<DomainNotification, bool> notificationHandler,
            IMediatorHandler mediator) : base(notificationHandler)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(UserViewModel), (int)System.Net.HttpStatusCode.OK)]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = _userRepository.GetUser(id);
            var viewModel = _mapper.Map<UserViewModel>(user);
            return await Task.FromResult(Response(viewModel));
        }

        [HttpGet("get-followers/{userId}")]
        [ProducesResponseType(typeof(bool), (int)System.Net.HttpStatusCode.OK)]
        public async Task<IActionResult> GetFollowers(int userId)
        {
            var userFollowers = _userRepository.GetUserFollowers(userId);
            var viewModel = _mapper.Map<IEnumerable<UserViewModel>>(userFollowers);
            return await Task.FromResult(Response(viewModel));
        }

        [HttpPost("follow-or-unfollow")]
        [ProducesResponseType(typeof(bool), (int)System.Net.HttpStatusCode.OK)]
        public async Task<IActionResult> Follow(FollowUserInputModel input)
        {
            var command = _mapper.Map<FollowOrUnfollowUserCommand>(input);
            var result = await _mediator.SendCommand(command);

            return await Task.FromResult(Response(result));
        }
    }
}
