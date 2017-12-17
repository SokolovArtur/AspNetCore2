FROM nginx:1.13

RUN apt-get update \
    && apt-get install -y --no-install-recommends mc

COPY docker/default.conf /etc/nginx/conf.d/default.conf
