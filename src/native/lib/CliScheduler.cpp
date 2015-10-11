#include "CliScheduler.hpp"
#include "Common.hpp"

namespace mesosclr {

CliScheduler::CliScheduler(long managedSchedulerDriverId, ManagedSchedulerInterface schedulerInterface) {
	_managedSchedulerDriverId = managedSchedulerDriverId;
	_schedulerInterface = schedulerInterface;
}

void CliScheduler::registered(SchedulerDriver* driver, const FrameworkID& frameworkId, const MasterInfo& masterInfo) {
	ByteArray* frameworkIdBytes = protobuf::Serialize(frameworkId);
	ByteArray* masterInfoBytes = protobuf::Serialize(masterInfo);

	_schedulerInterface.registered(_managedSchedulerDriverId, frameworkIdBytes, masterInfoBytes);
	delete frameworkIdBytes;
	delete masterInfoBytes;
}

void CliScheduler::reregistered(SchedulerDriver*, const MasterInfo& masterInfo) {
	ByteArray* masterInfoBytes = protobuf::Serialize(masterInfo);

	_schedulerInterface.reregistered(_managedSchedulerDriverId, masterInfoBytes);
	delete masterInfoBytes;
}

void CliScheduler::disconnected(SchedulerDriver* driver) {
	_schedulerInterface.disconnected(_managedSchedulerDriverId);
}

void CliScheduler::resourceOffers(SchedulerDriver* driver, const vector<Offer>& offers) {
	Collection* offersCollection = protobuf::SerializeVector(offers);
	_schedulerInterface.resourceOffers(_managedSchedulerDriverId, offersCollection);

	//TODO: delete offersCollection
}

void CliScheduler::offerRescinded(SchedulerDriver* driver, const OfferID& offerId) {
	ByteArray* offerIdBytes = protobuf::Serialize(offerId);

	_schedulerInterface.offerRescinded(_managedSchedulerDriverId, offerIdBytes);
	delete offerIdBytes;
}

void CliScheduler::statusUpdate(SchedulerDriver* driver, const TaskStatus& status) {
	ByteArray* statusBytes = protobuf::Serialize(status);

	_schedulerInterface.statusUpdate(_managedSchedulerDriverId, statusBytes);
	delete statusBytes;
}

void CliScheduler::frameworkMessage(SchedulerDriver* driver, const ExecutorID& executorId, const SlaveID& slaveId, const string& data) {
	ByteArray* executorIdBytes = protobuf::Serialize(executorId);
	ByteArray* slaveIdBytes = protobuf::Serialize(slaveId);
	ByteArray dataBytes = StringToByteArray(data);

	_schedulerInterface.frameworkMessage(_managedSchedulerDriverId, executorIdBytes, slaveIdBytes, &dataBytes);
	delete executorIdBytes;
	delete slaveIdBytes;
}

void CliScheduler::slaveLost(SchedulerDriver* driver, const SlaveID& slaveId) {
	ByteArray* slaveIdBytes = protobuf::Serialize(slaveId);

	_schedulerInterface.slaveLost(_managedSchedulerDriverId, slaveIdBytes);
	delete slaveIdBytes;
}

void CliScheduler::executorLost(SchedulerDriver* driver, const ExecutorID& executorId, const SlaveID& slaveId, int status) {
	ByteArray* executorIdBytes = protobuf::Serialize(executorId);
	ByteArray* slaveIdBytes = protobuf::Serialize(slaveId);

	_schedulerInterface.executorLost(_managedSchedulerDriverId, executorIdBytes, slaveIdBytes, status);
	delete executorIdBytes;
	delete slaveIdBytes;
}

void CliScheduler::error(SchedulerDriver* driver, const string& message) {
	ByteArray messageBytes = StringToByteArray(message);
	_schedulerInterface.error(_managedSchedulerDriverId, &messageBytes);
}

}
