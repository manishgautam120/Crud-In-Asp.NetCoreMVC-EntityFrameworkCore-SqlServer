using Microsoft.AspNetCore.Mvc;
using OA.DATA;
using OA.SERVICE;
using OA.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OA.WEB.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IUserProfileService userProfileService;
        public UserController(IUserService _userService, IUserProfileService _userProfileService)
        {
            this.userService = _userService;
            this.userProfileService = _userProfileService;
        }
        public IActionResult Index()
        {
            List<UserViewModel> model = new List<UserViewModel>();
            userService.GetUsers().ToList().ForEach(u =>
            {
                UserProfile userProfile = userProfileService.GetUserProfile(u.Id);
                UserViewModel user = new UserViewModel
                {
                    Id = u.Id,
                    FirstName = userProfile.FirstName,
                    LastName = userProfile.LastName,
                    Name = $"{userProfile.FirstName} {userProfile.LastName}",
                    Email = u.Email,
                    Address = userProfile.Address,
                    UserName = u.UserName
                };
                model.Add(user);
            });

            return View(model);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            UserViewModel model = new UserViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {

                User userEntity = new User
                {

                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password,
                    AddedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    UserProfile = new UserProfile
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Address = model.Address,
                        AddedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString()
                    }
                };

                userService.InsertUser(userEntity);
                if (userEntity.Id > 0)
                {
                    return RedirectToAction("index");
                }
            }
            return View(model);
        }

        public ActionResult EditUser(int? id)
        {
            UserViewModel model = new UserViewModel();
            if (id.HasValue && id != 0)
            {
                User userEntity = userService.GetUser(id.Value);
                UserProfile userProfileEntity = userProfileService.GetUserProfile(id.Value);
                model.FirstName = userProfileEntity.FirstName;
                model.LastName = userProfileEntity.LastName;
                model.Address = userProfileEntity.Address;
                model.Email = userEntity.Email;
                model.UserName = userEntity.UserName;
                model.Password = userEntity.Password;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditUser(UserViewModel model)
        {
            User userEntity = userService.GetUser(model.Id);
            userEntity.Email = model.Email;
            userEntity.ModifiedDate = DateTime.Now;
            userEntity.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            userEntity.UserName = model.UserName;
            userEntity.Password = model.Password;


            UserProfile userProfileEntity = userProfileService.GetUserProfile(model.Id);
            userProfileEntity.FirstName = model.FirstName;
            userProfileEntity.LastName = model.LastName;
            userProfileEntity.Address = model.Address;
            userProfileEntity.ModifiedDate = DateTime.Now;
            userProfileEntity.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            userEntity.UserProfile = userProfileEntity;
            userService.UpdateUser(userEntity);
            if (userEntity.Id > 0)
            {
                return RedirectToAction("index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            UserProfile userProfile = userProfileService.GetUserProfile(id);
            string name = $"{userProfile.FirstName} {userProfile.LastName}";
            UserViewModel vw = new UserViewModel();
            vw.Id = id;
            vw.Name = name;

            return View(vw);
        }

        [HttpPost]
        public ActionResult DeleteUser(int id, UserViewModel model)
        {
            userService.DeleteUser(model.Id);
            return RedirectToAction("Index");
        }

    }
}

