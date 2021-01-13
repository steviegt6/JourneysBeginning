using JourneysBeginning.Content.Configs;
using log4net;
using log4net.Core;
using System;

namespace JourneysBeginning.Common.Utilities
{
    public static class ExtensionMethods
    {
        #region log4net

        public static void Verbose(this ILog logger, string message)
        {
            if (ClientSidePersonalConfig.Instance.VerboseLogging)
                logger.Logger.Log(LogManager.GetLogger(JourneysBeginning.Instance.Name).GetType(), Level.Verbose, message, null);
        }

        public static void Verbose(this ILog logger, string message, Exception exception)
        {
            if (ClientSidePersonalConfig.Instance.VerboseLogging)
                logger.Logger.Log(LogManager.GetLogger(JourneysBeginning.Instance.Name).GetType(), Level.Verbose, message, exception);
        }

        #endregion log4net
    }
}