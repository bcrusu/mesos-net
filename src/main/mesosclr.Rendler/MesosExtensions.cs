using mesos;

namespace mesosclr.Rendler
{
    internal static class MesosExtensions
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

		public static void SendTaskErrorStatus(this IExecutorDriver driver, TaskID taskId)
		{
			driver.SendStatusUpdate(new TaskStatus
				{
					task_id = taskId,
					state = TaskState.TASK_ERROR
				});
		}

        public static bool IsTerminal(this TaskState state)
        {
            return state == TaskState.TASK_FINISHED ||
                   state == TaskState.TASK_FAILED ||
                   state == TaskState.TASK_KILLED ||
                   state == TaskState.TASK_LOST ||
                   state == TaskState.TASK_ERROR;
        }
    }
}
