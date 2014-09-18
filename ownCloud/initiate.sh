#!/bin/bash

if [ ! -d /var/www/owncloud/data ] ; then
  mkdir /var/www/owncloud/data
  chmod 755
fi

if [ -d /var/www/owncloud ] ; then
  chown -R www-data:www-data /var/www/owncloud
fi

/etc/init.d/rsyslog start
/etc/init.d/apache2 start

tail -f /var/log/syslog
