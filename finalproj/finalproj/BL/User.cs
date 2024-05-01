using System;
using System.Collections.Generic;
using System.Data;
using finalproj.BL;

namespace finalproj.BL
{
    public class User
    {
        private int id;
        private string username;
        private string firstName;
        private string lastName;
        private string email;
        private string password;
        private DateTime dateOfBirth;
        private string gender;
        private string typeOfIBD;
        private string profilePicture;

        public static List<User> Users = new List<User>();

        public User(string username, string firstName, string lastName, string email, string password, DateTime dateOfBirth, string gender, string typeOfIBD, string profilePicture)
        {
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            TypeOfIBD = typeOfIBD;
            ProfilePicture = profilePicture;
        }

        public User()
        {
        }

        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Username { get => username; set => username = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public string Gender { get => gender; set => gender = value; }
        public string TypeOfIBD { get => typeOfIBD; set => typeOfIBD = value; }
        public string ProfilePicture { get => profilePicture; set => profilePicture = value; }
        public int Id { get => id; set => id = value; }

        public bool Insert()
        {
            if (!Users.Exists(user => user.Email == this.Email || user.Username == this.Username))
            {
                DBservicesUser dbs = new DBservicesUser();
                dbs.Insert(this);
                return true;
            }
            return false;
        }

        public List<User> Read()
        {
            DBservicesUser dbs = new DBservicesUser();
            return dbs.Read();
        }

        //      public User ReadOne(string email)
        //{
        //    DBservicesUser dbs = new DBservicesUser();
        //    return dbs.ReadOne(email);
        //}

        public int Update()
        {
            DBservicesUser dbs = new DBservicesUser();
            return dbs.Update(this);
        }

        public User LogIn()
        {
            DBservicesUser dbs = new DBservicesUser();
            return dbs.LogIn(this);
        }


        public bool Delete()
        {
            DBservicesUser dbs = new DBservicesUser();
            return dbs.Delete(this);
        }
    }
}
