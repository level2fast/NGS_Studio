using System.Collections.Generic;
using NGS_Studio.Models;

namespace NGS_Studio.Data
{
    public static class UserData
    {
        public static IList<User> Barbers { get; private set; }

        static UserData()
        {
            Barbers = new List<User>();

            Barbers.Add(new User
            {
                Name = "Baboon",
                Location = "Africa & Asia",
                Details = "Baboons are African and Arabian Old World Barbers belonging to the genus Papio, part of the subfamily Cercopithecinae.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
            });
        }
    }
}
