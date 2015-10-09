#include "MonoExecutorDriver.hpp"
#include "../ByteArray.hpp"
#include <mono/metadata/appdomain.h>

namespace mesosclr {
extern "C" {
MonoExecutorDriver* mesosclr_mono_ExecutorDriver_Initialize(long managedExecutorDriverId) {
	MonoExecutor* executor = new MonoExecutor(managedExecutorDriverId);
	MonoExecutorDriver* driver = new MonoExecutorDriver(executor);

	//TODO: MonoDomain *a = mono_domain_get();

	return driver;
}

void mesosclr_mono_ExecutorDriver_Finalize(MonoExecutorDriver *driver) {
	MonoExecutor *executor = driver->getExecutor();
	delete driver;
	delete executor;
}

int mesosclr_mono_ExecutorDriver_Start(MonoExecutorDriver *driver) {
	return 0; //TODO
}

int mesosclr_mono_ExecutorDriver_Stop(MonoExecutorDriver *driver) {
	return 0; //TODO
}

int mesosclr_mono_ExecutorDriver_Abort(MonoExecutorDriver *driver) {
	return 0; //TODO
}

int mesosclr_mono_ExecutorDriver_Join(MonoExecutorDriver *driver) {
	return 0; //TODO
}

int mesosclr_mono_ExecutorDriver_SendStatusUpdate(MonoExecutorDriver *driver, ByteArray* status) {
	return 0; //TODO
}

int mesosclr_mono_ExecutorDriver_SendFrameworkMessage(MonoExecutorDriver *driver, ByteArray* data) {
	return 0; //TODO
}
}
}
