namespace LiveHAPI.IQCare.Infrastructure.Repository
{
   public abstract class BaseRepository
    {
        internal EMRContext Context;
        internal string DbSecurity = "ttwbvXWpqb5WOLfLrBgisw==";
        
        protected BaseRepository(EMRContext context)
        {
            Context = context;
        }

        public string GetSqlDecrptyion()
        {
            return $"Open symmetric key Key_CTC decryption by password=N'{DbSecurity}';";
        }
    }
}
