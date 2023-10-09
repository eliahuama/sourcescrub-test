# sourcescrub-test
Prerequisities:
* Docker installed
* .Net 7+ installed

How to run?
* restore solution with "dotnet restore" in project root.
* run "docker-compose up --build", if you want to view the ongoing logs, and "docker-compose up -d --build" if you want to start in daemon mode.
To stop, either ctrl+c for ongoing one, or "docker-compose down" for daemon.

Swagger is available at localhost:5000/swagger, postgres at localhost:5432 with postgres/Jump4Fun!
