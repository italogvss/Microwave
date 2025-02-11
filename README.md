# Back-Micro-ondas
Esta solução ASP.NET Core é uma aplicação web modular que combina backend e frontend em uma única solução.
O backend é uma API RESTful desenvolvida em ASP.NET Core, seguindo os princípios SOLID e utilizando Entity Framework Core para persistência de dados.
O frontend é uma aplicação React + TypeScript, que se comunica com a API e é servida pelo mesmo ambiente de hospedagem.

## Tecnologias Utilizadas

- ASP.NET Core (C#) → Backend RESTful
- Entity Framework Core → ORM para manipulação do banco
- Swagger → Documentação da API 
- JWT Authentication → Segurança e autenticação
- React + TypeScript → Interface do usuário
- SQL Server → Banco de dados

## Inicialização
No diretorio da solução, verifique se há uma build do front em `Web/wwwroot` caso não, faça a build de um do repositorio [Microwave-Front]([Microwave-Front](https://github.com/italogvss/Microwave-front))

Insira a string do banco de dados em `API/appsettings.json`

String: `Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Microwave;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False`

No diretorio dos projetos `API` e `Web` execute no terminal `dotnet run` para iniciar as aplicações de cliente e servidor


Acesse  [http://localhost:5009](http://localhost:5009) para visualizar o microondas

## Configurações

É possivel fazer as configurações da aplicação no arquivo 
`src/appConfig`

## Estrutura da Solução

- Web: Projeto com o front, precisa ser buildado
- API: Projeto com a implementação da camada de Controle e Serviço
- Shared.Data: Projeto com a implementação da camada de Persistência de dados
- Shared.Model: Modelos de dados e DTOs
