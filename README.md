# APIRestFul

## MongoDB Docker
https://hub.docker.com/r/tutum/mongodb
<pre>docker run -d -p 27017:27017 -p 28017:28017 -e AUTH=no tutum/mongodb</pre>

## Redis Docker
https://hub.docker.com/_/redis
<pre>docker run --name rediscurso -p 6379:6379 -d redis redis-server --appendonly no</pre>
