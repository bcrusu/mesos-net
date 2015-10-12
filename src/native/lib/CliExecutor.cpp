#include "CliExecutor.hpp"
#include "Common.hpp"

namespace mesosclr {

CliExecutor::CliExecutor(long managedExecutorDriverId, ManagedExecutorInterface executorInterface) {
	_managedExecutorDriverId = managedExecutorDriverId;
	_executorInterface = executorInterface;
}

void CliExecutor::registered(ExecutorDriver* driver, const ExecutorInfo& executorInfo, const FrameworkInfo& frameworkInfo,
		const SlaveInfo& slaveInfo) {
	ScopedByteArray executorInfoBytes = protobuf::Serialize(executorInfo);
	ScopedByteArray frameworkInfoBytes = protobuf::Serialize(frameworkInfo);
	ScopedByteArray slaveInfoBytes = protobuf::Serialize(slaveInfo);
	_executorInterface.registered(_managedExecutorDriverId, executorInfoBytes.Ptr(), frameworkInfoBytes.Ptr(), slaveInfoBytes.Ptr());
}

void CliExecutor::reregistered(ExecutorDriver* driver, const SlaveInfo& slaveInfo) {
	ScopedByteArray slaveInfoBytes = protobuf::Serialize(slaveInfo);
	_executorInterface.reregistered(_managedExecutorDriverId, slaveInfoBytes.Ptr());
}

void CliExecutor::disconnected(ExecutorDriver* driver) {
	_executorInterface.disconnected(_managedExecutorDriverId);
}

void CliExecutor::launchTask(ExecutorDriver* driver, const TaskInfo& task) {
	ScopedByteArray taskBytes = protobuf::Serialize(task);
	_executorInterface.launchTask(_managedExecutorDriverId, taskBytes.Ptr());
}

void CliExecutor::killTask(ExecutorDriver* driver, const TaskID& taskId) {
	ScopedByteArray taskIdBytes = protobuf::Serialize(taskId);
	_executorInterface.killTask(_managedExecutorDriverId, taskIdBytes.Ptr());

}

void CliExecutor::frameworkMessage(ExecutorDriver* driver, const string& data) {
	ByteArray dataBytes = StringToByteArray(data);
	_executorInterface.frameworkMessage(_managedExecutorDriverId, &dataBytes);
}

void CliExecutor::shutdown(ExecutorDriver* driver) {
	_executorInterface.shutdown(_managedExecutorDriverId);
}

void CliExecutor::error(ExecutorDriver* driver, const string& message) {
	//TODO: marshal as char* ByteArray messageBytes = StringToByteArray(message);
	_executorInterface.error(_managedExecutorDriverId, &messageBytes);
}

}
