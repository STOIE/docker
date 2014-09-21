FROM registry/ubuntu:base_latest

MAINTAINER Aaron Nicoli <aaronnicoli@gmail.com>


RUN apt-get update
RUN apt-get install -y libevent-dev wget

WORKDIR /tmp/
RUN wget http://memcached.org/latest -e use_proxy=yes -e http_proxy=httpproxy.somedomain:3128
RUN tar -zxvf memcached-*.tar.gz && cd ./memcached-* && ./configure && make && make test && sudo make install
RUN rm -rf ./memcached-*

WORKDIR /
