using System.Security.Claims;
using Blog.Business.DTOs;
using Blog.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers
{
  [Route("api/comment")]
  [ApiController]
  [Authorize]
  public class CommentController : ControllerBase
  {
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
      _commentService = commentService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody] CommentDTO commentDTO)
    {
      if (commentDTO == null)
      {
        return BadRequest("CommentDTO cannot be null.");
      }

      var user = HttpContext.User;
      var userName = user.FindFirst(ClaimTypes.Name)?.Value;

      if (string.IsNullOrEmpty(userName))
      {
        return BadRequest("User name not found in claims.");
      }

      commentDTO.CreatedBy = userName;

      await _commentService.CreateAsync(commentDTO);
      return Created();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCommentById(int id)
    {
      var comment = await _commentService.GetByIdAsync(id);
      if (comment == null)
      {
        return NotFound();
      }
      return Ok(comment);
    }

    [HttpGet("post/{postId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCommentsByPostId(int postId)
    {
      var comments = await _commentService.GetByPostIdAsync(postId);
      return Ok(comments);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteComment(int id)
    {
      var existingComment = await _commentService.GetByIdAsync(id);
      if (existingComment == null)
      {
        return NotFound();
      }

      await _commentService.DeleteAsync(id);
      return NoContent();
    }
  }
}
