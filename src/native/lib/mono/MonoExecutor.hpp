#ifndef MONO_MONOEXECUTOR_HPP_
#define MONO_MONOEXECUTOR_HPP_

#include <string>
#include <mesos/executor.hpp>

using std::string;
using namespace mesos;

namespace mesosclr {

class MonoExecutor: public Executor {
public:
	MonoExecutor(long managedExecutorDriverId);

	virtual void registered(ExecutorDriver* driver, const ExecutorInfo& executorInfo, const FrameworkInfo& frameworkInfo, const SlaveInfo& slaveInfo);
	virtual void reregistered(ExecutorDriver* driver, const SlaveInfo& slaveInfo);
	virtual void disconnected(ExecutorDriver* driver);
	virtual void launchTask(ExecutorDriver* driver, const TaskInfo& task);
	virtual void killTask(ExecutorDriver* driver, const TaskID& taskId);
	virtual void frameworkMessage(ExecutorDriver* driver, const string& data);
	virtual void shutdown(ExecutorDriver* driver);
	virtual void error(ExecutorDriver* driver, const string& message);

private:
	long _managedExecutorDriverId;
};

}

#endif /* MONO_MONOEXECUTOR_HPP_ */
