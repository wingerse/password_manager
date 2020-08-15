using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using PasswordManagerV3.Views;
using ErrorWindow = PasswordManagerV3.Views.AlertWindows.ErrorWindow;

namespace PasswordManagerV3.Other
{
    internal static class ErrorMessages
    {
        public static void ShowErrorMessage(string message, Window owner)
        {
            new ErrorWindow {Text = new TextBlock(new Run(message)), Owner = owner}.ShowDialog();
        }
    }
}