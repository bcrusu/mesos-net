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
typedef void Executor_Error(long managedDriverId, char* message);

class ManagedExecutorInterface {
public:
	Executor_Registered* Registered;
	Executor_Reregistered* Reregistered;
	Executor_Disconnected* Disconnected;
	Executor_LaunchTask* LaunchTask;
	Executor_KillTask* KillTask;
	Executor_FrameworkMessage* FrameworkMessage;
	Executor_Shutdown* Shutdown;
	Executor_Error* Error;
};

}

#endif /* MANAGEDEXECUTORINTERFACE_HPP_ */
