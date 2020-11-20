FROM nginx

RUN apt-get update
RUN apt-get install npm -y
RUN npm install -g grunt-cli karma

