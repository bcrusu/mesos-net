#include "Common.hpp"
#include "CliSchedulerDriver.hpp"
#include "ManagedSchedulerInterface.hpp"

namespace mesosclr {
extern "C" {

CliSchedulerDriver* mesosclr_SchedulerDriver_Initialize(long managedSchedulerDriverId, ManagedSchedulerInterface* schedulerInterface,
		ByteArray* frameworkInfoBytes, ByteArray* masterAddressBytes, bool implicitAcknowledgements,
		ByteArray* credentialBytes) {
	FrameworkInfo frameworkInfo = protobuf::Deserialize<FrameworkInfo>(frameworkInfoBytes);
	std::string masterAddress(masterAddressBytes->Data, masterAddressBytes->Size);
	Credential credential = protobuf::Deserialize<Credential>(credentialBytes);

	CliScheduler* scheduler = new CliScheduler(managedSchedulerDriverId, *schedulerInterface);
	CliSchedulerDriver* driver;

	if (credentialBytes) {
		Credential credential = protobuf::Deserialize<Credential>(credentialBytes);
		driver = new CliSchedulerDriver(scheduler, frameworkInfo, masterAddress, implicitAcknowledgements, credential);
	} else {
		driver = new CliSchedulerDriver(scheduler, frameworkInfo, masterAddress, implicitAcknowledgements);
	}

	return driver;
}

void mesosclr_SchedulerDriver_Finalize(CliSchedulerDriver *driver) {
	CliScheduler *scheduler = driver->getScheduler();
	delete driver;
	delete scheduler;
}

int mesosclr_SchedulerDriver_Start(CliSchedulerDriver *driver) {
	return driver->start();
}

int mesosclr_SchedulerDriver_Stop(CliSchedulerDriver *driver, bool failover) {
	return driver->stop(failover);
}

int mesosclr_SchedulerDriver_Abort(CliSchedulerDriver *driver) {
	return driver->abort();
}

int mesosclr_SchedulerDriver_Join(CliSchedulerDriver *driver) {
	return driver->join();
}

int mesosclr_SchedulerDriver_Run(CliSchedulerDriver *driver) {
	return driver->run();
}

int mesosclr_SchedulerDriver_RequestResources(CliSchedulerDriver *driver, ByteArrayCollection* requests) {
	std::vector<Request> requestsVector = protobuf::DeserializeVector<Request>(requests);
	return driver->requestResources(requestsVector);
}

int mesosclr_SchedulerDriver_LaunchTasks(CliSchedulerDriver *driver, ByteArrayCollection* offerIds, ByteArrayCollection* tasks,
		ByteArray* filtersBytes) {
	std::vector<OfferID> offerIdsVector = protobuf::DeserializeVector<OfferID>(offerIds);
	std::vector<TaskInfo> tasksVector = protobuf::DeserializeVector<TaskInfo>(tasks);
	Filters filters = protobuf::Deserialize<Filters>(filtersBytes);
	return driver->launchTasks(offerIdsVector, tasksVector, filters);
}

int mesosclr_SchedulerDriver_KillTask(CliSchedulerDriver *driver, ByteArray* taskIdBytes) {
	TaskID taskId = protobuf::Deserialize<TaskID>(taskIdBytes);
	return driver->killTask(taskId);
}

int mesosclr_SchedulerDriver_AcceptOffers(CliSchedulerDriver *driver, ByteArrayCollection* offerIds, ByteArrayCollection* operations,
		ByteArray* filtersBytes) {
	std::vector<OfferID> offerIdsVector = protobuf::DeserializeVector<OfferID>(offerIds);
	std::vector<Offer::Operation> operationsVector = protobuf::DeserializeVector<Offer::Operation>(operations);
	Filters filters = protobuf::Deserialize<Filters>(filtersBytes);
	return driver->acceptOffers(offerIdsVector, operationsVector, filters);
}

int mesosclr_SchedulerDriver_DeclineOffer(CliSchedulerDriver *driver, ByteArray* offerIdBytes, ByteArray* filtersBytes) {
	OfferID offerId = protobuf::Deserialize<OfferID>(offerIdBytes);
	Filters filters = protobuf::Deserialize<Filters>(filtersBytes);
	return driver->declineOffer(offerId, filters);
}

int mesosclr_SchedulerDriver_ReviveOffers(CliSchedulerDriver *driver) {
	return driver->reviveOffers();
}

int mesosclr_SchedulerDriver_SuppressOffers(CliSchedulerDriver *driver) {
	return driver->suppressOffers();
}

int mesosclr_SchedulerDriver_AcknowledgeStatusUpdate(CliSchedulerDriver *driver, ByteArray* statusBytes) {
	TaskStatus status = protobuf::Deserialize<TaskStatus>(statusBytes);
	return driver->acknowledgeStatusUpdate(status);
}

int mesosclr_SchedulerDriver_SendFrameworkMessage(CliSchedulerDriver *driver, ByteArray* executorIdBytes, ByteArray* slaveIdBytes,
		ByteArray* dataBytes) {
	ExecutorID executorId = protobuf::Deserialize<ExecutorID>(executorIdBytes);
	SlaveID slaveId = protobuf::Deserialize<SlaveID>(slaveIdBytes);
	std::string data(dataBytes->Data, dataBytes->Size);
	return driver->sendFrameworkMessage(executorId, slaveId, data);
}

int mesosclr_SchedulerDriver_ReconcileTasks(CliSchedulerDriver *driver, ByteArrayCollection* statuses) {
	std::vector<TaskStatus> statusesVector = protobuf::DeserializeVector<TaskStatus>(statuses);
	return driver->reconcileTasks(statusesVector);
}
}
}
