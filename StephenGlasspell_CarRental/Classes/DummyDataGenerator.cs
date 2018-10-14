using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

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
    class DummyDataGenerator
    {
        static DummyDataGenerator instance = null;
        

        DummyDataGenerator()
        {
           
        }

        public static DummyDataGenerator getInstance()
        {
            if(instance == null)
            {
                instance = new DummyDataGenerator();
            }

            return instance;
        }

        int chanceOfHavingHyphonatedName = 3;  // 10% chance of having a hyphonated name;
        int chanceOfHavingMiddleName = 20;      // 20% chance of having a middle name;
        int chanceOFHavingCityPrefix = 5;
        int chanceOfHavingCityAppendage = 30;

        String[] titlesMale = {"Mr.", "Dr." };
        String[] titlesFemale = { "Miss.", "Mrs.", "Ms.", "Dr." };
        String[] firstNamesMale = { "Aaron",
"Ab",
"Abraham",
"Absalom",
"Adam",
"Addison",
"Adel",
"Adolf",
"Adrian",
"Ælfweard",
"Albert",
"Alec",
"Alexander",
"Alfred",
"Alistair",
"Alvin",
"Anderson",
"Andrew",
"Andy",
"Anthony",
"Archibald",
"Archie",
"Arlo",
"Armistead",
"Arnaut",
"Arnold",
"Arthur",
"Ashley",
"Austen",
"Austin",
"Barnabe",
"Baron",
"Bartholomew",
"Bayard",
"Ben",
"Benedict",
"Benjamin",
"Bernard",
"Bertram",
"Blake",
"Booth",
"Brad",
"Bramwell",
"Brock",
"Brooks",
"Bubba",
"Bubby",
"Buck",
"Bud(nickname)",
"Byron",
"Caleb",
"Callum",
"Calvin",
"Cardew",
"Carl",
"Carlton",
"Cary",
"Chad",
"Chadwick",
"Chance",
"Chandos",
"Charlie",
"Chas",
"Chet",
"Christian",
"Christopher",
"Claire",
"Clare",
"Claude",
"Clay",
"Cleve",
"Cliff",
"Clint",
"Colby",
"Cole",
"Colin",
"Colman",
"Coloman",
"Colton",
"Conway",
"Corbin",
"Corey",
"Curtis",
"Curtley",
"Cuthbert",
"Dallas",
"Dana",
"Daniel",
"Danny",
"David",
"Davy",
"Delbert",
"Derek",
"Derrick",
"Dexter",
"Dickon",
"Dirk",
"Dobie",
"Donald",
"Doug",
"Dougie",
"Douglas",
"Drew",
"Drummond",
"Dwight",
"Earl",
"Ebenezer",
"Ed",
"Eddie",
"Edgar",
"Edmund",
"Edward",
"Edwin",
"Elbert",
"Elias",
"Elliot",
"Ellwood",
"Elmer",
"Elton",
"Emil",
"Erastus",
"Eric",
"Ernest",
"Ethan",
"Evelyn",
"Ezekiel",
"Fabian",
"Felix",
"Floyd",
"Francis",
"Frank",
"Franklin",
"Frederick",
"Gabriel",
"Gale",
"Galton",
"Gareth",
"Garth",
"Gary",
"Gavin",
"Geoffrey",
"George",
"Gerald",
"Gerard",
"Gilbert",
"Glen",
"Gorden",
"Gordon",
"Graham",
"Grant",
"Grayson",
"Gregory",
"Griffin",
"Gulliver",
"Hal",
"Hank",
"Harold",
"Harry",
"Hedworth",
"Henry",
"Herbert",
"Herman",
"Hervey",
"Hilary",
"Holbrook",
"Hope",
"Horace",
"Howard",
"Hubert",
"Hudson",
"Hugh",
"Hugo",
"Humphrey",
"Hunter",
"Ian",
"Increase",
"Ivy",
"Jack",
"Jackson",
"Jacob",
"Jaime",
"Jake",
"James",
"Jamie",
"Jared",
"Jason",
"Jasper",
"Jay",
"Jeb",
"Jeff",
"Jeffery",
"Jeffrey",
"Jeremy",
"Jermaine",
"Jerome",
"Jerry",
"Jess",
"Jesse",
"Jim",
"Jimmy",
"Job",
"Jodie",
"Joel",
"Joey",
"Joey",
"John",
"Johnson",
"Jolyon",
"Jonas",
"Jonathan",
"Joseph",
"Joshua",
"Julian",
"Julius",
"Justin",
"Kay",
"Keith",
"Ken",
"Kendrick",
"Kenneth",
"Kenny",
"Kevin",
"Kian",
"Kim",
"Kimble",
"Kurt",
"Kyle",
"Lanny",
"Larry",
"Laurence",
"Lawrence",
"Lawton",
"Leonard",
"Leopold",
"Lester",
"Lewis",
"Lindsay",
"Linus",
"Louis",
"Luther",
"Lyle",
"Malachi",
"Malcolm",
"Mandy",
"Manuel",
"Marcus",
"Mark",
"Marshall",
"Martin",
"Marvin",
"Matt",
"Matthew",
"Maurice",
"Maximilian",
"Melvin",
"Merle",
"Merlin",
"Michael",
"Miles",
"Milo",
"Murray",
"Myron",
"Nate",
"Nathan",
"Neil",
"Newt",
"Newton",
"Nicholas",
"Nicolas",
"Noel",
"Nolan",
"Norman",
"Nowell",
"Oliver",
"Orlando",
"Osbert",
"Oscar",
"Osric",
"Oswald",
"Otto",
"Owen",
"Palmer",
"Patrick",
"Patsy",
"Paul",
"Peleg",
"Pete",
"Peter",
"Philip",
"Phillipps",
"Quentin",
"Raife",
"Ralph",
"Ramsey",
"Randall",
"Rathbone",
"Ray",
"Raymond",
"Reginald",
"Rendell",
"Reuben",
"Rex",
"Rich",
"Richard",
"Richie",
"Ricky",
"Rob",
"Robbie",
"Robert",
"Roderick",
"Rodger",
"Rodney",
"Roger",
"Rogers",
"Ron",
"Ronald",
"Rowland",
"Rufus",
"Rupert",
"Russell",
"Samuel",
"Sebastian",
"Seth",
"Shahaf",
"Shane",
"Shawn",
"Shayne",
"Sigmund",
"Simon",
"Skyler",
"Sol",
"Stefan",
"Stephen",
"Steve",
"Swaine",
"Sylvester",
"Taran",
"Tate",
"Tazewell",
"Terence",
"Thaddeus",
"Thomas",
"Tim",
"Timmy",
"Timothy",
"Tobias",
"Toby",
"Tony",
"Tracy",
"Travis",
"Trevor",
"Tristan",
"Troy",
"Ultan",
"Ulysses",
"Valentine",
"Vicary",
"Victor",
"Vince",
"Vincent",
"Vivian",
"Wadsworth",
"Waldo",
"Walter",
"Wayne",
"Whitney",
"Wilfred",
"Wilfried",
"William",
"Winnie",
"Winston",
"Zack",
"Zadoc",
"Zechariah"
 };
        String[] firstNamesFemale = {"Ashley",
"Ava",
"Aya",
"Barb",
"Becca",
"Becky",
"Bessie",
"Breanna",
"Brenda",
"Brett",
"Briana",
"Brianne",
"Brina",
"Camryn",
"Carlene",
"Carly",
"Catriona",
"Chrissie",
"Chrissy",
"Christiane",
"Christina",
"Claire",
"Clarice",
"Dana",
"Darla",
"Dawn",
"Debbie",
"Diane",
"Dove",
"Earlene",
"Ellyse",
"Elsie",
"Erika",
"Eve",
"Evelyn",
"Githa",
"Hayden",
"Hilary",
"Jamie",
"Janet",
"Jeanie",
"Jodi",
"Johnnie",
"Johnny",
"Kadia",
"Katy",
"Kendra",
"Kim",
"Kimberly",
"Kirsty",
"Kristen",
"Lexi",
"Lisabeth",
"Lizette",
"Lizzie",
"Malvina",
"Marge",
"Mel",
"Misty",
"Muriel",
"Myrtle",
"Olivia",
"Oona",
"Pam",
"Patricia",
"Penny",
"Randi",
"Randy",
"Raven",
"Sadie",
"Salma",
"Selma",
"Shanna",
"Skyler",
"Tanith",
"Taryn",
"Ursula",
"Whitney",
"Willa"
 };

        String[] surnames = {"Abram",
"Acton",
"Addington",
"Adley",
"Ainsley",
"Ainsworth",
"Alby",
"Allerton",
"Alston",
"Altham",
"Alton",
"Anderton",
"Ansley",
"Anstey",
"Appleton",
"Asheton",
"Ashley",
"Ashton",
"Astley",
"Atherton",
"Atterton",
"Axton",
"Badger",
"Barclay",
"Barlow",
"Barney",
"Barton",
"Beckwith",
"Benson",
"Bentham",
"Bentley",
"Berkeley",
"Beverly",
"Bing",
"Birkenhead",
"Blackwood",
"Blakeley",
"Blakely",
"Blankley",
"Blyth",
"Blythe",
"Bradford",
"Bradley",
"Bradly",
"Bradshaw",
"Brady",
"Brandon",
"Branson",
"Braxton",
"Breeden",
"Brent",
"Bristol",
"Brixton",
"Browning",
"Brownrigg",
"Buckingham",
"Budd",
"Burton",
"Byron",
"Camden",
"Carlisle",
"Carlton",
"Carlyle",
"Cason",
"Charlton",
"Chatham",
"Chester",
"Cholmondeley",
"Churchill",
"Clapham",
"Clare",
"Clayden",
"Clayton",
"Clifford",
"Clifton",
"Clinton",
"Clive",
"Colby",
"Colgate",
"Colton",
"Compton",
"Coombs",
"Copeland",
"Cornish",
"Cotton",
"Crawford",
"Cromwell",
"Cumberbatch",
"Dalton",
"Darby",
"Darlington",
"Davenport",
"Dayton",
"Deighton",
"Denholm",
"Digby",
"Dryden",
"Dudley",
"Eastaughffe",
"Eastoft",
"Easton",
"Elton",
"Emsworth",
"Enfield",
"England",
"Everleigh",
"Everly",
"Farley",
"Fawcett",
"Fulton",
"Garfield",
"Garrick",
"Gladstone",
"Goody",
"Graeme",
"Graham",
"Gresham",
"Hackney",
"Hadlee",
"Hadleigh",
"Hadley",
"Hailey",
"Hale",
"Hales",
"Haley",
"Hallewell",
"Halsey",
"Hamilton",
"Hampton",
"Harlan",
"Harley",
"Harlow",
"Harrington",
"Hartford",
"Hastings",
"Hayden",
"Hayes",
"Hayhurst",
"Hayley",
"Holton",
"Home",
"Hornsby",
"Huckabee",
"Huxley",
"Kelsey",
"Kendal",
"Kendall",
"Kenley",
"Kensley",
"Kent",
"Kimberley",
"Kimberly",
"Kinsley",
"Kirby",
"Lancaster",
"Landon",
"Langdon",
"Langley",
"Langston",
"Law",
"Leighton",
"Lester",
"Lincoln",
"Lindsay",
"Lindsey",
"Livingstone",
"Manley",
"Marlee",
"Marleigh",
"Marley",
"Marlowe",
"Marston",
"Merton",
"Middleton",
"Milton",
"Mitchell",
"Morley",
"Morton",
"Myerscough",
"Nash",
"Nibley",
"Northcott",
"Norton",
"Oakes",
"Oakley",
"Ogden",
"Paxton",
"Payton",
"Perry",
"Peyton",
"Pickering",
"Pinkerton",
"Prescott",
"Presley",
"Preston",
"Quinton",
"Ramsay",
"Ramsey",
"Rayden",
"Redfield",
"Reed",
"Reid",
"Remington",
"Ridley",
"Riley",
"Rodney",
"Roscoe",
"Rowley",
"Royal",
"Royston",
"Rutherford",
"Rutland",
"Rylan",
"Ryland",
"Ryley",
"Scarboro",
"Scarbrough",
"Shelby",
"Sheldon",
"Shelley",
"Shelly",
"Sherwood",
"Shipley",
"Shirley",
"Skelton",
"Snape",
"Snowdon",
"Soames",
"Southey",
"Spalding",
"Spaulding",
"Springfield",
"Stafford",
"Stanford",
"Stanley",
"Stansfield",
"Stanton",
"Stapleton",
"Start",
"Stratford",
"Sutherland",
"Sutton",
"Sydney",
"Tattersall",
"Tatum",
"Tenley",
"Tewksbury",
"Thackeray",
"Thornton",
"Thorpe",
"Tickle",
"Tindall",
"Tinley",
"Trollope",
"Tyndall",
"Upton",
"Vance",
"Wade",
"Wakefield",
"Walcott",
"Wallace",
"Walpole",
"Warwick",
"Washington",
"Webley",
"Wedgwood",
"Weld",
"Wellington",
"Wentworth",
"Wesley",
"Westbrook",
"Westcott",
"Weston",
"Wharton",
"Wheatley",
"Whitby",
"Wilberforce",
"Willoughby",
"Winchester",
"Windsor",
"Winterbourne",
"Winthrop",
"Wordsworth",
"Yardley",
"Yeardley",
"York",
"Yorke"
 };

        String[] streetTypes = {"Avenue",
"Road",
"Street",
"Drive",
"Place"
};
        String[] states = {
        "ACT","NSW","NT","QLD","SA","TAS","VIC","WA"};

        public   String[] allCustomerDetails()
        {
            String[] results = new string[] { };
            List<String> resultsList = new List<String>();
            String[] customerfields = CustomerFields();

            foreach (String field in customerfields)
            {
                resultsList.Add(field);
            }
            foreach (String field in CustomerContactFields(customerfields[1], customerfields[3], customerfields[4]))
            {
                resultsList.Add(field);
            }
            foreach(String field in creditCardDetails(customerfields[0], customerfields[1], customerfields[3]))
            {
                resultsList.Add(field);
            }
            foreach(String field in drivingLicenseDetails())
            {
                resultsList.Add(field);
            }

            results = resultsList.ToArray();

            return results;
        }

        public String[] CustomerFields()
        {
            Random ran = new Random();
            Random rand = new Random(DateTime.Now.Millisecond + ran.Next(0, 100000));
            bool male = rand.Next(0, 100) % 2 == 1 ? true : false;

            String[] results = new String[5];   // Title, Firstname, MiddleName?, LastName, DateOfBirth
            results[0] = title(male);
            results[1] = firstName(male);
            String middle = middleName(male);
            while (middle.Equals(results[1]))
            {
                middle = middleName(male);
            }
            results[2] = middle;
            results[3] = surname();
            results[4] = DOB();

            return results;
        }

        public String[] CustomerContactFields(String firstname, String lastname, String dob)
        {
            String[] result = new String[8]; // HouseNo, StreetName, Suburb, City, State, Postcode, email, phone
            result[0] = houseNo();
            String street = "";
            while (surname().Equals(lastname))
            {
                street = surname();
            }
            result[1] = streetName();

            String suburb = surname();
            while (surname().Equals(lastname))
            {
                suburb = surname();
            }
            result[2] = suburb;

            String scity = "";
            while (surname().Equals(suburb))
            {
                scity = surname();
            }
            result[3] = city();
            result[4] = state();
            result[5] = postcode(result[4]);
            result[6] = email(firstname, lastname, result[0], result[1], dob);
            result[7] = phone();
            
            return result;
        }

        public String[] creditCardDetails(String title, String firstname, String lastname)
        {
            String[] results = new String[5];   // Credit Card Type, Name on Card, Credit Card Number, Expiry, Security Code

            String[] creditCardTypes = new string[] { "Visa", "MasterCard", "Amex" };
            Random rand = new Random();
            int index = rand.Next(0, 2);

            results[0] = creditCardTypes[index];
            results[1] = firstname + " " + lastname;

            String creditCardNumber = "";
            while (creditCardNumber.Length < 16)
            {
                int i = rand.Next(0, 10);
                creditCardNumber += i.ToString();
            }

            results[2] = creditCardNumber;

            results[3] = rand.Next(1,10).ToString() + "/" + rand.Next(20,24).ToString();
            results[4] = rand.Next(150,1000).ToString();



            return results;

        }

        private String title(bool male)
        {
            String results = "";

            if (male)
            {
                Random ran = new Random();
                Random rand = new Random(DateTime.Now.Millisecond + ran.Next(0, 100000));
                int index = ran.Next(1,2) -1;
                results = (titlesMale[index]);
            }
            else
            {
                Random ran = new Random();
                Random rand = new Random(DateTime.Now.Millisecond + ran.Next(0, 100000));
                int index = ran.Next(1,4) -1;
                results = (titlesFemale[index]);
            }

            return results;

        }

        private  String firstName(bool male)
        {
            String result = "";
            Random ran = new Random();
            Random rand = new Random(DateTime.Now.Millisecond + ran.Next(0, 100000));

            if (male)
            {
                int index = rand.Next(0, firstNamesMale.Length);
                result = firstNamesMale[index];

                if (rand.Next(0, 100) >= (100 - chanceOfHavingHyphonatedName))
                {
                    index = rand.Next(0, firstNamesMale.Length);
                    result += "-" + firstNamesMale[index];
                }

            }
            else
            {
                int index = rand.Next(0, firstNamesFemale.Length);
                result = firstNamesFemale[index];

                if (rand.Next(0, 100) >= (100 - chanceOfHavingHyphonatedName))
                {
                    index = rand.Next(0, firstNamesFemale.Length);
                    result += "-" + firstNamesFemale[index];
                }
            }

            return result;
        }

        private String middleName(bool male)
        {
            String result = "";
            Random ran = new Random();
            Random rand = new Random(DateTime.Now.Millisecond + ran.Next(0, 100000));
            if (rand.Next(0,100) > (100 - chanceOfHavingMiddleName))
            {
                result = firstName(male);
            }

            return result;
        }

        private  String surname()
        {
            Random ran = new Random();
            Random rand = new Random(DateTime.Now.Millisecond + ran.Next(0, ran.Next(0,10000)));
            int index = rand.Next(0, surnames.Length);
            String result = surnames[index];

            if (rand.Next(0, 100) >= (100 - chanceOfHavingHyphonatedName))
            {
                index = rand.Next(0, surnames.Length);
                result += "-" + surnames[index];
            }

            return result;
        }

        private String DOB()
        {
            DateTime dob;

            Random ran = new Random();
            Random rand = new Random(DateTime.Now.Millisecond + ran.Next(0, 100000));
            int age = rand.Next(26, 65);
            int year = DateTime.Now.Year - age;
            int month = rand.Next(1, 12);
            int day = rand.Next(0, 28);

            try
            {
                dob = new DateTime(year, month, day);
                return dob.ToString("yyyy-MM-dd");
            }catch(Exception e)
            {
                return DOB();
            }
        }

        private  String houseNo()
        {
            String result = "";

            Random ran = new Random();
            Random rand = new Random(DateTime.Now.Millisecond + ran.Next(0, 100000));
            int index = rand.Next(0, 200);
            result = index.ToString();

            return result;
        }

        private String streetName()
        {
            String result = "";

            Random ran = new Random();
            Random rand = new Random(DateTime.Now.Millisecond + ran.Next(0, 100000));
            int index = rand.Next(0, surnames.Length);
            result = surnames[index];

            index = rand.Next(0, streetTypes.Length);
            result += " " + streetTypes[index];

            return result;
        }

        private  String city()
        {
            String result = "";

            String[] prefixes = { "", "New ", "Greater ", "Upper ", "Lower " };
            String[] appendages = {"", " City", "ville", " Bay", " Quays" };

            Random ran = new Random();
            Random rand = new Random((int)(DateTime.Now.Millisecond / Math.PI));
            int index = 0;
            if (ran.Next(0,100) > (100 - chanceOFHavingCityPrefix))
            {
                index = rand.Next(0, prefixes.Length);
                result = prefixes[index];
            }
            
            result += surname();

            if (ran.Next(0, 100) > (100 - chanceOfHavingCityAppendage))
            {
                index = rand.Next(0, appendages.Length);
                result += appendages[index];
            }


            return result;
        }

        private String state()
        {
            String result = "";
            Random ran = new Random();
            Random rand = new Random(DateTime.Now.Millisecond + ran.Next(0, 100000));
            int index = rand.Next(0, states.Length);
            result = states[index];

            return result;
        }

        private  String postcode(String state)
        {
            String result = "";
            Random ran = new Random();
            Random rand = new Random(DateTime.Now.Millisecond + ran.Next(0, 100000));
            int index = 0;

            // TODO improve this inefficient code. 
            while(DataValidator.stateForPostcode(index) != state)
            {
                index = rand.Next(0, 10000);
            }
           String resultString = index.ToString();
            while(resultString.Length < 4)
            {
               resultString = resultString.Insert(0, "0");
            }

            result = index.ToString();

            return result;
        }

        private String email(String firstname, String lastname, String houseno, String street, String dob)
        {
            String result = "";

            String[] emailTypes = {"gmail.com", "hotmail.com", "yahoo.com" };
            String[] dumbWords = { "Rocks", "4eva", "god", "Password", "Secret", "Cool" };
            String[] specialCharacters = { "!", "#", "$", "%", "&", "*", "(", ")", "]", "[" };

            Random ran = new Random();
            Random rand = new Random(DateTime.Now.Millisecond + ran.Next(0, 100000));
            int method = rand.Next(0, 3);

            switch (method){
                case 0:
                    int dumbword = rand.Next(0, dumbWords.Length);
                    int emailtype = rand.Next(0, emailTypes.Length);
                    result = firstname + dumbWords[dumbword];
                    if(result.Length > 20)
                    {
                        result = result.Substring(0, 20);
                    }
                    result +="@" + emailTypes[emailtype];
                    break;

                case 1:
                    String[] randomElements = { firstname, lastname, houseno,};
                    int numberOfRandomElementsToIncludeInEmail = rand.Next(02, 3);
                    for(int i = 0; i < numberOfRandomElementsToIncludeInEmail; i++)
                    {
                       int element = rand.Next(0, randomElements.Length);
                        result += randomElements[element];
                    }

                    int numberOfSpecialCharactersToPepperThroughoutEmail = rand.Next(0, 5);
                    for(int i = 0; i < numberOfSpecialCharactersToPepperThroughoutEmail; i++)
                    {
                        int emailindex = rand.Next(0, result.Length);
                        int specialCharacter = rand.Next(0, specialCharacters.Length);
                       result = result.Insert(emailindex, specialCharacters[specialCharacter]);
                    }

                    if (result.Length > 20)
                    {
                        result = result.Substring(0, 20);
                    }
                    int emailType = rand.Next(0, emailTypes.Length);
                    result += "@" + emailTypes[emailType];
                    break;


                default:
                    int index = rand.Next(0, emailTypes.Length);
                    result = firstname + lastname;
                    if (result.Length > 20)
                    {
                        result = result.Substring(0, 20);
                    }
                    emailType = rand.Next(0, emailTypes.Length);
                    result += "@" + emailTypes[emailType];

                    break;
            }
            return result;
        }

        private  String phone()
        {
            String result = "0";
            Random ran = new Random();
            Random rand = new Random(DateTime.Now.Millisecond + ran.Next(0, 100000));
            for (int i = 0; i < 9; i++)
            {
                result += rand.Next(0, 10).ToString();
            }

            return result;


        }

        private String[] drivingLicenseDetails()
        {
            String[] results = new string[5];  // Country, License Number, Issue, Expiry, Convictions

            results[0] = "Australia";
            String drivingLicenseNumber = "";
            Random rand = new Random();
            while(drivingLicenseNumber.Length < 8)
            {
                drivingLicenseNumber += rand.Next(0, 10).ToString();
            }

            results[1] = drivingLicenseNumber;
            results[2] = new DateTime(rand.Next(1970, 2017), rand.Next(1, 13), rand.Next(1, 28)).ToString("yyyy-MM-dd");
            results[3] = new DateTime(rand.Next(2030, 2060), rand.Next(1, 13), rand.Next(1, 28)).ToString("yyyy-MM-dd");
            results[4] = "0";

            return results;
        }

        public void makeCustomerRecordsToFile(int numberOfRecordsToMake)
        {
           if(File.Exists(Directory.GetCurrentDirectory() + "/sql.txt"))
            {
                File.Delete(Directory.GetCurrentDirectory() + "/sql.txt");
            }

            List<String> creditCardNumbers = createUniqueNumbers(16, numberOfRecordsToMake);
            List<String> phoneNumbers = createUniqueNumbers(10, numberOfRecordsToMake);
            List<String> drivingLicenseNumber = createUniqueNumbers(7, numberOfRecordsToMake);


            StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + "/sql.txt");
            int cust = 0; 

            for (int i = 0; i < numberOfRecordsToMake; i++)
            {
                String[] s = DummyDataGenerator.getInstance().allCustomerDetails();
                
                

                sw.WriteLine("INSERT INTO Customer ( Title, FirstName, MiddleName, LastName, DateOfBirth) " +
                    "VALUES ('" + s[0] +"', '" + s[1] + "', '" + s[2] + "', '" + s[3] + "', '" + s[4] + "' );");

                sw.WriteLine("INSERT INTO CustomerContact " +
                    "(CustomerID, HouseNumber, StreetName, Suburb, City, State, Postcode, Email, MobilePhone) " +
                    "VALUES ("+ cust.ToString() +", '" + s[5] +"', '"+s[6]+"', '"+s[7]+"', '"+s[8]+"', '"+s[9]+"', '"+s[10]+"', '"+s[11]+"', '"+ phoneNumbers.ElementAt(cust) +"');");

                sw.WriteLine("INSERT INTO CreditCard " +
                    "(CustomerID, CredCardID, CredCardType, NameOnCredCard, CredCardNumber, CredCardExpiry, CredCardSecurityCode) " +
                    "VALUES(" + cust.ToString() + ", '" + cust.ToString() + "', '" + s[13] + "', '" + s[14] + "', '" + creditCardNumbers.ElementAt(cust) + "', '" + s[16] + "', '" + s[17] + "');");

                sw.WriteLine("INSERT INTO DrivingLicense (CustomerID, Country, DrivingLicenseNumber, IssueDate, ExpiryDate, Convictions) " +
                    "VALUES(" + cust.ToString() + ", '" + s[18] + "', '" + drivingLicenseNumber.ElementAt(cust) + "', '" + s[20] + "', '" + s[21] + "', '" + s[22] + "');");

                //      Title Firstname   MiddleName LastName    DOB 
                //      HouseNo Street Suburb  City State   Postcode email   phone 
                //     creditcardType  NameOnCard CreditCardNumber    Expiry Security Code 
                //     DrivingLicenseCountry   LicenseNo IssueDate   ExpiryDate Convictions


                cust++;
                sw.WriteLine();
            }
            sw.Close();
            
        }

        public List<String> createUniqueNumbers(int lengthOfNumber, int numberOfNumbers)
        {
            List<String> Numbers = new List<String>();

            while(Numbers.Count < numberOfNumbers)
            {
                String Number = "";
                Random rand = new Random(DateTime.Now.Millisecond);
                while(Number.Length < lengthOfNumber)
                {
                    Number += rand.Next(10).ToString();
                }
                if (!Numbers.Contains(Number))
                {
                    Numbers.Add(Number);
                }
            }

            return Numbers;
        }


    }
}
