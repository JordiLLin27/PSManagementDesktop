﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PSManagement.Commands
{
    class CustomCommands
    {
        public static readonly RoutedUICommand Exit = new RoutedUICommand(

            "Exit",
            "Exit",
            typeof(CustomCommands),
            new InputGestureCollection() {

                new KeyGesture(Key.F4, ModifierKeys.Alt)

            }

        );

        public static readonly RoutedUICommand Help = new RoutedUICommand(

            "Help",
            "Help",
            typeof(CustomCommands),
            new InputGestureCollection() {

                new KeyGesture(Key.F1)

            }

        );
    }
}
