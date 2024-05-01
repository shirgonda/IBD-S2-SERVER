using System;
using System.Collections.Generic;
using System.Data;
using finalproj.BL;

namespace finalproj.BL
{
    public class Rights
    {
        private User user;

        private string prompt;
        
        private string answer;

        // public static List<User> Users = new List<User>();

        public Rights(User user, string prompt, string answer)
        {
            Username = user;
            Prompt = prompt;
            Answer = answer;
        }

        public Rights()
        {
        }

/*        public List<Rights> Read()
        {
            DBservicesRights dbs = new DBservicesRights();
            return dbs.Read();
        }*/
        public User Username { get => user; set => user = value; }
        public string Prompt { get => prompt; set => prompt = value; }
        public string Answer { get => answer; set => answer = value; }

        //public string QueryChatGPT()
        //{

        //}

    }
}
