# mmt

## Building

`cd backend\Jimx.MMT.API`
`docker build -t jimx.mmt.api:1.0 -f Dockerfile ..`

`cd frontend\mmt-app`
`docker build -t jimx.mmt.frontend:alpha -f .\Dockerfile .`

## Transferring

Save:
`docker save -o api_1.0.tar jimx.mmt.api:1.0`
`docker save -o alpha.ui.tar jimx.mmt.frontend:alpha`

Transfer:
`scp api_1.0.tar alpha.ui.tar root@server:/root`

Cleanup:
`docker stop Jimx.MMT.API`
`docker stop Jimx.MMT.Frontend`
`docker rm Jimx.MMT.API`
`docker rm Jimx.MMT.Frontend`
`docker rmi jimx.mmt.api:1.0`
`docker rmi --force jimx.mmt.frontend:alpha`

Load:
`docker load -i api_1.0.tar`
`docker load -i alpha.ui.tar`

## Running

`docker run --name Jimx.MMT.API -p 15401:80 -e "ASPNETCORE_ENVIRONMENT=Development" -e "GENERAL_BASEURL=(PLACEHOLDER1)" -e "GENERAL_FRONTENDURL=(PLACEHOLDER2)" -e "ASPNETCORE_URLS=http://+:80" -e "ConnectionStrings__DefaultConnection=(PLACEHOLDER2)"  --network=mylocalnet -dt jimx.mmt.api:1.0`

`docker run -dt -p 15402:80 --name Jimx.MMT.Frontend jimx.mmt.frontend:alpha`

## Testing

`curl http://localhost:15401/health`