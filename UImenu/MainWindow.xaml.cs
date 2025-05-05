using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using EmployeeLib;

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using EmployeeLib;

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using EmployeeLib;
using Newtonsoft.Json;

namespace UImenu
{
    public partial class MainWindow : Window
    {
        private List<Employee> _employees = new List<Employee>();
        private const string FilePath = "employees.json";  // Путь к файлу для сохранения

        public MainWindow()
        {
            InitializeComponent();
            LoadEmployees();  // Загружаем данные при старте
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = NameTextBox.Text;
                DateTime birthday = BirthdayPicker.SelectedDate ?? throw new Exception("Выберите дату рождения");
                DateTime startDate = StartDatePicker.SelectedDate ?? throw new Exception("Выберите дату начала работы");
                char gender = MaleRadio.IsChecked == true ? 'M' : 'F';
                Education education = (Education)Enum.Parse(typeof(Education),
                    ((ComboBoxItem)EducationComboBox.SelectedItem).Content.ToString());
                CurrentPosition position = (CurrentPosition)Enum.Parse(typeof(CurrentPosition),
                    ((ComboBoxItem)PositionComboBox.SelectedItem).Content.ToString());
                decimal salary = decimal.Parse(SalaryTextBox.Text);

                Employee emp = new Employee(name, birthday, startDate, gender, education, position, salary);

                if (!emp.CheckName())
                {
                    MessageBox.Show("Имя некорректно!");
                    return;
                }

                _employees.Add(emp);

                RefreshEmployeeList();
                SaveEmployees();  // Сохраняем данные в файл

                MessageBox.Show("Сотрудник успешно добавлен! 🎉");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void RefreshEmployeeList()
        {
            EmployeeDataGrid.ItemsSource = null;
            EmployeeDataGrid.ItemsSource = _employees;
        }

        private void SaveEmployees()
        {
            try
            {
                string json = JsonConvert.SerializeObject(_employees, Formatting.Indented);  // Сериализуем в JSON
                File.WriteAllText(FilePath, json);  // Сохраняем в файл
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
            }
        }

        private void LoadEmployees()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string json = File.ReadAllText(FilePath);  // Читаем данные из файла
                    _employees = JsonConvert.DeserializeObject<List<Employee>>(json);  // Десериализуем
                    RefreshEmployeeList();  // Обновляем отображение
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }
    }
}


