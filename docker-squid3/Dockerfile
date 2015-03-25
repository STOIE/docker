FROM ubuntu:latest

MAINTAINER Aaron Nicoli <aaronnicoli@gmail.com>


RUN apt-get update && apt-get install -y squid3
ADD ./run.sh /var/run.sh

RUN mkdir -p /var/default/squid3 && cp -R /etc/squid3/* /var/default/squid3/


VOLUME ["/etc/squid3", "/var/spool/squid3"]

EXPOSE 3128

CMD ["/var/run.sh"]
