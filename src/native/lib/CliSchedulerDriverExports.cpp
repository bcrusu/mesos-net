#include "Common.hpp"
#include "CliSchedulerDriver.hpp"
#include "ManagedSchedulerInterface.hpp"

namespace mesosnet {
extern "C" {

CliSchedulerDriver* mesosnet_SchedulerDriver_Initialize(long managedSchedulerDriverId, ManagedSchedulerInterface* schedulerInterface,
		ByteArray* frameworkInfoBytes, const char* masterAddressChars, bool implicitAcknowledgements,
		ByteArray* credentialBytes) {
	FrameworkInfo frameworkInfo = protobuf::Deserialize<FrameworkInfo>(frameworkInfoBytes);
	std::string masterAddress(masterAddressChars);

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

void mesosnet_SchedulerDriver_Finalize(CliSchedulerDriver *driver) {
	CliScheduler *scheduler = driver->getScheduler();
	delete driver;
	delete scheduler;
}

int mesosnet_SchedulerDriver_Start(CliSchedulerDriver *driver) {
	return driver->start();
}

int mesosnet_SchedulerDriver_Stop(CliSchedulerDriver *driver, bool failover) {
	return driver->stop(failover);
}

int mesosnet_SchedulerDriver_Abort(CliSchedulerDriver *driver) {
	return driver->abort();
}

int mesosnet_SchedulerDriver_Join(CliSchedulerDriver *driver) {
	return driver->join();
}

int mesosnet_SchedulerDriver_Run(CliSchedulerDriver *driver) {
	return driver->run();
}

int mesosnet_SchedulerDriver_RequestResources(CliSchedulerDriver *driver, ByteArrayCollection* requests) {
	std::vector<Request> requestsVector = protobuf::DeserializeVector<Request>(requests);
	return driver->requestResources(requestsVector);
}

int mesosnet_SchedulerDriver_LaunchTasks(CliSchedulerDriver *driver, ByteArrayCollection* offerIds, ByteArrayCollection* tasks,
		ByteArray* filtersBytes) {
	std::vector<OfferID> offerIdsVector = protobuf::DeserializeVector<OfferID>(offerIds);
	std::vector<TaskInfo> tasksVector = protobuf::DeserializeVector<TaskInfo>(tasks);
	Filters filters = protobuf::Deserialize<Filters>(filtersBytes);
	return driver->launchTasks(offerIdsVector, tasksVector, filters);
}

int mesosnet_SchedulerDriver_KillTask(CliSchedulerDriver *driver, ByteArray* taskIdBytes) {
	TaskID taskId = protobuf::Deserialize<TaskID>(taskIdBytes);
	return driver->killTask(taskId);
}

int mesosnet_SchedulerDriver_AcceptOffers(CliSchedulerDriver *driver, ByteArrayCollection* offerIds, ByteArrayCollection* operations,
		ByteArray* filtersBytes) {
	std::vector<OfferID> offerIdsVector = protobuf::DeserializeVector<OfferID>(offerIds);
	std::vector<Offer::Operation> operationsVector = protobuf::DeserializeVector<Offer::Operation>(operations);
	Filters filters = protobuf::Deserialize<Filters>(filtersBytes);
	return driver->acceptOffers(offerIdsVector, operationsVector, filters);
}

int mesosnet_SchedulerDriver_DeclineOffer(CliSchedulerDriver *driver, ByteArray* offerIdBytes, ByteArray* filtersBytes) {
	OfferID offerId = protobuf::Deserialize<OfferID>(offerIdBytes);
	Filters filters = protobuf::Deserialize<Filters>(filtersBytes);
	return driver->declineOffer(offerId, filters);
}

int mesosnet_SchedulerDriver_ReviveOffers(CliSchedulerDriver *driver) {
	return driver->reviveOffers();
}

int mesosnet_SchedulerDriver_SuppressOffers(CliSchedulerDriver *driver) {
	return driver->suppressOffers();
}

int mesosnet_SchedulerDriver_AcknowledgeStatusUpdate(CliSchedulerDriver *driver, ByteArray* statusBytes) {
	TaskStatus status = protobuf::Deserialize<TaskStatus>(statusBytes);
	return driver->acknowledgeStatusUpdate(status);
}

int mesosnet_SchedulerDriver_SendFrameworkMessage(CliSchedulerDriver *driver, ByteArray* executorIdBytes, ByteArray* slaveIdBytes,
		ByteArray* dataBytes) {
	ExecutorID executorId = protobuf::Deserialize<ExecutorID>(executorIdBytes);
	SlaveID slaveId = protobuf::Deserialize<SlaveID>(slaveIdBytes);
	std::string data(dataBytes->Data, dataBytes->Size);
	return driver->sendFrameworkMessage(executorId, slaveId, data);
}

int mesosnet_SchedulerDriver_ReconcileTasks(CliSchedulerDriver *driver, ByteArrayCollection* statuses) {
	std::vector<TaskStatus> statusesVector = protobuf::DeserializeVector<TaskStatus>(statuses);
	return driver->reconcileTasks(statusesVector);
}
}
}
