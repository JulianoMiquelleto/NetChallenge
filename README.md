# eclipse_works


# docker
Para funcionamento do projeto utilizando o docker:

Acessar a pasta NetChallenge <br>
cd NetChallenge

criar a imagem<br>
docker build -t net-challenge-image -f Dockerfile .

criar o container<br>
docker create --name net-challenge net-challenge-image

em seguida executar o container<br>
docker run net-challenge
