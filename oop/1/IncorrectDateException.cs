using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class IncorrectDateException : Exception
{
    public IncorrectDateException() : base("Неверно введена дата") { }
    public IncorrectDateException(string message) : base($"Неверно введена дата: {message}") { }
}
