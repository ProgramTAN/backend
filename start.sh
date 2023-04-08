#!/bin/bash
wget https://raw.githubusercontent.com/ProgramTAN/backend/main/docker-compose.yml
wget https://raw.githubusercontent.com/ProgramTAN/backend/main/start.sh
chmod +x ./start.sh
sudo docker compose down --volumes --remove-orphans
sudo docker compose up -d --build --force-recreate
