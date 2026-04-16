using System;
using System.Windows;
using ufanet;

namespace UfanetApp
{
    public partial class AddSubscriberWindow : Window
    {
        public AddSubscriberWindow()
        {
            InitializeComponent();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbPersonalAccount.Text) ||
                string.IsNullOrWhiteSpace(tbFullName.Text) ||
                string.IsNullOrWhiteSpace(tbPhone.Text) ||
                string.IsNullOrWhiteSpace(tbAddress.Text))
            {
                MessageBox.Show("Заполните все поля!", "Внимание");
                return;
            }

            try
            {
                using (var db = new УфанетEntities())
                {
                    var subscriber = new subscribers
                    {
                        personal_account = tbPersonalAccount.Text.Trim(),
                        full_name = tbFullName.Text.Trim(),
                        phone = tbPhone.Text.Trim(),
                        address = tbAddress.Text.Trim(),
                        connection_date = DateTime.Now,
                        is_active = true
                    };

                    db.subscribers.Add(subscriber);
                    await db.SaveChangesAsync();
                }

                MessageBox.Show("Абонент добавлен!", "Успешно");
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}