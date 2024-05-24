FROM node:alpine as builder
ARG SERVICE
WORKDIR /usr/src/app

COPY --chown=node:node package*.json lerna.json tsconfig.json ./
COPY --chown=node:node packages/shared-components ./packages/shared-components
COPY --chown=node:node packages/${SERVICE} ./packages/${SERVICE}
RUN npm install --loglevel notice --unsafe-perm

ENV SERVICE_NAME=${SERVICE}

EXPOSE 5000
CMD ["npm", "--prefix", "services/${SERVICE}", "start"]