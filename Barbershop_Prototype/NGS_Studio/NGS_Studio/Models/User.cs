﻿using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;

namespace NGS_Studio.Models
{
	public class User
	{
		[PrimaryKey]
		public string PhoneNumber { get; set; }

		public int Age { get; set; }

		public string Name { get; set; }

		public string Email { get; set; }
		public string Barber { get; set; }

		public bool IsBarber { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public string Location { get; set; }

		public string Details { get; set; }

		public string ImageUrl { get; set; }

	}
}
