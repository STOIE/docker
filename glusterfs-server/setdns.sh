#!/bin/bash

if [ "$1" == "" ] ; then
  exit 0;
fi

DDNS=$(cat /etc/resolv.conf |grep nameserver |head -n 1 |awk '{ print ( $2 ) }');
wget --no-check-certificate --http-user="gv$1" --http-passwd="asdfgfs$1" -q http://$DDNS
