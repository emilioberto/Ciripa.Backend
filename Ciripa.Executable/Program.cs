using Ciripa.Executable;
using Ciripa.Executable.Forms;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ciripa.Executable
{
    public static class Program
    {
        private const string ExecutableFolder = "Ciripa";
        private const string BackendPath = "Backend";
        private const string ElectronPath = "Electron";
        private const string BackendExecutableFileName = "Ciripa.Web.exe";
        private const string ElectronExecutableFileName = "Ciripa.exe";

        private static string CiripaPath
        {
            get
            {
                var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var ciripaPath = Path.Combine(appDataPath, ExecutableFolder);
                return ciripaPath;
            }
        }

        public static void Main()
        {
            try
            {
                RunInternal();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private static void RunInternal()
        {
            using (var splashScreen = new SplashScreen())
            {
                splashScreen.Show();

                var currentVersionPath = Path.Combine(CiripaPath, GetCurrentVersion());
                InstallOrUpdate(CiripaPath, currentVersionPath);

                var backendExecutableFilePath = Path.Combine(currentVersionPath, BackendPath, BackendExecutableFileName);
                var electronExecutableFilePath = Path.Combine(currentVersionPath, ElectronPath, ElectronExecutableFileName);

                // TODO hide console on production
                var backendProcess = Process.Start(new ProcessStartInfo(backendExecutableFilePath)
                {
                    WorkingDirectory = Path.Combine(currentVersionPath, BackendPath),
                    UseShellExecute = true
                });

                if (backendProcess == null)
                {
                    throw new Exception("Should never happen");
                }

                var electronProcess = Process.Start(new ProcessStartInfo(electronExecutableFilePath)
                {
                    WorkingDirectory = Path.Combine(currentVersionPath, ElectronPath),
                    UseShellExecute = false
                });

                if (electronProcess == null)
                {
                    throw new Exception("Should never happen");
                }

                splashScreen.Close();

                WaitForProcesses(electronProcess, backendProcess);
            }
        }

        private static void WaitForProcesses(params Process[] processes)
        {
            var tasks = processes.Select(p => new ProcessTask(p)).ToList();
            Task.WhenAny(tasks.Select(t => t.Task)).Wait();

            foreach (var task in tasks.Where(t => !t.IsCompleted))
            {
                task.Process.Kill();
            }
        }

        private static string GetCurrentVersion()
        {
            var process = Process.GetCurrentProcess();
            var creation = File.GetCreationTime(process.MainModule.FileName);
            var result = creation.ToString("yyyy_MM_dd_hh_mm_ss");
            return result;
        }

        private static void InstallOrUpdate(string ciripaFolder, string currentVersionPath)
        {
            if (Directory.Exists(currentVersionPath))
            {
                return;
            }

            if (!Directory.Exists(ciripaFolder))
            {
                Directory.CreateDirectory(ciripaFolder); // New "install"
            }
            else
            {
                CleanUpVersions(ciripaFolder); // Updating, deleting old versions
            }

            Directory.CreateDirectory(currentVersionPath);

            using (var assetsManager = new AssetsManager())
            {
                var archive = assetsManager.Archive;
                archive.ExtractToDirectory(currentVersionPath);
            }

        }

        private static void CleanUpVersions(string ciripaFolder)
        {
            foreach (var directory in Directory.EnumerateDirectories(ciripaFolder).OrderByDescending(directory => directory))
            {
                Directory.Delete(directory, true);
            }
        }

        private static void TryDeleteFile(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch
            {
                // Who cares
            }
        }
    }
}