using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Serilog.Exceptions;

namespace DbWatchdog
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.WithExceptionDetails()
                .WriteTo.File(".\\log\\DbWatchdog.log", rollingInterval: RollingInterval.Day)
                .WriteTo.Console()  
                .CreateLogger();
            
            //Log.Information($"DbWatchdog start. {ThisAssembly.Git.Branch} v{ThisAssembly.Git.BaseTag}-{ThisAssembly.Git.Commits} ({ThisAssembly.Git.Commit})");
            Log.Information("DbWatchdog start.");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}