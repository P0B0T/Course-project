using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсач.Database
{
    public class РемонтКомпьютернойТехникиGetContext : РемонтКомпьютернойТехникиContext
    {
        static РемонтКомпьютернойТехникиGetContext _context;

        public static РемонтКомпьютернойТехникиGetContext GetContext()
        {
            _context ??= new РемонтКомпьютернойТехникиGetContext();

            return _context;

        }
    }
}
