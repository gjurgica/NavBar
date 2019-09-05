using Classes;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReadingFromCsvFile
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string path = @"C:\Users\Asus\Desktop\Navigation.csv";
                List<NavBarClass> navbar = new List<NavBarClass>();
                using (TextReader reader = File.OpenText(path))
                {
                    CsvReader csv = new CsvReader(reader);
                    csv.Configuration.Delimiter = ";";
                    csv.Configuration.MissingFieldFound = null;
                    while (csv.Read())
                    {
                        NavBarClass Record = csv.GetRecord<NavBarClass>();
                        navbar.Add(Record);
                    }
                }
                List<NavBarClass> newList = new List<NavBarClass>().OrderBy(x => x.MenuName).ToList();
                foreach (var nav in navbar)
                {
                    if (nav.ParentID == "NULL")
                    {
                        newList.Add(Add(nav));
                    }
                }
                foreach (var nav in newList)
                {
                    foreach (var child in navbar)
                    {
                        if (nav.ID == child.ParentID)
                        {
                            nav.Children.Add(Add(child));
                        }
                    }
                }
                foreach (var nav in navbar)
                {
                    foreach (var newNav in newList)
                    {
                        foreach (var child in newNav.Children)
                        {
                            if (child.ID == nav.ParentID)
                            {
                                child.Children.Add(Add(nav));
                            }
                        }
                    }
                }
                foreach (var nav in newList)
                {
                    Console.WriteLine($".{nav.MenuName}");
                    foreach (var child in nav.Children)
                    {
                        if (child.isHidden == "False")
                        {
                            Console.WriteLine($"....{child.MenuName}");
                        }
                        foreach (var item in child.Children)
                        {
                            Console.WriteLine($".......{item.MenuName}");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read");
                Console.WriteLine(e.Message);

            }
            Console.ReadLine();
        }
        public static NavBarClass Add(NavBarClass model)
        {
            return new NavBarClass
            {
                ID = model.ID,
                MenuName = model.MenuName,
                ParentID = model.ParentID,
                isHidden = model.isHidden,
                LinkURL = model.LinkURL,
                Children = new List<NavBarClass>()
            };

        }
    }
}
