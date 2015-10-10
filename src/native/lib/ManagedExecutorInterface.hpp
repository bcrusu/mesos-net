#ifndef MANAGEDEXECUTORINTERFACE_HPP_
#define MANAGEDEXECUTORINTERFACE_HPP_

#include "Common.hpp"

namespace mesosclr {

typedef void Executor_Registered(long managedDriverId, ByteArray* executorInfo, ByteArray* frameworkInfo, ByteArray* slaveInfo);
typedef void Executor_Reregistered(long managedDriverId, ByteArray* slaveInfo);
typedef void Executor_Disconnected(long managedDriverId);
typedef void Executor_LaunchTask(long managedDriverId, ByteArray* taskInfo);
typedef void Executor_KillTask(long managedDriverId, ByteArray* taskId);
typedef void Executor_FrameworkMessage(long managedDriverId, ByteArray* data);
typedef void Executor_Shutdown(long managedDriverId);
typedef void Executor_Error(long managedDriverId, ByteArray* message);

class ManagedExecutorInterface {
public:
	Executor_Registered* registered;
	Executor_Reregistered* reregistered;
	Executor_Disconnected* disconnected;
	Executor_LaunchTask* launchTask;
	Executor_KillTask* killTask;
	Executor_FrameworkMessage* frameworkMessage;
	Executor_Shutdown* shutdown;
	Executor_Error* error;
};

}

#endif /* MANAGEDEXECUTORINTERFACE_HPP_ */
