#include "Common.hpp"
#include "CliExecutorDriver.hpp"
#include "ManagedExecutorInterface.hpp"

namespace mesosclr {
extern "C" {

CliExecutorDriver* mesosclr_ExecutorDriver_Initialize(long managedExecutorDriverId, ManagedExecutorInterface* executorInterface) {
	CliExecutor* executor = new CliExecutor(managedExecutorDriverId, *executorInterface);
	CliExecutorDriver* driver = new CliExecutorDriver(executor);

	return driver;
}

void mesosclr_ExecutorDriver_Finalize(CliExecutorDriver *driver) {
	CliExecutor *executor = driver->getExecutor();
	delete driver;
	delete executor;
}

int mesosclr_ExecutorDriver_Start(CliExecutorDriver *driver) {
	return driver->start();
}

int mesosclr_ExecutorDriver_Stop(CliExecutorDriver *driver) {
	return driver->stop();
}

int mesosclr_ExecutorDriver_Abort(CliExecutorDriver *driver) {
	return driver->abort();
}

int mesosclr_ExecutorDriver_Join(CliExecutorDriver *driver) {
	return driver->join();
}

int mesosclr_ExecutorDriver_Run(CliExecutorDriver *driver) {
	return driver->run();
}

int mesosclr_ExecutorDriver_SendStatusUpdate(CliExecutorDriver *driver, ByteArray* taskStatusBytes) {
	TaskStatus taskStatus = protobuf::Deserialize<TaskStatus>(taskStatusBytes);
	return driver->sendStatusUpdate(taskStatus);
}

int mesosclr_ExecutorDriver_SendFrameworkMessage(CliExecutorDriver *driver, ByteArray* data) {
	std::string dataString(data->Data, data->Size);
	return driver->sendFrameworkMessage(dataString);
}

}
}
