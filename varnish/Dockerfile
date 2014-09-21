FROM registry/ubuntu:base_latest

MAINTAINER Aaron Nicoli <aaronnicoli@gmail.com>


RUN apt-get update
RUN apt-get install -y curl

RUN curl -x http://httpproxy.ris.environment.gov.au:3128 https://repo.varnish-cache.org/ubuntu/GPG-key.txt | apt-key add -
RUN echo "deb https://repo.varnish-cache.org/ubuntu/ precise varnish-4.0" >> /etc/apt/sources.list.d/varnish-cache.list

RUN apt-get update
RUN apt-get install -y varnish
