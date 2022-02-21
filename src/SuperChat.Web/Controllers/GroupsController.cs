using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperChat.Web.Models;
using SuperChat.Web.Repositories;
using System;

namespace SuperChat.Web.Controllers
{
    [Authorize]
    public class GroupsController : Controller
    {
        private readonly IGroupsRepository _groupsRepository;

        public GroupsController(IGroupsRepository groupsRepository)
        {
            _groupsRepository = groupsRepository;
        }

        // GET: GroupsController
        public ActionResult Index()
        {
            ViewData.Model = _groupsRepository.Get();

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
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new GroupViewModel
                {
                    Name = collection["Name"]
                };

                _groupsRepository.Add(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GroupsController/Edit/5
        public ActionResult Edit(Guid id)
        {
            ViewData.Model = _groupsRepository.Get(id);

            return View();
        }

        // POST: GroupsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = _groupsRepository.Get(id);

                model.Name = collection["Name"];

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GroupsController/Delete/5
        public ActionResult Delete(Guid id)
        {
            _groupsRepository.Remove(id);

            return RedirectToAction(nameof(Index));
        }

        // POST: GroupsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}