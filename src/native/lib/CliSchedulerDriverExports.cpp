#include "Common.hpp"
#include "CliSchedulerDriver.hpp"
#include "ManagedSchedulerInterface.hpp"

namespace mesosclr {
extern "C" {
CliSchedulerDriver* mesosclr_SchedulerDriver_Initialize(long managedSchedulerDriverId, ManagedSchedulerInterface* schedulerInterface,
		ByteArray* frameworkInfo, std::string* masterAddress, bool implicitAcknowledgements, ByteArray* credential) {
	//CliScheduler* scheduler = new CliScheduler(managedSchedulerDriverId, *schedulerInterface);
	//CliSchedulerDriver* driver = new CliSchedulerDriver(scheduler);
	//return driver;

	return NULL; //TODO
}

void mesosclr_SchedulerDriver_Finalize(CliSchedulerDriver *driver) {

}

int mesosclr_SchedulerDriver_Start(CliSchedulerDriver *driver) {
	return 0; //TODO
}

int mesosclr_SchedulerDriver_Stop(CliSchedulerDriver *driver, bool failover) {
	return 0; //TODO
}

int mesosclr_SchedulerDriver_Abort(CliSchedulerDriver *driver) {
	return 0; //TODO
}

int mesosclr_SchedulerDriver_Join(CliSchedulerDriver *driver) {
	return 0; //TODO
}

int mesosclr_SchedulerDriver_RequestResources(CliSchedulerDriver *driver, Collection* requests) {
	return 0; //TODO
}

int mesosclr_SchedulerDriver_LaunchTasksForOffer(CliSchedulerDriver *driver, ByteArray* offerId, Collection tasks, ByteArray* filters) {
	return 0; //TODO
}

int mesosclr_SchedulerDriver_LaunchTasksForOffers(CliSchedulerDriver *driver, Collection* offerIds, Collection tasks, ByteArray* filters) {
	return 0; //TODO
}

int mesosclr_SchedulerDriver_KillTask(CliSchedulerDriver *driver, ByteArray* taskId) {
	return 0; //TODO
}

int mesosclr_SchedulerDriver_AcceptOffers(CliSchedulerDriver *driver, Collection* offerIds, Collection* operations, ByteArray* filters) {
	return 0; //TODO
}

int mesosclr_SchedulerDriver_DeclineOffer(CliSchedulerDriver *driver, ByteArray* offerId, ByteArray* filters) {
	return 0; //TODO
}

int mesosclr_SchedulerDriver_ReviveOffers(CliSchedulerDriver *driver) {
	return 0; //TODO
}

int mesosclr_SchedulerDriver_SuppressOffers(CliSchedulerDriver *driver) {
	return 0; //TODO
}

int mesosclr_SchedulerDriver_AcknowledgeStatusUpdate(CliSchedulerDriver *driver, ByteArray* status) {
	return 0; //TODO
}

int mesosclr_SchedulerDriver_SendFrameworkMessage(CliSchedulerDriver *driver, ByteArray* executorId, ByteArray* slaveId, ByteArray* data) {
	return 0; //TODO
}

int mesosclr_SchedulerDriver_ReconcileTasks(CliSchedulerDriver *driver, Collection* statuses) {
	return 0; //TODO
}
}
}
