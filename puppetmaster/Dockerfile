FROM ubuntu:latest

ADD ./apt/trusted.gpg.d/ /etc/apt/trusted.gpg.d/
ADD ./apt/sources.list.d/ /etc/apt/sources.list.d/

RUN apt-get update
RUN apt-get install -y puppetmaster puppetdb puppetmaster-passenger

EXPOSE 8140
