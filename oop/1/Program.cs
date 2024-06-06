using System.Collections.Generic;
using System;

internal class Program
{
    public static List<Kinoteatr> kinoteatrs = new List<Kinoteatr>();

    static void Main(string[] args)
    {
        while (true)
        {
            try
            {
                Console.WriteLine("Добро пожаловать в кассу кинотеатра!");
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Добавить кинотеатр");
                Console.WriteLine("2. Удалить кинотеатр");
                Console.WriteLine("3. Редактировать кинотеатр");
                Console.WriteLine("4. Просмотреть кинотеатры");
                Console.WriteLine("5. Добавить сеанс");
                Console.WriteLine("6. Удалить сеанс");
                Console.WriteLine("7. Редактировать сеанс");
                Console.WriteLine("8. Просмотреть сеанс");
                Console.WriteLine("9. Просмотреть сеанс");
                Console.WriteLine("10. Копировать кинтеатр");
                Console.WriteLine("11. Копировать сеанс");
                Console.WriteLine("12. Операция инкремента");
                Console.WriteLine("13. Сравнить сеансы");
                Console.WriteLine("14. Отсортировать сеансы");
                Console.WriteLine("15. Найти минимальный сеанс");
                Console.WriteLine("16. Найти максимальный сеанс");
                int option;
                while (!int.TryParse(Console.ReadLine(), out option) ||
                option < 1 || option > 17)
                {
                    Console.WriteLine("Неверный ввод. Пожалуйста, введите число от 1 до 17.");
                }
                switch (option)
                {
                    case 1:
                        Kinoteatr.AddKinoteatr(kinoteatrs);
                        break;
                    case 2:
                        Kinoteatr.DeleteKinoteatr(kinoteatrs);
                        break;
                    case 3:
                        Kinoteatr.EditKinoteatr(kinoteatrs);
                        break;
                    case 4:
                        Kinoteatr.ViewKinoteatr(kinoteatrs);
                        break;
                    case 5:
                        Kinoteatr.AddSeans(kinoteatrs);
                        break;
                    case 6:
                        Kinoteatr.DeleteSeans(kinoteatrs);
                        break;
                    case 7:
                        Kinoteatr.EditSeans(kinoteatrs);
                        break;
                    case 8:
                        Kinoteatr.ViewSeans(kinoteatrs);
                        break;
                    case 9:
                        Kinoteatr.ViewSeans(kinoteatrs);
                        break;
                    case 10:
                        Kinoteatr.CopyKinoteatr(kinoteatrs);
                        break;
                    case 11:
                        Kinoteatr.CopySeans(kinoteatrs);
                        break;
                    case 12:
                        Kinoteatr.UseIncrement(kinoteatrs);
                        break;
                    case 13:
                        Kinoteatr.CompareSeans(kinoteatrs);
                        break;
                    case 14:
                        foreach (Kinoteatr kino in kinoteatrs)
                        {
                            kino.GetSeans().Sort();
                        }
                        break;
                    case 15:
                        Kinoteatr.SearchMinItem(kinoteatrs);
                        break;
                    case 16:
                        Kinoteatr.SearchMaxItem(kinoteatrs);
                        break;
                    case 17:
                        Console.WriteLine("Завершение программы.");
                        return;
                    default:
                        return;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            
        }
    }
}

