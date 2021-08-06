namespace dotnet_webapi.Config 
{
    public class MongoDbConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }

        public string User { get; set; }
        public string Password { get; set; }

        public string ConnectionString 
        { 
            // Read only property
            get
            {
                return $"mongodb://{User}:{Password}@{Host}:{Port}";
            } 
        }
    }
}