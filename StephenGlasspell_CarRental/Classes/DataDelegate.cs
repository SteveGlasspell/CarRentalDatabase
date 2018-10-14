using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Media.Imaging;
using System.Windows.Input;

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
    class DataDelegate
    {
        private static DataDelegate instance = null;
        public static bool debugMode = false;

        public static DataSet currentDataSet = null;

        public static String currentUserName = "";
        public static int EmployeeID;
        public static DateTime loginStart;

        public static int CustomerID;
        public static BitmapImage customerImage;
        public static DateTime timeCustomerImageTaken;

        public static Cursor previousCursor;

        public static List<String> errorMessages = new List<string>();
   
        
        // 
        public static int minimumDriverAgePermitted = 25;

        // Emojis  (Segoe UI Emoji)
        public const string emoji_Customer = "🕴";
        public const string emoji_Booking = "📝";
        public const string emoji_Vehicles = "🚘";
        public const string emoji_Employees = "😍";

        public const string symbol_ArrowLeft = "";
        public const string symbol_ArrowRight = "";


        public const string SUCCESS = "✅";
        public const string FAIL = "❎";


        DataDelegate()
        {
            
        }

        public static DataDelegate getInstance()
        {
            if (instance == null)
            {
                instance = new DataDelegate();
            }
            
            return instance;
        }

        public static void loadConfig()
        {
            String currentDirectory = Directory.GetCurrentDirectory();
            String filename = "config.txt";

            String path = currentDirectory + "\\" + filename;

            try
            {
                StreamReader streamreader = new StreamReader(path);
                String line;
                while ((line = streamreader.ReadLine()) != null)
                {
                    parseConfigLine(line);
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message.ToString(), "Could not read from Config File");
            }
        }

        public static void writeConfig(String property, String value)
        {
            String currentDirectory = Directory.GetCurrentDirectory();
            String filename = "config.txt";

            String path = currentDirectory + "\\" + filename;

            try
            {
                StreamReader streamreader = new StreamReader(path);
                String line;
                int lineCount = 0;
                while ((line = streamreader.ReadLine()) != null)
                {
                    String[]  propertyValue = line.Split(',');
                    String prop = propertyValue[0];
                    String val = propertyValue[1];

                    if(prop == property)
                    {

                    }

                    lineCount++;
                }

            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show(e.Message.ToString(), "Could not write to Config File");
            }
        }

         static void parseConfigLine(String line)
        {

        }

    }
}
