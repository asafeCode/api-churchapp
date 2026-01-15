# 📘 API ChurchApp – Sistema de Gestão para Igrejas

> API backend robusta para gerenciamento administrativo de igrejas, com foco em segurança, escalabilidade, arquitetura e patterns modernos em .NET.

---

## 🛠️ Tecnologias

Esse projeto utiliza:

* **C#**
* **.NET Core / ASP.NET Core**
* **APIs RESTful**
* **SQL Server e PostgreSQL**
* **RabbitMQ (via MassTransit)**
* **Docker**
* **Azure (deploy / CI/CD)**
* **k6** (para testes de carga)
* **Clean Architecture & SOLID**

---

## 🚀 Descrição

O **API ChurchApp** é um backend para gestão de igrejas que oferece funcionalidades como:

* Controle de usuários com autenticação segura
* Fluxos de cadastro por convite
* Perfis diferenciados (admin / membro)
* Gerenciamento de dados e regras de negócio específicas
* Mensageria para tarefas assíncronas (como exclusão postergada)
* Processos resilientes contra concorrência
* Deploy preparado para ambientes de produção em cloud

Projetado com foco em **arquitetura escalável e testes de cargas reais**, o back-end demonstra boas práticas aplicadas em projetos de nível profissional.

---

## 📌 Funcionalidades Principais

### 🔐 Autenticação e Gestão de Usuários

* Login/Logout
* Refresh tokens
* Políticas de segurança de senha
* Confirmação de exclusão de usuário

### 📋 Cadastro via Convite

* Usuário convidado recebe link válido por tempo limitado
* Fluxo seguro para onboarding de membro

### 🕊️ Processos Assíncronos

* Exclusão “soft + hard delete” com delayed messages
* Mensageria com **RabbitMQ** para desacoplamento e escalabilidade

### 💪 Concorrência e Resiliência

* Controle de concorrência com operações atômicas no banco
* Testes de carga com **k6** para validação sob alto tráfego

### ☁️ Deploy & Infraestrutura

* Preparado para **Azure App Service**
* Pipelines de **CI/CD**

---

## 📂 Estrutura do Projeto

```
.
├── src/                       # Código da aplicação
├── tests/                    # Testes unitários e de integração
├── .github/                 # Workflows de CI/CD
├── Dockerfile              # Containerização da aplicação
├── TesourariaApp.sln       # Solução .NET
└── README.md               # Documentação do projeto
```

> Estrutura organizada para facilitar manutenção, testes e evolução da aplicação.

---

## 🚀 Como Executar Localmente

### Pré-requisitos

Antes de começar, você precisa ter instalado:

```bash
dotnet 8 SDK
Docker e Docker Compose
RabbitMQ em container ou serviço
SQL Server ou PostgreSQL
```

---

### 🔹 Rodando a API

1. Clone este repositório

```bash
git clone https://github.com/asafeCode/api-churchapp.git
```

2. Entre na pasta

```bash
cd api-churchapp
```

3. Inicie os ambientes via Docker

```bash
docker compose up
```

4. Rodando a API .NET

```bash
dotnet run --project src/ApiChurchApp
```

5. Acesse a documentação
   Normalmente via Swagger em:

```
http://localhost:<porta>/swagger
```

---

## 🧪 Testes

O projeto inclui testes de unidade e validação:

```bash
cd tests/ValidatorsTest
dotnet test
```

Testes automáticos garantem que validações de entrada, regras de negócio e fluxos principais funcionem conforme esperado.

---

## 📈 Contribuições

Contribuições são bem-vindas!
Se quiser melhorar ou sugerir novas features enviando PRs com:

* Novos endpoints
* Melhorias de arquitetura
* Testes automatizados

---

## 📄 Licença

Esse repositório está sob a licença **MIT** — dê uma estrela ⭐ se você gostou e acha que esse projeto pode ajudar outras pessoas!

---

## 👍 Fale comigo

Se quiser discutir o projeto ou me chamar para oportunidades:

**LinkedIn:** [https://www.linkedin.com/in/matheus-asafe](https://www.linkedin.com/in/matheus-asafe)
**GitHub:** [https://github.com/asafeCode](https://github.com/asafeCode)

---

Se quiser, posso também gerar **badges de build/testes**, **diagrama de arquitetura (Markdown)** ou até uma **versão visual com imagens** para o README 👊

[1]: https://github.com/GabrielMajeri/aspnet-core-web-api-react-spa-template?utm_source=chatgpt.com "GitHub - GabrielMajeri/aspnet-core-web-api-react-spa-template: Modern web application template with ASP.NET Core on the back end and React on the front end"
