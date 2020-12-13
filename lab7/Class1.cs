using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    class Employee
    {
        private static int count = 0;
        public int ID { get; private set; }
        public string surname { get; private set;}
        public int departmentID { get; private set; }
        public Employee(string surname, Department department)
        {
            this.surname = surname;
            departmentID = department.ID;
            count++;
            ID = count;
        }
        public override string ToString()
        {
            return surname;
        }
    }

    class Department
    {
        private static int count = 0;
        public string name;
        public int ID { get; private set; }

        public Department(string name)
        {
            this.name = name;
            count++;
            ID = count;
        }
        public override string ToString()
        {
            return name;
        }
    }

    class EmployeesInDepartments
    {
        public int departmentID { get; private set; }
        public int employeeID { get; private set; }

        public EmployeesInDepartments(int employeeID, int departmentID)
        {
            this.employeeID = employeeID;
            this.departmentID = departmentID;
        }

    }
}
