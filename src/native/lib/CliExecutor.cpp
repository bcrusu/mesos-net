#include "CliExecutor.hpp"

namespace mesosclr {

CliExecutor::CliExecutor(long managedExecutorDriverId, ManagedExecutorInterface executorInterface) {
	_managedExecutorDriverId = managedExecutorDriverId;
	_executorInterface = executorInterface;
}

void CliExecutor::shutdown(ExecutorDriver* driver) {
	//TODO:
}

}
