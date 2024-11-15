# mmt

## Building

`cd backend\Jimx.MMT.API`
`docker build -t jimx.mmt.api:1.1 -f Dockerfile ..`

`cd frontend\mmt-app`
`npm run build`

## Transferring

Save:
`docker save -o mmt-api.1_1.tar jimx.mmt.api:1.1`
`tar -czf mmt-ui.1_1.tar -C .\frontend\mmt-app\dist\mmt-app .` 

Transfer:
`scp mmt-api.1_1.tar mmt-ui.1_1.tar root@server:/root`

Cleanup:
`docker stop Jimx.MMT.API`
`docker rm Jimx.MMT.API`
`docker rmi jimx.mmt.api:1.1`

`cd /var/www/mmt-ui`
`rm -ri *`

Load:
`docker load -i mmt-api.1_1.tar`
`tar -xf mmt-ui.1_1.tar -C /var/www/mmt-ui`

## Running

`docker run --name Jimx.MMT.API -p 15401:80 -e "ASPNETCORE_ENVIRONMENT=Development" -e "GENERAL_BASEURL=(PLACEHOLDER1)" -e "GENERAL_FRONTENDURL=(PLACEHOLDER2)" -e "ASPNETCORE_URLS=http://+:80" -e "ConnectionStrings__DefaultConnection=(PLACEHOLDER3)"  --network=mylocalnet -dt jimx.mmt.api:1.1`

`sudo service nginx restart` or `sudo service nginx start`

## Testing

`curl http://localhost:15401/health`