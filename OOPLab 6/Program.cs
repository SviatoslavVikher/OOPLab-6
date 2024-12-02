using System;

class Date
{
    // Захищені поля
    private int year;
    private int month;
    private int day;
    private int hours;
    private int minutes;

    // Властивості
    public int Year { get => year; set => year = value; }
    public int Month { get => month; set => month = value; }
    public int Day { get => day; set => day = value; }
    public int Hours { get => hours; set => hours = value; }
    public int Minutes { get => minutes; set => minutes = value; }

    // Конструктори
    public Date() { }

    public Date(int year, int month, int day, int hours, int minutes)
    {
        this.year = year;
        this.month = month;
        this.day = day;
        this.hours = hours;
        this.minutes = minutes;
    }

    public Date(Date other)
    {
        this.year = other.year;
        this.month = other.month;
        this.day = other.day;
        this.hours = other.hours;
        this.minutes = other.minutes;
    }

    // Перевизначення ToString()
    public override string ToString()
    {
        return $"{Day:D2}/{Month:D2}/{Year} {Hours:D2}:{Minutes:D2}";
    }
}

class Airplane
{
    // Захищені поля
    private string departureCity;
    private string arrivalCity;
    private Date departureDate;
    private Date arrivalDate;

    // Властивості
    public string DepartureCity { get => departureCity; set => departureCity = value; }
    public string ArrivalCity { get => arrivalCity; set => arrivalCity = value; }
    public Date DepartureDate { get => departureDate; set => departureDate = value; }
    public Date ArrivalDate { get => arrivalDate; set => arrivalDate = value; }

    // Конструктори
    public Airplane() { }

    public Airplane(string departureCity, string arrivalCity, Date departureDate, Date arrivalDate)
    {
        this.departureCity = departureCity;
        this.arrivalCity = arrivalCity;
        this.departureDate = departureDate;
        this.arrivalDate = arrivalDate;
    }

    public Airplane(Airplane other)
    {
        this.departureCity = other.departureCity;
        this.arrivalCity = other.arrivalCity;
        this.departureDate = new Date(other.departureDate);
        this.arrivalDate = new Date(other.arrivalDate);
    }

    // Методи
    public int GetTotalTime()
    {
        TimeSpan travelTime = new DateTime(arrivalDate.Year, arrivalDate.Month, arrivalDate.Day, arrivalDate.Hours, arrivalDate.Minutes, 0) -
                              new DateTime(departureDate.Year, departureDate.Month, departureDate.Day, departureDate.Hours, departureDate.Minutes, 0);
        return (int)travelTime.TotalMinutes;
    }

    public bool IsArrivingToday()
    {
        return departureDate.Year == arrivalDate.Year &&
               departureDate.Month == arrivalDate.Month &&
               departureDate.Day == arrivalDate.Day;
    }

    // Перевизначення ToString()
    public override string ToString()
    {
        return $"Відправлення: {departureCity} ({departureDate}), Прибуття: {arrivalCity} ({arrivalDate}), Час у дорозі: {GetTotalTime()} хвилин.";
    }
}

class Program
{
    public static Airplane[] CreateAirplanesArray(int n)
    {
        Airplane[] airplanes = new Airplane[n];
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Введіть дані для рейсу {i + 1}:");
            Console.Write("Місто відправлення: ");
            string departureCity = Console.ReadLine();
            Console.Write("Місто прибуття: ");
            string arrivalCity = Console.ReadLine();

            Console.WriteLine("Дата відправлення:");
            Date departureDate = ReadDate();
            Console.WriteLine("Дата прибуття:");
            Date arrivalDate = ReadDate();

            airplanes[i] = new Airplane(departureCity, arrivalCity, departureDate, arrivalDate);
        }
        return airplanes;
    }

    public static void PrintAirplane(Airplane airplane)
    {
        Console.WriteLine(airplane);
    }

    public static void PrintAllAirplanes(Airplane[] airplanes)
    {
        foreach (var airplane in airplanes)
        {
            PrintAirplane(airplane);
        }
    }

    private static Date ReadDate()
    {
        Console.Write("Рік: ");
        int year = int.Parse(Console.ReadLine());
        Console.Write("Місяць: ");
        int month = int.Parse(Console.ReadLine());
        Console.Write("День: ");
        int day = int.Parse(Console.ReadLine());
        Console.Write("Години: ");
        int hours = int.Parse(Console.ReadLine());
        Console.Write("Хвилини: ");
        int minutes = int.Parse(Console.ReadLine());
        return new Date(year, month, day, hours, minutes);
    }

    static void Main()
    {
        Console.Write("Введіть кількість рейсів: ");
        int n = int.Parse(Console.ReadLine());
        Airplane[] airplanes = CreateAirplanesArray(n);

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1 - Вивести всі рейси");
            Console.WriteLine("2 - Вивести інформацію про конкретний рейс");
            Console.WriteLine("3 - Вийти");
            Console.Write("Ваш вибір: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    PrintAllAirplanes(airplanes);
                    break;
                case 2:
                    Console.Write("Введіть номер рейсу (1 - {0}): ", n);
                    int index = int.Parse(Console.ReadLine()) - 1;
                    if (index >= 0 && index < n)
                        PrintAirplane(airplanes[index]);
                    else
                        Console.WriteLine("Некоректний номер рейсу.");
                    break;
                case 3:
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Некоректний вибір. Спробуйте ще раз.");
                    break;
            }
        }
    }
}

