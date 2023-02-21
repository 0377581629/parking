using System;

namespace SyncDataModels
{
    public class ClientInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class Tenancy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Describe { get; set; }
    }
}