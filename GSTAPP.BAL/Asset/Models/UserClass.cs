using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSTAPP.BAL
{
    public class UserClass
    {
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string OwnerName { get; set; }
        public int UserId { get; set; }
        public int OwnerId { get; set; }
        public static string RandomString(int length)
        {
            return RandomValues.RandomString(length);
        }
    }

    public static class RandomValues
    {
        public static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}