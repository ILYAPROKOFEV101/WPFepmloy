
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using EmployeeLib; // подключаешь свою библиотеку

namespace EmployeeLibTests
{
    [TestClass]
    public sealed class EmployeeLibTests
    {

[TestMethod]
[DataRow(1000.0d, 0, 1150.0d)]  // DIRECTOR, без дополнительных бонусов
[DataRow(1000.0d, 1, 1030.0d)]  // MANAGER, без дополнительных бонусов
[DataRow(1000.0d, 2, 1050.0d)]  // JUNIOR, без дополнительных бонусов
[DataRow(1000.0d, 3, 1100.0d)]  // MIDDLE, без дополнительных бонусов
[DataRow(1000.0d, 4, 1150.0d)]  // SENIOR, без дополнительных бонусов
public void MonthPaymentTests(double salary, int positionIndex, double expected)
{
    // Arrange
    DateTime fixedToday = new DateTime(2025, 4, 18); // Фиксированная дата для тестов
    DateTime birthday = fixedToday.AddYears(-35);    // Возраст: 35 лет
    DateTime workStartDate = fixedToday.AddYears(-5); // Стаж: 5 лет (меньше 10)

    // Преобразуем индекс позиции в enum
    CurrentPosition position = positionIndex switch
    {
        0 => CurrentPosition.DIRECTOR,
        1 => CurrentPosition.MANAGER,
        2 => CurrentPosition.JUNIOR,
        3 => CurrentPosition.MIDDLE,
        4 => CurrentPosition.SENIOR,
        _ => throw new ArgumentOutOfRangeException(nameof(positionIndex), "Invalid position index")
    };

    Employee employee = new Employee(
        name: "Вася",
        birthday: birthday,
        workStartDate: workStartDate,
        gender: 'м',
        education: Education.SPECIALIZED_SECONDARY, // Без высшего образования
        position: position,
        salary: (decimal)salary
    );

    // Act
    decimal actual = employee.MonthPayment(fixedToday);

    // Assert
    Assert.AreEqual((decimal)expected, actual);
}



[TestMethod]
[DataRow("Фz", true)]               // Латинская буква разрешена
[DataRow("Федор Борисович", true)]   // Пробелы разрешены, цифр нет
[DataRow("Федор1", true)]           // Одна цифра в конце разрешена
[DataRow("Федор Борисович1", false)] // Более одной цифры
[DataRow("1Федор", false)]          // Цифра в первой позиции
public void CheckName_ValidatesNamesCorrectly(string name, bool expected)
{
    // Arrange
    Employee employee = new Employee(
        name: name,
        birthday: DateTime.Now.AddYears(-30),
        workStartDate: DateTime.Now.AddYears(-5),
        gender: 'м',
        education: Education.HIGHER,
        position: CurrentPosition.MANAGER,
        salary: 1000.0M
    );

    // Act
    bool actual = employee.CheckName();

    // Assert
    Assert.AreEqual(expected, actual);
}
       
    

[TestMethod]
[DataRow('м', 65, 0)]               // Мужчина, возраст 65 лет
[DataRow('м', 60, 365 * 5 + 1)]     // Мужчина, возраст 60 лет
[DataRow('ж', 58, 730)]             // Женщина, возраст 58 лет
public void TimeUntilRetirementTests(char gender, int years, int expected)
{
    // Arrange
    DateTime fixedToday = new DateTime(2025, 4, 18); // Фиксированная дата для тестов
    DateTime birthday = fixedToday.AddYears(-years); // Дата рождения на основе возраста

    Employee employee = new Employee(
        name: "Test",
        birthday: birthday,
        workStartDate: fixedToday.AddYears(-10), // Пример даты начала работы
        gender: gender,
        education: Education.HIGHER,
        position: CurrentPosition.MANAGER,
        salary: 1000.0M
    );

    // Act
    int actual = employee.TimeUntilRetirement(fixedToday);

    // Assert
    Assert.AreEqual(expected, actual);
} }
   
}    


    

