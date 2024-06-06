using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class EmptyListException:Exception
{
    public EmptyListException() : base("Список пуст"){ }
    public EmptyListException(string message) : base($"Список пуст {message}") { }
}
