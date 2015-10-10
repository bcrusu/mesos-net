#ifndef CLIEXECUTOR_HPP_
#define CLIEXECUTOR_HPP_

#include <string>
#include <mesos/executor.hpp>

#include "ManagedExecutorInterface.hpp"

using std::string;
using namespace mesos;

namespace mesosclr {

class CliExecutor: public Executor {
public:
	CliExecutor(long managedExecutorDriverId, ManagedExecutorInterface executorInterface);

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
	ManagedExecutorInterface _executorInterface;
};

}

#endif /* CLIEXECUTOR_HPP_ */
