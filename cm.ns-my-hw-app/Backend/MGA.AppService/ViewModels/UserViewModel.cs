using MGA.CrossCutting.Data;
using System;

namespace MGA.AppService.ViewModels
{
    public class UserViewModel
    {
		public int ID { get; set; }
		public DateTime DateCreated { get; set; }
		public int UserID { get; set; }
		public virtual User User { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int? UserIDUpdated { get; set; }
		public virtual User UserUpdated { get; set; }
		public DateTime? DateExcluded { get; set; }
		public int? UserIDExcluded { get; set; }
		public virtual User UserExcluded { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
