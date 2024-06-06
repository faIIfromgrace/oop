using System.Text.RegularExpressions;
using System;

public abstract class Seans:IComparable<Seans>
{
    protected string data;
    protected string time;
    protected string name;
    public string GetData() { return data; }
    public string GetTime() { return time; }
    public string GetName() { return name; }
    public static bool operator ==(Seans s1,Seans s2)
    {
        if (s1 is null || s2 is null) { return false; }
        return s1.data == s2.data && s1.time == s2.time;
    }
    public static bool operator !=(Seans s1, Seans s2)
    {
        return !(s1 == s2);
    }
    public int CompareTo(Seans other)
    {
        if (this == other) { return 0; }
        if (this > other) { return 1; }
        return -1; 
    }
    public DateTime ToDateTime()
    {
        string[] dateParts = data.Split('.');
        string[] timeParts = time.Split(':');
        return new DateTime(
            int.Parse(dateParts[2]),  // Year
            int.Parse(dateParts[1]),  // Month
            int.Parse(dateParts[0]),  // Day
            int.Parse(timeParts[0]),  // Hour
            int.Parse(timeParts[1]),  // Minute
            0                         // Second
        );
    }
    public static bool operator <(Seans s1,Seans s2)
    {
        return s1.ToDateTime() < s2.ToDateTime();
    }
    public static bool operator >(Seans s1, Seans s2)
    {
        return s1.ToDateTime() > s2.ToDateTime();
    }
    public Seans(string data, string time, string name)
    {
        this.data = data;
        this.time = time;
        this.name = name;
    }
    public Seans()
    {
        this.data = "29.05.2024";
        this.time = "16:45";
        this.name = "no name";
        Console.WriteLine($"Был вызван конструктор без параметров для {GetType()}");
    }
    public Seans(Seans other)
    {
        this.data = other.data;
        this.time = other.time;
        this.name = other.name;
    }
    ~Seans()
    {
        Console.WriteLine($"был вызван деструктор {GetType()}");
    }
    public abstract void Print();
    public static Seans CreateSeans()
    {
        Console.WriteLine("Выберите способ создания:\n1) конструктор без параметров\n2) конструктор с параметрами");
        int createMothod;
        while (!int.TryParse(Console.ReadLine(), out createMothod) || (createMothod != 1 && createMothod != 2))
        {
            Console.WriteLine("Неверный ввод, введите целое число от 1 до 2");
        }
        string name = "";
        string data = "";
        string time = "";
        if (createMothod == 2)
        {
            Console.WriteLine("Введите дату: ");
            
            while (true)
            {
                data = Console.ReadLine();
                if (Regex.IsMatch(data, @"^(0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[012])\.(202[4-9]|20[3-9][0-9]|2[1-9][0-9]{2}|[3-9][0-9]{3})$"))
                {
                    break;
                }
                else
                {
                    throw new IncorrectDateException();
                    //Console.WriteLine("Неверный ввод. Введите дату в формате dd.mm.yyyy, где dd не превышает 31, mm не превышает 12, а yyyy 2024+");
                }
            }
            Console.WriteLine("Введите время: ");
            
            while (true)
            {
                time = Console.ReadLine();
                if (Regex.IsMatch(time, @"^([01]?[0-9]|2[0-3]):[0-5][0-9]$"))
                {
                    break;
                }
                else
                {
                    throw new IncorrectTimeException();
                    //Console.WriteLine("Неверный ввод. Введите время в формате xx:xx, где xx не превышает 23, а yy не превышает 59");
                }
            }
            Console.WriteLine("Введите название: ");
            
            while (true)
            {
                name = Console.ReadLine();
                if (Regex.IsMatch(name, @"^[a-zA-Zа-яА-Я\s]*$"))
                {
                    break;
                }
                else
                {
                    throw new ArgumentException("Неверный ввод. Введите название, содержащее только английские и русские символы");
                    //Console.WriteLine("Неверный ввод. Введите название, содержащее только английские и русские символы");
                }
            }
        }
        
        Console.WriteLine("Введите тип сеанса: \n1) бронирование\n 2) оплаченые билеты");
        int seansType;
        while (!int.TryParse(Console.ReadLine(),out seansType)||(seansType !=1 && seansType != 2))
        {
            Console.WriteLine("Неверный ввод, введите целое число от 1 до 2");
        }
        if (seansType == 1)
        {
            if (createMothod == 2)
            {
                Console.WriteLine("Введите номер бронирования: ");
                int reservationNum;
                while (!int.TryParse(Console.ReadLine(), out reservationNum))
                {
                    Console.WriteLine("Неверный ввод, введите целое число");
                }
                return new Reservation(data, time, name, reservationNum);
            }
            else
            {
                return new Reservation();
            }
        }
        else
        {
            if (createMothod == 2)
            {
                Console.WriteLine("Введите способ оплаты: \n1) карта\n 2) наличные");
                int payMetod;
                while (!int.TryParse(Console.ReadLine(), out payMetod) || (payMetod != 1 && payMetod != 2))
                {
                    Console.WriteLine("Неверный ввод, введите целое число от 1 до 2");
                }
                return new PaidTickets(data, time, name, (PaidTickets.PaymentMetods)(payMetod - 1));
            }
            else
            {
                return new PaidTickets();
            }
        }
    }
}
