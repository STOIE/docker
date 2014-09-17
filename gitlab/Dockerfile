FROM ubuntu:latest

MAINTAINER Aaron Nicoli <aaronnicoli@gmail.com>

ADD ./apt/brightbox-ruby-ng.key /var/
ADD ./apt/git-core-ppa.key /var/
ADD ./apt/nginx-stable.key /var/

RUN apt-key add /var/brightbox-ruby-ng.key && rm -f /var/brightbox-ruby-ng.key
RUN apt-key add /var/git-core-ppa.key && rm -f /var/git-core-ppa.key
RUN apt-key add /var/nginx-stable.key && rm -f /var/nginx-stable.key

RUN apt-get update
RUN apt-get install -y build-essential cmake postgresql-client \
      nginx git-core openssh-server redis-server python2.7 python-docutils \
      libpq-dev zlib1g-dev libyaml-dev libssl-dev \
      libgdbm-dev libreadline-dev libncurses5-dev libffi-dev \
      libxml2-dev libxslt-dev libcurl4-openssl-dev libicu-dev \
      ruby2.0 ruby2.0-dev
RUN gem install --no-ri --no-rdoc bundler

ADD ./assets/setup /app/setup
RUN chmod 755 /app/setup/install
RUN /app/setup/install

ADD ./assets/config /app/setup/config
ADD ./assets/init /app/init
RUN chmod 755 /app/init

EXPOSE 22
EXPOSE 80
EXPOSE 443

VOLUME ["/home/git/data"]

#
# Run using:
#   docker run --name='gitlab' -it --rm -p 10022:22 -p 10080:80 -e 'GITLAB_PORT=10080' -e 'GITLAB_SSH_PORT=10022' image_name
#     Default user pass: username: root  ::  password: 5iveL!fe
#

ENTRYPOINT ["/app/init"]
CMD ["app:start"]
