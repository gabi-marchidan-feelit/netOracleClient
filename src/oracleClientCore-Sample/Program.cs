using System;
using System.Data.OracleClient;

namespace SimpleTest
{
    class Program
    {
        static void Main(string[] args)
        {

            string dataSource = "(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=sdev.rgis.com)(PORT=1533))(CONNECT_DATA=(SERVER=dedicated)(SID=sdev)))";
            string userID = "EMPPORTAL";
            string password = "holidays17";
            string connectionString = string.Format("Data Source={0};User ID={1};Password={2}", dataSource, userID, password);

            Console.WriteLine("Start.");

            Console.WriteLine("create OracleConnection object...");
            //https://stackoverflow.com/questions/9218847/how-do-i-handle-database-connections-with-dapper-in-net
            using (System.Data.Common.DbConnection connection = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=sdev.rgis.com)(PORT=1533))(CONNECT_DATA=(SERVER=dedicated)(SID=sdev)));User ID=EMPPORTAL;Password=holidays17"))
            {
                Console.WriteLine("Open connection...");
                connection.Open();

                Console.WriteLine("Create command...");
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM SCHED.RGIS_HR_PERSON where PERSON_ID=1126547";

                    Console.WriteLine("Execute reader...");
                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine("*** User tables (sample): ***");
                        
                        while (reader.Read())
                        {
                            //string tableName = reader.GetString(reader.GetOrdinal("TABLE_NAME"));
                            //string tablespace_name = reader.GetString(reader.GetOrdinal("TABLESPACE_NAME"));
                            //int rows = reader.GetInt32(reader.GetOrdinal("NUM_ROWS"));
                            //Console.WriteLine(tableName + " in tablespace " + tablespace_name + " has " + rows.ToString() + " rows.");
                            var badgeId = reader.GetString(reader.GetOrdinal("BADGE_ID"));
                            var firstName = reader.GetString(reader.GetOrdinal("FIRST_NAME"));
                            var lastName = reader.GetString(reader.GetOrdinal("LAST_NAME"));
                            var nationalId = reader.GetString(reader.GetOrdinal("NATIONAL_IDENTIFIER"));
                            Console.WriteLine(string.Format("badgeId: {0}\nfirstName: {1}\nlastName: {2}\nnationalId: {3}", badgeId, firstName, lastName, nationalId));
                        };
                    }
                    Console.WriteLine("End reader...");
                    Console.WriteLine();
                 
                }
            }
            Console.WriteLine("Done.");
            Console.Read();
        }
    }
}
