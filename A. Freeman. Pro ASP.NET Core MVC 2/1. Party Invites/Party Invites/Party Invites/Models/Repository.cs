namespace Party_Invites.Models
{
    using System;
    using System.Collections.Generic;



    public static class Repository
    {
        public static List<GuestResponce> Responses { get; } = new List<GuestResponce>();

        public static void Add(GuestResponce responce) => Responses.Add(responce ?? throw new ArgumentNullException(nameof(responce)));
    }
}