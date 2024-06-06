using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Reservation : Seans
{
    int resrvationNumber;
    public int getNum()
    {
        return resrvationNumber;
    }
    public Reservation(string data, string time, string name, int num) : base(data, time, name)
    {
        resrvationNumber = num;
    }
    public Reservation() : base()
    {
        resrvationNumber = 0;
    }
    public Reservation(Reservation other)
    {
        this.data = other.data;
        this.time = other.time;
        this.name = other.name;
        resrvationNumber = other.resrvationNumber;
    }
    
    public override void Print()
    {
        Console.WriteLine($"Сеанс: {GetName()}, Дата: {GetData()}, Время: {GetTime()}, номер бронирования: {resrvationNumber}");
    }
}
