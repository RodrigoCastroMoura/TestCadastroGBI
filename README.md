# GbiTestCadastro

## Descrição
GbiTestCadastro é uma aplicação .NET Core 6 que utiliza SQLite como banco de dados. Esta aplicação permite o cadastro de usuários e é configurada para rodar em um contêiner Docker.

## Pré-requisitos
Antes de começar, certifique-se de ter os seguintes softwares instalados em sua máquina:

- [Docker](https://www.docker.com/get-started)

## Estrutura do Projeto
- **src/**: Contém o código-fonte da aplicação
  - **GbiTestCadastro.Api/**: Projeto da API
  - **GbiTestCadastro.Application/**: Camada de aplicação
  - **GbiTestCadastro.Domain/**: Camada de domínio
  - **GbiTestCadastro.Infra/**: Infraestrutura (repositórios, contexto de dados)
  - **GbiTestCadastro.Dto/**: Data Transfer Objects (DTOs)
- **create_table.sql**: Script SQL para criar a tabela `Usuarios`
- **Dockerfile**: Arquivo de configuração do Docker

## Configuração

### 1. Clone o repositório
Clone este repositório para sua máquina local usando o comando abaixo:

```bash
git https://github.com/RodrigoCastroMoura/TestCadastroGBI.git
cd GbiTestCadastro

```
## Comandos Docker

### 1. Construir a imagem Docker
```bash
docker build -t myproject_image .
```
### 2. Construir a Conterner Docker
```bash
docker run -d -p 5000:80 --name myproject_container myproject_image

curl http://http://localhost:5000/swagger/index.html

```