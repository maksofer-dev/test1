using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace RussianCosmetics.View
{
    /// <summary>
    /// Логика взаимодействия для CounterAgentWindow.xaml
    /// </summary>
    public partial class CounterAgentListWindow : Window
    {
        public CounterAgentListWindow()
        {
            InitializeComponent();
            
            dGridAgents.ItemsSource = IntangibleAssetsDBEntities.GetContext().CounterAgents.ToList();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            AddCounterAgent addCounterAgent = new AddCounterAgent(null);
            addCounterAgent.ShowDialog();

        }

        private void backBN_Click(object sender, RoutedEventArgs e)
        {
            MainMenuWindow mainMenu = new MainMenuWindow(userID, roleID);
            mainMenu.Show();
            counterAgentWindow.Close();

        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            var agentsForRemoving = dGridAgents.SelectedItems.Cast<CounterAgent>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {agentsForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    IntangibleAssetsDBEntities.GetContext().CounterAgents.RemoveRange(agentsForRemoving);
                    IntangibleAssetsDBEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    dGridAgents.ItemsSource = IntangibleAssetsDBEntities.GetContext().CounterAgents.ToList();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            IntangibleAssetsDBEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            dGridAgents.ItemsSource = IntangibleAssetsDBEntities.GetContext().CounterAgents.ToList();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddCounterAgent editAgentWindow = new AddCounterAgent((sender as Button).DataContext as CounterAgent);
            editAgentWindow.ShowDialog();
        }

        private void screenBN_Click(object sender, RoutedEventArgs e)
        {
            CreateScreenShotFromVisual(dGridAgents);
        }
    }
}
