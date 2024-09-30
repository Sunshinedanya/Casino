namespace Casino.DB
{
    public static class DB
    {
        public static CasinoDBEntities Current = new CasinoDBEntities();
        public static User LoggedUser;
    }
}