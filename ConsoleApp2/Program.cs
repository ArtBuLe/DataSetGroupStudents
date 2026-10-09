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

            Groups.Rows.Add(0, "MonsterHunter");
            Groups.Rows.Add(1, "EldenRing");
            Groups.Rows.Add(2, "Sekiro");

            DataTable Students = new DataTable();
            Students.Columns.Add("Id", typeof(int));
            Students.Columns.Add("Name", typeof(string));
            Students.Columns.Add("Age", typeof(int));
            Students.Columns.Add("GroupId", typeof(int));
            Students.PrimaryKey = new[] { Students.Columns["Id"]! };

            Students.Rows.Add(0, "Art", 18, 0);
            Students.Rows.Add(1, "Gleb", 20, 1);
            Students.Rows.Add(2, "Anton", 21, 2);


            dataSet.Tables.Add(Students);
            dataSet.Tables.Add(Groups);

            dataSet.Relations.Add
                (
                    "GroupStudents",
                    Groups.Columns["Id"]!,
                    Students.Columns["GroupId"]!
                );

            foreach (DataRow row in Students.Rows)
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