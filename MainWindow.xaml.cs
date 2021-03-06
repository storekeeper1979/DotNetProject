﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MainSIMS;

namespace InventoryApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonLogIn_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrEmpty(tbUser.Text))
            {
                MessageBox.Show("Please enter your username.", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbUser.Focus();
                return;
            }
            try
            {
               using (InventoryDBEntitiesFK login = new InventoryDBEntitiesFK())
                {
                    var query = from user in login.Users
                                where user.EmployeeName == tbUser.Text && user.Password == passwBox.Password && user.Role == ComboBoxRole.Text                           
                                select user;

                    if (query.SingleOrDefault() != null)
                    {
                        switch (ComboBoxRole.Text)
                        {
                            case "Admin":
                                AdminView admin = new AdminView();
                                admin.Show();
                                break;
                            case "Manager":
                                ManagerView manager = new ManagerView();
                                manager.Show();
                                break;
                            case "Employee":
                                EmployeeView empl = new EmployeeView();
                                empl.Show();
                                break;
                            case "Supplier":
                                SupplierView supplier = new SupplierView();
                                supplier.Show();
                                break;
                        }
                        // Just for testing purpose, I'll keep this message here for now
                        MessageBox.Show("You have been successfully logged in.", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                        Globals.currentUser = (User)query.SingleOrDefault();
                    }
                    else
                    {
                        MessageBox.Show("Your username or password is incorrect.", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    }                 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("We messed up the code here!!! Please ask PROF to HELP!!! " + ex.Message , "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            tbUser.Clear();
            passwBox.Clear();
            ComboBoxRole.SelectedIndex = -1;
        }
     
        private void passwBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var box = sender as PasswordBox;
           // this.Title = "Password typed: " + box.Password;
        }       
    }
}
