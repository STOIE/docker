FROM ubuntu:latest

MAINTAINER Aaron Nicoli <aaronnicoli@gmail.com>

ADD ./owncloud.list /etc/apt/sources.list.d/owncloud.list
ADD ./owncloud.key /var/owncloud.key
RUN apt-key add /var/owncloud.key && rm -f /var/owncloud.key

RUN echo "#!/bin/sh\nexit 0" > /usr/sbin/policy-rc.d

RUN apt-get update
RUN apt-get install -y owncloud

ADD ./initiate.sh /var/initiate.sh

EXPOSE 80
EXPOSE 443

CMD ["/var/initiate.sh"]
