using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace SuperChat.Web.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        public async Task<IActionResult> Join(string id)
        {
            return await Task.FromResult(View(nameof(Index)));
        }
    }
}