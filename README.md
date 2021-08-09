# Aluraflix.API - Alura Challenge Backend

Deploy realizado com sucesso! API disponível em https://aluraflix-api-mc.herokuapp.com/

## Semana 3 - autenticação, paginação e deploy

Nesta semana, o desafio será complementar a API adicionando paginação nas requisições de vídeos e categorias. Além disso, foi solicitado para segurança dos recursos proporcionados pela API, adicionar um método de autenticação.

- Para garantir a segurança dos dados, implemente algum tipo de autenticação, para que só os usuários autenticados possam acessar as rotas de GET, POST, PUT e DELETE.
Caso a autenticação não seja válida, retornar uma mensagem informando Não autorizado ou Credenciais inválidas.
Caso usuário e senha inválido, informar Usuário e senha inválido.
```
/usuarios/authenticate
```
Endpoint público que aceita requisições HTTP POST contendo o nome do usuário e senha no corpo da mensagem. Se o usuário ou senha estiverem corretos, os detalhes do usuário são retornados.
```
/usuarios
```
Rota segura que aceita requisições HTTP GET e retorna uma lista de todos os usuários na aplicação se o HTTP Authorization Header contém credenciais válidas de uma autenticação básica. Caso contrário, a resposta é um 401 Unauthorized.

- Criar o seguinte endpoint com um número fixo de filmes disponível, sem a necessidade de autenticação:
```
GET /videos/free
```
### Para testar no Postman
#### Instruções sobre como usar o app Postman para autenticar um usuário com a API, e fazer uma requisição autenticada com autenticação básica para recuperar dados da API:
1. Download: https://www.getpostman.com/
2. Abra uma nova aba de request clicando no botão (+) no final das abas.
3. Mude o tipo de método de http request para "POST".
4. No campo de URL digite o endereço da rota de autenticação da API. Por exemplo, http://localhost:5000/users/authenticate.
5. Na aba "Body", mude o tipo da mensagem para "raw", o formato para "JSON (application/json)".
6. Clique no botão "Send". Digite o conteúdo abaixo na área do corpo da mensagem:
```
{
    "username": "test",
    "password": "test"
}
```
7. Você deve receber uma mensagem de "200 OK" contendo os detalhes do usuário no corpo da resposta, o que indica que o usuário e senha estão corretos.
#### Para fazer uma consulta com as credenciais de autenticação básica:
1. Abra uma nova aba de request clicando no botão (+) no final das abas.
2. Mude o tipo de método de http request para "GET".
3. No campo de URL digite o endereço desejado. Por exemplo, http://localhost:5000/videos.
4. Na aba de "Authorization" abaixo da URL, mude para "Basic Auth", e digite test no Username e test em "Password".
5. Clique no botão "Send".
6. Você deve receber uma mensagem de "200 OK" contendo um JSON com os registros que você deseja visualizar.

## Semana 2 - implementação da feature de Categorias:

Depois de alguns testes com usuários, foi definido que a próxima feature a ser desenvolvida nesse projeto é a divisão dos vídeos por categoria, para melhorar a experiência de organização da lista de vídeos pelo usuário.

Dividimos a implementação dessa feat da seguinte forma:
1. Adicionar categorias e seus campos na base de dados;
2. Rotas CRUD para /categorias;
3. Incluir campo categoriaId no modelo video;
4. Escrever os testes necessários.

## Semana 1 - iniciando o projeto:

Após alguns testes com protótipos feitos pelo time de UX de uma empresa, foi requisitada a primeira versão de uma plataforma para compartilhamento de vídeos. A plataforma deve permitir ao usuário montar playlists com links para seus vídeos preferidos, separados por categorias.

Os times de frontend e UI já estão trabalhando no layout e nas telas. Para o backend, as principais funcionalidades a serem implementadas são:
1. API com rotas implementadas segundo o padrão REST;
2. Validações feitas conforme as regras de negócio;
3. Implementação de base de dados para persistência das informações;
4. Serviço de autenticação para acesso às rotas GET, POST, PUT e DELETE.
