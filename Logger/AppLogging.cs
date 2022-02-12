using System;
using System.Data.SqlClient;
using System.Diagnostics;
using log4net;
using log4net.Config;

namespace Logger
{

    /// <summary>
    /// Auteur      : Marius Kossi APALOO (MKA)
    /// Date        : 13.04.2014
    /// Description :
    /// Log des exceptions
    /// </summary>
    public class ApplicationLogger
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ApplicationLogger));
        //private static readonly ILog log = LogManager.GetLogger(typeof(Startup));

        /// <summary>
        /// Enregistrer une exception
        /// </summary>
        /// <param name="exception">Exception interceptée</param>
        public static void LogError(Exception exception)
        {
            string logLine = exception.Message;

            //if (exception.GetType() == typeof(SqlException))
            //{
            //    logLine += string.Format("Serveur = {0} / Source = {1} / Procedure = {2} / NUmber = {3} ", ((SqlException)exception).Server, ((SqlException)exception).Source, ((SqlException)exception).Procedure, ((SqlException)exception).Number);
            //}

            log.Error(logLine, exception);
        }

        public static void LogInformation(string action)
        {
            var ret = log4net.LogManager.GetRepository().Configured;

            log.Info(action);
        }

    }
}
