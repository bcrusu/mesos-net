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
	return 0; //TODO
}

int mesosclr_ExecutorDriver_Stop(CliExecutorDriver *driver) {
	return 0; //TODO
}

int mesosclr_ExecutorDriver_Abort(CliExecutorDriver *driver) {
	return 0; //TODO
}

int mesosclr_ExecutorDriver_Join(CliExecutorDriver *driver) {
	return 0; //TODO
}

int mesosclr_ExecutorDriver_SendStatusUpdate(CliExecutorDriver *driver, ByteArray* status) {
	return 0; //TODO
}

int mesosclr_ExecutorDriver_SendFrameworkMessage(CliExecutorDriver *driver, ByteArray* data) {
	return 0; //TODO
}

}
}
