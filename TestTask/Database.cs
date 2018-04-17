namespace TestTask
{
    class Database
    {
        private static DatabaseContext db; // Контекст для работы с базой

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
