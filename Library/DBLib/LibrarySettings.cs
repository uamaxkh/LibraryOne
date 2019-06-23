using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBLib
{
    public static class LibrarySettings
    {
        public static double getFinePerDay()
        {
            return 25;
        }
        public static int getDaysForTaking()
        {
            return 10;
        }
    }
}