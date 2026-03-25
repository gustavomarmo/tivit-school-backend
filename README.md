# 🎓 Edu Connect (TIVIT School) - Sistema de Gestão Escolar Fullstack

![Status do Projeto](https://img.shields.io/badge/Status-Em_Desenvolvimento-success)
![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![React](https://img.shields.io/badge/React-Vite-61DAFB?logo=react&logoColor=black)
![MySQL](https://img.shields.io/badge/MySQL-8.0.44-4479A1?logo=mysql&logoColor=white)
![Azure](https://img.shields.io/badge/Deployed_on-Azure-0089D6?logo=microsoft-azure)

O **Edu Connect** (também conhecido internamente como TIVIT School) é um sistema de gestão escolar moderno, escalável e completo, projetado para modernizar a interação entre Alunos, Professores e a Coordenação. O sistema abrange todo o ciclo de vida acadêmico, desde o fluxo de matrícula até o lançamento de notas, acompanhamento de frequência e geração de boletins. 

Um dos grandes diferenciais do projeto é a integração com **Inteligência Artificial**, permitindo aos professores a geração automática de exercícios com base nos materiais didáticos disponibilizados na plataforma.

## ✨ Principais Funcionalidades

* **Autenticação e Segurança:** Login com JWT, recuperação de senha, e verificação via OTP.
* **Dashboards Personalizados:** Visões específicas e indicadores rápidos para Alunos, Professores e Coordenadores.
* **Gestão de Matrículas:** Fluxo completo em etapas para novos alunos, upload de documentos e tela de aprovação para a coordenação.
* **Gestão Acadêmica:**
  * Criação e gerenciamento de Turmas e Disciplinas.
  * Lançamento de Notas e Frequência.
  * Visualização e geração de Boletins.
* **Geração de Exercícios com IA:** Ferramenta integrada (`AiService`) que lê materiais enviados e gera listas de exercícios automaticamente.
* **Mural e Notificações:** Sistema de eventos, calendário acadêmico integrado e notificações em tempo real.
* **Armazenamento na Nuvem:** Integração com Azure Blob Storage para upload de materiais, fotos e documentos.

## 🛠️ Arquitetura e Tecnologias

O projeto está dividido em duas frentes principais:

### Frontend (`/tivit-school-frontend`)
* **Framework:** React (criado com Vite)
* **Estilização:** CSS Modules (para escopo local e organização)
* **Gerenciamento de Estado/Contexto:** Context API (`AuthContext`, `DialogContext`, `ThemeContext` com suporte a Dark/Light mode)
* **Integração:** Axios
* **Deploy:** Azure Static Web Apps via GitHub Actions

### Backend (`/edu-connect-backend`)
* **Framework:** C# .NET (Web API)
* **ORM:** Entity Framework Core
* **Banco de Dados:** MySQL (versão 8.0.44)
* **Documentação de API:** Swagger / OpenAPI
* **Infraestrutura/Deploy:** Docker, GitHub Actions, Azure App Service
* **Serviços Adicionais:** Envio de E-mails via SMTP, integração com IA e Azure Blob Storage.

---

## 🚀 Como Executar o Projeto Localmente

### Pré-requisitos
* [Node.js](https://nodejs.org/) (versão 18+ recomendada)
* [.NET SDK](https://dotnet.microsoft.com/download) (compatível com a versão do projeto, ex: .NET 8)
* [MySQL](https://dev.mysql.com/downloads/) (versão 8.0.44)
* Uma IDE de sua escolha (Visual Studio, VS Code, Rider, etc.)

### 1. Configurando e Rodando o Backend
1. Navegue até o diretório do backend:
   ```bash
   cd edu-connect-backend

2. Configure o banco de dados e as chaves de API:

Abra ou crie o arquivo appsettings.Development.json.

Configure sua ConnectionStrings apontando para sua instância local do MySQL.

Adicione as credenciais de SMTP, JWT e as chaves da API de IA e Azure Blob Storage nas variáveis correspondentes (presentes na classe EduConnectVariables).

Aplique as migrações do banco de dados:

Bash
dotnet ef database update
Execute a aplicação:

Bash
dotnet run
Acesse o Swagger para testar os endpoints: http://localhost:<porta>/swagger

2. Configurando e Rodando o Frontend
Navegue até o diretório do frontend:

Bash
cd tivit-school-frontend
Instale as dependências:

Bash
npm install
Configure as variáveis de ambiente:

Crie um arquivo .env.development na raiz do frontend.

Defina a URL base da API (ex: VITE_API_URL=http://localhost:<porta_do_backend>).

Inicie o servidor de desenvolvimento:

Bash
npm run dev
Acesse a aplicação no navegador (geralmente em http://localhost:5173).

📂 Estrutura de Diretórios Resumida
Plaintext
📦 Raiz do Repositório
 ┣ 📂 edu-connect-backend          # API em C# .NET
 ┃ ┣ 📂 Configuration              # Configurações de CORS, JWT, Swagger, etc.
 ┃ ┣ 📂 Controller                 # Endpoints da API (Alunos, Notas, Matricula, etc.)
 ┃ ┣ 📂 DTO                        # Objetos de Transferência de Dados
 ┃ ┣ 📂 Mapper                     # Mapeamento entre DTOs e Models
 ┃ ┣ 📂 Migrations                 # Migrações do Entity Framework
 ┃ ┣ 📂 Model                      # Entidades de Domínio
 ┃ ┣ 📂 Repository                 # Acesso a Dados (Padrão Repository)
 ┃ ┣ 📂 Service                    # Regras de Negócio e Lógica de Aplicação
 ┃ ┗ 📂 Util                       # Serviços externos (AI, Blob, Tokens)
 ┃
 ┗ 📂 tivit-school-frontend        # Interface de Usuário em React/Vite
   ┣ 📂 public                     # Assets públicos
   ┗ 📂 src
     ┣ 📂 assets                   # Imagens e estilos globais (.css)
     ┣ 📂 components               # Componentes reutilizáveis (Botões, Modais, Tabelas, Sidebar)
     ┣ 📂 contexts                 # Context API (Autenticação, Tema)
     ┣ 📂 hooks                    # Custom Hooks para chamadas de API
     ┣ 📂 layouts                  # Estruturas de página (MainLayout)
     ┣ 📂 pages                    # Páginas da aplicação (Dashboard, Matrícula, Boletim, etc.)
     ┗ 📂 services                 # Configuração do Axios e chamadas base
👨‍💻 Autor
Gustavo Marmo Desenvolvedor Trainee | Engenharia de Software

Projeto desenvolvido como sistema prático de Gestão Escolar (Edu Connect / TIVIT School).