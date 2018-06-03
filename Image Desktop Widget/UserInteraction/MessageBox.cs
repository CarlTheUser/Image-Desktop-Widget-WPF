using System;
using System.Windows;

namespace UserInteraction
{
    public sealed class MessageBox
    {

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button)
        {
            MessageBoxResult messageBoxResult = MessageBoxResult.None;

            MessageBoxWindow messageBoxWindow = new MessageBoxWindow(messageBoxText, caption, button);

            messageBoxWindow.ShowDialog();

            messageBoxResult = messageBoxWindow.MessageBoxResult;

            return messageBoxResult;
        }

        public static MessageBoxResult Show(string messageBoxText, string caption)
        {
            return Show(messageBoxText, caption, MessageBoxButton.OK);
        }

        public static MessageBoxResult Show(string messageBoxText)
        {
            return Show(messageBoxText, String.Empty);
        }

    }
}
