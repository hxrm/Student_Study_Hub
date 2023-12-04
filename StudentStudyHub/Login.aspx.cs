using StudentStudyHub.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace StudentStudyHub
{

    public partial class Login : System.Web.UI.Page
    {
        // Variables for password and dehelper 
        string hpWord;
        DBHelper dbHelper;

        // Database path and connection string
        static string relativePath = "|DataDirectory|\\TimeAppDB.mdf";
       // string connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={relativePath};Integrated Security=True"; 
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\StudentStudyHub\\StudentStudyHub\\App_Data\\StudyDB.mdf;Integrated Security=True";


        // Default constructor
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Submit button click 
        protected void submitButton_Click(object sender, EventArgs e)
        {
            saveMsg.Visible = false;
            string pW = passWord.Text;
            string eM = email.Text;
            // Checking if the email field is empty
            if (string.IsNullOrWhiteSpace(eM))
            {
                // Show the error message
                emailError.Visible = true;
                // Add the is-invalid class to the TextBox
                email.CssClass += " is-invalid";         
            }
            if (string.IsNullOrWhiteSpace(pW))
            {
                // Show the error message
                passWordError.Visible = true;
                passWord.CssClass += " is-invalid";
                return;
            }
            if(!string.IsNullOrWhiteSpace(pW)&& !string.IsNullOrWhiteSpace(eM))
            {
                passWord.CssClass = passWord.CssClass.Replace("is-invalid", "").Trim();
                email.CssClass = email.CssClass.Replace("is-invalid", "").Trim();
            }

            // Hash the password for comparison
            hpWord = HashPassword(ReturnBytes(pW));

            // Check if the user exists in the database
            bool user = PullData();
            if (user)
            {
                // If the user exists, display the Menu window
                Response.Redirect("Home.aspx");
            }
            else if(!user)
            {
                // Show an error message if user login fails
                 saveMsg.Visible = true;
                
            }
        }
        // method to hash password 
        public static string HashPassword(byte[] passwordArray)
        {
            string password;
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in passwordArray)
            {

                stringBuilder.Append(b.ToString("X2"));
            }
            password = stringBuilder.ToString();
            return password;
        }
        // Method to convert a password into a byte array and hash it
        public static byte[] ReturnBytes(String password)
        {
            // Create an instance of the SHA-256 hashing algorithm.
            using (HashAlgorithm algorithm = SHA256.Create())
            {
                // Convert the plaintext password into a byte array using UTF-8 encoding.
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash of the password bytes.
                byte[] hashBytes = algorithm.ComputeHash(passwordBytes);

                // Return the resulting hash as a byte array.
                return hashBytes;
            }
        }
        // Method to retrieve user data from the database
        public bool PullData()
        {
            int user = -1;
            string eMail = email.Text;
            string pWord = hpWord;
            bool loggedIn = false;

            //    string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\TimeApplication\\TimeApplication\\TimeAppDB.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM [User] WHERE EMAIL = @EMAIL";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {

                    command.Parameters.AddWithValue("@EMAIL", eMail);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Check if the email exists in the database.
                        {
                            string dbPassword = reader["P_WORD"].ToString();

                            if (pWord == dbPassword) // Check if the entered password matches the stored hashed password.
                            {
                                loggedIn = true;
                                user = Convert.ToInt32(reader["Id"]); // Retrieve the user ID corresponding to the email
                                dbHelper = new DBHelper(user);
                            }
                        }
                        connection.Close();
                    }
                }
            }
            return loggedIn;
        }
        // Button click to navigate back to the registration page
        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            Response.Redirect("Register.aspx");
        }

       
    }//end class
}