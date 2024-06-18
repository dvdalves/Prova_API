# Repositório para entrega do desafio

## Instruções de Construção e Execução

1. Clone o repositório.
2. Abra o projeto em sua IDE preferida.
3. Configure sua conexão com o SQL Server no appsettings.json (API).
4. Rode o comando Update-Database no Console dos Pacotes NuGet ou dotnet ef update-database no terminal.
5. Configure os múltiplos projetos (API e MVC) para serem executados.

## Tecnologias
- .NET 8
- SQL Server
- SQLite
- Entity Framework
- NUnit (Testes de unidade)
- ASP.NET MVC (HTML, CSS, Bootstrap)
- Swagger (Documentação)

## Apresentação e Arquitetura
O projeto foi desenvolvido seguindo uma arquitetura em três camadas, separando:

- Frontend e Lógica de Negócio (MVC): Garante a separação das responsabilidades e facilita a manutenção.
- API: Independente, seguindo as práticas do Domain-Driven Design (DDD), com a camada de domínio no centro da aplicação, incluindo modelos de negócio e a infraestrutura responsável pelo acesso a dados, repositórios e migrations.
- Banco de Dados (SQL Server): Configurado para trabalhar de maneira integrada com a API, proporcionando escalabilidade e manutenção eficiente.
  
A API foi projetada para aderir ao nível 2 de maturidade de Richardson, com uma interface que inclui classes de configuração e controllers que seguem o padrão REST. Decidi não aumentar a complexidade com uma camada de negócios dedicada na API, pois o frontend já possui uma camada de serviço para tratar a lógica de negócio. Assim, a API foca em receber os objetos e interagir com o banco de dados. Adicionalmente, não foi considerado necessário o uso de ViewModels ou DTOs.

MVC (Frontend)
O frontend foi implementado utilizando ASP.NET MVC, escolhido por minha familiaridade com este framework. Embora opções como Razor Pages ou SPAs com Angular ou Blazor fossem viáveis, o MVC satisfaz plenamente as necessidades do projeto. Ele poderia inclusive ser separado em um repositório distinto, pois não há dependências diretas com a API, mas ambos permanecem na mesma solução por conveniência.

O MVC comunica-se com a API via HTTP, enviando e recebendo dados em JSON, e inclui uma camada de serviço que prepara a lógica de negócio antes da comunicação com a API. As páginas seguem o padrão MVC, com funcionalidades para CRUD de produtos e uma página "Utils" para adicionar múltiplos produtos em memória usando SQLite, facilitando os testes de paginação.
Além disso, há uma página de testes para datas no formato ISO 8601, embora as datas dos produtos adicionados também sigam o padrão exigido.

Os projetos foram desenvolvidos aderindo às boas práticas de orientação a objetos e aos princípios SOLID.

## Documentação do Swagger
![image](https://github.com/dvdalves/Prova_API/assets/109628134/7273ca1c-bc1f-4d31-aa93-e1dadccc6638)

## MVC (Frontend)
Seguindo o requisito da paginação do desafio
![image](https://github.com/dvdalves/Prova_API/assets/109628134/4a7864f3-2dca-4544-be80-81da1cc53748)

Na aba Utils, pode adicionar múltiplos produtos em memória para teste da paginação (máximo de 1000)
![image](https://github.com/dvdalves/Prova_API/assets/109628134/a562979f-4b2d-42e8-969b-343ea5c2a455)
![image](https://github.com/dvdalves/Prova_API/assets/109628134/f551b91b-38b1-49ef-8e13-084cd66f3127)

Na aba Data e Hora trás a data atual no formato solicitado no desafio
![image](https://github.com/dvdalves/Prova_API/assets/109628134/56e97515-fe9d-48fc-adff-213d7b2293e4)

## Testes
![image](https://github.com/dvdalves/Prova_API/assets/109628134/3c50a4ce-a9ac-46bc-b776-fcf9f0381d75)
