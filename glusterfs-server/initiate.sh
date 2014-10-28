#!/bin/bash

# Create dirs
if [ ! -d /var/log/supervisor ] ; then
  mkdir /var/log/supervisor
fi
if [ ! -d /var/log/glusterfs ] ; then
  mkdir /var/log/glusterfs
fi
if [ ! -d /data/brick ] ; then
  mkdir /data/brick
fi

# Set some pems
if [ -d /var/log/supervisor ] ; then
  chmod 755 /var/log/supervisor
  chown root:root /var/log/supervisor
fi
if [ -d /var/log/glusterfs ] ; then
  chmod 755 /var/log/glusterfs
  chown root:root /var/log/glusterfs
fi

if [ -f /data/host ] ; then
  HOSTNUM=$(cat /data/host);
  if [ "${HOSTNUM}" != "" ] ; then
    /var/setdns.sh $HOSTNUM
    /usr/sbin/glusterd
    sleep 10
  else
    exit 1;
  fi
else
  exit 1;
fi

/etc/init.d/rsyslog start
/etc/init.d/supervisor start

tail -f /var/log/syslog
