FROM nginx:1.19-alpine

## Copy our default nginx config
COPY default.conf /etc/nginx/conf.d/

## Remove default nginx website
RUN rm -rf /usr/share/nginx/html/*

## Copy over the artifacts in dist folder to default nginx public folder
COPY dist /usr/share/nginx/html/self-service-portal

CMD ["nginx", "-g", "daemon off;"]