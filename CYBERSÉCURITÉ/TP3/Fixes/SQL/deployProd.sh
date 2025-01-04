#!/bin/bash
docker stop tp3_cnt
docker container rm tp3_cnt
docker image rm tp3_img
docker stop tp3_cnt_bd
docker container rm tp3_cnt_bd

docker volume create --name tp3_vol --opt device=/home/lubuntu/Documents/tp3-test --opt o=bind --opt type=none
docker build -t tp3_img -f ./docker/DockerfileAPI .
#docker run -p 15556:5555 --name tp3_cnt tp3_img
docker run -d -p 15556:5556 --mount source=tp3_vol,target=/mnt/app/ --name tp3_cnt tp3_img

docker build -t tp3_img -f ./docker/DockerfileDB .
#docker run -p 15556:5555 --name tp3_cnt tp3_img
docker run -d -p 15555:5555 --mount source=tp3_vol,target=/mnt/app/ --name tp3_cnt_bd tp3_img