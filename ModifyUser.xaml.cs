﻿using System;
using System.Windows;


namespace MainSIMS
{
    /// <summary>
    /// Interaction logic for ModifyUser.xaml
    /// </summary>
    public partial class ModifyUser : Window
    {
       // AdminView win = new AdminView();
        InventoryDBEntitiesFK db;

        public ModifyUser()
        {
            InitializeComponent();
            db = new InventoryDBEntitiesFK();
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {   
            try
            {
                using (InventoryDBEntitiesFK ctx = new InventoryDBEntitiesFK())
                {
                    User u = ctx.Users.Find(Convert.ToInt32(lblUserId.Content));
                    u.EmployeeName = tbUserNameInModify.Text;
                    u.Password = tbPasswordInmodify.Text;
                    u.Role = comboBoxRoleInModify.Text;
                    ctx.SaveChanges();
                    MessageBox.Show("Cogradulations!!! Record has been modified");
                  //  win.refreshUsersList();
                    this.Close();

                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
