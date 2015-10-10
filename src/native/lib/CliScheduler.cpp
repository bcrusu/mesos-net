#include "CliScheduler.hpp"

namespace mesosclr {

CliScheduler::CliScheduler(long managedSchedulerDriverId, ManagedSchedulerInterface schedulerInterface) {
	_managedSchedulerDriverId = managedSchedulerDriverId;
	_schedulerInterface = schedulerInterface;
}

}
