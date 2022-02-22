using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperChat.Web.Models;
using SuperChat.Web.Repositories;
using System;
using System.Threading.Tasks;

namespace SuperChat.Web.Controllers
{
    [Authorize]
    public class GroupsController : Controller
    {
        private readonly IGroupRepository _groupsRepository;

        public GroupsController(IGroupRepository groupsRepository)
        {
            _groupsRepository = groupsRepository;
        }

        // GET: GroupsController
        public async Task<ActionResult> Index()
        {
            ViewData.Model = await _groupsRepository.Get();

            return View();
        }

        // GET: GroupsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GroupsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            var model = new GroupViewModel
            {
                Name = collection["Name"]
            };

            await _groupsRepository.Add(model);

            await _groupsRepository.Commit();

            return RedirectToAction(nameof(Index));
        }

        // GET: GroupsController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            ViewData.Model = await _groupsRepository.Get(id);

            return View();
        }

        // POST: GroupsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, IFormCollection collection)
        {
            var viewModel = new GroupViewModel
            {
                Id = id,
                Name = collection["Name"]
            };
            
            await _groupsRepository.Update(viewModel);

            await _groupsRepository.Commit();

            return RedirectToAction(nameof(Index));
        }

        // GET: GroupsController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            await _groupsRepository.Remove(id);

            await _groupsRepository.Commit();

            return RedirectToAction(nameof(Index));
        }
    }
}