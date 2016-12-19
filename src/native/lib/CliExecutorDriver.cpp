#include "CliExecutorDriver.hpp"

namespace mesosnet {

CliExecutorDriver::CliExecutorDriver(CliExecutor* executor) :
		MesosExecutorDriver(executor) {
	_executor = executor;
}

CliExecutor* CliExecutorDriver::getExecutor() {
	return _executor;
}

}
