using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсач
{
    public class BufferClasses
    {
        public static class Data
        {
            public static int Id;
        }

        public static class DataAutorization
        {
            public static bool Login = false;

            public static string Surname;

            public static string Name;

            public static string Patronymic;

            public static string Right;
        }

        public static class SearchCheck
        {
            public static string Parameter;
        }

        public static class AddEdit
        {
            public static string Or;
        }
    }
}
