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
                    new KeyGesture(Key.I, ModifierKeys.Control)
                }
            );

        public static readonly RoutedUICommand Manage = new RoutedUICommand
            (
                "Manage",
                "Manage",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.M, ModifierKeys.Control)
                }
            );

        public static readonly RoutedUICommand InventoryCrud = new RoutedUICommand
           (
               "InventoryCrud",
               "InventoryCrud",
               typeof(CustomCommands)
           );

        public static readonly RoutedUICommand CategoryCrud = new RoutedUICommand
           (
               "CategoryCrud",
               "CategoryCrud",
               typeof(CustomCommands)
           );

        public static readonly RoutedUICommand ColorCrud = new RoutedUICommand
           (
               "ColorCrud",
               "ColorCrud",
               typeof(CustomCommands)
           );

        public static readonly RoutedUICommand ItemCrud = new RoutedUICommand
           (
               "ItemCrud",
               "ItemCrud",
               typeof(CustomCommands)
           );


        public static readonly RoutedUICommand Refresh = new RoutedUICommand
            (
                "Refresh",
                "Refresh",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.F5)
                }
            );

        public static readonly RoutedUICommand CleanFilter = new RoutedUICommand
            (
                "CleanFilter",
                "CleanFilter",
                typeof(CustomCommands)
            );

        public static readonly RoutedUICommand CheckConnection = new RoutedUICommand
           (
               "CheckConnection",
               "CheckConnection",
               typeof(CustomCommands)
           );
    }

}
