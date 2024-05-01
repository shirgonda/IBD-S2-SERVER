using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using finalproj.BL;
using Microsoft.Extensions.Configuration;

public class DBservicesUser
{
    public DBservicesUser()
    {

    }
    public SqlConnection connect(String conString)
    {
        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }
    public int Insert(User user)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateUserInsertCommandWithStoredProcedure("spInsertUser", con, user);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    private SqlCommand CreateUserInsertCommandWithStoredProcedure(string spName, SqlConnection con, User user)
    {
        SqlCommand cmd = new SqlCommand(spName, con); // Create the command object and assign the stored procedure name

        cmd.CommandType = CommandType.StoredProcedure; // Set the command type to StoredProcedure
        cmd.CommandTimeout = 10; // Time to wait for the execution. The default is 30 seconds

        // Add parameters with values to the command object
        cmd.Parameters.AddWithValue("@Username", user.Username);
        cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
        cmd.Parameters.AddWithValue("@LastName", user.LastName);
        cmd.Parameters.AddWithValue("@Email", user.Email);
        cmd.Parameters.AddWithValue("@Password", user.Password);
        cmd.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
        cmd.Parameters.AddWithValue("@Gender", user.Gender);
        cmd.Parameters.AddWithValue("@TypeOfIBD", user.TypeOfIBD);
        cmd.Parameters.AddWithValue("@ProfilePicture", user.ProfilePicture ?? (object)DBNull.Value);  // Handle possible null value

        return cmd;
    }
    public int Update(User user)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateUpdateCommandWithStoredProcedure("spUpdateUser", con, user);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    private SqlCommand CreateUpdateCommandWithStoredProcedure(string spName, SqlConnection con, User user)
    {
        SqlCommand cmd = new SqlCommand(spName, con); // Create the command object and assign the stored procedure name

        cmd.CommandType = CommandType.StoredProcedure; // Set the command type to StoredProcedure
        cmd.CommandTimeout = 10; // Time to wait for the execution, the default is 30 seconds

        // Add parameters with values to the command object, matching the database schema
        cmd.Parameters.AddWithValue("@Username", user.Username);
        cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
        cmd.Parameters.AddWithValue("@LastName", user.LastName);
        cmd.Parameters.AddWithValue("@Email", user.Email);
        cmd.Parameters.AddWithValue("@Password", user.Password);
        cmd.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
        cmd.Parameters.AddWithValue("@Gender", user.Gender);
        cmd.Parameters.AddWithValue("@TypeOfIBD", user.TypeOfIBD);
        cmd.Parameters.AddWithValue("@ProfilePicture", user.ProfilePicture ?? (object)DBNull.Value); // Handle possible null value for optional fields
        return cmd;
    }

    private void AddUserParameters(SqlCommand cmd, User user)
    {
        cmd.Parameters.AddWithValue("@Username", user.Username);
        cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
        cmd.Parameters.AddWithValue("@LastName", user.LastName);
        cmd.Parameters.AddWithValue("@Email", user.Email);
        cmd.Parameters.AddWithValue("@Password", user.Password);
        cmd.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
        cmd.Parameters.AddWithValue("@Gender", user.Gender);
        cmd.Parameters.AddWithValue("@TypeOfIBD", user.TypeOfIBD);
        cmd.Parameters.AddWithValue("@ProfilePicture", user.ProfilePicture ?? (object)DBNull.Value);
    }

    public List<User> Read()
    {
        List<User> users = new List<User>();
        string connectionString = GetConnectionString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("spReadUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.Open();
                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        User user = new User
                        {
                            Id = dataReader.GetInt32(0), // Get the Id from the first column
                            Username = dataReader["Username"].ToString(),
                            FirstName = dataReader["FirstName"].ToString(),
                            LastName = dataReader["LastName"].ToString(),
                            Email = dataReader["Email"].ToString(),
                            Password = dataReader["Password"].ToString(),
                            DateOfBirth = Convert.ToDateTime(dataReader["DateOfBirth"]),
                            Gender = dataReader["Gender"].ToString(),
                            TypeOfIBD = dataReader["TypeOfIBD"].ToString(),
                            ProfilePicture = dataReader["ProfilePicture"] != DBNull.Value ? dataReader["ProfilePicture"].ToString() : null
                        };
                        users.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error in Read: {ex.Message}", ex);
            }
        }

        return users;
    }

    private string GetConnectionString()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
        return configuration.GetConnectionString("myProjDB");
    }

    public User LogIn(User user)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateLogInCommandWithStoredProcedureWithoutParameters("spLogInUser", con, user);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            if (dataReader.Read())
            {
                User loggedInUser = new User
                {
                    Id = dataReader.GetInt32(0),
                    Username = dataReader["Username"].ToString(),
                    FirstName = dataReader["FirstName"].ToString(),
                    LastName = dataReader["LastName"].ToString(),
                    Email = dataReader["Email"].ToString(),
                    Password = dataReader["Password"].ToString(),
                    DateOfBirth = Convert.ToDateTime(dataReader["DateOfBirth"]),
                    Gender = dataReader["Gender"].ToString(),
                    TypeOfIBD = dataReader["TypeOfIBD"].ToString(),
                    ProfilePicture = dataReader["ProfilePicture"].ToString()
                };
                con.Close();
                return loggedInUser;
            }
            return null;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }



    private SqlCommand CreateLogInCommandWithStoredProcedureWithoutParameters(String spName, SqlConnection con, User user)
    {

        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con;

        cmd.CommandText = spName;

        cmd.CommandTimeout = 10;

        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@email", user.Email);

        cmd.Parameters.AddWithValue("@password", user.Password);


        return cmd;
    }

    internal bool Delete(User user)
    {
        SqlConnection con = null;
        try
        {
            con = connect("myProjDB");
            SqlCommand cmd = new SqlCommand("spDeleteUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", user.Email);

            var result = cmd.ExecuteNonQuery();
            return result > 0;
        }
        catch (Exception ex)
        {
            throw new Exception("Error deleting user: " + ex.Message);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }


    public User ReadOne(string email)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateReadOneCommandWithStoredProcedureWithoutParameters("spReadOneUser", con, email);

        try
        {
            using (SqlDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    User user = new User
                    {
                        Id = dataReader.GetInt32(0),
                        FirstName = dataReader["FirstName"].ToString(),
                        LastName = dataReader["LastName"].ToString(),
                        Email = dataReader["Email"].ToString(),
                        Password = dataReader["Password"].ToString(),
                        DateOfBirth = Convert.ToDateTime(dataReader["DateOfBirth"]),
                        Gender = dataReader["Gender"].ToString(),
                        TypeOfIBD = dataReader["TypeOfIBD"].ToString(),
                        ProfilePicture = dataReader["ProfilePicture"] != DBNull.Value ? dataReader["ProfilePicture"].ToString() : null
                    };
                    return user;
                }
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error in Read: {ex.Message}", ex);
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

        return null;
    }



    private SqlCommand CreateReadOneCommandWithStoredProcedureWithoutParameters(String spName, SqlConnection con, string email)
    {

        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con;

        cmd.CommandText = spName;

        cmd.CommandTimeout = 10;

        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@email", email);


        return cmd;
    }


}
