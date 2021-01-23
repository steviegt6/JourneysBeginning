using MonoMod.Cil;

namespace JourneysBeginning.Common.Utilities
{
    public static class ILHelper
    {
        /// <summary>
        /// Creates an <see cref="ILCursor"/> with an <see cref="ILContext"/>.
        /// </summary>
        public static void CreateCursor(this ILContext context, out ILCursor c) => c = new ILCursor(context);

        internal static void LogILError(string opcode, string toMatch) => JourneysBeginning.ModLogger.Error($"[IL] Unable to match \"{opcode}\" \"{toMatch}\"!");

        internal static void LogILError(string opcode, string toMatch, int index) => JourneysBeginning.ModLogger.Error($"[IL] Unable to match \"{opcode}\" \"{toMatch}\" ({index - 1})!");

        internal static void LogILCompletion(string methodName) => JourneysBeginning.ModLogger.Debug($"[IL] Successfuly patched {methodName}!");
    }
}