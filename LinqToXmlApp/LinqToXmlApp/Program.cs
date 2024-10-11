using System.Xml;
using System.Xml.Linq;

namespace LinqToXmlApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ReadXmlFile();
            //CreateXmlFile();
           // EditXmlFile();
            DeleteXmlElementFile();
        }

        public static void ReadXmlFile()
        {
            string filePath = "Employee.xml";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath); 

            XmlNodeList employeeList = xmlDoc.GetElementsByTagName("Employee");

            foreach (XmlNode employeeNode in employeeList)
            {
                string name = employeeNode["Name"]!.InnerText;
                string Salary = employeeNode["Salary"]!.InnerText;

                Console.WriteLine($"Name: {name}, Salary: {Salary}"); 
            }
        }

        public static void CreateXmlFile()
        {
            Employees employees = new Employees()
            {
                EmployeeList = new List<Employee>() {
                 new Employee() { Id=1, Name="Emp1", Salary=10 },
                 new Employee() { Id=2, Name="Emp2", Salary=12 },
                 new Employee() { Id=3, Name="Emp3", Salary=85 },
                 new Employee() { Id=4, Name="Emp4", Salary=20 },
                 new Employee() { Id=5, Name="Emp5", Salary=90 } 
               }  
            };

            XElement employeeList = new XElement("Employees");
            foreach (Employee employee in employees.EmployeeList)
            {
                employeeList.Add(new XElement("Employee", new XAttribute("EmpId", employee.Id),
                                        new XElement("Name", employee.Name),
                                        new XElement("Salary", employee.Salary))); 
            }

            XDocument doc = new XDocument(new XDeclaration("1.0", "utf8", "yes"), 
                            new XComment("This is dynamically created list of Employees"), employeeList); 
            doc.Save("DynamicallyEmployeeData.xml");
        }

        public static void EditXmlFile() 
        {
            string filePath = "DynamicallyEmployeeData.xml";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
             
           XmlNode userNode = xmlDoc.SelectSingleNode("/Employees/Employee[@EmpId='3']");

            if (userNode != null)
            {
                userNode["Name"].InnerText = "Jonathan Doe";
                userNode["Salary"].InnerText = "100000";

                xmlDoc.Save(filePath);
            }
        }

        public static void DeleteXmlElementFile()
        {
            string filePath = "DynamicallyEmployeeData.xml";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            XmlNode userNode = xmlDoc.SelectSingleNode("/Employees/Employee[@EmpId='2']");

            if (userNode!=null)
            {
                userNode.ParentNode.RemoveChild(userNode);
                xmlDoc.Save(filePath);
            } 
        }
    }
}
