using System.Collections.Generic;
using System;

namespace NGS_Studio.Models
{
	public class User
	{
		public string PhoneNumber { get; set; }

		public int Age { get; set; }

		public string Name { get; set; }

		public string Email { get; set; }

		public string Barber { get; set; }

		public bool IsBarber { get; set; }

		public bool IsOwner { get; set; }

		public bool IsClient { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public string Location { get; set; }

		public string Details { get; set; }

		public string ImageUrl { get; set; }

		public string checkin { get; set; }


        public static implicit operator List<object>(User v)
        {
            throw new NotImplementedException();
        }
    }
}
