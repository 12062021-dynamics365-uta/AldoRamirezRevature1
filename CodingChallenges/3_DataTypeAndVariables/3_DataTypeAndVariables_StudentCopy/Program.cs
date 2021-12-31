using System;

namespace _3_DataTypeAndVariablesChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Print Values
            byte myByte = 255;
            Console.WriteLine($"{myByte} {PrintValues(myByte)}");
            sbyte mySbyte = -128;
            Console.WriteLine($"{mySbyte} {PrintValues(mySbyte)}");
            int myInt = 2147483647;
            Console.WriteLine($"{myInt} {PrintValues(myInt)}");
            uint myUint = 4294967295;
            Console.WriteLine($"{myUint} {PrintValues(myUint)}");
            short myShort = -32768;
            Console.WriteLine($"{myShort} {PrintValues(myShort)}");
            ushort myUShort = 65535;
            Console.WriteLine($"{myUShort} {PrintValues(myUShort)}");
            float myFloat = -31.1289f;
            Console.WriteLine($"{myFloat} {PrintValues(myFloat)}");
            double myDouble = -12.1231250;
            Console.WriteLine($"{myDouble} {PrintValues(myDouble)}");
            char myCharacter = 'A';
            Console.WriteLine($"{myCharacter} {PrintValues(myCharacter)}");
            bool myBool = true;
            Console.WriteLine($"{myBool} {PrintValues(myBool)}");
            string myText = "I control text";
            Console.WriteLine($"{myText} {PrintValues(myText)}");
            string numString = "15";
            Console.WriteLine($"{numString} {PrintValues(numString)}");
            decimal myDecimal = 3.001002003m;
            Console.WriteLine($"{myDecimal} {PrintValues(myDecimal)}");
            long myLong = 9223372036854775807;
            Console.WriteLine($"{myLong} {PrintValues(myLong)}");
            ulong myUlong = 18446744073709551615;
            Console.WriteLine($"{myUlong} {PrintValues(myUlong)}\n");

            string notInt = "I control text";
            string isInt = "20";
            Console.WriteLine($"String To Int: {notInt} = {StringToInt(notInt).ToString()}");
            Console.WriteLine($"String To Int: {isInt} = {StringToInt(isInt)}");
        }

        /// <summary>
        /// This method has an 'object' type parameter. 
        /// 1. Create a switch statement that evaluates for the data type of the parameter
        /// 2. You will need to get the data type of the parameter in the 'case' part of the switch statement
        /// 3. For each data type, return a string of exactly format, "Data type => <type>" 
        /// 4. For example, an 'int' data type will return ,"Data type => int",
        /// 5. A 'ulong' data type will return "Data type => ulong",
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string PrintValues(object obj)
        {
            switch (Type.GetTypeCode(obj.GetType()))
            {
                case TypeCode.Byte:
                    return "Data type => byte";
                case TypeCode.SByte:
                    return "Data type => sbyte";
                case TypeCode.Int32:
                    return "Data type => int";
                case TypeCode.UInt32:
                    return "Data type => uint";
                case TypeCode.Int16:
                    return "Data type => short";
                case TypeCode.UInt16:
                    return "Data type => ushort";
                case TypeCode.Single:
                    return "Data type => float";
                case TypeCode.Double:
                    return "Data type => double";
                case TypeCode.Char:
                    return "Data type => char";
                case TypeCode.Boolean:
                    return "Data type => bool";
                case TypeCode.String:
                    return "Data type => string";
                case TypeCode.Decimal:
                    return "Data type => decimal";
                case TypeCode.Int64:
                    return "Data type => long";
                case TypeCode.UInt64:
                    return "Data type => ulong";
                default:
                    return "Data type => object";
            }
        }

        /// <summary>
        /// THis method has a string parameter.
        /// 1. Use the .TryParse() method of the Int32 class (Int32.TryParse()) to convert the string parameter to an integer. 
        /// 2. You'll need to investigate how .TryParse() and 'out' parameters work to implement the body of StringToInt().
        /// 3. If the string cannot be converted to a integer, return 'null'. 
        /// 4. Investigate how to use '?' to make non-nullable types nullable.
        /// </summary>
        /// <param name="numString"></param>
        /// <returns></returns>
        public static int? StringToInt(string numString)
        {
            bool isInt = Int32.TryParse(numString, out int convertedInt);

            if (isInt)
                return convertedInt;
            else
                return null;
        }
    }// end of class
}// End of Namespace
