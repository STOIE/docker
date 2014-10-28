FROM ubuntu:latest

MAINTAINER Aaron Nicoli <aaronnicoli@gmail.com>

ENV DEBIAN_FRONTEND noninteractive

ADD ./apt/semiosis-glusterfs-3.5.list /etc/apt/sources.list.d/
ADD ./apt/key /var/key
RUN apt-key add /var/key && rm -f /var/key

RUN apt-get update && apt-get install -yq glusterfs-server

# Copy over some scripts
ADD ./initiate.sh /var/initiate.sh
ADD ./setdns.sh /var/setdns.sh

VOLUME ["/data"]

# Expose the GlusterFS port(s)
EXPOSE 111/udp
EXPOSE 111
EXPOSE 24007
EXPOSE 24008
EXPOSE 24009
EXPOSE 24010
EXPOSE 24011
EXPOSE 24012
EXPOSE 24013

# Expose the NFS port(s)
EXPOSE 38465
EXPOSE 38466
EXPOSE 38467

CMD ["/var/initiate.sh"]
