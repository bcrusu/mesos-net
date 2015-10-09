#include "MonoExecutor.hpp"

namespace mesosclr {

MonoExecutor::MonoExecutor(long managedExecutorDriverId) {
	_managedExecutorDriverId = managedExecutorDriverId;
}

void MonoExecutor::shutdown(ExecutorDriver* driver) {
	//TODO:
}

}
