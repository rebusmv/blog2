using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repository
{
    public interface IRepository
    {
        public Post GetPost(int id);
        public List<Post> GetAllPosts();
        public void AddPost(Post post);
        public void UpdatePost(Post post);
        public void RemovePost(int id);
        public Task<bool> SaveChangesAsync();
    }
}
