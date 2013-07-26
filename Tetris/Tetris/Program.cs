// Made by IzYZacee
//You must not take credit or in other ways steal this project
//If you wish to contribute you can PM me at http://reddit.com/u/izyzacee/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
 
namespace Tetris
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
          //  Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}