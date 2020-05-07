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


        public static readonly RoutedUICommand LimpiarInventarioCrud = new RoutedUICommand
            (
                "LimpiarInventarioCrud",
                "LimpiarInventarioCrud",
                typeof(CustomCommands)
            );

        public static readonly RoutedUICommand LimpiarCategoriaCrud = new RoutedUICommand
           (
               "LimpiarCategoriaCrud",
               "LimpiarCategoriaCrud",
               typeof(CustomCommands)
           );

        public static readonly RoutedUICommand LimpiarColorCrud = new RoutedUICommand
           (
               "LimpiarColorCrud",
               "LimpiarColorCrud",
               typeof(CustomCommands)
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

        public static readonly RoutedUICommand UpdatePIN = new RoutedUICommand
           (
               "UpdatePIN",
               "UpdatePIN",
               typeof(CustomCommands)
           );

        public static readonly RoutedUICommand Load = new RoutedUICommand
          (
              "Load",
              "Load",
              typeof(CustomCommands)
          );

        public static readonly RoutedUICommand OpenNumPad = new RoutedUICommand
          (
              "OpenNumPad",
              "OpenNumPad",
              typeof(CustomCommands)
          );

        public static readonly RoutedUICommand UnDoChanges = new RoutedUICommand
          (
              "UnDoChanges",
              "UnDoChanges",
              typeof(CustomCommands),
              new InputGestureCollection()
              {
                  new KeyGesture(Key.Z, ModifierKeys.Control)
              }
          );

        public static readonly RoutedUICommand SelectSize = new RoutedUICommand
          (
              "SelectSize",
              "SelectSize",
              typeof(CustomCommands)
          );

        public static readonly RoutedUICommand Sell = new RoutedUICommand
          (
              "Sell",
              "Sell",
              typeof(CustomCommands)
          );

        public static readonly RoutedUICommand Discount = new RoutedUICommand
         (
             "Discount",
             "Discount",
             typeof(CustomCommands)
         );
    }

}
