using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeProject_RetrieveDataFromTextFile_Sort_Search
{
    internal class Program
    {
        //Properties of Student
        class Student
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
        static void Main(string[] args)
        {
            // CreateAndWrite();
            ReadAndSortAndSearchStudentData();
            Console.ReadLine();
        }

        private static void ReadAndSortAndSearchStudentData()
        {
            bool fileExists = File.Exists(@"C:\Users\ASUS\source\repos\PracticeProject-RetrieveDataFromTextFile-Sort-Search\StudentData.txt");

            if (fileExists)
            {
                try
                {
                    string[] lines = File.ReadAllLines(@"C:\Users\ASUS\source\repos\PracticeProject-RetrieveDataFromTextFile-Sort-Search\StudentData.txt");

                    // Parse student data and store it in a list of Student objects
                    List<Student> students = new List<Student>();
                    foreach (string line in lines)
                    {
                        string[] data = line.Split(',');
                        if (data.Length >= 2)
                        {
                            string name = data[0].Trim();
                            int age;
                            if (int.TryParse(data[1].Trim(), out age))
                            {
                                students.Add(new Student { Name = name, Age = age });
                            }
                        }
                    }

                    // Sort students by name
                    students = students.OrderBy(s => s.Name).ToList();

                    // Display sorted student data
                    Console.WriteLine("Sorted Student Data:");
                    foreach (var student in students)
                    {
                        Console.WriteLine($"Name: {student.Name}, Age: {student.Age}");
                    }

                    // Search for a student by name
                    Console.Write("\nEnter student name to search: ");
                    string searchName = Console.ReadLine().Trim();
                    Student foundStudent = students.FirstOrDefault(s => s.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));

                    if (foundStudent != null)
                    {
                        Console.WriteLine($"Student found - Name: {foundStudent.Name}, Age: {foundStudent.Age}");
                    }
                    else
                    {
                        Console.WriteLine("Student not found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("File doesn't exist in the given location");
            }
        }

        private static void CreateAndWrite()
        {
            //creating a txt file by giving location
            FileStream fs = new FileStream(@"C:\Users\ASUS\source\repos\PracticeProject-RetrieveDataFromTextFile-Sort-Search\StudentData.txt", FileMode.Create, FileAccess.Write);
            //creating a obj for writing
            StreamWriter writing = new StreamWriter(fs);
            //lets assign text to fieds with object
            try
            {
                writing.WriteLine("Ram,25");
                writing.WriteLine("chris,29");
                writing.WriteLine("Hamesworth,35");
                writing.WriteLine("Tony,52");
                writing.WriteLine("Stark,125");
                writing.WriteLine("jonsnow,98");
                writing.WriteLine("Cersie lannister,69");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //flush means the buffer data if anything is remaining will be written to the file
                writing.Flush();
                //close and save
                writing.Close();
                writing.Dispose();
                fs.Close();
                fs.Dispose();
            }
        }
    }

}
