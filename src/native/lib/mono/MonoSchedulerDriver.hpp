#ifndef MONO_MONOSCHEDULERDRIVER_HPP_
#define MONO_MONOSCHEDULERDRIVER_HPP_

#include <mesos/scheduler.hpp>
#include "MonoScheduler.hpp"

using namespace mesos;

namespace mesosclr {

class MonoSchedulerDriver: MesosSchedulerDriver {
public:
	MonoSchedulerDriver(MonoScheduler* scheduler, const FrameworkInfo& framework, const std::string& master, bool implicitAcknowledgements);
	MonoSchedulerDriver(MonoScheduler* scheduler, const FrameworkInfo& framework, const std::string& master, bool implicitAcknowledgements,
			const Credential& credential);

	MonoScheduler* getScheduler();

private:
	MonoScheduler* _scheduler;
};

}

#endif /* MONO_MONOSCHEDULERDRIVER_HPP_ */
