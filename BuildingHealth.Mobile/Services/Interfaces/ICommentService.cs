using BuildingHealth.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingHealth.Mobile.Services.Interfaces
{
    public interface ICommentService
    {
        public Task<List<Comment>> GetProjectComments(string projectId);
        public Task PostComment(Comment comment);
    }
}
