using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Threading;

/*
 *  Programmer  : Stephen Glasspell J381116@tafe.wa.edu.au
 *  Date        : Sept/ Oct 2018
 *  Purpose     : Car Rental Database Application.
 *                This is an application for use in the offices of a Vehicle Hire company
 *                to be used to handle the administration of the business,
 *                including bookings, customer data, vehicle data and employee data.
 *                Secondary tools such as financial reporting and calendar views
 *                can be added to make all business tasks easier for the business.
 *                
 *  Version     : Draft.              
 */

namespace StephenGlasspell_CarRental
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        private int failedAttempts = 0;

        public LoginPage()
        {
            InitializeComponent();
           
        }

       void playLoginSound()
        {
            String path = System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\Assets\audio\StartApplication.wav");
            Audio.play(path);
        }

      

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void handleLogin()
        {
            string[] results = Database.getInstance().datasetToStringArray(Database.getInstance().select("", new string[] { "Employee as T1, EmployeeLogin as T2 " }, new string[] { "T1.EmployeeID", "T1.FirstName" }, "WHERE T1.EmployeeID = T2.EmployeeID and T2.Username", "=", txtUsername.Text + "' and T2.Password = '" + pwbPassword.Password + "", new String[] { "" }));


            if (results.Length != 0)
            {
                DataDelegate.EmployeeID = int.Parse(results[0]);
                DataDelegate.currentUserName = results[1];
               
                DataDelegate.loginStart = DateTime.Now;
                

                MainWindow.getInstance().Show();
                MainWindow.getInstance().enableWindow();
                CommonTasks.getInstance().Show();
                this.Close();
                             
            }
            else
            {
                failedAttempts++;
                if (failedAttempts >= 3)
                {
                    

                    MessageBox.Show("Too many login attempts. This application will now close. Goodbye.", "Login Failed");
                    Environment.Exit(0);
                }
                else
                {
                    MessageBox.Show("Incorrect Credentials", "Access Denied");
                }
            }
            
            
        }

        private void btnLoginSubmit_Click(object sender, RoutedEventArgs e)
        {
            handleLogin();
        }

     

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Thread thread = new Thread(playLoginSound);
                ;
            thread.IsBackground = true;
            thread.Start();
            thread.Name = "thdLoginAudio";

            
        }

     
    }
}
