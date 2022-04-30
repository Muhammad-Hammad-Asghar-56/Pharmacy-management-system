using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy_management_system.BL;

namespace Pharmacy_management_system
{
    class Program
    {
        static int workers_Count = 0;
        static int gtzchecker = 0;
        static int workersize = 10;
        static int billslimit = 10;
        static int stockmed_limit = 10;
        static int billcounter = 0;
        static int stockcounter = 2;
        static int usercount = 0;
        static int option;
        static int pack, packsize, price;
        static string namecheck;
        //             header
        //_________________________________________________________________________________________________________________
        //             worker variables declaration

        static List<worker> workersList = new List<worker>();

        //             Add new Users

        static List<Maccounts> usersList = new List<Maccounts>();

        //             stock variables declaration
        static string[] med_name = new string[stockmed_limit];
        static int[] price_per = new int[stockmed_limit];
        static int[] total_med = new int[stockmed_limit];
        //             bills variables declaration

        static string[] billmed = new string[billslimit];
        static int[] qty = new int[billslimit];
        static int[] bill = new int[billslimit];
        static float[] disc = new float[billslimit];
        static float[] pay = new float[billslimit];
        static string[] biller = new string[billslimit];
        //             return bill variable declaration
        static string[] rbillmed = new string[billslimit];
        static int[] rqty = new int[billslimit];
        static int[] rbill = new int[billslimit];
        static float[] rdisc = new float[billslimit];
        static float[] rpay = new float[billslimit];
        static string[] rbiller = new string[billslimit];
        static int returncounter = 0;
        static string names;
        static void Main(string[] args)
        {
            //________________________________intiallize the stock first________________________________
            med_name[0] = "panadol";
            med_name[1] = "disprin";

            total_med[0] = 500;
            total_med[1] = 500;

            price_per[0] = 2;
            price_per[1] = 3;
            int x;
            //_____________________________________constant admin______________________________________
            Maccounts s = new Maccounts("admin", "1a23!", "admin"); 
            usersList.Add(s);
            Console.Clear();
            load();

            mainheader();
            Console.Write("Press 1 for login and 0 for exit the program");
            x = int.Parse(Console.ReadLine());
            Console.Clear();

            while (x != 0)
            {
                mainheader();
                string role = login();
                Console.Clear();

                // ************************************** admin block ******************************************************
                if (role == "admin")
                {
                    mainheader();
                    option = mainmenu();
                    Console.Clear();
                    while (option < 5)
                    {
                        //____________________________     billing __________________________________
                        if (option == 1)
                        {
                            x = billmanagement();
                            Console.Clear();
                            while (x < 4)
                            {
                                mainheader();
                                if (x == 1)
                                {
                                    //---------------------------billing-------------------------
                                    bills();
                                    paused();
                                }
                                //-------------------------------bill history--------------------
                                else if (x == 2)
                                {
                                    billhistory();
                                    paused();
                                }
                                //-------------------------------Bill return---------------------
                                else if (x == 3)
                                {
                                    billreturn();
                                    paused();
                                }
                                x = billmanagement();
                                Console.Clear();
                            }
                        }
                        // _________________________option of admin to add users___________________________________
                        else if (option == 2)
                        {
                            mainheader();
                            x = usermenu();
                            Console.Clear();
                            while (x == 1 || x == 2)
                            {
                                mainheader();
                                //---------------------------option for add user-------------------------------
                                if (x == 1)
                                {
                                    usersList.Add(getInfoOfAccount());

                                }
                                //------------------option for delete users----------------------------------------
                                if (x == 2)
                                {
                                    int del = delUsernumber();
                                    usersList.RemoveAt(del);
                                }
                                x = usermenu();
                            }
                        }
                        //__________________ main option user for the worker management___________________________
                        else if (option == 3)
                        {
                            mainheader();
                            x = workermenu();
                            Console.Clear();
                            while (x != 3)
                            {
                                //------------------sub option for add som user----------------------
                                if (x == 1)
                                {
                                    mainheader();
                                    workersList.Add(hiringInfomationFromConsole());
                                    paused();
                                }
                                //-----------------sub option for generate worker sorted list-----------------
                                if (x == 2)
                                {
                                    mainheader();
                                    sorting(workersList);
                                    showSortWorker(workersList);
                                    paused();
                                }
                                mainheader();
                                x = workermenu();
                                Console.Clear();
                            }
                        }
                        //______________________ main option for stock management______________________________
                        else if (option == 4)
                        {
                            x = stockmenu();
                            Console.Clear();
                            while (x < 3)
                            {
                                //--------------------sub option for add new stock------------------
                                if (x == 1)
                                {
                                    addstock();
                                    Console.Clear();
                                }
                                //--------------------sub option for backup-------------------------
                                if (x == 2)
                                {
                                    stockneed();
                                    paused();
                                }
                                x = stockmenu();
                            }
                        }
                        mainheader();
                        option = mainmenu();
                        Console.Clear();
                    }
                }
                // ************************************** User block ******************************************************

                else if (role == "user")
                {
                    option = userloginmenu();
                    Console.Clear();
                    while (option < 4)
                    {
                        //                    ########     BILLING  ######
                        if (option == 1)
                        {
                            x = billmanagement();

                            Console.Clear();
                            while (x < 4)
                            {
                                //-------------------------sub option for billing -------------------------------------------------
                                mainheader();
                                if (x == 1)
                                {
                                    bills();
                                    paused();
                                }
                                //-------------------------sub option for return bill -----------------------------------------------
                                if (x == 2)
                                {
                                    billhistory();
                                    paused();
                                }
                                x = billmanagement();
                                Console.Clear();
                            }
                        }
                        //_________________________________ option for status ______________________________________
                        else if (option == 2)
                        {

                            for (int i = 0; i < usercount; i++)
                            {
                                if (names == usersList[i].userName)
                                {
                                    mainheader();
                                    Console.WriteLine("*********************************************************************************************************");
                                    Console.WriteLine("                          Welcome  {0}", names);
                                    Console.WriteLine(" The name of the user is :{0} ", usersList[i].userName);
                                    Console.WriteLine(" The salary is : {0}", workersList[i].salary);
                                    Console.WriteLine("The role in the pharmacy is :{0}", usersList[i].userRole);
                                    paused();
                                    break;
                                }
                            }
                            paused();
                        }
                        option = userloginmenu();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine("$$$$$$$$$$$$$$$$$$$$!!!!!!!!!!!!!!!!!   Warning!!!!!! $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
                    Console.WriteLine("");
                    Console.WriteLine("Check the login");
                }
                mainheader();

                Console.Write("Press 1 for login and 0 for exit the program");
                x = int.Parse(Console.ReadLine());

                store(workersList);
            }
        }
        static void mainheader()
        {
            Console.WriteLine("    ____  __                                                                                                         ");
            Console.WriteLine("   / __ |/ /__  ____  _____ ____ ___  ____  _______  __                                                              ");
            Console.WriteLine("  / /_/ / __  // __ `/ ___// __ `__ |/ __ `/ ___/ / / /                                                              ");
            Console.WriteLine(" / ____/ / / // /_/ / /   / / / / / / /_/ / /__/ /_/ /                                                               ");
            Console.WriteLine("/_/   /_/ /_/ |__,_/_/   /_/ /_/ /_/|__,_/|___/____,/                                                                ");
            Console.WriteLine("                                              /____/                                                                 ");
            Console.WriteLine("                               __  ___                                                  __                           ");
            Console.WriteLine("                              /  |/  /___ _____  ____ _____ ____  ____ ___  ___  ____  / /_                          ");
            Console.WriteLine("                             / /|_/ / __ `/ __ |/ __ `/ __ `/ _ |/ __ `__ |/__ |/ __ |/ __/                          ");
            Console.WriteLine("                            / /  / / /_/ / / / / /_/ / /_/ /  __/ / / / / /  __/ / / / /_                            ");
            Console.WriteLine("                           /_/  /_/|__,_/_/ /_/|__,_/|__, /|___/_/ /_/ /_/|___/_/ /_/|__/                            ");
            Console.WriteLine("                                                    /____/                                                           ");
            Console.WriteLine("                                                       _____            __                                           ");
            Console.WriteLine("                                                      / ___/__  _______/ /____  ____ ___                             ");
            Console.WriteLine("                                                      |__ |/ / / / ___/ __/ _ |/ __ `__ |                            ");
            Console.WriteLine("                                                     ___/ / /_/ (__  ) /_/  __/ / / / / /                            ");
            Console.WriteLine("                                                    /____/|__, /____/|__/|___/_/ /_/ /_/                             ");
            Console.WriteLine("                                                         /____/                                                      ");
        }
        static int mainmenu()
        {
            Console.WriteLine("************************************************************************************************************************");
            Console.WriteLine("                                         Main     Menu");
            Console.WriteLine("************************************************************************************************************************");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("  CHOOSE ONE OF THE OPTION :");
            Console.WriteLine("1: BILL MANAGEMENT :");
            Console.WriteLine("2: User MANAGEMENT :");
            Console.WriteLine("3: Worker MANAGMENT :");
            Console.WriteLine("4: Stock MANAGMENT :");
            Console.WriteLine("5: EXIT THE PROGRAM ");
            option = int.Parse(Console.ReadLine());
            Console.Clear();
            return option;
        }
        //                 Helping function
        static bool limitCheck(int i, int backup)
        {
            if (i <= backup)
            {
                return true;
            }
            return false;
        }
        static void paused()
        {
            Console.ReadKey();
            Console.Clear();
        }
        static string login()
        {

            string passwordcheck;

            Console.Write("Enter the user name :");
            names = Console.ReadLine();
            Console.Write("Enter the user password :");
            passwordcheck = Console.ReadLine();

            for (int i = 0; i < usersList.Count; i++)
            {
                if ((usersList[i].userName == names) && (usersList[i].userPassword == passwordcheck))
                return usersList[i].userRole;
            }
            string empty = " ";
            return empty;
        }

        //                 Hiring Management

        static worker hiringInfomationFromConsole()
        {
            /* Here we introduce new worker and return the object in  main menu and catch by list 
             */
            string person_name = " ";
            while (true)
            {
                Console.Write("Name of new hired person name : ");
                person_name = Console.ReadLine();
                if (notempty(person_name))
                {
                    break;
                }
            }
            Console.Write("Name of new hired person age  : ");
            int age = validintloop(18);
            Console.Write("Name of new hired person religion : ");
            string religion = Console.ReadLine();

            Console.Write("Name of new hired person  salary: ");
            int salary = validintloop(0);

            Console.Write("Name of new hired person's post : ");
            string post = Console.ReadLine();

            Console.Clear();
            worker work = new worker(salary, age, person_name, religion, post);  // use the constructor to allocate the workerList 
            return work;
        }
        static int largest(int start, int end, List<worker> arr)
        {
            int idx = start;
            if (arr[2] != null)
            {
                worker largests = new worker();
                largests = arr[start];
                for (int i = start; i < end; i++)
                {
                    if (largests.salary < arr[i].salary)
                    {
                        largests = arr[i];
                        idx = i;
                    }
                }
            }
            return idx;
        }
        static void class_shifter(List<worker> workList, int intialIndex, int finalIndex)
        {
            worker changer = new worker();
            changer = workList[intialIndex];
            workList[intialIndex] = workList[finalIndex];
            workList[finalIndex] = changer;
        }
        static void sorting(List<worker> arr)
        {
            int index;
            if (arr[0] != null)
            {
                for (int i = 0; i < workersList.Count; i++)
                {
                    index = largest(i, workersList.Count, arr);
                    //        person_name shift
                    class_shifter(arr, i, index);
                }
            }
            else
            {
                Console.WriteLine(">------------------------ first you have to enter the record ---------------------------<");
            }
        }
        static void showSortWorker(List<worker> arr)
        {
            //             show  workers
            Console.WriteLine("Worker         name        post         Salary      Religion");
            foreach (var i in arr)
            {
                i.dispalyRecord();
            }
            paused();
        }
        static bool adminpasswordchecker(string adminpassword)
        {
            string admincheck;
            Console.Write("Enter the admin password to check :");
            admincheck = Console.ReadLine();
            if (admincheck == adminpassword)
            {
                return true;
            }
            return false;
        }



        //                 STOCK MANAGEMENT
        static void addstock()
        {
            mainheader();

            if (stockcounter < stockmed_limit)
            {
                Console.Write("Enter the mediConsole.Readline()e name :");
                med_name[stockcounter] = Console.ReadLine();
                Console.Write("Enter the mediConsole.Readline()e packs : ");
                pack = int.Parse(Console.ReadLine());
                Console.Write("Enter the mediConsole.Readline()e tablet in pack : ");
                packsize = validintloop(0);
                Console.Write("Enter the mediConsole.Readline()e price (pkr) : ");
                price = validintloop(0);

                price_per[stockcounter] = price / packsize;
                total_med[stockcounter] = packsize * pack;
                if (adminpasswordchecker(usersList[0].userPassword))
                {
                    Console.WriteLine(">---------------------------------------Save--------------------------------------------------<");
                    stockcounter = stockcounter + 1;
                }
                else
                {
                    Console.WriteLine("Admin password is not correct");
                    Console.WriteLine("data is  not feed");
                }
            }
        }
        static void stockneed()
        {
            mainheader();
            int backup;
            Console.Write("Enter the tablets no. for backup :");
            backup = int.Parse(Console.ReadLine());
            for (int i = 0; i < stockcounter; i++)
            {
                if (med_name[i] != " ")
                {
                    if (limitCheck(total_med[i], backup))
                    {
                        Console.Write("Your bussniess need{0}   {1} Tablets run bussiness properly", med_name[i], backup - total_med[i]);
                    }
                }
            }
        }

        //                 BILL MANAGEMENT
        static bool passwordchecker(string u, string p)
        {
            for (int i = 0; i < usersList.Count; i++)
            {
                if (u == usersList[i].userName && p == usersList[i].userPassword)
                {
                    biller[billcounter] = usersList[i].userName;
                    return true;
                }
            }
            return false;
        }
        static void billreturn()
        {
            string passwordcheck;
            string userscheck;
            int option;
            if (billcounter < billslimit)
            {
                Console.WriteLine("med_name \t     total_med     \t price_per");
                for (int i = 0; i < stockcounter; i++)
                {
                    Console.WriteLine("{0}     {1}     {2}", med_name[i], total_med[i], price_per[i]);
                }
                Console.Write("Enter the name of medicine : ");
                rbillmed[returncounter] = Console.ReadLine();
                Console.Write("Enter the qty of medicine : ");
                rqty[returncounter] = validintloop(0);
                for (int i = 0; i < 10; i++)
                {
                    if (rbillmed[returncounter] == med_name[i])
                    {
                        total_med[i] = total_med[i] + rqty[returncounter];
                        rbill[returncounter] = rqty[returncounter] * price_per[i];
                        break;
                    }
                }
                float dis;
                Console.Write("Enter the dicount :");
                dis = float.Parse(Console.ReadLine());
                rdisc[returncounter] = (rbill[returncounter] * dis) / 100;
                rpay[returncounter] = rbill[returncounter] - rdisc[returncounter];
                Console.Write("The bill is  {0}", rbill[returncounter]);
                Console.Write("The discount  is  {0}", rdisc[returncounter]);
                Console.Write("The bill after discount  is {0}", rpay[returncounter]);
            }
            Console.Write("enter the user name :");
            userscheck = Console.ReadLine();
            Console.Write("enter the user name :");
            passwordcheck = Console.ReadLine();
            if (passwordchecker(userscheck, passwordcheck))
            {
                returncounter = returncounter + 1;
            }
        }
        static void bills()
        {
            string passwordcheck;
            string userscheck;
            int option;
            if (billcounter < billslimit)
            {
                Console.WriteLine("med_name \t\t total_med \t price_per");
                for (int i = 0; i < stockcounter; i++)
                {
                    Console.WriteLine("{0}\t\t{1}\t{2}", med_name[i], total_med[i], price_per[i]);
                }
            }
            Console.Write("Enter the name of medicine : ");
            billmed[billcounter] = Console.ReadLine();
            //               check the qty non zero
            Console.Write("Enter the qty of medicine : ");
            qty[billcounter] = validintloop(0);
            for (int i = 0; i < 10; i++)
            {
                if (medicinecheck(billmed[billcounter], med_name[i]))
                {
                    total_med[i] = total_med[i] - qty[billcounter];
                    bill[billcounter] = qty[billcounter] * price_per[i];
                    break;
                }
            }
            float dis;
            Console.Write("Enter the dicount :");
            dis = float.Parse(Console.ReadLine());
            disc[billcounter] = (bill[billcounter] * dis) / 100;
            pay[billcounter] = bill[billcounter] - disc[billcounter];
            Console.WriteLine("The bill is  {0}", bill[billcounter]);
            Console.WriteLine("The discount  is  {0}", disc[billcounter]);
            Console.WriteLine("The bill after discount  is  {0}", pay[billcounter]);

            Console.Write("Enter the user name :");
            userscheck = Console.ReadLine();
            Console.Write("enter the user password :");
            passwordcheck = Console.ReadLine();

            if (passwordchecker(userscheck, passwordcheck))
            {
                billcounter = billcounter + 1;
            }
        }
        static bool medicinecheck(string n, string x)
        {
            if (n == x)
            {
                return true;
            };
            return false;
        }
        static void billhistory()
        {
            Console.WriteLine("*************************************************************************************");
            Console.WriteLine("                             Bill history                                            ");
            Console.WriteLine("*************************************************************************************");
            Console.WriteLine("bill no.     medicine      disc    pay   biller");
            Console.WriteLine("");
            Console.WriteLine("");
            for (int i = 0; i < billcounter; i++)
            {
                Console.WriteLine("{0}          {1}            {2}           {3}         {4}", (i + 1), billmed[i], disc[i], pay[i], biller[i]);
            }
        }
        //                MANAGEMENT MENU
        static int billmanagement()
        {
            mainheader();
            Console.WriteLine("");
            Console.WriteLine("                         BILL MANAGEMENT");
            Console.WriteLine("  CHOOSE ONE OF THE OPTION :");
            Console.WriteLine("1: MAKE A BILL FOR COUSTMER :");
            Console.WriteLine("2: BILL HISTORY :");
            Console.WriteLine("3: RETURN OF A BILL :");
            Console.WriteLine("4: Exit to the BILL MANAGEMENT :");
            option = int.Parse(Console.ReadLine());
            return option;
        }
        static int usermenu()
        {
            Console.WriteLine("-------------------USER Management------------------");
            Console.WriteLine("1: Add user name and password.");
            Console.WriteLine("2: Delete User profile.");
            Console.WriteLine("Enter th option(1/2) : ");
            option = int.Parse(Console.ReadLine());
            return option;
        }
        static int workermenu()
        {

            Console.WriteLine("");
            Console.WriteLine("  CHOOSE ONE OF THE OPTION :");
            Console.WriteLine("1: HIRING STAFF :");
            Console.WriteLine("2: GENERATE WORKER LIST IN ORDER :");
            Console.WriteLine("3: EXIT THE PROGRAM ");
            option = int.Parse(Console.ReadLine());
            return option;
        }
        static int stockmenu()
        {
            mainheader();
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("**********************Stock value**************************");
            Console.WriteLine("___________________________________________________________");
            Console.WriteLine("1: Add stock.");
            Console.WriteLine("2: Need of stock.");
            Console.WriteLine("3: FOR GO IN MAIN MENU.");
            option = int.Parse(Console.ReadLine());
            return option;
        }
        static int userloginmenu()
        {
            Console.Clear();
            mainheader();
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("*******************************************************************");
            Console.WriteLine("Your option is :");
            Console.WriteLine("1: bill management ");
            Console.WriteLine("2: Personal status ");
            option = int.Parse(Console.ReadLine());
            return option;
        }

        //                 User Management
        static Maccounts getInfoOfAccount()
        {
            Maccounts newAccount = new Maccounts();
            while (true)
            {
                Console.Write("Enter the name of the new user Name :");
                newAccount.userName = Console.ReadLine();
                while (true)
                {
                    Console.Write("Enter the name of the new user password  include(small letters and special character( ! / @ / # ) ) :");
                    newAccount.userPassword = Console.ReadLine();
                    if (passwordvalidation(newAccount.userPassword))
                    {
                        break;
                    }
                }
                Console.Write("Enter the name of the new role :");
                newAccount.userRole = Console.ReadLine();
                if (IsAnotherAcountExist(newAccount))
                {
                    break;
                }
            }
            store(workersList, usersList);
            return newAccount;
        }

        static bool IsAnotherAcountExist(Maccounts newAccount)
        {
            foreach (var i in usersList)
            {
                if (i.userName == newAccount.userName)
                {
                    return false;
                }
            }
            return true;
        }
        static int delUsernumber()
        {
            int del=0;
            while (true)
            {
                Console.WriteLine("Sr. no          username      user password");
                for (int i = 0; i < usersList.Count; i++)
                {
                    Console.Write("{0}    :", i + 1);
                    usersList[i].displayAccounts();
                }
                Console.Write("Enter the no. you want to delete user :");
                del = int.Parse(Console.ReadLine());
                del = del - 1;
                if (del< usersList.Count)
                {
                    return del;
                }
            }
            return del;
        }
//              commas read function
static string getstringfield(string record, int field)
{
    int commas = 1;
    string read = "";
    char coma = ',';
    for (int i = 0; i < record.Length; i++)
    {
        if (record[i] == coma)
        {
            commas = commas + 1;
        }
        else if (commas == field)
        {
            read = read + record[i];
        }
    }
    return read;
}

        //              File handling
        static void store(List<worker> arr, List<Maccounts>)
        {
            StreamWriter file = new StreamWriter("D:\\2nd Semester (C#)\\pharmacy mangment system\\record.txt");
            for (int i = 0; i < workersList.Count; i++)
            {
                file.WriteLine(arr[i].salary+ ","+ arr[i].person_name+ ","+ arr[i].age+","+ arr[i].religion+","+ arr[i].post);
            }
            file.WriteLine("Turns");
            for (int i = 0; i < billcounter; i++)
            {
                file.WriteLine(qty[i] + "," + billmed[i] + "," + bill[i] + "," + disc[i] + "," +pay[i] + "," + biller[i]);
            }
            file.WriteLine("Turns");
            for (int i = 0; i < stockcounter; i++)
            {
                file.WriteLine(price_per[i] + "," + med_name[i] + "," + total_med[i]);
            }
            file.WriteLine("Turns" );
            foreach (var i in usersList)
            {
                file.WriteLine(i.userPassword + "," + i.userName + "," + i.userRole);
            }
            file.WriteLine("Ends");
            file.Flush();
            file.Close();
        }
        static worker fill_worker_array(string line)
        {
            worker s=new worker();
            s.salary = int.Parse(getstringfield(line, 1));
            s.person_name = getstringfield(line, 2);
            s.age = int.Parse(getstringfield(line, 3));
            s.religion = getstringfield(line, 4);
            s.post = getstringfield(line, 5);
            
            return s;
        }
        static void load()
        {
            string line;
            string path = "D:\\2nd Semester (C#)\\pharmacy mangment system\\record.txt";
            StreamReader file = new StreamReader(path);
            while(true)
            {
                line = file.ReadLine();
                if (line == "" || line == "Turns")
                {
                    break;
                }
                workersList.Add(fill_worker_array(line));
                workers_Count = workers_Count+1;
            }
            for (billcounter = 0; billcounter < 10; billcounter++)
            {
                line = file.ReadLine();
                if (line == "" || line == "Turns")
                {
                    break;
                }
                qty[billcounter] = int.Parse(getstringfield(line, 1));
                billmed[billcounter] = getstringfield(line, 2);
                bill[billcounter] = int.Parse(getstringfield(line, 3));
                disc[billcounter] = int.Parse(getstringfield(line, 4));
                pay[billcounter] = int.Parse(getstringfield(line, 5));
                biller[billcounter] = getstringfield(line, 6);
            }
            Console.WriteLine("Bills read");

            for (stockcounter = 0; stockcounter < 10; stockcounter++)
            {
                line = file.ReadLine();
                if (line == "" || line == "Turns")
                {
                    break;
                }
                price_per[stockcounter] = int.Parse(getstringfield(line, 1));
                med_name[stockcounter] = getstringfield(line, 2);
                total_med[stockcounter] = int.Parse(getstringfield(line, 3));
            }

            for (usercount = 0; usercount < 10; usercount++)
            {
                line = file.ReadLine();
                if (line == "" || line == "Ends")
                {
                    break;
                }
                usersList.Add(PopulateListFromFile(line));
            }
            file.Close();
        }
        static Maccounts PopulateListFromFile(string line)
        {
            Maccounts file = new Maccounts();
            file.userPassword = getstringfield(line,1);
            file.userName = getstringfield(line,2);
            file.userRole = getstringfield(line,3);
            return file;
        }
static bool specialcharacter(string password)
{
    for (int i = 0;i< password.Length; i++)
    {
        if (password[i] == '~' || password[i] == '!' || password[i] == '$')
        {
            return true;
        }
    }
    return false;
}
static bool smallcharacter(string password)
{
    int alphabet;
    for (int i = 0; password[i] != '\0'; i++)
    {
        for (int num = 97; num < 123; num++)
        {
            alphabet = password[i];
            if (alphabet == num)
            {
                return true;
            }
        }
    }
    return false;
}
static bool passwordvalidation(string password)
{
    if (specialcharacter(password) && smallcharacter(password))
    {
        return true;
    }
    return false;
}
static bool greaterthanzero(int num)
{
    if (num >= 0)
    {
        return true;
    }
    return false;
}
static bool notempty(string word)
{
    if (word == " ")
    {
        return false;
    }
    return true;
}
static int validintloop(int limit)
{
    int num;
    while (true)
    {
        num = int.Parse(Console.ReadLine());
        if (num >= limit)
        {
            break;
        }
        else
        {
            Console.WriteLine("~~~~~~~~~~!!!!!!!!    WRONG    !!!!!!!!~~~~~~~~~~~");
            Console.WriteLine("ENter again : ");
        }
    }
    return num;
}
    }
}