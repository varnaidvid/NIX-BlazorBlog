using Blog.Business.DTOs;
using System.Collections.Generic;

namespace Blog.Business.Interfaces
{
  public interface ICommentService
  {
    Task CreateAsync(CommentDTO commentDTO);

    Task DeleteAsync(int id);

    Task<CommentDTO> GetByIdAsync(int id);

    Task<List<CommentDTO>> GetByPostIdAsync(int postId);
  }
}
