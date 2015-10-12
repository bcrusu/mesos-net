#!/bin/bash

SCRIPTDIR=$(cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd)

WORKDIR=${SCRIPTDIR}/work
APPWORKDIR=${WORKDIR}/rendler

APPBUILDDIR=${SCRIPTDIR}/../src/main/com.bcrusu.mesosclr.Rendler/bin/Debug
SOBUILDPATH=${SCRIPTDIR}/../src/native/Debug/libmesosclr.so

copy_files() {
	if [ ! -f "$SOBUILDPATH" ]; then
		echo "Could not find libmesosclr.so: $SOBUILDPATH..."
		exit 1
	fi

	if [ ! -f "$APPBUILDDIR/rendler.exe" ]; then
		echo "Could not find rendler.exe : $APPBUILDDIR/rendler.exe..."
		exit 1
	fi

	if [ ! -d "$WORKDIR" ]; then
		echo "Creating cluster work dir: $WORKDIR..."
		mkdir "$WORKDIR"
	fi

	if [ ! -d "$APPWORKDIR" ]; then
		echo "Creating rendler work dir: $APPWORKDIR..."
		mkdir "$APPWORKDIR"
	fi

	cp -u $APPBUILDDIR/* $APPWORKDIR
	cp -u $SOBUILDPATH   $APPWORKDIR
}

start_cluster() {
	copy_files

	export LD_LIBRARY_PATH=/usr/local/lib:${APPWORKDIR}:${LD_LIBRARY_PATH}

	mesos-master --cluster=mesosclr --ip=127.0.0.50 --port=5050 --allocation_interval=1secs --registry=in_memory --quorum=1 --quiet \
		--log_dir=${WORKDIR}/master/logs --work_dir=${WORKDIR}/master/data &

	sleep 0.3s

	mesos-slave --master=127.0.0.50:5050 --ip=127.0.0.51 --port=5051 --resources="cpus:2;mem:512" --attributes=name:slave1 --quiet \
		--frameworks_home=${WORKDIR} --log_dir=${WORKDIR}/slave1/logs --work_dir=${WORKDIR}/slave1/data &

	sleep 0.1s

	mesos-slave --master=127.0.0.50:5050 --ip=127.0.0.52 --port=5052 --resources="cpus:2;mem:512" --attributes=name:slave2 --quiet \
		--frameworks_home=${WORKDIR} --log_dir=${WORKDIR}/slave2/logs --work_dir=${WORKDIR}/slave2/data &

	sleep 0.1s

	mesos-slave --master=127.0.0.50:5050 --ip=127.0.0.53 --port=5053 --resources="cpus:2;mem:512" --attributes=name:slave3 --quiet \
		--frameworks_home=${WORKDIR} --log_dir=${WORKDIR}/slave3/logs --work_dir=${WORKDIR}/slave3/data &
}

stop_cluster() {
	killall -q mesos-slave
	killall -q mesos-master
	#TODO: kill rendler executors
}

clean_cluster_dir() {
	echo "Removing cluster work dir at: $WORKDIR..."
	rm -rf "$WORKDIR"

	echo "Removing temp Mesos dir at: /tmp/mesos..."
	rm -rf /tmp/mesos
}

if [ "$(id -u)" != "0" ]; then
   echo "Mesos requires to be executed as root."
   exit 1
fi

if [ -z "$SCRIPTDIR" ]; then
	echo "Could not detect current script dir..."
	exit 1
fi

case "$1" in
	start)
		echo "Starting..."
		start_cluster
		echo "Done."
		;;
	stop)
		echo "Stopping..."
		stop_cluster
		echo "Done."
		;;
	clean)
		echo "Cleaning cluster dir..."
		clean_cluster_dir
		echo "Done."
		;;
	restart)
		echo "Restarting..."
		stop_cluster
	
		if [ "$2" = "-c" ]; then
			clean_cluster_dir
		fi

		start_cluster
		echo "Done."
		;;
	*)
		echo "Usage: cluster {start|stop|restart|clean}"
		exit 1
		;;
esac
