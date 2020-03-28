using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PSManagement.Commands
{

    public static class CustomCommands
    {
        public static readonly RoutedUICommand Exit = new RoutedUICommand
            (
                "Exit",
                "Exit",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
                }
            );

        public static readonly RoutedUICommand Sales = new RoutedUICommand(

                "Sales",
                "Sales",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.S, ModifierKeys.Control)
                }
            );

        public static readonly RoutedUICommand Inventory = new RoutedUICommand
            (
                "Inventory",
                "Inventory",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.I,ModifierKeys.Control)
                }
            );
    }

}
