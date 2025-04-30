# GeekShopping_JuniorDev

Este repositório contém a implementação de uma loja virtual baseada em uma arquitetura de microsserviços, utilizando .NET 6. O projeto foi desenvolvido com o objetivo de aprender e aplicar conceitos de microsserviços, comunicação entre APIs, autenticação, autorização e integração com RabbitMQ.

## Tecnologias Utilizadas

- **Backend**: C# com .NET 6
- **Frontend**: ASP.NET Core MVC
- **API Gateway**: Ocelot
- **Autenticação**: OAuth2, OpenID, JWT, Duende Identity Server
- **Mensageria**: RabbitMQ
- **Banco de Dados**: Microsoft SQL Server
- **Containerização**: Docker

## Estrutura do Projeto

A estrutura do repositório é organizada da seguinte forma:

- **GeekShopping.sln**: Arquivo de solução do Visual Studio que agrupa todos os projetos.
- **FrontEnd/GeekShopping.Web**: Aplicação web utilizando ASP.NET Core MVC.
- **Services**: Microsserviços responsáveis por funcionalidades específicas.
- **GeekShopping.APIGateway**: API Gateway utilizando Ocelot.
- **GeekShopping.IdentityServer**: Serviço de autenticação e autorização.

## Funcionalidades

- **GeekShopping.CartAPI**: Gerenciamento de carrinho de compras, aplicação de cupons de desconto e processamento de pagamentos.
- **GeekShopping.CouponAPI**: Gerenciamento de cupons de desconto.
- **GeekShopping.Email**: Serviço de envio de e-mails.
- **GeekShopping.OrderAPI**: Processamento de pedidos de compra.
- **GeekShopping.PaymentAPI**: Processamento de pagamentos.
- **GeekShopping.ProductAPI**: Gerenciamento de produtos.
- **GeekShopping.Web**: Interface do usuário para interação com a loja virtual.
- **GeekShopping.APIGateway**: Roteamento de requisições para os microsserviços.
- **GeekShopping.IdentityServer**: Autenticação e autorização de usuários.

## Como Executar o Projeto

### Pré-requisitos

- .NET 6 SDK
- Docker
- Visual Studio 2022 ou superior

### Passos para execução

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/juniorti91/GeekShopping_JuniorDev.git
   cd GeekShopping_JuniorDev
