using MySql.Data.MySqlClient;

namespace TestTask
{
    class Database
    {
        private static DatabaseContext db;

        public static void ActivateContext()
        {
            db = new DatabaseContext();
        }

        public static DatabaseContext GetContext()
        {
            return db;
        }
    }
}
