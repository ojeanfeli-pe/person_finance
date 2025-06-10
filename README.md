# 💸 Finance API - Controle de Finanças Pessoais

[![License](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![Build](https://img.shields.io/badge/Build-passing-brightgreen)](https://github.com/ojeanfeli-pe/person_finance)
[![.NET](https://img.shields.io/badge/.NET-blue)](https://dotnet.microsoft.com/en-us/download)

Bem-vindo(a) ao projeto **Finance API**!  
Este sistema tem como objetivo gerenciar finanças pessoais de forma simples e prática, proporcionando controle de categorias, usuários e transações.

---

## ✨ Funcionalidades Principais

### 💻 **Frontend (React)**
- 📄 **Listagem de transações
- 🧾 **Cadastro e edição de transações
- 🧭 **Navegação com React Router

### 💳 **Gestão Financeira**
- ✅ **CRUD completo de transações** (entradas e saídas)
- 🏷️ **Categorização de gastos** com CRUD de categorias
- 🔍 **Filtros inteligentes** para consulta de transações por usuário

### 📊 **Relatórios e Documentação**
- 📚 Documentação automática via Swagger UI
- ⚙️ API Minimal com endpoints bem definidos

---

## 🛠 Stack Tecnológica

| Categoria        | Tecnologias                                                 |
|-----------------|-------------------------------------------------------------|
| **Backend** | **.NET**, **ASP.NET Core Minimal APIs** |
| **FrontEnd** | **React**, **React.Js + TypeScript** |
| **Banco** | **Entity Framework Core** (Code First) |
| **Documentação**| **Swagger UI** |
| **Versionamento**| **Git** + **GitHub** |
---

## 🚀 Como Rodar o Projeto Localmente (Back-End)

### 1. Clone o repositório

```bash
git clone https://github.com/ojeanfeli-pe/person_finance.git
cd person_finance
cd FinanceSolution
cd API
code .
```
### 2. Instale as dependências
Certifique-se de ter o **.NET SDK** instalado.

Instale essas dependências:

```bash
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
```

Confira se o Entity Framework foi instalado com sucesso

```bash
dotnet-ef
```

### 3. Crie o banco de dados

Execute o comando para aplicar as migrações (se existir uma pasta `Migrations` no projeto):

```bash
1 - dotnet ef migrations add InitialCreate
2 - dotnet ef database update
```

### 4. Execute a aplicação

```bash
dotnet run
```

#### Será gerada a porta: https://localhost:5000 para realizar as requisições.

As requisições podem ser feitas pelo [Swagger](http://localhost:5000/swagger/index.html), ou pelos arquivos de requisição que foram colocados dentro do projeto: `_users.http`, `_functions.http`, `_categories.http`.

---
## 🚀 Como Rodar o visual do Projeto (Front-End)

### 1. Baixar o Node.js e o NPM

 https://nodejs.org/pt

 ### 2. Abrir a aplicação e instalar dependências e iniciar o Front

```bash
cd person_finance
cd front
npm install - Vai intalar todas as dependências necessárias
npm start - iniciar o front
```


## 📂 Estrutura do Projeto Back-End

- ├── **Data**
- │   └── AppDataContext.cs  
- │  
- ├── **Migrations**  
- │  
- ├── **Models**  
- │   ├── Category.cs  
- │   ├── Transaction.cs  
- │   └── User.cs  
- │  
- ├── **Requests**  
- │   ├── _categories.http  
- │   ├── _transactions.http  
- │   └── _users.http  
- │  
- ├── **Program.cs**  

## 📂 Estrutura do Projeto Front-End
├── src
└── - ├── **components**
    - │   └── Button.tsx  
    - │   └── Header.tsx
    - ├── **Models**  
    - │    └── Categoria.ts
    - │     └── NovaTransacao.ts
    - │     └── Transacao.ts
    - ├── **pages**  
    - │   ├── trasacoes
    - │          └── CadastrarTransacao.tsx
    - │          └── ListarTransacoes.tsx
    - │   ├── Home.tsx
    - │── **styles**     
    - │      └── App.css
    - │      └── home.css
    - │      └── index.css
    - │      └── transacao.css
    - │      └── Header.css
    - │── **App.tsx**    
    - ├── **index.tsx** 

![image](https://github.com/user-attachments/assets/1e95270c-5238-4850-9d03-3ea6283d52cf)

--- 
## 👨‍💻 Desenvolvedores

- [Pablo Pasquim](https://github.com/pablopasquim)
- [Jean Moreira](https://github.com/ojeanfeli-pe)
- [André Nichelle](https://github.com/Nichele135)




