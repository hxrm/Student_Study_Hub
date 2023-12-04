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

    public partial class Register : System.Web.UI.Page
    {
        // Database path and connection string
        static string relativePath = "|DataDirectory|\\TimeAppDB.mdf";
        // string connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={relativePath};Integrated Security=True";
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\StudentStudyHub\\StudentStudyHub\\App_Data\\StudyDB.mdf;Integrated Security=True";
        // Variables for password, email validation, and threads
        string hpWord;
        bool validPWord, validEMail, validFName, validSName;
        // Error messages for various fields and Validation instance
        string fNameErrorMessage, sNameErrorMessage, emailErrorMessage, passwordErrorMessage;
        
        public Validation v = new Validation();

        // Default constructor
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void createUser_Click(object sender, EventArgs e)
        {
           // Obtain email, password, and confirmation from the UI fields
            string inputEmail = email.Text;
            string p = passWord.Text;
            string c = conP.Text;

            // Validate email 
            validEMail = v.TryReceiveString(inputEmail, out emailErrorMessage);
            emailError.Text = emailErrorMessage;
            emailError.Visible = true;
            // Validate first name
            validFName = v.TryReceiveString(fName.Text, out fNameErrorMessage);
            fNameError.Text = fNameErrorMessage;
            fNameError.Visible = true;

            // Validate surname
            validSName = v.TryReceiveString(sName.Text, out sNameErrorMessage);
            sNameError.Text = sNameErrorMessage;
            sNameError.Visible = true;

            // Validate Password Match 
            ProcessPassword(p, c);
            
            // Validation: Check if the required fields are filled
            if (validFName && validSName && validEMail && validPWord)
            { 
                // Process email validation
                ProcessEmail(inputEmail);
                
                // Hash the password and save user data if the email is valid
                hpWord = HashPassword(returnBytes(p));
                
                //if email is not registered 
                if (validEMail)
                {// Save new user data to the database
                    SaveData();
                }
            }
        }
        public void ProcessEmail(string email)
        {
            //reset error 
            emailError.Visible = false;

            bool validEmail = v.TryReceiveEmail(email, out emailErrorMessage);
            
            //if email returns invalid then error is visible
            emailError.Text = emailErrorMessage;
            if (!validEmail)
            {
                emailError.Visible = true;
                
            }
            // if email returns valid 
            if (validEmail)
            {
                //pass email to bool return method to ensure is a new user email
                bool newEmail = CheckData(email);

                //if email already registered 
                if (newEmail)
                {// display error 
                    emailError.Text = "Email has already been registered";
                    emailError.Visible = true;
                }
                else
                {//valid new email
                    validEMail = true;
                }
            }
        }
        // Method to process password validation
        public void ProcessPassword(string password, string confirm)
        {  //Declare password variables
            string pWord = password;
            string confP = confirm;

            //reset error message 
            conPError.Visible = false;

            // if password does not match confirmation password 
            if (pWord != confP)
            {  //display error 
                conPError.Text = "Passwords do not match.";
                conPError.Visible = true;
            }
            else
            {  //if match, validate password format 
                validPWord = v.TryReceivePassword(pWord, out passwordErrorMessage);
                //conPError.Visible = false;
                passWordError.Text = passwordErrorMessage;
                passWordError.Visible = true;

            }
        }
        // Submit registration button click event

        // Method to hash the password
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

       

        // Method to convert password to bytes and hash
        public static byte[] returnBytes(string password)
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
        // Method to check if email is already registered in the database
        public bool CheckData(string email)
        {
            //int user = -1;
            string eMail = email;
            bool registered = false;

            //  string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\TimeApplication\\TimeApplication\\TimeAppDB.mdf;Integrated Security=True";

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
                            string dbEmail = reader["EMAIL"].ToString();
                            // if email matches email in database
                            if (eMail == dbEmail) 
                            {
                                registered = true;

                            }
                            else
                            {
                                registered = false;
                            }
                        }
                        connection.Close();
                    }
                }
            }
            return registered;
        }
        // Method to save user data into the database
        public void SaveData()
        {
            string f_Name = fName.Text;
            string s_Name = sName.Text;
            string e_Mail = email.Text;
            string p_word = hpWord;

            // string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\TimeApplication\\TimeApplication\\TimeAppDB.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO [User] (F_NAME, S_NAME, EMAIL, P_WORD) " +
                  "VALUES (@F_NAME, @S_NAME, @EMAIL, @P_WORD)";


                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@F_NAME", f_Name);
                    command.Parameters.AddWithValue("@S_NAME", s_Name);
                    command.Parameters.AddWithValue("@EMAIL", e_Mail);
                    command.Parameters.AddWithValue("@P_WORD", p_word);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {

                        saveMsg.CssClass = "text-success";
                        saveMsg.Text = "Registration successful! You can now log in.";
                        saveMsg.Visible = true;
                        
                        Login();
                    }
                    else
                    {
                        saveMsg.CssClass = "text-danger";
                        saveMsg.Text = "Failed to insert student data.";
                        saveMsg.Visible = true;
                    }
                }
                connection.Close();
            }

        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
        private void Login()
        {
            Response.Redirect("Login.aspx");

        }
        // Method to reset the UI elements and error messages
        public void Reset1()
        {
            emailError.Visible = false;
            saveMsg.Visible = false;
            passWordError.Visible = false;
            fNameError.Visible = false;
            sNameError.Visible = false;
            conPError.Visible = false;

        }
    }
}// end class 