using UnityEngine;

namespace Moein.Log
{
    public class Constants
    {
        public static string Tittle = "Moein Logger ";
        public static string VersionNumber = "(v0.1.4)";
        public static Color defaultColor = Color.black;

        public static string TiitleText()
        {
            return Tittle + " " + VersionNumber;
        }
    }
}