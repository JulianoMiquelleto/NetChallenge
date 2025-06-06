# net Challenge - EclipseWorks<br><br>
## Desafio .Net => https://meteor-ocelot-f0d.notion.site/NET-C-5281edbec2e4480d98552e5ca0242c5b

# Refinamento de requisitos, oportunidade de melhorias

  - Conhecer melhor as regras de negócios a respeito dos acessos de usuários e seus papéis.
  - Estabelecer uma forma de autenticação utliznado OAuth, Jwt.
  - Utilizar um banco de dados relacional de mercado, para o desenvovimento foi utilizado um banco de dados em memória.
  - Aumentar a cobertura de testes unitários.
  - Desenvolver as operações CRUD de projetos.
  - Criar um interface visual para o App.
  - Estabelcer um pattern estilo DDD ou arquitetura hexagonal, tendo em vista que são bastante conhecidas/utilizadas no mercado.
  - Utilizar um tratamento de exceções adequado, utilizando  middleware para tratamento.
  - Padronizar as mensagens e objetos de retorno das chamadas da Api, estabelendo um contrato com o consumidor dos endpoints.
  - Criar os pipelines de deploy e testes automatizados.
  - Disponibilizar o projeto na nuvem , utilzando Aws, Azure, GCP

# Docker
## Para funcionamento do projeto utilizando o docker:

Acessar a pasta NetChallenge <br>
cd NetChallenge

criar a imagem<br>
docker build -t net-challenge-image -f Dockerfile .

criar o container<br>
docker create --name net-challenge net-challenge-image

em seguida executar o container<br>
docker run net-challenge


# Collections do postman
As colections do postman foram disponibilizadas no repositório, para auxiliar nos teste.
NetChallenge-Collection.postman_collection.json