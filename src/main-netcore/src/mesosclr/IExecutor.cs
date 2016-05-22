using mesos;

namespace mesosclr
{
    public interface IExecutor
    {
        void Registered(IExecutorDriver driver, ExecutorInfo executorInfo, FrameworkInfo frameworkInfo, SlaveInfo slaveInfo);

        void Reregistered(IExecutorDriver driver, SlaveInfo slaveInfo);

        void Disconnected(IExecutorDriver driver);

        void LaunchTask(IExecutorDriver driver, TaskInfo taskInfo);

        void KillTask(IExecutorDriver driver, TaskID taskId);

        void FrameworkMessage(IExecutorDriver driver, byte[] data);

        void Shutdown(IExecutorDriver driver);

        void Error(IExecutorDriver driver, string message);
    }
}
