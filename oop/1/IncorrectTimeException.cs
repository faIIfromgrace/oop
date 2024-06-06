using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class IncorrectTimeException : Exception
{
    public IncorrectTimeException() : base("Неверно введено время") { }
    public IncorrectTimeException(string message) : base($"Неверно введено время: {message}") { }
}
