using MGA.CrossCutting.Data;
using System;

namespace MGA.Domain.Models
{
    public class Account : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
		public string Password { get; set; }
	}
}
