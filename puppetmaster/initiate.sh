#!/bin/bash

# do some stuff...

cat /etc/apache2/sites-enabled/puppetmaster.conf |sed s/dd40882ba0ea.stoie.org.pem/$HOSTNAME.stoie.org.pem/g >> /tmp/output;
cat /tmp/output >| /etc/apache2/sites-enabled/puppetmaster.conf;
rm -f /tmp/output

/etc/init.d/rsyslog start
/etc/init.d/apache2 start
/etc/init.d/puppetdb start
