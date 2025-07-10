# Serviço de Antecipação de Recebíveis

Este projeto fornece um serviço para empresas anteciparem seus recebíveis (notas fiscais). Ele consiste em um backend .NET Core e um frontend React.

## Decisões de Arquitetura

O backend é construído usando **Arquitetura Onion** com princípios de **Clean Code** e elementos de **Domain-Driven Design (DDD)**. Isso garante:
- Separação de preocupações
- Alta testabilidade
- Escalabilidade
- Facilidade de manutenção

Camadas principais:
- **Domain**: Contém a lógica de negócios e entidades.
- **Application**: Casos de uso e lógica da aplicação.
- **Infrastructure**: Acesso a dados (Entity Framework), serviços externos.
- **Api**: Controladores da API web.

### Banco de Dados
- **PostgreSQL** é usado como banco de dados.
- **Entity Framework Core** para ORM.
- **Liquibase** para versionamento e migrações do banco de dados, gerenciado via Docker.

## Tecnologias

### Backend
- .NET 8.0
- Entity Framework Core
- PostgreSQL
- Liquibase

### Frontend
- React
- Axios para chamadas de API
- Tailwind CSS para estilização

## Como Executar

### Pré-requisitos
- Docker e Docker Compose
- .NET 8.0 SDK
- Node.js

### Passos

1. **Inicie o banco de dados e execute as migrações**:
   ```bash
   cd Backend
   docker-compose up -d
   ```

2. **Execute o backend**:
   ```bash
   cd Backend/src/Size.Receivables.Api
   dotnet run
   ```

3. **Execute o frontend**:
   ```bash
   cd Frontend
   npm install
   npm start
   ```

O backend estará disponível em `https://localhost:5001` e o frontend em `http://localhost:3005`.

## Melhorias Futuras

- Adicionar usuários com autenticação e autorização.
- Implementar tratamento global de exceções e logging mais avançados.
- Escrever testes unitários e de integração.
- Configurar uma pipeline de CI/CD.
- Implementar um cálculo de limite de crédito mais sofisticado.
---