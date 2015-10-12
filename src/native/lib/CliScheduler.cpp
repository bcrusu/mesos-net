#include "CliScheduler.hpp"
#include "Common.hpp"

namespace mesosclr {

CliScheduler::CliScheduler(long managedSchedulerDriverId, ManagedSchedulerInterface schedulerInterface) {
	_managedSchedulerDriverId = managedSchedulerDriverId;
	_schedulerInterface = schedulerInterface;
}

void CliScheduler::registered(SchedulerDriver* driver, const FrameworkID& frameworkId, const MasterInfo& masterInfo) {
	ScopedByteArray frameworkIdBytes = protobuf::Serialize(frameworkId);
	ScopedByteArray masterInfoBytes = protobuf::Serialize(masterInfo);
	_schedulerInterface.registered(_managedSchedulerDriverId, frameworkIdBytes.Ptr(), masterInfoBytes.Ptr());
}

void CliScheduler::reregistered(SchedulerDriver*, const MasterInfo& masterInfo) {
	ScopedByteArray masterInfoBytes = protobuf::Serialize(masterInfo);
	_schedulerInterface.reregistered(_managedSchedulerDriverId, masterInfoBytes.Ptr());
}

void CliScheduler::disconnected(SchedulerDriver* driver) {
	_schedulerInterface.disconnected(_managedSchedulerDriverId);
}

void CliScheduler::resourceOffers(SchedulerDriver* driver, const vector<Offer>& offers) {
	ScopedByteArrayCollection offersCollection = protobuf::SerializeVector(offers);
	_schedulerInterface.resourceOffers(_managedSchedulerDriverId, offersCollection.Ptr());
}

void CliScheduler::offerRescinded(SchedulerDriver* driver, const OfferID& offerId) {
	ScopedByteArray offerIdBytes = protobuf::Serialize(offerId);
	_schedulerInterface.offerRescinded(_managedSchedulerDriverId, offerIdBytes.Ptr());
}

void CliScheduler::statusUpdate(SchedulerDriver* driver, const TaskStatus& status) {
	ScopedByteArray statusBytes = protobuf::Serialize(status);
	_schedulerInterface.statusUpdate(_managedSchedulerDriverId, statusBytes.Ptr());
}

void CliScheduler::frameworkMessage(SchedulerDriver* driver, const ExecutorID& executorId, const SlaveID& slaveId, const string& data) {
	ScopedByteArray executorIdBytes = protobuf::Serialize(executorId);
	ScopedByteArray slaveIdBytes = protobuf::Serialize(slaveId);
	ByteArray dataBytes = StringToByteArray(data);
	_schedulerInterface.frameworkMessage(_managedSchedulerDriverId, executorIdBytes.Ptr(), slaveIdBytes.Ptr(), &dataBytes);
}

void CliScheduler::slaveLost(SchedulerDriver* driver, const SlaveID& slaveId) {
	ScopedByteArray slaveIdBytes = protobuf::Serialize(slaveId);
	_schedulerInterface.slaveLost(_managedSchedulerDriverId, slaveIdBytes.Ptr());
}

void CliScheduler::executorLost(SchedulerDriver* driver, const ExecutorID& executorId, const SlaveID& slaveId, int status) {
	ScopedByteArray executorIdBytes = protobuf::Serialize(executorId);
	ScopedByteArray slaveIdBytes = protobuf::Serialize(slaveId);
	_schedulerInterface.executorLost(_managedSchedulerDriverId, executorIdBytes.Ptr(), slaveIdBytes.Ptr(), status);
}

void CliScheduler::error(SchedulerDriver* driver, const string& message) {
	_schedulerInterface.error(_managedSchedulerDriverId, message.c_str());
}

}
