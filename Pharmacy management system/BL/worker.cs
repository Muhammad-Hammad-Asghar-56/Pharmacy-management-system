using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_management_system.BL
{
    class worker
    {
        public int salary;
        public int age;
        public string person_name;
        public string religion;
        public string post;

        public worker(int salary, int age, string person_name, string religion, string post)
        {
            this.salary = IsValidLimit(salary, 0);
            this.person_name = person_name;
            this.religion = religion;
            this.post = post;
            this.age = IsValidLimit(age, 18);
        }
        public worker()
        {
            salary = 0;
            age = 0;
            person_name = "";
            religion = "";
            post = " ";
        }
        public int IsValidLimit(int num, int limit)
        {
            while (true)
            {
                if (num < limit)
                {
                    break;
                }
                num = int.Parse(Console.ReadLine());
            }
            return num;
        }
        public void dispalyRecord()
        {
            Console.WriteLine("{0}   {1}   {2}", person_name,salary,post);
        }
    }
}
