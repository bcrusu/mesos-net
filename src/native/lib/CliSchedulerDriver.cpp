#include "CliSchedulerDriver.hpp"

namespace mesosnet {

CliSchedulerDriver::CliSchedulerDriver(CliScheduler* scheduler, const FrameworkInfo& framework, const std::string& master,
		bool implicitAcknowledgements) :
		MesosSchedulerDriver(scheduler, framework, master, implicitAcknowledgements) {
	_scheduler = scheduler;
}

CliSchedulerDriver::CliSchedulerDriver(CliScheduler* scheduler, const FrameworkInfo& framework, const std::string& master,
		bool implicitAcknowledgements, const Credential& credential) :
		MesosSchedulerDriver(scheduler, framework, master, implicitAcknowledgements, credential) {
	_scheduler = scheduler;
}

CliScheduler* CliSchedulerDriver::getScheduler() {
	return _scheduler;
}

}
