using System;
using System.IO;
using System.Security;
using System.Configuration;

//Probleme au avoir un token >> Resolved by reverting the assembly "Microsoft.IdentityModel.Clients.ActiveDirectory" to Version = 3.13.9.1126.


namespace Tool.CIO.CRM.Connect
{
    public class Configuration
    {
        #region Properties
        /// <summary>
        /// The root address of the Dynamics CRM service.
        /// </summary>
        /// <example>https://myorg.crm.dynamics.com</example>
        public string ServiceUrl { get; set; }

        /// <summary>
        /// The redirect address provided when the application was registered in Microsoft Azure
        /// Active Directory or AD FS.
        /// </summary>
        /// <remarks>Required only with a web service configured for OAuth authentication.</remarks>
        /// <seealso cref="https://msdn.microsoft.com/library/dn531010.aspx#bkmk_redirect"/>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// The client ID that was generated when the application was registered in Microsoft Azure
        /// Active Directory or AD FS.
        /// </summary>
        /// <remarks>Required only with a web service configured for OAuth authentication.</remarks>
        public string ClientId { get; set; }

        /// <summary>
        /// The user name of the logged on user or null.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///  The password of the logged on user or null.
        /// </summary>
        public SecureString Password { get; set; }

        /// <summary>
        ///  The domain of the logged on user account or null.
        /// </summary>
        /// <remarks>Required only with a web service configured for Active Directory authentication.</remarks>
        public string Domain { get; set; }

        public string SecretKey { get; set; }

        #endregion Properties

        #region Constructors

        public Configuration() { }

        public Configuration(string Login, string Pwd, string URL, string AzureID, string Redirect, string p_SecretKey)
        {
            Username = Login;
            ServiceUrl = URL;
            ClientId = AzureID;
            RedirectUrl = Redirect;
            Domain = "Olimpico.biz";
            SecretKey = p_SecretKey;

            var password = Pwd;
            Password = new SecureString();
            foreach (char c in password) Password.AppendChar(c);
        }

        #endregion Constructors
    }

    public class FileConfiguration : Configuration
    {
        #region Properties
        public string PathToConfig { get; set; }

        public string Name { get; set; }

        #endregion Properties

        #region Constructors
        public FileConfiguration() : base() { }
        public FileConfiguration(string Login, string Pwd, string URL, string AzureID, string Redirect, string SecretKey) : base(Login, Pwd, URL, AzureID, Redirect, SecretKey) { }
        public FileConfiguration(string name) : base()
        {
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), Environment.GetCommandLineArgs()[0]);

            Load(name, String.Concat(path, ".config"));
        }
        #endregion Constructors

        #region Methods
        public virtual void Load(string connectionName, string path)
        {
            // Check passed parameters.
            if (string.IsNullOrEmpty(path) || !System.IO.File.Exists(path))
                throw new ArgumentException("The specified app.config file path is invalid.", this.ToString());
            else
                PathToConfig = path;

            try
            {
                // Read the app.config file and obtain the app settings.
                System.Configuration.Configuration config = null;
                ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                configFileMap.ExeConfigFilename = PathToConfig;
                config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

                var appSettings = config.AppSettings.Settings;

                // Get the connection string.
                ConnectionStringSettings connection;
                if (string.IsNullOrEmpty(connectionName))
                {
                    // No connection string name specified, so use the first one in the file.
                    connection = config.ConnectionStrings.ConnectionStrings[0];
                    Name = connection.Name;
                }
                else
                {
                    connection = config.ConnectionStrings.ConnectionStrings[connectionName];
                    Name = connectionName;
                }

                // Get the connection string parameter values.
                if (connection != null)
                {
                    var parameters = connection.ConnectionString.Split(';');
                    foreach (string parameter in parameters)
                    {
                        var trimmedParameter = parameter.Trim();
                        if (trimmedParameter.StartsWith("Url="))
                            ServiceUrl = parameter.Replace("Url=", String.Empty).TrimStart(' ');

                        if (trimmedParameter.StartsWith("Username="))
                            Username = parameters[1].Replace("Username=", String.Empty).TrimStart(' ');

                        if (trimmedParameter.StartsWith("Password="))
                        {
                            var password = parameters[2].Replace("Password=", String.Empty).TrimStart(' ');

                            Password = new SecureString();
                            foreach (char c in password) Password.AppendChar(c);
                        }
                        if (trimmedParameter.StartsWith("Domain="))
                            Domain = parameter.Replace("Domain=", String.Empty).TrimStart(' ');
                    }
                }
                else
                    throw new Exception("The specified connection string could not be found.");

                // Get the Azure Active Directory application registration settings.
                RedirectUrl = appSettings["RedirectUrl"]?.Value;
                ClientId = appSettings["ClientId"]?.Value;
            }
            catch (InvalidOperationException e)
            {
                throw new Exception("Required setting in app.config does not exist or is of the wrong type.", e);
            }
        }

        public virtual void Save()
        {
            throw new NotImplementedException();
        }

        public virtual void AddConnection()
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}
