/*  
 Copyright © 2009 Project Mercury Team Members (http://mpe.codeplex.com/People/ProjectPeople.aspx)

 This program is licensed under the Microsoft Permissive License (Ms-PL).  You should 
 have received a copy of the license along with the source code.  If not, an online copy
 of the license can be found at http://mpe.codeplex.com/license.
*/

namespace ProjectMercury
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    public class InvalidEnumArgumentException : ArgumentException
    {
        public InvalidEnumArgumentException() : base() { }

        public InvalidEnumArgumentException(string message) : base(message) { }

        public InvalidEnumArgumentException(string message, Exception innerException) : base(message, innerException) { }

        public InvalidEnumArgumentException(string argumentName, int invalidValue, Type enumClass)
            : base(String.Format("The value of argument '{0}' ({1}) is invalid for Enum type '{2}'.", argumentName, invalidValue.ToString(CultureInfo.CurrentCulture), enumClass.Name), argumentName) { }
    }
}