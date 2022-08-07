using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MoeGoe_GUI
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWin());
        }
    }

    public class CommandLine
    {
        private readonly Process process;

        public delegate void onOutputHandler(CommandLine sender, string e);
        public event onOutputHandler OutputHandler;

        public CommandLine()
        {
            try
            {
                process = new Process()
                {
                    StartInfo = new ProcessStartInfo("cmd.exe")
                    {
                        Arguments = "/k",
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                process.OutputDataReceived += Command_OutputDataReceived;
                process.ErrorDataReceived += Command_ErrorDataReceived;
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        void Command_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            OnOutput(e.Data);
        }

        void Command_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            OnOutput(e.Data);
        }

        private void OnOutput(string data)
        {
            OutputHandler?.Invoke(this, data);
        }

        public void Write(string data)
        {
            try
            {
                process.StandardInput.WriteLine(data);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }
    }
}
