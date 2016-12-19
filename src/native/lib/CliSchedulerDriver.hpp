#ifndef CLISCHEDULERDRIVER_HPP_
#define CLISCHEDULERDRIVER_HPP_

#include <mesos/scheduler.hpp>

#include "CliScheduler.hpp"

using namespace mesos;

namespace mesosnet {

class CliSchedulerDriver: public MesosSchedulerDriver {
public:
	CliSchedulerDriver(CliScheduler* scheduler, const FrameworkInfo& framework, const std::string& master, bool implicitAcknowledgements);
	CliSchedulerDriver(CliScheduler* scheduler, const FrameworkInfo& framework, const std::string& master, bool implicitAcknowledgements,
			const Credential& credential);

	CliScheduler* getScheduler();

private:
	CliScheduler* _scheduler;
};

}

#endif /* CLISCHEDULERDRIVER_HPP_ */
