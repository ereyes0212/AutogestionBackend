#!/usr/bin/env bash
host="$1"
shift
cmd="$@"

echo "Esperando a que MySQL est� disponible en $host:3306..."

while ! timeout 1 bash -c "echo > /dev/tcp/$host/3306" 2>/dev/null; do
  echo "MySQL no est� listo, esperando..."
  sleep 2
done

echo "MySQL est� listo, ejecutando el comando..."
exec $cmd
