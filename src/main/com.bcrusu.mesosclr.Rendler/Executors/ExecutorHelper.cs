using mesos;

namespace com.bcrusu.mesosclr.Rendler.Executors
{
    internal static class ExecutorHelper
    {
        public static void SendTaskRunningStatus(this IExecutorDriver driver, TaskID taskId)
        {
            driver.SendStatusUpdate(new TaskStatus
            {
                task_id = taskId,
                state = TaskState.TASK_RUNNING
            });
        }

        public static void SendTaskFinishedStatus(this IExecutorDriver driver, TaskID taskId)
        {
            driver.SendStatusUpdate(new TaskStatus
            {
                task_id = taskId,
                state = TaskState.TASK_FINISHED
            });
        }
    }
}
