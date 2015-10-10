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
typedef void Scheduler_Error(long managedDriverId, char* message);

class ManagedSchedulerInterface {
	Scheduler_Registered* Registered;
	Scheduler_Reregistered* Reregistered;
	Scheduler_ResourceOffers* ResourceOffers;
	Scheduler_OfferRescinded* OfferRescinded;
	Scheduler_StatusUpdate* StatusUpdate;
	Scheduler_FrameworkMessage* FrameworkMessage;
	Scheduler_Disconnected* Disconnected;
	Scheduler_SlaveLost* SlaveLost;
	Scheduler_ExecutorLost * ExecutorLost;
	Scheduler_Error* Error;
};

}

#endif /* MANAGEDSCHEDULERINTERFACE_HPP_ */
