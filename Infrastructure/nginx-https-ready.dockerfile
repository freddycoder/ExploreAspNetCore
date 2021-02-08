FROM nginx

COPY ./Infrastructure/config/default.conf /etc/nginx/conf.d/

RUN apt-get update && apt-get install python-certbot-nginx -y && apt-get install cron -y