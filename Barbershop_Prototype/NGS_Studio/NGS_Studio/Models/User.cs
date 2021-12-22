using SQLite;
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

		//public static FirebaseClient firebase = new FirebaseClient("https://ngs-studios-bd357-default-rtdb.firebaseio.com/");
		//public static async Task<List<User>> GetAllUsers()
		//{

		//	return (await firebase
		//	  .Child("User")
		//	  .OnceAsync<User>()).Select(item => new User
		//	  {
		//		  PhoneNumber = item.Object.PhoneNumber,
		//		  Name = item.Object.Name
		//	  }).ToList();
		//}
		//public static async Task AddUser(string phonenumber, string name)
		//{

		//	await firebase
		//	  .Child("Users")
		//	  .PostAsync(new User() { PhoneNumber = phonenumber, Name = name });
		//}
		//public static async Task<User> GetUser(string PhoneNumber)
		//{
		//	var allUsers = await GetAllUsers();
		//	await firebase
		//	  .Child("Users")
		//	  .OnceAsync<User>();
		//	return allUsers.Where(a => a.PhoneNumber == PhoneNumber).FirstOrDefault();
		//}
		//public static async Task UpdateUser(string PhoneNumber, string name)
		//{
		//	var toUpdateUser = (await firebase
		//	  .Child("Users")
		//	  .OnceAsync<User>()).Where(a => a.Object.PhoneNumber == PhoneNumber).FirstOrDefault();

		//	await firebase
		//	  .Child("Users")
		//	  .Child(toUpdateUser.Key)
		//	  .PutAsync(new User() { PhoneNumber = PhoneNumber, Name = name });
		//}

		//public static async Task DeleteUser(string PhoneNumber)
		//{
		//	var toDeleteUser = (await firebase
		//	  .Child("Users")
		//	  .OnceAsync<User>()).Where(a => a.Object.PhoneNumber == PhoneNumber).FirstOrDefault();
		//	await firebase.Child("Users").Child(toDeleteUser.Key).DeleteAsync();

		//}
	}
}
