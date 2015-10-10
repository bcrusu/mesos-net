#ifndef MANAGEDSCHEDULERINTERFACE_HPP_
#define MANAGEDSCHEDULERINTERFACE_HPP_

#include "Common.hpp"

namespace mesosclr {

typedef void Scheduler_Registered(long managedDriverId, ByteArray* frameworkId, ByteArray* masterInfo);
typedef void Scheduler_Reregistered(long managedDriverId, ByteArray* masterInfo);
typedef void Scheduler_ResourceOffers(long managedDriverId, ByteArray* offers);
typedef void Scheduler_OfferRescinded(long managedDriverId, ByteArray* offerId);
typedef void Scheduler_StatusUpdate(long managedDriverId, ByteArray* status);
typedef void Scheduler_FrameworkMessage(long managedDriverId, ByteArray* executorId, ByteArray* slaveId, ByteArray* data);
typedef void Scheduler_Disconnected(long managedDriverId);
typedef void Scheduler_SlaveLost(long managedDriverId, ByteArray* slaveId);
typedef void Scheduler_ExecutorLost(long managedDriverId, ByteArray* executorId, ByteArray* slaveId, int status);
typedef void Scheduler_Error(long managedDriverId, ByteArray* message);

class ManagedSchedulerInterface {
	Scheduler_Registered* registered;
	Scheduler_Reregistered* reregistered;
	Scheduler_ResourceOffers* resourceOffers;
	Scheduler_OfferRescinded* offerRescinded;
	Scheduler_StatusUpdate* statusUpdate;
	Scheduler_FrameworkMessage* frameworkMessage;
	Scheduler_Disconnected* disconnected;
	Scheduler_SlaveLost* slaveLost;
	Scheduler_ExecutorLost * executorLost;
	Scheduler_Error* error;
};

}

#endif /* MANAGEDSCHEDULERINTERFACE_HPP_ */
