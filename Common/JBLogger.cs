using log4net.Core;
using System;

namespace JourneysBeginning.Common
{
    public static class JBLogger
    {
        public static Level Undefined;

        public static void Load()
        {
            Log("Loading JBLogger...");

            Undefined = new Level(Level.Info.Value, "UNDEFINED");

            Log("Loaded JBLogger!");
        }

        public static void Unload()
        {
            Log("Unloading JBLogger...");

            Undefined = null;

            Log("Unloaded JBLogger!");
        }

        /// <summary>
        /// Quick method for logging.
        /// </summary>
        /// <param name="message">The message you want to be logged.</param>
        /// <param name="logLevel">The level of logging (<see cref="Level.Debug"/>, <see cref="Level.Info"/>, <see cref="Level.Warn"/>, <see cref="Level.Error"/>, etc.). Defaults to <see cref="Level.Info"/></param>.
        /// <param name="exception">The exception if one was thrown. Defaults to null.</param>
        /// <param name="mod">The mod that's doing the logging. Defaults to <see cref="JourneysBeginning"/></param>.
        public static void Log(object message, Level logLevel = default, Exception exception = null, Type mod = null) => JourneysBeginning.Instance.Logger.Logger.Log(mod ?? typeof(JourneysBeginning), logLevel == default ? Level.Info : logLevel, message, exception);
    }
}