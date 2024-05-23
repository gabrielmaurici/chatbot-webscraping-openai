FROM node:alpine

RUN apk update && \
    apk upgrade && \
    apk add --update ca-certificates && \
    apk add chromium --update-cache --repository http://nl.alpinelinux.org/alpine/edge/community \
    rm -rf /var/cache/apk/*

WORKDIR /usr/src/app

COPY package.json ./

RUN npm install

COPY . .

EXPOSE 3000

CMD ["npm", "start"]