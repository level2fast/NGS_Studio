using System;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Database.Query;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NGS_Studio.Models;

namespace NGS_Studio.Services
{
    class UserTableService
    {

        //Connect app with firebase using API Url  
        public static FirebaseClient firebase = new FirebaseClient("https://ngs-studios-bd357-default-rtdb.firebaseio.com/");

        //Read All    
        public static async Task<List<User>> GetAllUser()
        {
            try
            {
                var userlist = (await firebase
                .Child("User")
                .OnceAsync<User>()).Select(item =>
                new User
                {
                    Name = item.Object.Name,
                    PhoneNumber = item.Object.PhoneNumber,
                    Email = item.Object.Email,
                    IsBarber = item.Object.IsBarber,
                    IsClient = item.Object.IsClient,
                }).ToList();
                return userlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Read     
        public static async Task<User> GetUser(string phoneNumber)
        {
            try
            {
                var allUsers = await GetAllUser();
                await firebase
                .Child("User")
                .OnceAsync<User>();
                return allUsers.Where(a => a.PhoneNumber == phoneNumber).FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        //Read     
        public static async Task<List<User>> GetAllBarbers()
        {
            try
            {
                var allUsers = await GetAllUser();
                return allUsers.Where(a => a.IsBarber == true).ToList();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        public static async Task<List<User>> GetAllClients()
        {
            try
            {
                var allUsers = await GetAllUser();
                return allUsers.Where(a => a.IsClient == true).ToList();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        /// <summary>
        /// Inserts a user into the database
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task<bool> AddUser(User usr)
        {
            try
            {
                await firebase
                .Child("User")
                .PostAsync(usr);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        //Update     
        public static async Task<bool> UpdateUser(User usr)
        {
            try
            {
                var toUpdateUser = (await firebase
                .Child("User")
                .OnceAsync<User>()).Where(a => a.Object.PhoneNumber == usr.PhoneNumber).FirstOrDefault();
                await firebase
                .Child("User")
                .Child(toUpdateUser.Key)
                .PutAsync(usr);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        //Delete User    
        public static async Task<bool> DeleteUser(User usr)
        {
            try
            {
                var toDeletePerson = (await firebase
                .Child("User")
                .OnceAsync<User>()).Where(a => a.Object.PhoneNumber == usr.PhoneNumber).FirstOrDefault();
                await firebase.Child("User").Child(toDeletePerson.Key).DeleteAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

    }
}
