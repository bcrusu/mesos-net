#ifndef MONO_MONOEXECUTORDRIVER_HPP_
#define MONO_MONOEXECUTORDRIVER_HPP_

#include <mesos/executor.hpp>

#include "MonoExecutor.hpp"

using namespace mesos;

namespace mesosclr {

class MonoExecutorDriver: MesosExecutorDriver {
public:
	MonoExecutorDriver(MonoExecutor* executor);
	MonoExecutor* getExecutor();

private:
	MonoExecutor* _executor;
};

}

#endif /* MONO_MONOEXECUTORDRIVER_HPP_ */
