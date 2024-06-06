using System;
using System.Collections;

public class MyList<T> : IEnumerable where T : IComparable<T>
{
    private T[] items;
    private int count;
    public int Count
    {
        get { return count; }
    }
    public MyList(int size)
    {
        items = new T[size];
        count = 0;
    }

    public void Add(T item)
    {
        if (count == items.Length)
        {
            // Увеличиваем размер массива, если он заполнен
            Array.Resize(ref items, items.Length * 2);
        }
        items[count] = item;
        count++;
    }
    public void Remove(T item)
    {
        int index = Find(item);
        if (index != -1)
        {
            RemoveAt(index);
        }
    }
    public void RemoveAt(int index)
    {
        if (index < 0 || index >= count)
        {
            throw new IndexOutOfRangeException("Индекс находится за пределами диапазона списка.");
        }

        for (int i = index; i < count - 1; i++)
        {
            items[i] = items[i + 1];
        }

        count--;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException("Индекс находится за пределами диапазона списка.");
            }
            return items[index];
        }
        set
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException("Индекс находится за пределами диапазона списка.");
            }
            items[index] = value;
        }
    }

    public int Find(T obj)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].Equals(obj))
            {
                return i;
            }
        }
        return -1;
    }

    public T Min()
    {
        if (items.Length == 0)
        {
            Console.WriteLine("Массив пуст");
            return default(T);
        }

        T min = items[0];
        for (int i = 1; i < count; i++)
        {
            if (items[i].CompareTo(min) < 0)
            {
                min = items[i];
            }
        }
        return min;
    }

    public T Max()
    {
        if (items.Length == 0)
        {
            Console.WriteLine( "Массив пуст");
            return default(T);
        }

        T max = items[0];
        for (int i = 0; i < count; i++)
        {
            if (items[i].CompareTo(max) > 0)
            {
                max = items[i];
            }
        }
        return max;
    }

    public void Sort()
    {
        for (int i = 0; i < count - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < count; j++)
            {
                if (items[j].CompareTo(items[minIndex]) < 0)
                {
                    minIndex = j;
                }
            }
            if (minIndex != i)
            {
                T temp = items[i];
                items[i] = items[minIndex];
                items[minIndex] = temp;
            }
        }
    }

    public IEnumerator GetEnumerator()
    {
        return items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
