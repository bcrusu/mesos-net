#include "CliExecutor.hpp"
#include "Common.hpp"

namespace mesosclr {

CliExecutor::CliExecutor(long managedExecutorDriverId, ManagedExecutorInterface executorInterface) {
	_managedExecutorDriverId = managedExecutorDriverId;
	_executorInterface = executorInterface;
}

void CliExecutor::registered(ExecutorDriver* driver, const ExecutorInfo& executorInfo, const FrameworkInfo& frameworkInfo,
		const SlaveInfo& slaveInfo) {
	ByteArray* executorInfoBytes = protobuf::Serialize(executorInfo);
	ByteArray* frameworkInfoBytes = protobuf::Serialize(frameworkInfo);
	ByteArray* slaveInfoBytes = protobuf::Serialize(slaveInfo);

	_executorInterface.registered(_managedExecutorDriverId, executorInfoBytes, frameworkInfoBytes, slaveInfoBytes);
	delete executorInfoBytes;
	delete frameworkInfoBytes;
	delete slaveInfoBytes;
}

void CliExecutor::reregistered(ExecutorDriver* driver, const SlaveInfo& slaveInfo) {
	ByteArray* slaveInfoBytes = protobuf::Serialize(slaveInfo);

	_executorInterface.reregistered(_managedExecutorDriverId, slaveInfoBytes);
	delete slaveInfoBytes;
}

void CliExecutor::disconnected(ExecutorDriver* driver) {
	_executorInterface.disconnected(_managedExecutorDriverId);
}

void CliExecutor::launchTask(ExecutorDriver* driver, const TaskInfo& task) {
	ByteArray* taskBytes = protobuf::Serialize(task);

	_executorInterface.launchTask(_managedExecutorDriverId, taskBytes);
	delete taskBytes;
}

void CliExecutor::killTask(ExecutorDriver* driver, const TaskID& taskId) {
	ByteArray* taskIdBytes = protobuf::Serialize(taskId);

	_executorInterface.killTask(_managedExecutorDriverId, taskIdBytes);
	delete taskIdBytes;

}

void CliExecutor::frameworkMessage(ExecutorDriver* driver, const string& data) {
	ByteArray dataBytes = StringToByteArray(data);
	_executorInterface.frameworkMessage(_managedExecutorDriverId, &dataBytes);
}

void CliExecutor::shutdown(ExecutorDriver* driver) {
	_executorInterface.shutdown(_managedExecutorDriverId);
}

void CliExecutor::error(ExecutorDriver* driver, const string& message) {
	ByteArray messageBytes = StringToByteArray(message);
	_executorInterface.error(_managedExecutorDriverId, &messageBytes);
}

}
