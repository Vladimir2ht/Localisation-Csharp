FROM postgres:17
COPY sql/scripts/0* /docker-entrypoint-initdb.d/