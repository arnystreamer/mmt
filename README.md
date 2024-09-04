# mmt

## Building

`cd backend\Jimx.MMT.API`
`docker build -t jimx.mmt.api:alpha -f Dockerfile ..`

`cd frontend\mmt-app`
`docker build -t jimx.mmt.frontend:alpha -f .\Dockerfile .`

## Transferring

Save:
`docker save -o alpha.tar jimx.mmt.api:alpha`
`docker save -o alpha.ui.tar jimx.mmt.frontend:alpha`

Transfer:
`scp alpha.tar alpha.ui.tar user@server:/folder`

Cleanup:
`docker stop Jimx.MMT.API`
`docker stop Jimx.MMT.Frontend`
`docker rm Jimx.MMT.API`
`docker rm Jimx.MMT.Frontend`
`docker rmi jimx.mmt.frontend:alpha`
`docker rmi --force jimx.mmt.frontend:alpha`

Load:
`docker load -i alpha.tar`
`docker load -i alpha.ui.tar`

## Running

`docker run --name Jimx.MMT.API -p 15401:80 -e "ASPNETCORE_ENVIRONMENT=Development" -e "GENERAL_BASEURL=(PLACEHOLDER1)" -e "ASPNETCORE_URLS=http://+:80" -e "ConnectionStrings__DefaultConnection=(PLACEHOLDER2)"  --network=mylocalnet -dt jimx.mmt.api:alpha`

`docker run -dt -p 15401:80 -e "ASPNETCORE_ENVIRONMENT=Development" -e "ASPNETCORE_URLS=http://+:80" --name Jimx.MMT.API jimx.mmt.api:alpha`

`docker run -dt -p 15402:80 --name Jimx.MMT.Frontend jimx.mmt.frontend:alpha`

## Testing

`curl http://localhost:15401/health`