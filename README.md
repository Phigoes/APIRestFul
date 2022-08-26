# APIRestFul

## SQl Server Docker
https://hub.docker.com/_/microsoft-mssql-server
<pre>docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MeuSQLDB@123" -p 11433:1433 --name sql1 --hostname sqlhome -d   mcr.microsoft.com/mssql/server:2022-latest</pre>

Connect to the SQL Server using Docker Desktop, and in docker-cli use the command below and put the password:
<pre>/opt/mssql-tools/bin/sqlcmd -S localhost -U sa</pre>

After that, execute the SQL command below:
<pre>CREATE DATABASE db_portal</pre>

## Redis Docker
https://hub.docker.com/_/redis
<pre>docker run --name rediscurso -p 6379:6379 -d redis redis-server --appendonly no</pre>
