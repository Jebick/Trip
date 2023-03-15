using Classes;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

Menu();
static void Menu()
{
    Console.Clear();
    Console.WriteLine("----------------------------------------------------------------------------------------\n" +
                      "|  Преподаватели(1)  |  Дисциплины(2)  |  Графики командировок(3)  |  Выход из программы(4)  |\n" +
                      "----------------------------------------------------------------------------------------");
    Console.Write("Введите код операции: ");
    char Code = Console.ReadKey(true).KeyChar;
    switch (Code)
    {
        case '1':
            TeacherMenu();
            break;
        case '2':
            DisciplineMenu();
            break;
        case '3':
            TripMenu();
            break;
        case '4':
            Environment.Exit(0);
            break;

    }
}

static void TeacherMenu()
{
    ICollection<Teacher> _Teachers = new List<Teacher>();
    int Teacher_id = -1;

    using (StreamReader reader = new StreamReader("Teachers.txt"))
    {
        while (!reader.EndOfStream)
        {
            _Teachers.Add(Teacher.ToClass(reader.ReadLine()));
        }
    }
    if (_Teachers.Count > 0) { Teacher_id = _Teachers.Last().Id; }
    Console.Clear();
    Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------\n" +
                      "|  Показать преподавателей(1)  |  Добавить преподавателя(2)  |  Удалить преподавателя(3)  |  Выход в главное меню(4)  |\n" +
                      "-----------------------------------------------------------------------------------------------------------------------");
    Console.Write("Введите код операции: ");
    char Code = Console.ReadKey(true).KeyChar;
    switch (Code)
    {
        case '1':
            ShowTeachers(_Teachers);
            break;
        case '2':
            AddTeacher(_Teachers, Teacher_id);
            break;
        case '3':
            DeleteTeacher(_Teachers);
            break;
        case '4':
            Menu();
            break;

    }
}
static void ShowTeachers(ICollection<Teacher> _Teachers)
{
    Console.Clear();
    if (_Teachers.Count == 0)
    {
        Console.WriteLine("Преподавателей нет");
    }
    else
    {
        foreach (Teacher Teachers in _Teachers)
        {
            Teachers.Show();
        }
    }
    Console.ReadKey();
    TeacherMenu();
}

static void AddTeacher(ICollection<Teacher> _Teachers, int Teachers_id)
{
    Console.Clear();
    Console.WriteLine("Введите ФИО преподавателя");
    string Name = Console.ReadLine();
    Console.WriteLine("Введите номер телефона");
    string Phone = Console.ReadLine();
    Console.WriteLine("Введите должность");
    string Position = Console.ReadLine();
    Console.WriteLine("Введите 0 если работник штатный или 1 если он внештатный");
    int k = int.Parse(Console.ReadLine());
    while( k != 0 || k != 1)
    {
        Console.WriteLine("Повторите ввод");
        k = int.Parse(Console.ReadLine());
    }
    bool State;
    if (k == 0)
    {
        State = true;
    }
    else
    {
        State = false;
    }
    Teachers_id++;
    Teacher Teachers = new Teacher(Teachers_id, Name, Phone, Position, State);
    _Teachers.Add(Teachers);
    using (StreamWriter writer = new StreamWriter("Teachers.txt", false))
    {
        foreach (Teacher _Teacher in _Teachers)
        {
            writer.WriteLine(_Teacher.ToString());
        }
    }
    TeacherMenu();
}

static void DeleteTeacher(ICollection<Teacher> _Teachers)
{
    Console.Clear();
    Console.WriteLine("Введите код преподавателя");
    int id = int.Parse(Console.ReadLine());
    var temp = _Teachers.Where(d => d.Id == id).First();
    if (temp is not null)
    {
        _Teachers.Remove(temp);
        using (StreamWriter writer = new StreamWriter("Teachers.txt", false))
        {
            foreach (Teacher _Teacher in _Teachers)
            {
                writer.WriteLine(_Teacher.ToString());
            }
        }
    }
    else
    {
        Console.WriteLine("Преподавателя с таким кодом не существует");
        Console.ReadKey();
    }
    TeacherMenu();
}

static void DisciplineMenu()
{
    ICollection<Discipline> _Disciplines = new List<Discipline>();
    int Disciplines_id = -1;

    using (StreamReader reader = new StreamReader("Disciplines.txt"))
    {
        while (!reader.EndOfStream)
        {
            _Disciplines.Add(Discipline.ToClass(reader.ReadLine()));
        }
    }
    if (_Disciplines.Count > 0)
    {
        Disciplines_id = _Disciplines.Last().Id;
    }
    Console.Clear();
    Console.WriteLine("-------------------------------------------------------------------------------------------------------------\n" +
                      "|  Показать дисциплины(1)  |  Добавить дисциплину(2)  |  Удалить дисциплину(3)  |  Выход в главное меню(4)  |\n" +
                      "-------------------------------------------------------------------------------------------------------------");
    Console.Write("Введите код операции: ");
    char Code = Console.ReadKey(true).KeyChar;
    switch (Code)
    {
        case '1':
            ShowDisciplines(_Disciplines);
            break;
        case '2':
            AddDiscipline(_Disciplines, Disciplines_id);
            break;
        case '3':
            DeleteDiscipline(_Disciplines);
            break;
        case '4':
            Menu();
            break;

    }
}

static void ShowDisciplines(ICollection<Discipline> Disciplines)
{
    Console.Clear();
    if (Disciplines.Count == 0)
    {
        Console.WriteLine("Дисциплин нет");
    }
    else
    {
        foreach (Discipline Discipline in Disciplines)
        {
            Discipline.Show();
        }
    }
    Console.ReadKey();
    DisciplineMenu();
}

static void AddDiscipline(ICollection<Discipline> Disciplines, int Discipline_id)
{
    Console.Clear();
    Console.WriteLine("Введите название дисциплины");
    string Name = Console.ReadLine();
    Console.WriteLine("Введите время");
    string Time = Console.ReadLine();
    Console.WriteLine("Введите специальность");
    string Speciality = Console.ReadLine();
    Console.WriteLine("Введите особые условия");
    string Conditions = Console.ReadLine();
    Discipline_id++;
    Discipline Discipline = new Discipline(Discipline_id, Name, Time, Speciality, Conditions); ;
    Disciplines.Add(Discipline);
    using (StreamWriter writer = new StreamWriter("Disciplines.txt", false))
    {
        foreach (Discipline _Discipline in Disciplines)
        {
            writer.WriteLine(_Discipline.ToString());
        }
    }
    DisciplineMenu();
}

static void DeleteDiscipline(ICollection<Discipline> Disciplines)
{
    Console.Clear();
    Console.WriteLine("Введите код специальности");
    int id = int.Parse(Console.ReadLine());
    var temp = Disciplines.Where(d => d.Id == id).First();
    if (temp is not null)
    {
        Disciplines.Remove(temp);
        using (StreamWriter writer = new StreamWriter("Disciplines.txt", false))
        {
            foreach (Discipline _Discipline in Disciplines)
            {
                writer.WriteLine(_Discipline.ToString());
            }
        }
    }
    else
    {
        Console.WriteLine("Специальности с таким кодом не существует");
        Console.ReadKey();
    }
    DisciplineMenu();
}
static void TripMenu()
{
    ICollection<Trip> _Trips = new List<Trip>();
    int Trip_id = -1;

    using (StreamReader reader = new StreamReader("Trips.txt"))
    {
        while (!reader.EndOfStream)
        {
            _Trips.Add(Trip.ToClass(reader.ReadLine()));
        }
    }
    if (_Trips.Count > 0) { Trip_id = _Trips.Last().Id; }
    Console.Clear();
    Console.WriteLine("-------------------------------------------------------------------------------------------------------------------\n" +
                      "|  Показать командировки(1)  |  Добавить командировку(2)  |  Удалить командировку(3)  |  Выход в главное меню(4)  |\n" +
                      "-------------------------------------------------------------------------------------------------------------------");
    Console.Write("Введите код операции: ");
    char Code = Console.ReadKey(true).KeyChar;
    switch (Code)
    {
        case '1':
            TripsShow(_Trips);
            break;
        case '2':
            TripAdd(_Trips, Trip_id);
            break;
        case '3':
            TripDelete(_Trips);
            break;
        case '4':
            Menu();
            break;

    }
}

static void TripsShow(ICollection<Trip> Trips)
{
    Console.Clear();
    if (Trips.Count == 0)
    {
        Console.WriteLine("Командировок нет");
    }
    else
    {
        foreach (Trip Trip in Trips)
        {
            Trip.Show();
        }
    }
    TripMenu();
}
static void TripAdd(ICollection<Trip> Trips, int Trip_id)
{
    ICollection<Teacher> _Teachers = new List<Teacher>();

    using (StreamReader reader = new StreamReader("Teachers.txt"))
    {
        while (!reader.EndOfStream)
        {
            _Teachers.Add(Teacher.ToClass(reader.ReadLine()));
        }
    }

    ICollection<Discipline> _Disciplines = new List<Discipline>();

    using (StreamReader reader = new StreamReader("Disciplines.txt"))
    {
        while (!reader.EndOfStream)
        {
            _Disciplines.Add(Discipline.ToClass(reader.ReadLine()));
        }
    }

    Console.Clear();
    if (_Disciplines.Count == 0)
    {
        Console.WriteLine("Дисциплин нет");
        Console.ReadKey();
        TripMenu();
    }
    else
    {
        foreach (Discipline Discipline in _Disciplines)
        {
            Discipline.Show();

        }
        Console.WriteLine("Введите номер дисциплины");
        int d_code = int.Parse(Console.ReadLine());
        string d_temp = _Disciplines.Where(d => d.Id == d_code).First().Name;
        if (d_temp != null)
        {
            Console.Clear();
            if (_Teachers.Count == 0)
            {
                Console.WriteLine("Преподавателей нет");
                Console.ReadKey();
                TripMenu();
            }
            else
            {
                foreach (Teacher Teacher in _Teachers)
                {
                    Teacher.Show();
                }
                Console.WriteLine("Введите номер нужного преподавателя");
                int t_code = int.Parse(Console.ReadLine());
                string t_temp = _Teachers.Where(d => d.Id == t_code).First().Name;
                if (t_temp != null)
                {
                    Console.Clear();
                    Console.WriteLine("Введите дату начала");
                    DateTime Start = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Введите дату окончания");
                    DateTime End = DateTime.Parse(Console.ReadLine());
                    int Contract = int.Parse(Console.ReadLine());
                    Trip_id++;
                    Trip Trip = new Trip(Trip_id, t_temp, d_temp, Start, End);
                    Trips.Add(Trip);
                    using (StreamWriter writer = new StreamWriter("Trips.txt", false))
                    {
                        foreach (Trip _Trip in Trips)
                        {
                            writer.WriteLine(_Trip.ToString());
                        }
                    }
                }
            }
        }
    }
    TripMenu();
}
static void TripDelete(ICollection<Trip> Trips)
{
    Console.Clear();
    Console.WriteLine("Введите код поездки");
    int id = int.Parse(Console.ReadLine());
    var temp = Trips.Where(d => d.Id == id).First();
    if (temp != null)
    {
        Trips.Remove(temp);
        using (StreamWriter writer = new StreamWriter("Trips.txt", false))
        {
            foreach (Trip Trip in Trips)
            {
                writer.WriteLine(Trip.ToString());
            }
        }
    }
    TripMenu();
}