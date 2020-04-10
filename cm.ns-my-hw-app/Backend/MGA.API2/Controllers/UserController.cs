using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MGA.AppService.Interfaces;
using MGA.AppService.ViewModels;

namespace MGA.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
		private readonly IUserAppService _userAppService;

		public UserController(IUserAppService userAppService)
		{
			_userAppService = userAppService;
		}

		// GET api/values
		[HttpGet]
        public IList<UserViewModel> Get()
        {
			IList<UserViewModel> returnValue = new List<UserViewModel>();

			try
			{
				returnValue = _userAppService.Get();
			}
			catch (Exception exp)
			{
				throw new Exception("Get: " + exp);
			}

			return returnValue;
		}

        // GET api/values/5
        [HttpGet("{id}")]
        public UserViewModel Get(int id)
        {
			UserViewModel returnValue = new UserViewModel();

			try
			{
				returnValue = _userAppService.Get(id);
			}
			catch (Exception exp)
			{
				throw new Exception("Get(id): " + exp);
			}

			return returnValue;
		}

		// GET api/values/5
		[HttpGet("Login")]
		public UserViewModel Login(string email, string password)
		{
			UserViewModel returnValue = new UserViewModel();

			try
			{
				returnValue = _userAppService.Login(email, password);
			}
			catch (Exception exp)
			{
				throw new Exception("Login(email,password)" + exp);
			}

			return returnValue;
		}

		// POST api/values
		[HttpPost]
        public UserViewModel Post([FromBody]UserViewModel value)
        {
			UserViewModel returnValue = new UserViewModel();

			try
			{
				returnValue = _userAppService.Post(value);
			}
			catch (Exception exp)
			{
				throw new Exception("Post: " + exp);
			}

			return returnValue;
		}

        // PUT api/values/5
        [HttpPut("{id}")]
        public UserViewModel Put(int id, [FromBody]UserViewModel value)
        {
			UserViewModel returnValue = new UserViewModel();

			try
			{
				returnValue = _userAppService.Put(value);
			}
			catch (Exception exp)
			{
				throw new Exception("Put: " + exp);
			}

			return returnValue;
		}

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
			try
			{
				_userAppService.Delete(id);
			}
			catch (Exception exp)
			{
				throw new Exception("Delete: " + exp);
			}
		}
    }
}
