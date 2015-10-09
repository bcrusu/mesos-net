#include "MonoExecutor.hpp"

namespace mesosclr {

MonoExecutor::MonoExecutor(long managedDriverId) {
	_managedDriverId = managedDriverId;
}

void MonoExecutor::shutdown(ExecutorDriver* driver) {
	//TODO:
}

}
