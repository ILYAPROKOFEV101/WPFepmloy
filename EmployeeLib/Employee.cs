using System.Globalization;
using EmployeeLib;

namespace EmployeeLib
{
    public enum Education
    {
        PRIMARY,
        SECONDARY,
        SPECIALIZED_SECONDARY,
        HIGHER,
        ADVANCED_DEGREE
    }

    public enum CurrentPosition
    {
        DIRECTOR,
        MANAGER,
        JUNIOR,
        MIDDLE,
        SENIOR
    }

    public class Employee
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime WorkStartDate { get; set; }
        public char Gender { get; set; }
        public Education Education { get; set; }
        public CurrentPosition Position { get; set; }
        public decimal Salary { get; set; }

        public Employee(string name, DateTime birthday, DateTime workStartDate, char gender, Education education, CurrentPosition position, decimal salary)
        {
            Name = name;
            Birthday = birthday;
            WorkStartDate = workStartDate;
            Gender = gender;
            Education = education;
            Position = position;
            Salary = salary;
        }

        public int TimeUntilRetirement(DateTime? today = null)
        {
            DateTime now = today ?? DateTime.Today;

            // Пенсионный возраст
            int retirementAge = (Gender == 'м' || Gender == 'M') ? 65 : 60;

            if (Birthday > now)
                throw new ArgumentOutOfRangeException("Birthday is in the future.");

            // Дата выхода на пенсию
            DateTime retirementDate = Birthday.AddYears(retirementAge);

            if (retirementDate <= now)
                return 0; // Уже на пенсии

            // Количество дней до пенсии
            return (retirementDate - now).Days;
        }
        
        public int Experience(DateTime? today = null)
        {
            DateTime now = today ?? DateTime.Today;

            if (WorkStartDate > now)
                throw new ArgumentOutOfRangeException("Work start date is in the future.");

            int years = now.Year - WorkStartDate.Year;
            if (WorkStartDate.Date > now.AddYears(-years)) years--;

            return years;
        }

        public decimal MonthPayment(DateTime? today = null)
        {
            // Базовая премия в зависимости от должности
            decimal basePercent = Position switch
            {
                CurrentPosition.DIRECTOR => 0.15m,
                CurrentPosition.SENIOR => 0.15m,
                CurrentPosition.MIDDLE => 0.10m,
                CurrentPosition.JUNIOR => 0.05m,
                CurrentPosition.MANAGER => 0.03m,
                _ => 0.0m
            };

            // Дополнительные бонусы
            decimal bonus = 0;

            // Бонус за стаж работы (>= 10 лет)
            if (Experience(today) >= 10)
                bonus += 0.20m;

            // Бонус за образование
            if (Education == Education.HIGHER)
                bonus += 0.10m;
            else if (Education == Education.ADVANCED_DEGREE)
                bonus += 0.15m;

            // Общая зарплата
            return Salary + Salary * (basePercent + bonus);
        }
        
        public bool CheckName()
        {
            // Проверка длины имени
            if (Name.Length < 2 || Name.Length > 15)
                return false;

            int digitCount = 0; // Счетчик цифр

            for (int i = 0; i < Name.Length; i++)
            {
                char c = Name[i];

                // Проверка допустимых символов
                if (!(char.IsLetter(c) || c == ' ' || c == '-'))
                {
                    // Если символ - цифра
                    if (char.IsDigit(c))
                    {
                        // Цифра в первой позиции недопустима
                        if (i == 0)
                            return false;

                        // Увеличиваем счетчик цифр
                        digitCount++;
                    }
                    else
                    {
                        // Недопустимый символ
                        return false;
                    }
                }
            }

            // Если цифр нет или их больше одной, возвращаем false
            return digitCount <= 1;
        }
        
    }
}


