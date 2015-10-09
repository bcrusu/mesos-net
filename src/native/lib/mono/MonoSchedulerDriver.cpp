#include "MonoSchedulerDriver.hpp"

namespace mesosclr {

MonoSchedulerDriver::MonoSchedulerDriver(MonoScheduler* scheduler, const FrameworkInfo& framework, const std::string& master,
		bool implicitAcknowledgements) :
		MesosSchedulerDriver(scheduler, framework, master, implicitAcknowledgements) {
	_scheduler = scheduler;
}

MonoSchedulerDriver::MonoSchedulerDriver(MonoScheduler* scheduler, const FrameworkInfo& framework, const std::string& master,
		bool implicitAcknowledgements, const Credential& credential) :
		MesosSchedulerDriver(scheduler, framework, master, implicitAcknowledgements, credential) {
	_scheduler = scheduler;
}

MonoScheduler* MonoSchedulerDriver::getScheduler() {
	return _scheduler;
}

}
