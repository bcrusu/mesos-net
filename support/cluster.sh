#!/bin/bash

SCRIPTDIR=$(cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )
CLUSTERDIR=${SCRIPTDIR}/cluster_work_dir

APPDIR=${SCRIPTDIR}/../src/main/com.bcrusu.mesosclr.Rendler/bin/Debug/
SOPATH=${SCRIPTDIR}/../src/native/Debug/libmesosclr.so

copy_files() {
	if [ ! -f "$SOPATH" ]; then
		echo "Could not find SO: $SOPATH..."
		exit 1
	fi

	if [ ! -f "$APPDIR/rendler.exe" ]; then
		echo "Could not find rendler: $APPDIR/rendler.exe..."
		exit 1
	fi

	if [ ! -d "$CLUSTERDIR" ]; then
		echo "Creating cluster work dir at: $CLUSTERDIR..."
		mkdir "$CLUSTERDIR"
	fi

	cp -u $APPDIR/* $CLUSTERDIR
	cp -u $SOPATH   $CLUSTERDIR
}

start_cluster() {
	copy_files

	#TODO:
#mesos-master --cluster=mesosclr --allocation_interval=1secs --port=5050 --registry=in_memory --quorum=1 --quiet --log_dir=/cluster/master/logs --work_dir=/cluster/master/data
#mesos-slave --master=127.0.0.33:5050 --port=5051 --resources=cpus:2;mem:512 --attributes=name:slave1 --quiet --frameworks_home=/cluster --log_dir=/cluster/slave1/logs --work_dir=/cluster/slave1/data
}

stop_cluster() {
	#TODO ps | grep | kill
}

clean_cluster_dir() {			
	echo "Removing cluster work dir at: $CLUSTERDIR..."
	sudo rm -rf "$CLUSTERDIR"
}


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


