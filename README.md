# CensusApp

Aplicação desenvolvida para atender ao desafio da árvore.

## Tecnologias
- Aspnet Core 5
- MONGO DB
- DOCKER
- ANGULAR (dashboards)

## Executando a aplicação
- Pré-requisitos
    - Docker e Docker compose
    - Newman ou Postman (para executar testes de integração e popular a base de dados)
### Passos
##### Clonar os repositórios
     - https://github.com/lanodecastro/census-app (backend)
     - https://github.com/lanodecastro/censusapp-frontend (frontend)
##### Executar o Docker Compose (diretório raíz do backend)
```docker-compose up --build -d```

O docker irá levantar 3 containers: MongoDB,backend e frontend.
> A construção do container frontend deverá levar mais tempo que os outros, já que possui mais depêndencias para baixar.

### Testando a aplicação
Utilize o Newman  para executar os testes a partir do terminal.
Para instalar execute
``npm install -g newman``

Execute ``newman run integra/census-app-integra.postman_collection.json``para iniciar o teste de integração.
O propósito deste teste é garantir que a aplicação está acessível e funcional

### Testando com o frontend
Um dos requisitos exigidos no desafio é disponibilizar um dashboard que permita visualizar a criação de novos cadastros em tempo real.
Para que a entrada de dados seja feita com agilizada, foi disponilizado um arquivo para servir de entrada de dados a partir do Newman ou Postman.

 - Acesse o endereço http://localhost usando deu browser de preferência
 - No terminal de comando, execute ```newman run integra/census-app-data.postman_collection.json -d integra/data.json --delay-request 3000```

>O parâmetro --delay-request define o tempo de intervalo entre as requisições. Para uma melhor experiência na vizualização do dashboard, foi configurado para 3 segundos,
>mas você pode ajustar conforme desejar

No navegador você verá um dashboard semelhante a este

<img src="https://github.com/lanodecastro/censusapp-frontend/blob/master/censusApp.PNG?raw=true">


