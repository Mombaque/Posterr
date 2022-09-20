using Api.Core;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StriderBackend.Api.Controllers.V1.InputModels;
using StriderBackend.Api.Controllers.V1.ViewModels;
using StriderBackend.Domain.Models;
using StriderBackend.Domain.Repositories;

namespace StriderBackend.Api.Controllers.V1
{
    public class PostController : ApiController
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(
            IPostRepository postRepository, 
            IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpGet("get-posts-by-user-id/{userId}")]
        public async Task<IActionResult> GetPostsByUserId(int userId)
        {
            var posts = _postRepository.GetUserPosts(userId);
            var viewModel = _mapper.Map<IEnumerable<PostViewModel>>(posts);
            return await Task.FromResult(Response(viewModel));
        }

        [HttpPost("save-post")]
        public async Task<IActionResult> SavePost(PostInputModel input)
        {
            var result = _mapper.Map<Post>(input);
            return await Task.FromResult(Response(result));
        }
    }
}
