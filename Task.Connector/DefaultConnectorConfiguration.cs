using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Integration.Data.Models;

namespace Task.Connector
{
    internal class DefaultConnectorConfiguration
    {
        public const string DefaultProvider = "MSSQL";
        public const string DefaultConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Itst1d\\Source\\Repos\\ultraded\\task\\Task.Connector\\Database1.mdf;Integrated Security=True";

        public static readonly ILogger DefaultLogger = new NullLogger();
    }
}
