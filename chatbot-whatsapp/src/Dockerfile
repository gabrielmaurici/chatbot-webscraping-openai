FROM node:alpine

RUN apk update && \
    apk upgrade && \
    apk add --update ca-certificates && \
    apk add chromium --update-cache --repository http://nl.alpinelinux.org/alpine/edge/community \
    rm -rf /var/cache/apk/*

# RUN apk add --no-cache \
#     chromium \
#     nss \
#     freetype \
#     harfbuzz \
#     ca-certificates \
#     ttf-freefont \
#     nodejs \
#     yarn

# ENV PUPPETEER_EXECUTABLE_PATH=/usr/bin/chromium-browser

# RUN yarn add puppeteer@13.5.0

# RUN addgroup -S pptruser && adduser -S -G pptruser pptruser \
#     && mkdir -p /home/pptruser/Downloads /app \
#     && chown -R pptruser:pptruser /home/pptruser \
#     && chown -R pptruser:pptruser /app

# USER pptruser

WORKDIR /usr/src/app

COPY package.json ./

RUN npm install

COPY . .

EXPOSE 3000

CMD ["npm", "start"]