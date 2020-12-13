using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            var otdelSnab = new Department("Отдел снабжения");
            var otdelKadr = new Department("Отдел кадров");
            var otdelKach = new Department("Отдел качества");
            var otdelDisp = new Department("Диспетчерский отдел");
            var otdelUrid = new Department("Юридический отдел");
            var otdelIT = new Department("Отдел Разработки");

            List<Department> departments = new List<Department>() { otdelSnab, otdelKadr, otdelKach, otdelDisp, otdelUrid, otdelIT };

            var p1 = new Employee("Ширшов", otdelIT);
            var p2 = new Employee("Артемьев", otdelSnab);
            var p3 = new Employee("Иванов", otdelKach);
            var p4 = new Employee("Смыслов", otdelDisp);
            var p5 = new Employee("Апельсинкина", otdelUrid);
            var p6 = new Employee("Хижняков", otdelIT);
            var p7 = new Employee("Савельев", otdelSnab);
            var p8 = new Employee("Астафьев", otdelIT);
            var p9 = new Employee("Полушкин", otdelDisp);
            var p10 = new Employee("Рябкин", otdelKadr);
            var p11 = new Employee("Абрикосова", otdelUrid);

            List<Employee> employees = new List<Employee>() { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11 };

            Console.WriteLine("Выведите список всех сотрудников и отделов, отсортированный по отделам:");
            var orderedEmployees = employees.OrderBy(i => i.departmentID);
            foreach (var selectedEmployee in orderedEmployees)
            {
                Console.WriteLine(departments.Where(selectedDepartment => selectedDepartment.ID == selectedEmployee.departmentID).First() + " - " + selectedEmployee.surname);
            }

            Console.WriteLine("--------------------------------------------------------");

            Console.WriteLine("Cписок всех сотрудников, у которых фамилия начинается с буквы А:");
            var employeesWithFirstLetterA = from neededEmployee in employees
                                            where neededEmployee.surname.StartsWith("А")
                                            select neededEmployee;
            foreach (var selectedEmployee in employeesWithFirstLetterA)
            {
                Console.WriteLine(selectedEmployee.surname);
            }

            Console.WriteLine("--------------------------------------------------------");

            foreach (var departmentWithEmployees in departments)
            {
                var numberOfEmployeesInDepartment = employees.Where(neededEmployee => neededEmployee.departmentID == departmentWithEmployees.ID).Count();
                Console.WriteLine(departmentWithEmployees.name + " Количество сотрудников:" + numberOfEmployeesInDepartment);
            }

            Console.WriteLine("--------------------------------------------------------");

            foreach (var selectedDepartment in departments)
            {
                var departmentWithEmployees = from neededEmployees in employees
                                               where selectedDepartment.ID == neededEmployees.departmentID
                                               select neededEmployees;
                if (departmentWithEmployees.All(selectedEmployee => selectedEmployee.surname.StartsWith("А")))
                {
                    Console.WriteLine(selectedDepartment.name);
                }
            }

            Console.WriteLine("--------------------------------------------------------");

            foreach (var selectedDepartment in departments)
            {
                var departmentWithEmployees = from neededEmployees in employees
                                              where selectedDepartment.ID == neededEmployees.departmentID
                                              select neededEmployees;
                
                if (departmentWithEmployees.Any(selectedEmployee => selectedEmployee.surname.StartsWith("А")))
                {
                    Console.WriteLine(selectedDepartment.name);
                }
            }

            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("MANY TO MANY PART");
            Console.WriteLine("--------------------------------------------------------");

            List<EmployeesInDepartments> manyToManyList = new List<EmployeesInDepartments>()
            {
                new EmployeesInDepartments(p1.ID, otdelDisp.ID),
                new EmployeesInDepartments(p1.ID, otdelIT.ID),
                new EmployeesInDepartments(p2.ID, otdelKach.ID),
                new EmployeesInDepartments(p2.ID, otdelKadr.ID),
                new EmployeesInDepartments(p3.ID, otdelSnab.ID),
                new EmployeesInDepartments(p3.ID, otdelUrid.ID),
                new EmployeesInDepartments(p4.ID, otdelDisp.ID),
                new EmployeesInDepartments(p4.ID, otdelUrid.ID),
                new EmployeesInDepartments(p5.ID, otdelIT.ID),
                new EmployeesInDepartments(p5.ID, otdelSnab.ID),
                new EmployeesInDepartments(p6.ID, otdelKach.ID),
                new EmployeesInDepartments(p6.ID, otdelKadr.ID),
                new EmployeesInDepartments(p7.ID, otdelUrid.ID),
                new EmployeesInDepartments(p8.ID, otdelIT.ID),
                new EmployeesInDepartments(p9.ID, otdelIT.ID),
                new EmployeesInDepartments(p10.ID, otdelSnab.ID),
            };


            var employeesWithConnection = manyToManyList.Join(
                employees,
                connection => connection.employeeID,
                employee => employee.ID,
                (connection, employee) => new { Surname = employee.surname, DepID = connection.departmentID });

            var resultCollection = departments.GroupJoin(
                employeesWithConnection,
                department => department.ID,
                connectionCollection => connectionCollection.DepID,
                (dep, conCol) => new {depName = dep.name, Surnames = conCol.Select(collection => collection.Surname) });

            foreach (var departmentCollection in resultCollection)
            {
                Console.WriteLine("-");
                Console.WriteLine(departmentCollection.depName);
                foreach(var empSurname in departmentCollection.Surnames)
                {
                    Console.WriteLine(empSurname);
                }
            }

            Console.WriteLine("--------------------------------------------------------");
            foreach (var departmentCollection in resultCollection)
            {
                Console.WriteLine("-");
                var numberOfEmployeesInDepartment = (departmentCollection.Surnames.Count());
                Console.WriteLine(departmentCollection.depName +" Количество сотрудников:"+ numberOfEmployeesInDepartment);
                
            }
            Console.ReadKey();
        }
    }
}
