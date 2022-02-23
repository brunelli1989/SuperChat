using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperChat.Web.Repositories;
using System;
using System.Threading.Tasks;

namespace SuperChat.Web.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IMessageRepository _messageRepository;

        public ChatController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<IActionResult> Join(Guid id)
        {
            ViewData.Model = await _messageRepository.Get(id);

            return View(nameof(Index));
        }
    }
}