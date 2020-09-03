using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Steam_Investor_App
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Global exception handling  
            Application.Current.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(AppDispatcherUnhandledException);
        }

        void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // In debug mode do not custom-handle the exception, let Visual Studio handle it

            e.Handled = false;

            ShowUnhandledException(e);

        }


        bool canShowMessage = true; //Prevents the app to show multiple error messages
        void ShowUnhandledException(DispatcherUnhandledExceptionEventArgs e)
        {
            if (canShowMessage == true)
            {
                
                e.Handled = true;

                string errorMessage = string.Format("An application error occurred.\nPlease check whether your data is correct and repeat the action. If this error occurs again there seems to be a more serious malfunction in the application, and you better close it.\n\nThe error got copied to you clipboard\n\nError: {0}",

                e.Exception.Message + (e.Exception.InnerException != null ? "\n" +
                e.Exception.InnerException.Message : null));

                MessageBox.Show(errorMessage, "Application Error", MessageBoxButton.OK,MessageBoxImage.Error);
                Clipboard.SetText(errorMessage);
                canShowMessage = false;

                App.Current.Shutdown();
            }
            
        }
    }
}