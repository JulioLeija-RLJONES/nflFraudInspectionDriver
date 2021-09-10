using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

namespace RLJones.FraudInspectionDriver.Classes
{
    public class ConnectionStringManager
    {
        private readonly string ConnectionName;
        private readonly Configuration AppConfig;
        private Dictionary<string, string> Parameters;
        /// <summary>
        /// Manages connection strings for the tool in the App.config file.
        /// <br/><br/>
        /// Remarks:
        /// <list type="number">
        /// <item><description>Connections preloaded: </description></item>
        /// <list type="bullet">
        /// <item>db_elptest in elpuatsqlserver.database.windows.net</item>
        /// </list>
        /// </list>
        /// </summary>
        /// <param name="connectionName"></param>
        public ConnectionStringManager(string connectionName)
        {
            Console.WriteLine("ConnectionStringMaanger: connectionName = " + connectionName);
            ConnectionName = connectionName;

            AppConfig = ConfigurationManager
                            .OpenExeConfiguration(ConfigurationUserLevel.None);

         
            string connStr = AppConfig.ConnectionStrings
                                      .ConnectionStrings[connectionName]
                                      .ConnectionString;

            Console.WriteLine("connection string: ");
            Console.WriteLine(connStr);

            Parse(connStr);
        }
        /// <summary>
        /// Breaks down connection string and stores parameters in Dictionary
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        private Dictionary<string, string> Parse(string connStr)
        {
            Parameters = new Dictionary<string, string>();

            string[] splittedConnStr = connStr.Split(';');

            foreach (string param in splittedConnStr)
            {
                string[] paramSplit = param.Split('=');

                if (paramSplit.Length == 2)
                    Parameters.Add(paramSplit[0].ToUpper(), paramSplit[1]);
            }
            return Parameters;
        }
        /// <summary>
        /// Gets a connection paremeter value using the parameter name.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns>Connection parameter value.</returns>
        public string GetParameter(string parameterName) 
        {
            parameterName = parameterName.ToUpper();

            if (Parameters.ContainsKey(parameterName))
                return Parameters[parameterName];

            return string.Empty;
        }
        /// <summary>
        /// Modifies a connection parameter stored in the Global Parameters variable.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        public void SetParameter(string parameterName, string value)
        {
            parameterName = parameterName.ToUpper();

            if (!Parameters.ContainsKey(parameterName))
                Parameters.Add(parameterName, value);
            else
                Parameters[parameterName] = value;
        }
        /// <summary>
        /// Removes a parameter from the global Parameters variable using the parameter name.
        /// </summary>
        /// <param name="parameterName"></param>
        public void RemoveParameter(string parameterName)
        {
            parameterName = parameterName.ToUpper();

            if (Parameters.ContainsKey(parameterName))
                Parameters.Remove(parameterName);
        }

        public string GetErrorMessages()
        {
            string errorsFound = string.Empty;

            foreach(KeyValuePair<string, string> kvp in Parameters)
            {
                if (kvp.Value == string.Empty)
                    errorsFound += string.Format("'{0}' cannot be empty.\n", kvp.Key);
            }

            return errorsFound;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Save()
        {
            AppConfig.ConnectionStrings
                     .ConnectionStrings[ConnectionName]
                     .ConnectionString = ToString();

            AppConfig.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        public override string ToString()
        {
            string connectionString = string.Empty;

            foreach(KeyValuePair<string, string> kvp in Parameters)
            {
                connectionString += string.Format("{0}={1};", kvp.Key, kvp.Value);
            }
            return connectionString;
        }
        /// <summary>
        /// Checks if solution is running in debug mode.
        /// </summary>
        /// <returns>True if solution is running in debug mode.</returns>
        public bool isDebugMode()
        {
            if (Debugger.IsAttached)
            {
                // Since there is a debugger attached, assume we are running from the IDE
                return true;
            }
            else
            {
                return false;
                // Assume we aren't running from the IDE
            }
        }

    }
}
