using log4net;
using log4net.Core;
using System;
using System.Reflection;
using Terraria.ModLoader;

namespace JourneysBeginning.Common
{
    public static class JBLogger
    {
        /// <summary>
        /// Quick method for logging.
        /// </summary>
        /// <param name="message">The message you want to be logged.</param>
        /// <param name="logLevel">The level of logging (<see cref="Level.Debug"/>, <see cref="Level.Info"/>, <see cref="Level.Warn"/>, <see cref="Level.Error"/>, etc.). Defaults to <see cref="Level.Info"/></param>.
        /// <param name="mod">The mod that's doing the logging. Defaults to <see cref="JourneysBeginning"/></param>.
        public static void Log(object message, Level logLevel = null, Mod mod = null)
        {
            mod = mod ?? JourneysBeginning.Instance;

            if (logLevel == Level.Warn)
                mod.Logger.Warn(message);
            else if (logLevel == Level.Error)
                mod.Logger.Error(message);
            else if (logLevel == Level.Debug)
                mod.Logger.Debug(message);
            else
                mod.Logger.Info(message);
        }
    }
}