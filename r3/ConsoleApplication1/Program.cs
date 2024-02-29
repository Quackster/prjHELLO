using System;
using Storage;
using Configuration;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        private static DatabaseManager DatabaseManager;
        private static string host = "localhost";
        private static string user = "root";
        private static string database = "young";
        private static string pw = "abcdef";
        private static uint mySQLport = 3306;
        private static ConfigurationModule mConfig;

        static void Main(string[] args)
        {
            Console.Title = "prjHELLO - Habbo Hotel v1";
            Console.WriteLine("#####################");
            Console.WriteLine("### Project HELLO ###");
            Console.WriteLine("#####################\n");

            try
            {
                mConfig = ConfigurationModule.LoadFromFile("settings.ini");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Failed to load configuration file, exception message was: " + ex.Message);
                return;
            }

            // Initialize database and test a connection by getting & releasing it
            DatabaseServer pDatabaseServer = new DatabaseServer(
                Program.Configuration["db1.server.host"],
                Program.Configuration.TryParseUInt32("db1.server.port"),
                Program.Configuration["db1.server.uid"],
                Program.Configuration["db1.server.pwd"]);

            Database pDatabase = new Database(
                Program.Configuration["db1.name"],
                Program.Configuration.TryParseUInt32("db1.minpoolsize"),
                Program.Configuration.TryParseUInt32("db1.maxpoolsize"));

            DatabaseManager = new DatabaseManager(pDatabaseServer, pDatabase);

            Console.Write("MySQL Status: ");
            if (DatabaseManager.GetClient() == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(false + "\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(true + "\n");
            }
            Console.ResetColor();
            Console.WriteLine("Starting up server...");

            NewSocket n = new NewSocket();
            n.Server();
        }
        public static DatabaseManager GetDatabase()
        {
            return DatabaseManager;
        }
        public static ConfigurationModule Configuration
        {
            get { return mConfig; }
        }
    }
}
