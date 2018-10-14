using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
    static class DataValidator
    {
        public enum dataFieldType
        {
            // default
            NOTSET,
            // login details
            username, password,

            // Personal details
            title, firstname, middlename, lastname, dateOfBirth,

            // Contact details
            houseNo, streetname, suburb, city, state, postcode, phone, email,

            // Driving License details
            drivingLicenseIssuingCountry,
            drivingLicenseNumber,
            drivingLicenseIssueDate,
            drivingLicenseExpiryDate,
            drivingLicenseConvictions,

            // Credit Card details
            creditCardType,
            creditCardNumber,
            creditCardNameOnCard,
            creditCardExpiryDate,
            creditCardSecurityCode,


           // VEHICLE DETAILS
           vehicleVIN,
           manufacturer,
           model,
           vehicleDetails,
           dailyRate,
           chassisNumber,
           dateOfFirstRegistration,
           suitableForHire,

           // BOOKING DETAILS
           scheduledHireBeginDateTime,
           scheduledHireReturnDateTime,
           actualHireBeginDateTime,
           actualHireResturnDateTime,
           odometerReadingPreHire,
           odometerReadingPostHire,
           totalCharge



        }

        public static Dictionary<dataFieldType, string> regexLookup = new Dictionary<dataFieldType, string>()
        {
            { dataFieldType.username,           "" },
            {dataFieldType.password,            "" },

            {dataFieldType.firstname,           @"^[a-zA-Z]{1}[a-zA-Z\-[:punct:][:space:]]{1,}[a-zA-Z]{1}$" },
            {dataFieldType.middlename,          @"^[a-zA-Z\s]*$"},
    
            { dataFieldType.lastname,            @"^[a-zA-Z]{1}[a-zA-Z\-[:punct:][:space:]]{1,}[a-zA-Z]{1}$"  },

            {dataFieldType.houseNo,             @"[a-zA-Z\d-\/]{1,}" },   // Allows numbers, letters, "-" and "/"
            {dataFieldType.streetname,          @"[a-zA-Z[:space:]'-/]{3,}" },
            {dataFieldType.suburb,              @"[a-zA-Z[:space:]'-/]{3,}" },
            {dataFieldType.city,                @"[a-zA-Z[:space:]'-/]{3,}" },
            {dataFieldType.state,               @"^[a-zA-Z]{2,}$" },
            {dataFieldType.postcode,            @"^[\d]{4}$" },              // Allows exactly four digits.  
            {dataFieldType.phone,               @"^[\d]{10}$" },
            {dataFieldType.email,               @"^(?:(?![.]{2,})[A-Za-z0-9!#$%&'*+-=?.^_`{|}~/]){0,64}[^.]@[^.](?:[a-zA-Z0-9]{0,253}\.)[a-zA-Z]{2,4}$"},
            {dataFieldType.creditCardNumber,    @"^[\d]{16}$" },
            {dataFieldType.creditCardNameOnCard, @"^[a-zA-Z]{1}[a-zA-Z\s]{1,}[a-zA-Z]{1}$" },
            {dataFieldType.creditCardSecurityCode, @"^[\d]{3}$" },

            {dataFieldType.drivingLicenseNumber,  @"\d{7}" },
        };

        // Method to check if all data entries in an array are valid.
        public static bool validateField(dataFieldType[] types, string[] inputs)
        {
            for (int i = 0; i < types.Length; ++i)
            {
                if(!validateField(types[i], inputs[i]))
                {
                    return false;
                }
            }
            return true;
        }

        // Method to check if a single data entry in a form is valid.
        public static bool validateField(dataFieldType type, string input)
        {
            

            // Test the input passes the Regex.
            string pattern = "";
            regexLookup.TryGetValue(type, out pattern);
            if (!String.IsNullOrEmpty(pattern))
            {
                try
                {
                    Regex regex = new Regex(pattern);
                    Match match = Regex.Match(input, pattern);
                    if (!match.Success)
                    {
                     
                        return false;
                    }
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show(e.Message.ToString(), "Error");
                }
            }
           
            

            // Further validation passed Regex...

            switch (type)
            {
                case dataFieldType.username:
                    break;

                case dataFieldType.password:
                    break;

                case dataFieldType.title:
                    break;

                case dataFieldType.firstname:
                    break;

                case dataFieldType.middlename:
                    break;

                case dataFieldType.lastname:
                    break;

                case dataFieldType.dateOfBirth:
                    try
                    {
                        DateTime dob = DateTime.Parse(input);
                        if (dob.CompareTo(DateTime.Now.AddYears(-DataDelegate.minimumDriverAgePermitted)) > 0)
                        {
                            return false;
                        }
                    }catch(Exception e)
                    {
                        // Not a date. :-(
                    }
                   
                    break;

                case dataFieldType.houseNo:
                    break;

                case dataFieldType.streetname:
                    break;

                case dataFieldType.suburb:
                    break;

                case dataFieldType.city:
                    break;

                case dataFieldType.state:          
                    break;

                case dataFieldType.postcode:
                    // Check the input is a real Australian poscode. 
                    int pcode;
                    int.TryParse(input, out pcode);
                    if(stateForPostcode(pcode) == "")
                    {
                        return false;
                    }
                    break;

                case dataFieldType.phone:
                    break;

                case dataFieldType.email:
                    break;

                case dataFieldType.drivingLicenseNumber:
                    
                    break;

                case dataFieldType.drivingLicenseIssuingCountry:
                    break;

                case dataFieldType.drivingLicenseIssueDate:
                    try
                    {
                        DateTime issueDate = DateTime.Parse(input);
                        if (issueDate.CompareTo(DateTime.Now) > 0)
                        {
                            return false;
                        }
                    }catch(Exception e)
                    {
                        // Not a date :-(
                    }
                    
                    break;

                case dataFieldType.drivingLicenseExpiryDate:
                    try
                    {
                        DateTime dlExpiryDate = DateTime.Parse(input);
                        if (dlExpiryDate.CompareTo(DateTime.Now) < 0)
                        {
                            return false;
                        }
                    }
                    catch(Exception e)
                    {
                        // Not a date :-(
                    }
                  
                    break;

                case dataFieldType.drivingLicenseConvictions:
                    if(input != "0")
                    {
                        return false;
                    }
                    break;

                case dataFieldType.creditCardType:
                    break;

                case dataFieldType.creditCardNumber:
                    break;
                case dataFieldType.creditCardNameOnCard:
                    break;

                case dataFieldType.creditCardExpiryDate:
                  
                    try
                    {
                        string[] date = input.Split('/'); 
                        string monthString = date[0];
                        string yearString = date[1];

                        if((monthString.Length > 2) || (yearString.Length > 2)){
                            return false;
                        }
                        int month = int.Parse(monthString);
                        int year = 2000 + int.Parse(date[1]);

                        DateTime expiryDate = new DateTime(year, month, 1);
                        if(expiryDate.CompareTo(DateTime.Now) < 0)
                        {
                            return false;
                        }

                    }catch(Exception e)
                    {
                        return false;
                    }

                   
                   
                    

                    
                    
                    break;

                case dataFieldType.creditCardSecurityCode:
                    break;

                case dataFieldType.vehicleVIN:
                    break;

                case dataFieldType.manufacturer:
                    break;

                case dataFieldType.model:
                    break;

                case dataFieldType.dailyRate:

                    Double test = 0;
                    input = input.Trim('$');
                    if(! double.TryParse(input, out test))
                    {
                        return false;
                    }
                    break;

                case dataFieldType.vehicleDetails:
                    break;

                case dataFieldType.chassisNumber:
                    break;
                    

                case dataFieldType.dateOfFirstRegistration:
                    break;


                case dataFieldType.suitableForHire:
                    break;

                case dataFieldType.scheduledHireBeginDateTime:
                    break;

                case dataFieldType.scheduledHireReturnDateTime:
                    break;
                case dataFieldType.actualHireBeginDateTime:
                    break;
           case dataFieldType.actualHireResturnDateTime:
                    break;
           case dataFieldType.odometerReadingPreHire:
                    break;
                case dataFieldType.odometerReadingPostHire:
                    break;
                case dataFieldType.totalCharge:
                    break;



                default:                 
                    return false;
            }
            return true;
        }

        // Returns a short State Code for the postcode entered.   (ie 6000 will return WA)
        public static string stateForPostcode(int p)
        {
            int postcode = p;
            string result = "";

            if (    isBetween(postcode, 200, 299)
                ||  isBetween(postcode, 2600, 2618)
                ||  isBetween(postcode, 2900, 2920))
            {
                result = "ACT";
                return result;
            }

            if (    isBetween(postcode, 7000, 7999))
            {
                result = "TAS";
                return result;
            }

            if (    isBetween(postcode, 800, 999))
            {
                result = "NT";
                return result;
            }

            if (    isBetween(postcode, 3000, 3999)
                ||  isBetween(postcode, 8000, 8999))
            {
                result = "VIC";
                return result;
            }

            if (    isBetween(postcode, 6000, 6797)
                ||  isBetween(postcode, 6800, 6999))
            {
                result = "WA";
                return result;
            }

            if(     isBetween(postcode, 5000, 5999))       
            {
                result = "SA";
                return result;
            }

            if ((   isBetween(postcode, 4000, 4999))
                ||  isBetween(postcode, 9000, 9999))
            {
                result = "QLD";
                return result;
            }

            if (    isBetween(postcode, 1000, 2599)
                ||  isBetween(postcode, 2619, 2899)
                ||  isBetween(postcode, 2921, 2999))
            {
                result = "NSW";
                return result;
            }
            
            return result;
        }


        static bool isBetween(double x, double i, double j)
        {
            return ((x >= i) && (x <= j)) ? true : false;
        }

        public static List<dataFieldType> guessTheDataType(String s)
        {
            List<dataFieldType> types = new List<dataFieldType>();

            foreach (dataFieldType data in (dataFieldType[])Enum.GetValues(typeof(dataFieldType)))
                {
                if(validateField(data, s))
                {
                    types.Add(data);
                }
            }
            return types;
        }

        public static DateTime stringToDate(String s)
        {
            DateTime date = new DateTime();
            if (String.IsNullOrEmpty(s))
            {
                return date;
            }

            int spaceIndex = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsWhiteSpace(s[i]))
                {
                    spaceIndex = i;
                    break;
                }
            }

            String shortDate = s.Substring(0, spaceIndex);
            String[] dateElements = shortDate.Split('/');
            String month;
            String day;
            String year;
            int m = 0;
            int d = 0;
            int y = 0;
          
            if(dateElements.Length == 3)
            {
                month = dateElements[0];
                day = dateElements[1];
                year = dateElements[2];
                try
                {
                    m = int.Parse(month);
                    d = int.Parse(day);
                    y = int.Parse(year);

                }catch(Exception e)
                {

                }
            }
            else
            {
                return date;
            }


            date = new DateTime(y, m, d);

            return date;
        }

        public static int getComboBoxIndexOf( ComboBox combo, String s)
        {
            // I'm not proud of this code, but it works. Don't judge me! :-)
            List<String> items = new List<String>();
            for (int i = 0; i < combo.Items.Count; i++)
            {
                items.Add(combo.Items[i].ToString());
            }
            int index = items.IndexOf("System.Windows.Controls.ComboBoxItem: " + s);
           

            return index;
        }
    }

    
}
