using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

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
    public class Database
    {

        private static Database instance = null;

        private SqlConnection conn = new SqlConnection();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        private SqlCommand command = new SqlCommand();

        public static string dbSource = "MSSQLSERVER021";  // OLYMPUS-MONS-FI, MSSQLSERVER021
        private static string dbName = "CarRental";

        private string dbUser;
        private string dbPasswd;

        // Database Structure Reference
        public enum Booking
        {
            BookingID,
            CustomerID,
            EmployeeID,
            BookingCreationDate,
            ScheduledHireBeginDateTime,
            ScheduledHireReturnDateTime,
            ActualCarHireBeginDateTime,
            ActualCarHireReturnDateTime,
            OdometerReadingPreHire,
            OdometerReadingPostHire,
            TotalBill
  
        };
        public enum CreditCard
        {
            CustomerID,
            CredCardType,
            NameOnCredCard,
            CredCardNumber,
            CredCardExpiry,
            CredCardSecurityCode
        };
        public enum Customer
        {   Title,
            FirstName,
            Middlename,
            LastName,
            DateOfBirth
        };
        public enum CustomerContact
        {
            CustomerID,
            HouseNumber,
            StreetName,
            Suburb,
            City,
            State,
            Postcode,
            MobilePhone,
            Email
        };
        public enum CustomerLogin
        {
            CustomerID,
            Username,
            Password
        };
        public enum DrivingLicense
        {
            CustomerID,
            DrivingLicenseNumber,
            Country,
            IssueDate,
            ExpiryDate,
            DrivingLicenseType,
            Convictions
        };
        public enum Employee
        {
            EmployeeID,
            FirstName,
            LastName,
            JobTitle,
            DateOfBirth
        };
        public enum EmployeeContact
        {
            EmployeeID,
            HouseNumber,
            StreetName,
            Suburb,
            City,
            State,
            Postcode,
            MobilePhone,
            Email
        };
        public enum EmployeeLogin
        {
            EmployeeID,
            Username,
            Password
        };
        public enum Vehicle
        {
            VehicleVIN,
            Image,
            Manufacturer,
            Model,
            DateOfFirstRegistration,
            ChassisNumber,
            VehicleDetails,
            SuitableForHire
        }
        public enum VehicleService
        {
            ServiceID,
            EmployeeID,
            VehicleVIN,
            ServiceDate,
            ServiceDetails,
            OdometerReading
        };


        private Database()
        {

        }


        public static Database getInstance()
        {
            if(instance == null)
            {
                instance = new Database();
            }
            return instance;
        }

        public void setDBSource(String s)
        {
            dbSource = s;
        }
        

        public int getNextCustID()
        {
            string str_connection = "Data Source = " +  dbSource + "; Initial Catalog = " + dbName + "; Integrated Security = True";
            string mySelect =   "SELECT * " +
                                "FROM dbo.Customer;";
            SqlConnection con = new SqlConnection(str_connection);
            SqlDataAdapter ada = new SqlDataAdapter(mySelect, con);
            DataSet ds = new DataSet();
            ada.Fill(ds);

            return  ds.Tables[0].Rows.Count + 1;
        }

        public DataSet selectStarFrom(String tableName)
        {
            DataSet ds = new DataSet();
            String results = "";
            String sql = "SELECT * FROM " + tableName + ";";
            if (DataDelegate.debugMode)
            {
                System.Windows.MessageBox.Show("The following SQL statement is being processed :\n" + sql, "SQL Statement");
            }
            try
            {
                string str_connection = "Data Source = " + dbSource + "; Initial Catalog = " + dbName + "; Integrated Security = True";
                SqlConnection con = new SqlConnection(str_connection);
                SqlCommand command = new SqlCommand(sql, con);

                SqlDataAdapter ada = new SqlDataAdapter(sql, con);
                
                ada.Fill(ds);

                StringBuilder rb = new StringBuilder();

                foreach (DataTable table in ds.Tables)
                {
                   
                        foreach (DataRow row in table.Rows)
                        {
                            foreach (object item in row.ItemArray)
                            {
                                rb.Append(item);
                                rb.Append(" ");
                            }
                            rb.Append("\n");
                        }
                        rb.Append("\n\n");                    
                }
                results = rb.ToString();
            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show(e.Message.ToString() + sql, "SQL Error");              
            }
            // return results;
            return ds;
        }

        public DataSet select(String criteria, String[] tables, String[] fieldNames, String where, String operatorSymbol, String value, String[] orderBy)
        {
            
            String resultString = "";
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (criteria.Equals("*"))
            {
                sb.Append(criteria);
                sb.Append(" ");
            }
            else
            {       
                for(int index = 0; index < fieldNames.Length; index++)
                {               
                    sb.Append(fieldNames[index]);    
                    if(index != fieldNames.Length - 1)
                    {
                        sb.Append(", ");
                    }
                }
            }
            
            sb.Append(" FROM ");

            for(int index = 0; index < tables.Length; index++)
            {
                sb.Append(tables[index]);
                if(index != tables.Length - 1)
                {
                    sb.Append(", ");
                }
            }

            if(where.Length > 0)
            {
                sb.Append(" ");
                sb.Append(where);
                sb.Append(" ");
                sb.Append(operatorSymbol);

                if(value.Length > 0)
                {               
                    sb.Append(" '");
                    sb.Append(value);
                    sb.Append("'");
                }              
            }

            if(orderBy.Length > 0)
            {
                sb.Append(" ");
                foreach(string orderByItem in orderBy)
                {
                    sb.Append(orderByItem);
                    sb.Append(" ");
                }
            }

            sb.Append(";");
            string mySelect = sb.ToString();

            if (DataDelegate.debugMode)
            {
                System.Windows.MessageBox.Show("The following SQL statement is being processed :\n" + mySelect, "SQL Statement");
            }

            string str_connection = "Data Source = " + dbSource + "; Initial Catalog = " + dbName + "; Integrated Security = True";
            //  string str_connection = "Data Source = OLYMPUS-MONS-FI; Initial Catalog = TSQLV3; Integrated Security = True";

            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(str_connection);
                SqlCommand command = new SqlCommand(mySelect, con);

                SqlDataAdapter ada = new SqlDataAdapter(mySelect, con);
                
                ada.Fill(ds);

                StringBuilder rb = new StringBuilder();

                foreach(DataTable table in ds.Tables)
                {
                   
                    foreach (DataRow row in table.Rows)
                    {
                        foreach (object item in row.ItemArray)
                        {
                            rb.Append(item);
                            rb.Append(" ");
                        }
                        rb.Append("\n");
                    }
                    rb.Append("\n\n");
                    
                }
                resultString = rb.ToString();

            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message.ToString() + "\n" + mySelect, "SQL Error.");
            }
            return ds;
          //  return resultString;
        }

        public int insert(String table, String[] fieldNames, String[] values)
        {
            string str_connection = "Data Source = " + dbSource + "; Initial Catalog = " + dbName + "; Integrated Security = True";
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO ");
            sb.Append("dbo.");
            sb.Append(table);

            sb.Append(" (");
            for (int index = 0; index < fieldNames.Length; index++)
            {
                sb.Append(fieldNames[index]);
                if(index != fieldNames.Length - 1)
                {
                    sb.Append(",");
                }
            }
            sb.Append(") ");

            sb.Append("VALUES (");
            for (int index = 0; index < values.Length; index++)
            {
                sb.Append("'");
                sb.Append(values[index]);
                sb.Append("'");

                if(index != values.Length - 1)
                {
                    sb.Append(",");
                }
            }
            sb.Append(");");
            string myInsert = sb.ToString();
            if (DataDelegate.debugMode)
            {
                System.Windows.MessageBox.Show("The following SQL statement is being processed :\n" + myInsert, "SQL Statement");
            }
            int recordsChanged = 0;

            try
            {
                SqlConnection con = new SqlConnection(str_connection);
                SqlCommand command = new SqlCommand(myInsert, con);
                
                
                con.Open();
                recordsChanged = command.ExecuteNonQuery();             
                con.Close();

            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show(e.Message.ToString(), "SQL Error.");
            }
            return recordsChanged;
        }

        public int update(String table, String[] fieldNames, String[] values, String where, String operatorSymbol, String value)
        {
            string str_connection = "Data Source = " + dbSource + "; Initial Catalog = " + dbName + "; Integrated Security = True";
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE ");
            sb.Append("dbo.");
            sb.Append(table);
            sb.Append(" SET ");

            
            for (int index = 0; index < fieldNames.Length; index++)
            {
                sb.Append(fieldNames[index]);
                sb.Append(" = ");
                sb.Append("'");
                sb.Append(values[index]);
                sb.Append("'");
            
                if (index != fieldNames.Length - 1)
                {
                    sb.Append(", ");
                }
            }

            sb.Append(where);
            sb.Append(" ");
            sb.Append(operatorSymbol);
            sb.Append(" ");
            sb.Append(value);
            sb.Append(";");
            
            string myUpdate = sb.ToString();
            if (DataDelegate.debugMode)
            {
                System.Windows.MessageBox.Show("The following SQL statement is being processed :\n" + myUpdate, "SQL Statement");
            }
            int recordsChanged = 0;

           

            try
            {
                SqlConnection con = new SqlConnection(str_connection);
                SqlCommand command = new SqlCommand(myUpdate, con);

                con.Open();
                recordsChanged = command.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message.ToString(), "SQL Error.");
            }
            return recordsChanged;
        }

        // Adding this function makes the database insecure. Find a way around this.
        public DataSet customSQL(String SQLcommand)
        {
            DataSet ds = new DataSet();
            String results = "";
            String sql = SQLcommand;
            if (DataDelegate.debugMode)
            {
                System.Windows.MessageBox.Show("The following SQL statement is being processed :\n" + sql, "Custom SQL Statement");
            }

            try
            {
                string str_connection = "Data Source = " + dbSource + "; Initial Catalog = " + dbName + "; Integrated Security = True";
                SqlConnection con = new SqlConnection(str_connection);
                SqlCommand command = new SqlCommand(sql, con);

                SqlDataAdapter ada = new SqlDataAdapter(sql, con);

                ada.Fill(ds);

                StringBuilder rb = new StringBuilder();

                foreach (DataTable table in ds.Tables)
                {

                    foreach (DataRow row in table.Rows)
                    {
                        foreach (object item in row.ItemArray)
                        {
                            rb.Append(item);
                            rb.Append(" ");
                        }
                        rb.Append("\n");
                    }
                    rb.Append("\n\n");
                }
                results = rb.ToString();
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message.ToString() + sql, "SQL Error");
            }

         
            // return results;
            return ds;
        }

        public String[] datasetToStringArray(DataSet d)
        {
            List<String> results = new List<string>();

            foreach (DataTable table in d.Tables)
            {

                foreach (DataRow row in table.Rows)
                {
                    foreach (object item in row.ItemArray)
                    {
                        results.Add(item.ToString());
                        
                    }              
                }
            }
            
            return results.ToArray();
        }

        public String datasetToString(DataSet d)
        {
            String result = "";

            StringBuilder rb = new StringBuilder();

            foreach (DataTable table in d.Tables)
            {

                foreach (DataRow row in table.Rows)
                {
                    foreach (object item in row.ItemArray)
                    {
                        rb.Append(item);
                        rb.Append(" ");
                    }
                    rb.Append("\n");
                }
                rb.Append("\n\n");

            }
            result = rb.ToString();

            return result;
        }

        public int countMatches(string table, string field, string value)
        {
            string str_connection = "Data Source = " + dbSource + "; Initial Catalog = " + dbName + "; Integrated Security = True";
            int recordsFound = 0;

            string sql = "SELECT " + field + " FROM " + table + " WHERE " + field + " = '" + value+ "'";
            DataSet ds = new DataSet();

            try
            {
                SqlConnection con = new SqlConnection(str_connection);
                SqlCommand command = new SqlCommand(sql, con);

                SqlDataAdapter ada = new SqlDataAdapter(sql, con);

                ada.Fill(ds);
                recordsFound = ds.Tables[0].Rows.Count;

            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message.ToString(), "SQL Error.");
            }

            return 0;
        }
        

        public void preloadCustomers()
        {
            //   DataDelegate.currentDataSet = selectStarFrom("Customer");
            DataDelegate.currentDataSet = this.selectStarFrom("Customer as T1, " +
                                                    "CustomerContact as T2, " +
                                                    "DrivingLicense as T3, " +
                                                    " CreditCard as T4 " +
                                                    "WHERE T1.CustomerID = T2.CustomerID " +
                                                    "and T1.CustomerID = T3.CustomerID " +
                                                    "and T1.CustomerID = T4.CustomerID " +
                                                    "ORDER BY T1.LastName ASC");

           
        }

    }
}
