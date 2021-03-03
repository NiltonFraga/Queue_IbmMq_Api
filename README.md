# Queue_IbmMq_Api
Api .Net que lê e escreve na fIla da IBMMQ atravês do Swagger 

# Descrição

Api .Net é capaz de ler e escrever em uma fila da IBM local. Para a execução de forma simples é necessario alguns passos:

OBS: Esse site ajuda a cria o container da fila. Link: https://developer.ibm.com/components/ibm-mq/tutorials/mq-connect-app-queue-manager-containers/

- Instale o Docker - Link: https://docs.docker.com/docker-for-windows/install/;
- Execute o Docker;
- Baixe a imagem da fila IBM docker pull ibmcom/mq - Links: https://hub.docker.com/r/ibmcom/mq/;
- Execute docker run --env LICENSE=accept --env MQ_QMGR_NAME=QM1 --publish 1414:1414 --publish 9443:9443 --detach --env MQ_APP_PASSWORD=passw0rd ibmcom/mq:latest;
- Execute a Api e teste pelos Controller do Swagger;

