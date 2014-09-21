FROM registry/ubuntu:base_latest

MAINTAINER Aaron Nicoli <aaronnicoli@gmail.com>


RUN apt-get update
RUN apt-get install -y curl

RUN curl -x http://httpproxy.somedomain:3128 http://www.rabbitmq.com/rabbitmq-signing-key-public.asc | apt-key add -
RUN echo "deb http://www.rabbitmq.com/debian/ testing main" > /etc/apt/sources.list.d/rabbitmq.list

RUN apt-get update
RUN apt-get install -y rabbitmq-server

# Logging + ulimit -n 2048
# rabbitmqctl start
