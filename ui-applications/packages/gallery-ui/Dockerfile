FROM node:alpine as build

WORKDIR /app

COPY package*.json ./

RUN npm install

COPY . . 

EXPOSE 5001

CMD ["npm", "run", "dev"]