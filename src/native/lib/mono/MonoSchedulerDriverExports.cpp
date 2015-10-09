#include "MonoSchedulerDriver.hpp"
#include "../Array.hpp"
#include "../ByteArray.hpp"

namespace mesosclr {
extern "C" {
MonoSchedulerDriver* mesosclr_mono_SchedulerDriver_Initialize(long managedSchedulerDriverId, ByteArray* frameworkInfo, std::string* masterAddress,
		bool implicitAcknowledgements, ByteArray* credential) {
	//MonoScheduler* scheduler = new MonoScheduler(managedSchedulerDriverId);
	//MonoSchedulerDriver* driver = new MonoSchedulerDriver(scheduler);
	//return driver;

	return NULL; //TODO
}

void mesosclr_mono_SchedulerDriver_Finalize(MonoSchedulerDriver *driver) {

}

int mesosclr_mono_SchedulerDriver_Start(MonoSchedulerDriver *driver) {
	return 0; //TODO
}

int mesosclr_mono_SchedulerDriver_Stop(MonoSchedulerDriver *driver, bool failover) {
	return 0; //TODO
}

int mesosclr_mono_SchedulerDriver_Abort(MonoSchedulerDriver *driver) {
	return 0; //TODO
}

int mesosclr_mono_SchedulerDriver_Join(MonoSchedulerDriver *driver) {
	return 0; //TODO
}

int mesosclr_mono_SchedulerDriver_RequestResources(MonoSchedulerDriver *driver, Array* requests) {
	return 0; //TODO
}

int mesosclr_mono_SchedulerDriver_LaunchTasksForOffer(MonoSchedulerDriver *driver, ByteArray* offerId, Array tasks, ByteArray* filters) {
	return 0; //TODO
}

int mesosclr_mono_SchedulerDriver_LaunchTasksForOffers(MonoSchedulerDriver *driver, Array* offerIds, Array tasks, ByteArray* filters) {
	return 0; //TODO
}

int mesosclr_mono_SchedulerDriver_KillTask(MonoSchedulerDriver *driver, ByteArray* taskId) {
	return 0; //TODO
}

int mesosclr_mono_SchedulerDriver_AcceptOffers(MonoSchedulerDriver *driver, Array* offerIds, Array* operations, ByteArray* filters) {
	return 0; //TODO
}

int mesosclr_mono_SchedulerDriver_DeclineOffer(MonoSchedulerDriver *driver, ByteArray* offerId, ByteArray* filters) {
	return 0; //TODO
}

int mesosclr_mono_SchedulerDriver_ReviveOffers(MonoSchedulerDriver *driver) {
	return 0; //TODO
}

int mesosclr_mono_SchedulerDriver_SuppressOffers(MonoSchedulerDriver *driver) {
	return 0; //TODO
}

int mesosclr_mono_SchedulerDriver_AcknowledgeStatusUpdate(MonoSchedulerDriver *driver, ByteArray* status) {
	return 0; //TODO
}

int mesosclr_mono_SchedulerDriver_SendFrameworkMessage(MonoSchedulerDriver *driver, ByteArray* executorId, ByteArray* slaveId, ByteArray* data) {
	return 0; //TODO
}

int mesosclr_mono_SchedulerDriver_ReconcileTasks(MonoSchedulerDriver *driver, Array* statuses) {
	return 0; //TODO
}
}
}
