using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static Resturants[] database = new Resturants[]
           {
               // I added the below line for testing other states
               //new Resturants{Id= 1,ResturantName = "C",Rate = "4",ServeRegularMeals=10, ServeGlutenFree=12, ServeNutFree=2, ServeVegetarian=4},

               new Resturants{Id= 1,ResturantName = "A",Rate = "5",ServeRegularMeals=36, ServeGlutenFree=0, ServeNutFree=0, ServeVegetarian=4},
                new Resturants{Id= 2,ResturantName = "B",Rate = "3",ServeRegularMeals=60, ServeGlutenFree=0, ServeNutFree=20, ServeVegetarian=20},
           };
        static void Main(string[] args)
        {
            try
            {

                int totalmealnum = 0, vegetriannum = 0, glutenfreenum = 0, nutfreenum = 0;

                Console.WriteLine("How many meals do you need in total?\r");

                Console.WriteLine("Type a number, and then press Enter");
                totalmealnum = Convert.ToInt32(Console.ReadLine());


                Console.WriteLine("------------------------\n");

                Console.WriteLine("How many vegetrian meals?\r");
                Console.WriteLine("Type a number, and then press Enter");
                vegetriannum = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("------------------------\n");

                Console.WriteLine("How many gluten free meals?\r");
                Console.WriteLine("Type a number, and then press Enter");
                glutenfreenum = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("------------------------\n");

                Console.WriteLine("How many nut free meals?\r");
                Console.WriteLine("Type a number, and then press Enter");
                nutfreenum = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("------------------------\n");

                Console.WriteLine("Expected meal orders:\r");

                int regularmeals = totalmealnum - (vegetriannum + glutenfreenum + nutfreenum);
                FindResturants(regularmeals, vegetriannum, glutenfreenum, nutfreenum);

                Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("Press enter to close...");
                Console.ReadLine();
            }
        }

        private static void FindResturants(int totalorder, int vegetarianorder, int nutfreeorder, int glutenfreeorder)
        {
            // I used these variable for the loop to recognize how many meals remain that should be ordered from the next resturant
            int totalorderprepare = totalorder, vegetarianorderprepare = vegetarianorder, nutfreeorderprepare = nutfreeorder,
                glutenfreeorderprepare = glutenfreeorder;

            // I needed these variables to understand when the team meals request are equal with the numbers has been ordered
            int totalresult = 0, vegetarianresult = 0, nutfreeresult = 0, glutenfreeresult = 0;

            // This is for the result
            StringBuilder result = new StringBuilder();

            var data = (from Resturants s in database
                        orderby s.Rate descending
                        select s);

            foreach (var query in data)
            {

                result.Append("Resturant " + query.ResturantName + ":");

                if (query.ServeVegetarian > 0 && vegetarianresult != vegetarianorder)
                {
                    if (query.ServeVegetarian > vegetarianorderprepare)
                    {
                        vegetarianresult += vegetarianorderprepare;
                        result.Append(vegetarianorderprepare + " vegetarian ");
                    }
                    else
                    {
                        vegetarianresult += query.ServeVegetarian;
                        result.Append(query.ServeVegetarian + " vegetarian ");
                        vegetarianorderprepare = vegetarianorder - query.ServeVegetarian;
                    }
                }

                if (query.ServeNutFree > 0 && nutfreeresult != nutfreeorder)
                {
                    if (query.ServeNutFree > nutfreeorderprepare)
                    {
                        nutfreeresult += nutfreeorderprepare;
                        result.Append(nutfreeorderprepare + " Nut free ");
                    }
                    else
                    {
                        nutfreeresult += query.ServeNutFree;
                        result.Append(query.ServeNutFree + " Nut free ");
                        nutfreeorderprepare = nutfreeorder - query.ServeNutFree;
                    }
                }

                if (query.ServeGlutenFree > 0 && glutenfreeresult != glutenfreeorder)
                {
                    if (query.ServeGlutenFree > glutenfreeorderprepare)
                    {
                        glutenfreeresult += glutenfreeorder;
                        result.Append(glutenfreeorder + " Gluten free ");
                    }
                    else
                    {
                        glutenfreeresult += query.ServeGlutenFree;
                        result.Append(query.ServeGlutenFree + " Gluten free ");
                        glutenfreeorderprepare = glutenfreeorder - query.ServeGlutenFree;
                    }
                }

                if (query.ServeRegularMeals > 0 && totalresult != totalorder)
                {
                    if (query.ServeRegularMeals > totalorderprepare)
                    {
                        totalresult += totalorderprepare;
                        result.Append(totalorderprepare + " others \n");
                    }
                    else
                    {
                        totalresult += query.ServeRegularMeals;
                        result.Append(query.ServeRegularMeals + " others \n");
                        totalorderprepare = totalorder - query.ServeRegularMeals;
                    }
                }

                if (vegetarianresult != vegetarianorder && nutfreeresult != nutfreeorder && glutenfreeresult != glutenfreeorder
                    && totalresult != totalorder)
                    break;
            }

            Console.WriteLine(result);

        }

        public static string DumpDataTable(DataTable table)
        {
            string data = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (null != table && null != table.Rows)
            {
                foreach (DataRow dataRow in table.Rows)
                {
                    foreach (var item in dataRow.ItemArray)
                    {
                        sb.Append(item);
                        sb.Append(',');
                    }
                    sb.AppendLine();
                }

                data = sb.ToString();
            }
            return data;
        }
    }

    public class Resturants
    {
        private int _Id, _ServeRegularMeals, _ServeGlutenFree,
            _ServeNutFree, _ServeVegetarian;
        private string _ResturantName, _Rate;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string ResturantName
        {
            get { return _ResturantName; }
            set { _ResturantName = value; }
        }

        public string Rate
        {
            get { return _Rate; }
            set { _Rate = value; }
        }

        public int ServeRegularMeals
        {
            get { return _ServeRegularMeals; }
            set { _ServeRegularMeals = value; }
        }

        public int ServeGlutenFree
        {
            get { return _ServeGlutenFree; }
            set { _ServeGlutenFree = value; }
        }

        public int ServeNutFree
        {
            get { return _ServeNutFree; }
            set { _ServeNutFree = value; }
        }
        public int ServeVegetarian
        {
            get { return _ServeVegetarian; }
            set { _ServeVegetarian = value; }
        }
    }
}
