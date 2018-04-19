using System;

namespace IPROJ.Contracts.Helpers
{
    public static class Argument
    {
        public static void OfWichValueShoulBeProvided(object argument, string nameof)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(nameof);
            }
        }
    }
}
