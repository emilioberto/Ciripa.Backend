using System.Diagnostics;
using System.Threading.Tasks;

namespace Ciripa.Executable
{
    internal sealed class ProcessTask
    {
        public readonly Process Process;
        public readonly Task Task;

        public ProcessTask(Process process)
        {
            Process = process;
            Task = Task.Run(() => process.WaitForExit());
        }

        public bool IsCompleted => Task.IsCompleted || Task.IsCanceled || Task.IsFaulted;

        public static implicit operator Task(ProcessTask source) => source.Task;
    }
}