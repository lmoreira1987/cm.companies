using System;
using System.ComponentModel;

namespace MGA.CrossCutting.Data
{
	public abstract class BaseEntity
	{
		[Description("Primary Key")]
		public int ID { get; set; }

		[Description("Date Created")]
		public DateTime DateCreated { get; set; }

		[Description("UserID")]
		public int UserID { get; set; }

		[Description("User")]
		public virtual User User { get; set; }

		[Description("Date Updated")]
		public DateTime? DateUpdated { get; set; }

		[Description("Updated User ID")]
		public int? UserIDUpdated { get; set; }

		[Description("UserUpdated")]
		public virtual User UserUpdated { get; set; }

		[Description("Date Excluded")]
		public DateTime? DateExcluded { get; set; }

		[Description("Deletion User ID")]
		public int? UserIDExcluded { get; set; }

		[Description("UserExcluded")]
		public virtual User UserExcluded { get; set; }
	}
}
