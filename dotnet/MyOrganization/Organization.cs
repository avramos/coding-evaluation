using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;

        public Organization()
        {
            root = CreateOrganization();
        }

        protected abstract Position CreateOrganization();

        /**
         * hire the given person as an employee in the position that has that title
         * 
         * @param person
         * @param title
         * @return the newly filled position or empty if no position has that title
         */
        public Position? Hire(Name person, string title)
        {
            if (person == null || string.IsNullOrEmpty(title)) return null;

            Employee potentialNewEmp = new Employee(person.GetHashCode(), person);
            
            return FindPositionAndFill(root, potentialNewEmp, title);
        }

        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");

            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "\t"));
            }

            return sb.ToString();
        }

        private Position? FindPositionAndFill(Position position, Employee potentialEmployee, string title)
        { 
            if (position == null || potentialEmployee == null) return null;

            if (position.GetTitle() == title && position.GetEmployee() == null) 
            { 
                position.SetEmployee(potentialEmployee); 
                return position; 
            }

            // Positional match did not happen; Let's drill down
            foreach (Position p in position.GetDirectReports())
            { 
                FindPositionAndFill(p, potentialEmployee, title);
            }

            return null;
        }
    }
}
