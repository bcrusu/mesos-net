#ifndef CLIEXECUTORDRIVER_HPP_
#define CLIEXECUTORDRIVER_HPP_

#include <mesos/executor.hpp>

#include "CliExecutor.hpp"

using namespace mesos;

namespace mesosclr {

class CliExecutorDriver: MesosExecutorDriver {
public:
	CliExecutorDriver(CliExecutor* executor);
	CliExecutor* getExecutor();

private:
	CliExecutor* _executor;
};

}

#endif /* CLIEXECUTORDRIVER_HPP_ */
