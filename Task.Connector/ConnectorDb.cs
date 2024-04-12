using Task.Integration.Data.DbCommon;
using Task.Integration.Data.DbCommon.DbModels;
using Task.Integration.Data.Models;
using Task.Integration.Data.Models.Models;

namespace Task.Connector
{
    public class ConnectorDb : IConnector
    {
        private string _provider;
        private DbContextFactory _contextFactory;
        public ILogger Logger { get; set; }


        // ConnectionString provided
        public ConnectorDb(string provider, ILogger logger)
        {   
            _provider = provider;
            Logger = logger;
        }
        
        public ConnectorDb() : this(
            provider: DefaultConnectorConfiguration.DefaultProvider,
            logger: DefaultConnectorConfiguration.DefaultLogger) { }


        private DbContextFactory SetupContextFactory(string connectionString) 
        {
            if (_contextFactory != null) return _contextFactory;
            return _contextFactory = new DbContextFactory(connectionString);
        }

        private DataContext GetContext()
        {
            return _contextFactory.GetContext(_provider);
        }

        private void GetSchema()
        {
            using (var db = GetContext())
            {
                var userEntity = db.Model.FindEntityType(typeof(User));
                if (userEntity == null)
                {
                    Logger.Error("");
                    throw new InvalidOperationException();
                }


                var list = new List<object>
                {
                    userEntity.GetProperties().First().GetColumnName,
                    userEntity.FindPrimaryKey().Properties,
                    userEntity.GetDeclaredProperties(),
                    userEntity.GetForeignKeyProperties(),
                    userEntity.GetDeclaredServiceProperties(),
                    userEntity.GetValueGeneratingProperties(),
                    userEntity.GetServiceProperties(),

                };
                
              

            }
        }

        public void StartUp(string connectionString = DefaultConnectorConfiguration.DefaultConnectionString)
        {
            SetupContextFactory(connectionString);
            Logger.Debug(String.Format("Starting up DBConnector provider={0} connectionString={1}", _provider, connectionString));

            GetSchema();
        }

        

        public void CreateUser(UserToCreate user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Property> GetAllProperties()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserProperty> GetUserProperties(string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool IsUserExists(string userLogin)
        {
            using(var db = GetContext())
            {
                return db.Users.Where(user =>  user.Login == userLogin).Any();
            }
        }

        public void UpdateUserProperties(IEnumerable<UserProperty> properties, string userLogin)
        {
            using (var db = GetContext())
            {
                var user = db.Users.Where(user=>user.Login == userLogin).FirstOrDefault();
                if (user == null) {
                    Logger.Warn(String.Format("User login={0} was not found. Properties are not updated"));
                    return;
                }

                foreach (var property in properties)
                {
                    // SetProperty(user, property, db);
                }
            }
        }

        
        public IEnumerable<Permission> GetAllPermissions()
        {
            throw new NotImplementedException();
        }

        public void AddUserPermissions(string userLogin, IEnumerable<string> rightIds)
        {
            throw new NotImplementedException();
        }

        public void RemoveUserPermissions(string userLogin, IEnumerable<string> rightIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetUserPermissions(string userLogin)
        {
            throw new NotImplementedException();
        }

    }
}