using System;
using System.Collections.Generic;
using Firebase.Database.Query;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NGS_Studio.Models;
using NGS_Studio.Data;
namespace NGS_Studio.Services
{
    class UserTableService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>        
        public static async Task<List<User>> GetAllUser()
        {
            try
            {
                var userlist = (await Globals.Instance.Firebase
                .Child("User")
                .OnceAsync<User>()).Select(item =>
                new User
                {
                    Name = item.Object.Name,
                    PhoneNumber = item.Object.PhoneNumber,
                    Email = item.Object.Email,
                    IsBarber = item.Object.IsBarber,
                    IsClient = item.Object.IsClient,
                    IsOwner = item.Object.IsOwner
                }).ToList();
                return userlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>        
        public static async Task<User> GetUser(string phoneNumber)
        {
            try
            {
                var allUsers = await GetAllUser();
                await Globals.Instance.Firebase
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>        
        public static async Task<User> GetUserByEmail(string email)
        {
            try
            {
                var allUsers = await GetAllUser();
                await Globals.Instance.Firebase
                .Child("User")
                .OnceAsync<User>();
                return allUsers.Where(a => a.Email == email).FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<User> GetOwner()        {
            try
            {
                var allUsers = await GetAllUser();
                return allUsers.Where(a => a.IsOwner == true).FirstOrDefault();
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
        /// <param name="usr"></param>
        /// <returns></returns>
        public static async Task<bool> AddUser(User usr)
        {
            try
            {
                await Globals.Instance.Firebase
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usr"></param>
        /// <returns></returns>        
        public static async Task<bool> UpdateUser(User usr)
        {
            try
            {
                var toUpdateUser = (await Globals.Instance.Firebase
                .Child("User")
                .OnceAsync<User>()).Where(a => a.Object.PhoneNumber == usr.PhoneNumber).FirstOrDefault();
                await Globals.Instance.Firebase
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usr"></param>
        /// <returns></returns>        
        public static async Task<bool> DeleteUser(User usr)
        {
            try
            {
                var toDeletePerson = (await Globals.Instance.Firebase
                .Child("User")
                .OnceAsync<User>()).Where(a => a.Object.PhoneNumber == usr.PhoneNumber).FirstOrDefault();
                await Globals.Instance.Firebase.Child("User").Child(toDeletePerson.Key).DeleteAsync();
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
