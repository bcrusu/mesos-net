#include "CliExecutor.hpp"

namespace mesosclr {

CliExecutor::CliExecutor(long managedExecutorDriverId) {
	_managedExecutorDriverId = managedExecutorDriverId;
}

void CliExecutor::shutdown(ExecutorDriver* driver) {
	//TODO:
}

}
