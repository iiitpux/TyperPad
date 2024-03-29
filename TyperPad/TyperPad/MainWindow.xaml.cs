﻿using Microsoft.Extensions.DependencyInjection;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TyperPad.Ui;

namespace TyperPad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AppState _appState = new();
        public MainWindow()
        {
            var services1 = new ServiceCollection();
            services1.AddWpfBlazorWebView();
            services1.AddSingleton<AppState>(_appState);
            services1.AddBlazorWebViewDeveloperTools();
            //services1.AddDbContext<ApplicationContext>(options =>
            //   options.UseSqlite("Data Source=BlazorWpfApp.db;"));
            var serviceProvider = services1.BuildServiceProvider();
            //using var context = serviceProvider.GetRequiredService<ApplicationContext>();
            //context.Database.EnsureCreated();



            InitializeComponent();

            this.blazorWebView1.Services = serviceProvider;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
              owner: this,
              messageBoxText: $"Current counter value is: {_appState.Counter}",
              caption: "Counter");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            blazorWebView1.WebView.CoreWebView2.ExecuteScriptAsync("showAlert()");
        }
    }
}
