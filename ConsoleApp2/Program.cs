using System.Data;
using System.Threading.Tasks.Dataflow;

namespace TEST
{
    class Program
    {
        static void Main()
        {
            DataSet dataSet = new DataSet();

            DataTable Groups = new DataTable();
            Groups.Columns.Add("Id", typeof(int));
            Groups.Columns.Add("Name", typeof(string));
            Groups.PrimaryKey = new[] { Groups.Columns["Id"]! };

            Groups.Rows.Add(0, "Программисты");
            Groups.Rows.Add(1, "Дизайнеры");
            Groups.Rows.Add(2, "Никто не знает кто они");

            DataTable Students = new DataTable();
            Students.Columns.Add("Id", typeof(int));
            Students.Columns.Add("Name", typeof(string));
            Students.Columns.Add("Age", typeof(int));
            Students.Columns.Add("GroupId", typeof(int));
            Students.PrimaryKey = new[] { Students.Columns["Id"]! };

            Students.Rows.Add(0, "Арт", 18, 0);
            Students.Rows.Add(1, "Матвей", 20, 0);
            Students.Rows.Add(2, "Ботвей", 21, 1);
            Students.Rows.Add(3, "Никита", 18, 0);
            Students.Rows.Add(4, "Мыкыта", 99, 2);
            Students.Rows.Add(5, "Катя", 17, 1);
            Students.Rows.Add(6, "Люба", 23, 2);
            Students.Rows.Add(7, "Карен", 17, 0);
            Students.Rows.Add(8, "Вадим", 21, 0);
            Students.Rows.Add(9, "НеВадим", 21, 1);


            dataSet.Tables.Add(Students);
            dataSet.Tables.Add(Groups);

            dataSet.Relations.Add
                (
                    "GroupStudents",
                    Groups.Columns["Id"]!,
                    Students.Columns["GroupId"]!
                );

            Console.WriteLine("Введите имя или айди группы : ");
            string group = Console.ReadLine()!;

            foreach (DataRow row in Students.Rows)
            {
                if  (
                    Convert.ToString(row.GetParentRow("GroupStudents")!["Name"]) == group ||
                    Convert.ToString(row["GroupId"]) == Convert.ToString(group)
                    )
                {
                    Console.WriteLine
                    (
                    $"{row["Id"],-3} | " +
                    $"{row["Name"],-10} | " +
                    $"{row["Age"],-10} | " +
                    $"{row["GroupId"],-2} - " +
                    $"{row.GetParentRow("GroupStudents")!["Name"],-10}"
                    );
                }
            }
        }
    }
}