📈 Controle de Finanças API
Uma API simples e eficiente para gerenciar finanças pessoais, permitindo o cadastro de usuários, categorias e transações financeiras.

Ideal para quem deseja controlar entradas e saídas financeiras de forma prática e segura!

🚀 Tecnologias Utilizadas
C#

ASP.NET Core Minimal API

Entity Framework Core

Swagger para documentação

Git e GitHub para versionamento

🛠️ Como Rodar o Projeto
1. Pré-requisitos
Antes de mais nada, certifique-se de ter instalado:

.NET SDK 8.0+ (obrigatório)

Git instalado (Download aqui) (obrigatório)

Para confirmar se o .NET está instalado, digite no terminal:

bash
Copiar
Editar
dotnet --version
Se aparecer a versão (tipo 8.0.100), está tudo certo!

2. Clone o repositório
bash
Copiar
Editar
git clone https://github.com/ojeanfeli-pe/person_finance.git
cd person_finance
3. Instale as dependências
bash
Copiar
Editar
dotnet restore
Se faltar algum pacote (por exemplo Entity Framework ou Swagger), instale manualmente:

bash
Copiar
Editar
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Swashbuckle.AspNetCore
4. Configure o banco de dados
Você precisará aplicar a migração inicial para criar as tabelas no banco:

bash
Copiar
Editar
dotnet ef migrations add InitialCreate
dotnet ef database update
(Se preferir, pode configurar manualmente também — te ajudo com isso se quiser! 🔥)

5. Rode o projeto
bash
Copiar
Editar
dotnet run
A API vai iniciar em algo como:

bash
Copiar
Editar
https://localhost:5001/swagger/index.html
Basta abrir esse link no navegador para acessar a documentação interativa do Swagger!

📂 Estrutura do Projeto
pgsql
Copiar
Editar
├── Data
│   └── AppDataContext.cs
├── Models
│   ├── Category.cs
│   ├── Transaction.cs
│   └── User.cs
├── Requests
│   ├── _categories.http
│   ├── _transactions.http
│   └── _users.http
├── Program.cs
📚 Endpoints Disponíveis
🧑‍💼 Usuários

Método	Rota	Descrição
GET	/api/users	Listar todos os usuários
POST	/api/users/register	Registrar novo usuário
POST	/api/users/login	Login de usuário
PUT	/api/users/{id}	Atualizar usuário
DELETE	/api/users/{id}	Deletar usuário
🗂️ Categorias

Método	Rota	Descrição
GET	/api/categories	Listar todas as categorias
💸 Transações

Método	Rota	Descrição
GET	/api/transactions	Listar todas as transações
GET	/api/transactions/user/{userId}	Listar transações de um usuário
POST	/api/transactions	Criar nova transação
DELETE	/api/transactions/{transactionId}/user/{userId}	Deletar transação de um usuário
👨‍💻 Desenvolvedores

Nome	GitHub
Pablo Pasquim	@pablopasquim
Jean Moreira	@ojeanfeli-pe
André Nichelle @Nichele135

🎯 Finalizando
Controle de Finanças API é um projeto focado em simplicidade e expansão fácil.
Explore, modifique e use para aprender mais sobre ASP.NET Minimal API, EF Core e boas práticas de backend!


