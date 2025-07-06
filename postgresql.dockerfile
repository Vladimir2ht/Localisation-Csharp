FROM postgres:17
COPY PostgreSql.Scripts/* /docker-entrypoint-initdb.d/