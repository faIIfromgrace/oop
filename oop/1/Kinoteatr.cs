using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;

class Kinoteatr
{
    private string name;
    private string address;
    private MyList<Seans> seans;
    public static Kinoteatr operator + (Kinoteatr kin, Seans sea)
    {
        kin.seans.Add(sea);
        return kin;
    }
    public static Kinoteatr operator ++(Kinoteatr kin)
    {
        if (kin.seans.Count == 0)
        {
            throw new EmptyListException("Список сеансов в кинотетре пуст.");
            //Console.WriteLine("Список сеансов в кинотетре пуст.");
            //Console.ReadLine();
            //return kin;
        }
        Seans sea = kin[0];
        if (sea is Reservation res)
        {
            kin.seans.Add(new Reservation(res));
        }
        if (sea is PaidTickets PT)
        {
            kin.seans.Add(new PaidTickets(PT));
        }
        return kin;
    }
    public Seans this[int index]
    {
        get
        {
            return this.seans[index];
        }
        set
        {
            seans[index] = value;
        }
    }
    public Kinoteatr()
    {
        Console.WriteLine($"Был вызван конструктор без параметров для кинотеатра");
        name = string.Empty;
        address = string.Empty;
        seans = new MyList<Seans>(10);
    }
    public Kinoteatr(string name, string address)
    {
        Console.WriteLine($"Был вызван конструктор с параметрами для кинотеатра");
        this.name = name;
        this.address = address;
        seans = new MyList<Seans>(10);
    }
    public Kinoteatr(Kinoteatr other)
    {
        Console.WriteLine($"был вызван конструктор копирования для кинотеатра");
        name = other.name;
        address = other.address;
        seans = new MyList<Seans>(10);
    }
    ~Kinoteatr()
    {
        Console.WriteLine($"был вызван деструктор для кинотеатра");
    }
    public string GetName() { return name; }
    public string GetAddress() { return address; }
    public MyList<Seans> GetSeans() { return seans; }
    public static bool AvailabilityOfKinoteatrs(List<Kinoteatr> kinoteatrs)
    {
        if (kinoteatrs.Count == 0)
        {
            throw new EmptyListException("Нет кинотеатров для обработки.");
            //Console.WriteLine("Нет кинотеатров для обработки.");
            //return true;
        }
        return false;
    }
    public static Kinoteatr CreateKinoteatr()
    {
        Console.WriteLine("Как вы хотите создать кинотетр?\n1) конструктор без параметров\n2) заполнить поля кинотеатра\nВедите целое число:");
        int action;
        while (!int.TryParse(Console.ReadLine(), out action) || (action < 0 || action > 3))
        {
            Console.WriteLine("Неверный ввод");
        }
        if (action == 1)
        {
            return new Kinoteatr();
        }
        Console.WriteLine("Введите название кинотеатра: ");
        string name;
        while (true)
        {
            name = Console.ReadLine();
            if (Regex.IsMatch(name, @"^[a-zA-Zа-яА-Я\s]*$"))
            {
                break;
            }
            else
            {
                Console.WriteLine("Неверный ввод. Введите название, содержащее только английские и русские символы");
            }
        }
        string address;
        while (true)
        {
            Console.WriteLine("Пожалуйста, введите адрес в формате 'улица номер улицы':");
            address = Console.ReadLine();
            if (Regex.IsMatch(address, @"^[\p{L}\s]+\s*\d+$"))
            {
                break;
            }
            else
            {
                Console.WriteLine("Неверный ввод. Пожалуйста, введите адрес в формате 'улица номер улицы':");
            }
        }
        return new Kinoteatr(name, address);
    }

    public static Kinoteatr CreateKinoteatr(MyList<Seans> list)
    {
        Kinoteatr kino = CreateKinoteatr();
        kino.seans = list;
        return kino;
    }

    public static void AddKinoteatr(List<Kinoteatr> kinoteatrs)
    {
        Kinoteatr kino = CreateKinoteatr();
        kinoteatrs.Add(kino);
        Console.WriteLine("Кинотеатр успешно добавлен.");
    }
    public static void CopyKinoteatr(List<Kinoteatr> kinoteatrs)
    {
        if (Empty(kinoteatrs))
        {
            return;
        }
        Console.WriteLine("Выберите кинотеатр: ");
        for (int i = 0; i < kinoteatrs.Count; i++)
        {
            Console.WriteLine($"{i + 1}) {kinoteatrs[i].GetName()}");
        }
        int appIndex;
        while (!int.TryParse(Console.ReadLine(), out appIndex) ||
       appIndex < 1 || appIndex > kinoteatrs.Count)
        {
            Console.WriteLine("Неверный ввод. Пожалуйста, введите номер кинотеатра из списка.");
        }
        Kinoteatr copiedKinoteatr = new Kinoteatr(kinoteatrs[appIndex - 1]);
        kinoteatrs.Add(copiedKinoteatr);
    }

    public static void AddSeans(List<Kinoteatr> kinoteatrs)
    {
        if (Empty(kinoteatrs))
        {
            return;
        }
        Console.WriteLine("Выберите кинотеатр, для которого добавляется сеанс: ");
        for (int i = 0; i < kinoteatrs.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {kinoteatrs[i].GetName()}");
        }
        int kinoIndex;
        while (!int.TryParse(Console.ReadLine(), out kinoIndex) ||
               kinoIndex < 1 || kinoIndex > kinoteatrs.Count)
        {
            Console.WriteLine("Неверный ввод.");
        }
        Kinoteatr selectedApp = kinoteatrs[kinoIndex - 1];
        //selectedApp.GetSeans().Add(Seans.CreateSeans());
        selectedApp += Seans.CreateSeans();
        Console.WriteLine("Сеанс успешно добавлен.");
    }

    public static void DeleteKinoteatr(List<Kinoteatr> kinoteatrs)
    {
        if (Empty(kinoteatrs))
        {
            return;
        }
        Console.WriteLine("Выберите кинотеатр: ");
        for (int i = 0; i < kinoteatrs.Count; i++)
        {
            Console.WriteLine($"{i + 1}) {kinoteatrs[i].GetName()}");
        }
        int appIndex;
        while (!int.TryParse(Console.ReadLine(), out appIndex) ||
       appIndex < 1 || appIndex > kinoteatrs.Count)
        {
            Console.WriteLine("Неверный ввод. Пожалуйста, введите номер кинотеатра из списка.");
        }
        kinoteatrs.RemoveAt(appIndex - 1);
        Console.WriteLine("Кинотеатр успешно удален.");
        GC.Collect();

    }

    public static void DeleteSeans(List<Kinoteatr> kinoteatrs)
    {
        if (Empty(kinoteatrs))
        {
            return;
        }
        Console.WriteLine("Выберите кинотеатр, для которого добавляется сеанс: ");
        for (int i = 0; i < kinoteatrs.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {kinoteatrs[i].GetName()}");
        }
        int appIndex;
        while (!int.TryParse(Console.ReadLine(), out appIndex) ||
               appIndex < 1 || appIndex > kinoteatrs.Count)
        {
            Console.WriteLine("Неверный ввод.");
        }
        Kinoteatr selectedKino = kinoteatrs[appIndex - 1];
        if (selectedKino.GetSeans().Count == 0)
        {
            //Console.WriteLine("В выбранном кинотеатре нет сеансов для удаления.");
            throw new EmptyListException("В выбранном кинотеатре нет сеансов для удаления.");
            //return;
        }
        Console.WriteLine("Выберите сеанс для удаления:");
        for (int i = 0; i < selectedKino.GetSeans().Count; i++)
        {
            Console.WriteLine($"{i + 1}.{selectedKino.GetSeans()[i].GetName()}");
        }
        int seansIndex;
        while (!int.TryParse(Console.ReadLine(), out seansIndex) ||
       seansIndex < 1 || seansIndex > selectedKino.GetSeans().Count)
        {
            Console.WriteLine("Неверный ввод. Пожалуйста, введите номер сеанса из списка.");
        }
        selectedKino.GetSeans().RemoveAt(seansIndex - 1);
        Console.WriteLine("Сеанс успешно удален.");
        GC.Collect();
        Console.ReadLine();
    }

    public static void EditKinoteatr(List<Kinoteatr> kinoteatrs)
    {
        if (AvailabilityOfKinoteatrs(kinoteatrs)) return;
        Console.WriteLine("Введите номер кинотеатра в списке для редактирования: ");
        for (int i = 0; i < kinoteatrs.Count; i++)
        {
            Console.WriteLine($"{i + 1}) {kinoteatrs[i].name}");
        }
        int kinoIndex;
        while (!int.TryParse(Console.ReadLine(), out kinoIndex) || kinoIndex < 1 || kinoIndex > kinoteatrs.Count)
        {
            Console.WriteLine("Неверный ввод. Пожалуйста, введите номер кинотеатра из списка.");
        }
        if (kinoteatrs[kinoIndex - 1] == null)
        {
            Console.WriteLine("Кинотеатр не найден.");
            return;
        }
        MyList<Seans> seansList = kinoteatrs[kinoIndex - 1].seans;
        kinoteatrs[kinoIndex - 1] = Kinoteatr.CreateKinoteatr(seansList);
        Console.WriteLine("Отредактировано.");
    }
    public static void EditSeans(List<Kinoteatr> kinoteatrs)
    {
        if (AvailabilityOfKinoteatrs(kinoteatrs)) return;
        Console.WriteLine("Введите номер кинотеатра в котором отредактируете сеанс: ");
        for (int i = 0; i < kinoteatrs.Count; i++)
        {
            Console.WriteLine($"{i + 1}) {kinoteatrs[i].name}");
        }
        int kinoIndex;
        while (!int.TryParse(Console.ReadLine(), out kinoIndex) || kinoIndex < 1 || kinoIndex > kinoteatrs.Count)
        {
            Console.WriteLine("Неверный ввод. Пожалуйста, введите номер кинотеатра из списка.");
        }
        if (kinoteatrs[kinoIndex - 1] == null)
        {
            Console.WriteLine("Кинотеатр не найден.");
            return;
        }
        if (kinoteatrs[kinoIndex - 1].GetSeans().Count == 0)
        {
            throw new EmptyListException("В выбранном кинотеатре нет сеансов для редактирования.");
            //Console.WriteLine("В выбранном кинотеатре нет сеансов для редактирования.");
            //return;
        }
        Console.WriteLine("Выберите сеанс для редактирования:");
        for (int i = 0; i < kinoteatrs[kinoIndex - 1].GetSeans().Count; i++)
        {
            Console.WriteLine($"{i + 1}.{kinoteatrs[kinoIndex - 1].GetSeans()[i].GetName()}");
        }
        int seansIndex;
        while (!int.TryParse(Console.ReadLine(), out seansIndex) || seansIndex < 1 || seansIndex > kinoteatrs[kinoIndex - 1].GetSeans().Count)
        {
            Console.WriteLine("Неверный ввод. Пожалуйста, введите номер сеанса из списка.");
        }

        kinoteatrs[kinoIndex - 1].GetSeans()[seansIndex - 1] = Seans.CreateSeans();
        Console.WriteLine("Сеанс успешно отредактирован.");
    }
    public static void CopySeans(List<Kinoteatr> kinoteatrs)
    {
        if (AvailabilityOfKinoteatrs(kinoteatrs)) return;
        Console.WriteLine("Введите номер кинотеатра в котором скопируете сеанс: ");
        for (int i = 0; i < kinoteatrs.Count; i++)
        {
            Console.WriteLine($"{i + 1}) {kinoteatrs[i].name}");
        }
        int kinoIndex;
        while (!int.TryParse(Console.ReadLine(), out kinoIndex) || kinoIndex < 1 || kinoIndex > kinoteatrs.Count)
        {
            Console.WriteLine("Неверный ввод. Пожалуйста, введите номер кинотеатра из списка.");
        }
        if (kinoteatrs[kinoIndex - 1] == null)
        {
            Console.WriteLine("Кинотеатр не найден.");
            return;
        }
        if (kinoteatrs[kinoIndex - 1].GetSeans().Count == 0)
        {
            throw new EmptyListException("В выбранном кинотеатре нет сеансов для копирования.");
            //Console.WriteLine("В выбранном кинотеатре нет сеансов для копирования.");
            //return;
        }
        Console.WriteLine("Выберите сеанс для копирования:");
        for (int i = 0; i < kinoteatrs[kinoIndex - 1].GetSeans().Count; i++)
        {
            Console.WriteLine($"{i + 1}.{kinoteatrs[kinoIndex - 1].GetSeans()[i].GetName()}");
        }
        int seansIndex;
        while (!int.TryParse(Console.ReadLine(), out seansIndex) || seansIndex < 1 || seansIndex > kinoteatrs[kinoIndex - 1].GetSeans().Count)
        {
            Console.WriteLine("Неверный ввод. Пожалуйста, введите номер сеанса из списка.");
        }

        if (kinoteatrs[kinoIndex - 1].GetSeans()[seansIndex - 1] is Reservation)
        {
            //kinoteatrs[kinoIndex - 1].GetSeans().Add(new Reservation((Reservation)kinoteatrs[kinoIndex - 1].GetSeans()[seansIndex - 1]));
            kinoteatrs[kinoIndex - 1] += new Reservation((Reservation)kinoteatrs[kinoIndex - 1].GetSeans()[seansIndex - 1]);
        }
        else if(kinoteatrs[kinoIndex - 1].GetSeans()[seansIndex - 1] is PaidTickets)
        {
            //kinoteatrs[kinoIndex - 1].GetSeans().Add(new PaidTickets((PaidTickets)kinoteatrs[kinoIndex - 1].GetSeans()[seansIndex - 1]));
            kinoteatrs[kinoIndex - 1] += new PaidTickets((PaidTickets)kinoteatrs[kinoIndex - 1].GetSeans()[seansIndex - 1]);
        }
        Console.WriteLine("Сеанс успешно скопирован.");
        Console.ReadLine();
    }
    public static void ViewKinoteatr(List<Kinoteatr> kinoteatrs)
    {
        if (Empty(kinoteatrs))
        {
            return;
        }
        for (int i = 0; i < kinoteatrs.Count; i++)
        {
            Console.WriteLine($"{i + 1}) {kinoteatrs[i].GetName()}");
        }
        Console.ReadLine();
    }
    public static void ViewSeans(List<Kinoteatr> kinoteatrs)
    {
        if (Empty(kinoteatrs))
        {
            return;
        }

        Kinoteatr selectedKino = SelectKinoteatr(kinoteatrs); 
        if (selectedKino.GetSeans().Count == 0)
        {
            Console.WriteLine("В выбранном кинотеатре нет сеансов для просмотра.");
            return;
        }
        Console.WriteLine("\nСеансы:");
        for (int i = 0; i < selectedKino.GetSeans().Count; i++)
        {
            selectedKino.GetSeans()[i].Print();
        }
    }
    public static void UseIncrement(List<Kinoteatr> kinoteatrs)
    {
        if (Empty(kinoteatrs))
        {
            return;
        }

        Kinoteatr selectedKino = SelectKinoteatr(kinoteatrs);
        selectedKino++;
    }
    public static void CompareSeans(List<Kinoteatr> kinoteatrs)
    {
        if (Empty(kinoteatrs))
        {
            return;
        }

        Kinoteatr selectedKino = SelectKinoteatr(kinoteatrs);
        if (selectedKino.GetSeans().Count == 0)
        {
            Console.WriteLine("В выбранном кинотеатре нет сеансов для сравнения.");
            return;
        }
        Console.WriteLine("Выберите первый сеанс для сравнения:");
        for (int i = 0; i < selectedKino.GetSeans().Count; i++)
        {
            Console.WriteLine($"{i + 1}.{selectedKino.GetSeans()[i].GetName()}");
        }
        int seansIndex1;
        while (!int.TryParse(Console.ReadLine(), out seansIndex1) ||
       seansIndex1 < 1 || seansIndex1 > selectedKino.GetSeans().Count)
        {
            Console.WriteLine("Неверный ввод. Пожалуйста, введите номер сеанса из списка.");
        }

        Console.WriteLine("Выберите второй сеанс для сравнения:");
        for (int i = 0; i < selectedKino.GetSeans().Count; i++)
        {
            Console.WriteLine($"{i + 1}.{selectedKino.GetSeans()[i].GetName()}");
        }
        int seansIndex2;
        while (!int.TryParse(Console.ReadLine(), out seansIndex2) ||
       seansIndex2 < 1 || seansIndex2 > selectedKino.GetSeans().Count)
        {
            Console.WriteLine("Неверный ввод. Пожалуйста, введите номер сеанса из списка.");
        }
        if (selectedKino[seansIndex1-1] == selectedKino[seansIndex2 - 1])
        {
            Console.WriteLine("Сеансы равны");
        }
        else if (selectedKino[seansIndex1 - 1] > selectedKino[seansIndex2 - 1])
        {
            Console.WriteLine("Первый сеанс больше второго");
        }
        else 
        {
            Console.WriteLine("Первый сеанс меньше второго");
        }
        Console.ReadLine();
    }
    public static void SearchMinItem(List<Kinoteatr> kinoteatrs)
    {
        if (Empty(kinoteatrs))
        {
            Console.WriteLine("Список пуст");
            Console.ReadLine();
            return;
        }

        Kinoteatr selectedKino = SelectKinoteatr(kinoteatrs);
        Console.WriteLine("Минимальный элемент: ");
        selectedKino.GetSeans().Min().Print();
        Console.ReadLine();
    }
    public static void SearchMaxItem(List<Kinoteatr> kinoteatrs)
    {
        if (Empty(kinoteatrs))
        {
            Console.WriteLine("Список пуст");
            Console.ReadLine();
            return;
        }

        Kinoteatr selectedKino = SelectKinoteatr(kinoteatrs);
        Console.WriteLine("Максимальный элемент: ");
        selectedKino.GetSeans().Max().Print();
        Console.ReadLine();
    }
    public static Kinoteatr SelectKinoteatr(List<Kinoteatr> kinoteatrs)
    {
        Console.WriteLine("Выберите кинотеатр: ");
        for (int i = 0; i < kinoteatrs.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {kinoteatrs[i].GetName()}");
        }
        int kinoIndex;
        while (!int.TryParse(Console.ReadLine(), out kinoIndex) ||
               kinoIndex < 1 || kinoIndex > kinoteatrs.Count)
        {
            Console.WriteLine("Неверный ввод.");
        }
        return kinoteatrs[kinoIndex - 1];
    }
    public static bool Empty(List<Kinoteatr> kinoteatrs)
    {
        if (kinoteatrs.Count == 0)
        {
            throw new EmptyListException();
            Console.WriteLine("Сначала добавьте кинотеатр.");
            return true;
        }
        return false;
    }
}
