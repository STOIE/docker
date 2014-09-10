FROM ubuntu:latest

VOLUME ["/etc/puppet", "/var/lib/puppet", "/var/lib/puppetdb"]

ADD ./apt/trusted.gpg.d/ /etc/apt/trusted.gpg.d/
ADD ./apt/sources.list.d/ /etc/apt/sources.list.d/

RUN apt-get update
RUN apt-get install -y puppetmaster puppetdb puppetmaster-passenger

# Should probably add some common certs here...
# ADD ./puppet/ssl/certs/e38a216fefce.stoie.org.pem /var/lib/pupp...
# ADD ./puppet/ssl/private_keys/e38a216fefce.stoie.org.pem
# ADD ./puppet/ssl/public_keys/e38a216fefce.stoie.org.pem
# ADD ./puppet/ssl/ca/signed/e38a216fefce.stoie.org.pem

# Lets add some config here...
#ADD ./puppet/etc/auth.conf /etc/puppet/auth.conf
#ADD ./puppet/etc/puppet.conf /etc/puppet/puppet.conf
#ADD ./puppet/etc/fileserver.conf /etc/puppet/fileserver.conf

# Add some for puppetdb too...
ADD ./puppetdb/conf.d/config.ini /etc/puppetdb/conf.d/config.ini
ADD ./puppetdb/conf.d/database.ini /etc/puppetdb/conf.d/database.ini

EXPOSE 8140
