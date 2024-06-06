using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PaidTickets : Seans
{
    public enum PaymentMetods
    {
        картой,
        наличные
    }
    PaymentMetods payMetod;
    public PaymentMetods getPayMetod()
    {
        return payMetod;
    }
    public PaidTickets(string data, string time, string name, PaymentMetods payMetod) : base(data, time, name)
    {
        this.payMetod = payMetod;
    }
    public PaidTickets() : base()
    {
        payMetod = PaymentMetods.наличные;
    }
    public PaidTickets(PaidTickets other)
    {
        this.data = other.data;
        this.time = other.time;
        this.name = other.name;
        payMetod = other.payMetod;
    }
    public override void Print()
    {
        Console.WriteLine($"Сеанс: {GetName()}, Дата: {GetData()}, Время: {GetTime()}, тип оплаты: {payMetod.ToString()}");
    }
}
