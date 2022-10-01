using Api.Core;
using AutoMapper;
using Domain.Core.Mediator;
using Domain.Core.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Posterr.Api.Controllers.V1.InputModels;
using Posterr.Api.Controllers.V1.ViewModels;
using Posterr.Domain.Commands.User;
using Posterr.Domain.Models;
using Posterr.Domain.Repositories;

namespace Posterr.Api.Controllers.V1
{
    [Route("[controller]")]
    public class PostController : ApiController
    {
        private readonly IPostRepository _postRepository;
        private readonly IMediatorHandler _mediator;
        private readonly IMapper _mapper;

        public PostController(
            IPostRepository postRepository, 
            IMediatorHandler mediatorHandler,
            IMapper mapper,
            IRequestHandler<DomainNotification, bool> notificationHandler) : base(notificationHandler)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _mediator = mediatorHandler;
        }

        [HttpGet("get-posts-by-user-id")]
        public async Task<IActionResult> GetPostsByUserId(GetPostsInputModel input)
        {
            var filter = _mapper.Map<GetPostsFilter>(input);
            var posts = _postRepository.GetUserPosts(filter);

            var viewModel = _mapper.Map<ICollection<PostViewModel>>(posts);
            return await Task.FromResult(Response(viewModel));
        }

        [HttpGet("get-posts")]
        [ProducesResponseType(typeof(PostViewModel), (int)System.Net.HttpStatusCode.OK)]
        public async Task<IActionResult> GetPosts(GetPostsInputModel input)
        {
            var filter = _mapper.Map<GetPostsFilter>(input);
            var posts = _postRepository.GetPosts(filter);

            var viewModel = _mapper.Map<IEnumerable<PostViewModel>>(posts);
            return await Task.FromResult(Response(viewModel));
        }

        [HttpPost("save-post")]
        public async Task<IActionResult> SavePost(SavePostInputModel input)
        {
            var command = _mapper.Map<SavePostCommand>(input);
            var result = await _mediator.SendCommand(command);

            return await Task.FromResult(Response(result));
        }
    }
}
