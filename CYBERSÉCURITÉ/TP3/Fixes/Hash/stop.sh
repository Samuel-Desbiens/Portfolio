docker-compose -f docker/docker-compose.yml --project-directory . down

docker image rm img_tp3
docker image rm img_tp3_back
docker image rm img_firstpuppet
sqlite3 ./project/banking.db < ./bd_scripts/seed.sql
