﻿using System;
using System.Windows.Forms;

namespace Automated_War_Level_Editor
{

    public class Program
    {
        
        [STAThread]
        public static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Window());

        }

    }

}
