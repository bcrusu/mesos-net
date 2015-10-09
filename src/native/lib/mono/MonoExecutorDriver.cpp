#include "MonoExecutorDriver.hpp"

namespace mesosclr {

MonoExecutorDriver::MonoExecutorDriver(MonoExecutor* executor) :
		MesosExecutorDriver(executor) {
	_executor = executor;
}

MonoExecutor* MonoExecutorDriver::getExecutor() {
	return _executor;
}

}
