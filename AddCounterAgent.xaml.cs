using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RJD_IntangibleValuesAccounting.View
{
    /// <summary>
    /// Логика взаимодействия для AddDisciplineWindow.xaml
    /// </summary>
    public partial class AddCounterAgent : Window
    {
        private CounterAgent _agent = new CounterAgent();
        public AddCounterAgent(CounterAgent selectedCounterAgent)
        {
            InitializeComponent();
            if (selectedCounterAgent != null)
            {
                addCounterAgent.Title = "РЖД - Изменение информации о контрагенте";
                _agent = selectedCounterAgent;

            }
            else
            {
                addCounterAgent.Title = "РЖД - Добавление контрагента";

            }
            DataContext = _agent;

        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_agent.CounterAgentName))
                errors.AppendLine("Укажите название организации");
            if (string.IsNullOrWhiteSpace(_agent.CounterAgentHolder))
                errors.AppendLine("Укажите владельца");
            if (string.IsNullOrWhiteSpace(_agent.CounterAgentAddress))
                errors.AppendLine("Укажите адресс организации");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_agent.CounterAgentID == 0)
            {
                IntangibleAssetsDBEntities.GetContext().CounterAgents.Add(_agent);
            }
            try
            {
                IntangibleAssetsDBEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                addCounterAgent.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        

    }
}
