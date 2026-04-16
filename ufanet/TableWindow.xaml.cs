using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ufanet;

namespace UfanetApp
{
    public partial class TableWindow : Window
    {
        public TableWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                using (var db = new УфанетEntities())
                {
                    var subscribers = await db.subscribers.ToListAsync();
                    dgSubscribers.ItemsSource = subscribers;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddSubscriberWindow();
            addWindow.ShowDialog();
            LoadData();
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgSubscribers.SelectedItem == null)
            {
                MessageBox.Show("Выберите абонента для удаления", "Внимание");
                return;
            }

            var result = MessageBox.Show("Удалить выбранного абонента?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var selected = (subscribers)dgSubscribers.SelectedItem;
                    using (var db = new УфанетEntities())
                    {
                        var subscriber = await db.subscribers.FindAsync(selected.subscriber_id);
                        if (subscriber != null)
                        {
                            db.subscribers.Remove(subscriber);
                            await db.SaveChangesAsync();
                            LoadData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка");
                }
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}