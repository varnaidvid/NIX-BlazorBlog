using Blog.BlazorServer.ViewModels;

namespace Blog.BlazorServer.Clients.Interfaces
{
  public interface ICommentClient
  {
    Task<HttpResponseMessage> AddAsync(CommentViewModel commentDTO);

    Task<HttpResponseMessage> GetByPostIdAsync(int postId);

    Task<HttpResponseMessage> DeleteAsync(int id);
  }
}
