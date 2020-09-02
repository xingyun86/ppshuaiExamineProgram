
using System;
using System.Windows;

namespace ExamineProgram
{
    class App
    {
        [STAThread]
        static void Main()
        {
            // 定义Application对象作为整个应用程序入口
            new Application().Run(new ExamineProgram(CSVUtils.OpenCSV(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "test.csv")));
        }
    }
}
