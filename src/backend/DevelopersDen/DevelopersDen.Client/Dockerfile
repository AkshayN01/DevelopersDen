FROM node:latest AS build
WORKDIR /app
COPY . /app/
RUN npm install
RUN npm run build

FROM nginx:alpine
COPY ./config/nginx.conf /etc/nginx/nginx.conf
COPY /home/akshayn/DevelopersDen/akshaynambly_info_chain.crt /etc/nginx
COPY /home/akshayn/ssl/server.key /etc/nginx
COPY --from=build /app/dist/developers-den.client/browser /usr/share/nginx/html
EXPOSE 80
EXPOSE 443
