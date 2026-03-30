<div align="center">

# 🎓 Tivit School

**Plataforma escolar completa para gestão acadêmica, matrícula digital e comunicação entre alunos, professores e coordenação.**

[![React](https://img.shields.io/badge/React-19.2-61DAFB?style=flat-square&logo=react)](https://react.dev/)
[![Vite](https://img.shields.io/badge/Vite-7.x-646CFF?style=flat-square&logo=vite)](https://vitejs.dev/)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-EF-CC2927?style=flat-square&logo=microsoftsqlserver)](https://www.microsoft.com/sql-server)
[![Azure](https://img.shields.io/badge/Azure-Deployed-0078D4?style=flat-square&logo=microsoftazure)](https://azure.microsoft.com/)

[Demo ao Vivo](https://icy-wave-0e2eac10f.5.azurestaticapps.net) · [API Backend](https://backend-tivitschool.graybush-2b90a918.brazilsouth.azurecontainerapps.io) · [Reportar Bug](../../issues) · [Sugerir Feature](../../issues)

</div>

---

## 📋 Índice

- [Sobre o Projeto](#-sobre-o-projeto)
- [Funcionalidades](#-funcionalidades)
- [Arquitetura](#-arquitetura)
- [Stack Tecnológica](#-stack-tecnológica)
- [Pré-requisitos](#-pré-requisitos)
- [Instalação e Execução](#-instalação-e-execução)
- [Variáveis de Ambiente](#-variáveis-de-ambiente)
- [Estrutura de Pastas](#-estrutura-de-pastas)
- [Perfis de Usuário](#-perfis-de-usuário)
- [API Reference](#-api-reference)
- [Deploy](#-deploy)
- [Contribuindo](#-contribuindo)
- [Licença](#-licença)

---

## 🎯 Sobre o Projeto

O **Edu Connect** é um sistema de gestão escolar full stack desenvolvido para digitalizar e centralizar os processos de uma instituição de ensino médio. A plataforma oferece experiências distintas e personalizadas para três perfis de usuário — **Aluno**, **Professor** e **Coordenador** — cobrindo desde o processo de matrícula digital até o lançamento de notas, controle de frequência e geração de exercícios com Inteligência Artificial.

### 🏗️ Repositórios

| Projeto | Repositório | Deploy |
|---|---|---|
| Frontend (React) | `tivit-school-react` | Azure Static Web Apps |
| Backend (.NET) | `edu-connect-backend` | Azure Container Apps |

---

## ✨ Funcionalidades

### 🎒 Aluno
- **Dashboard** com notas recentes, avisos e tarefas pendentes
- **Boletim escolar** com médias por bimestre e download em PDF
- **Matérias** — acesso a materiais, vídeos, arquivos e atividades por disciplina
- **Calendário** de eventos da escola e da turma
- **Portal de Matrícula** — processo completo em etapas (wizard) com validação OTP por e-mail, upload de documentos e pagamento via PIX

### 👩‍🏫 Professor
- **Dashboard** com KPIs, lista de alunos em atenção e próximas aulas
- **Lançamento de Notas** em lote por turma e disciplina
- **Chamada Digital** — registro de frequência por aula e horário
- **Gerenciamento de Matérias** — criação de tópicos e publicação de materiais (links, PDFs, atividades)
- **Exercícios com IA** — geração automática de questões de múltipla escolha a partir de PDFs usando LLM (Groq / LLaMA 3)

### 🏫 Coordenador
- **Dashboard** com estatísticas gerais, gráficos de desempenho por disciplina e situação acadêmica dos alunos
- **Gestão de Alunos** — CRUD completo com filtro e busca
- **Gestão de Professores** — CRUD com vinculação de disciplinas
- **Gestão de Turmas** — criação de turmas e vínculos disciplina-professor
- **Aprovação de Matrículas** — análise de documentos e comprovantes de pagamento com envio de e-mails automáticos
- **Calendário** de eventos gerais e por turma

### 🔐 Autenticação & Segurança
- Login com JWT (validade de 8h)
- Recuperação de senha via código OTP por e-mail (validade de 15 min)
- Controle de acesso baseado em perfil (RBAC) em todas as rotas e endpoints

---

## 🏛️ Arquitetura

```
┌─────────────────────────────────────────────────────────┐
│                     Cliente (Browser)                     │
│              React 19 + Vite + React Router               │
└────────────────────────┬────────────────────────────────┘
                         │ HTTPS / JSON (Axios)
┌────────────────────────▼────────────────────────────────┐
│               Backend (.NET 10 Web API)                   │
│  Controllers → Services → Repositories → Entity Framework│
│                                                           │
│  ┌──────────────┐  ┌──────────────┐  ┌───────────────┐  │
│  │  SQL Server  │  │ Azure Blob   │  │  Groq AI API  │  │
│  │  (EF Core)   │  │  (uploads)   │  │  (exercícios) │  │
│  └──────────────┘  └──────────────┘  └───────────────┘  │
│                          ┌──────────────┐                │
│                          │  SMTP Email  │                │
│                          │  (MailKit)   │                │
│                          └──────────────┘                │
└─────────────────────────────────────────────────────────┘
```

O backend segue uma arquitetura em camadas:

- **Controllers** — recebem as requisições HTTP e delegam ao serviço
- **Services** — contêm a regra de negócio
- **Repositories** — isolam o acesso ao banco de dados via EF Core
- **Mappers** — convertem entre entidades de domínio e DTOs
- **Middleware** — tratamento global de exceções com respostas padronizadas

---

## 🛠️ Stack Tecnológica

### Frontend
| Tecnologia | Versão | Uso |
|---|---|---|
| React | 19.2 | UI Framework |
| Vite | 7.x | Build tool & dev server |
| React Router DOM | 7.13 | Roteamento SPA |
| Axios | 1.13 | Requisições HTTP |
| Chart.js + react-chartjs-2 | 4.5 | Gráficos e dashboards |
| pdfjs-dist | 5.5 | Leitura de PDFs no browser |
| CSS Modules | — | Estilização por componente |
| Font Awesome | 6.5 | Ícones |

### Backend
| Tecnologia | Versão | Uso |
|---|---|---|
| .NET / ASP.NET Core | 10.0 | Web API Framework |
| Entity Framework Core | 10.0 | ORM |
| SQL Server | — | Banco de dados relacional |
| JWT Bearer | 10.0 | Autenticação stateless |
| BCrypt.Net | 4.1 | Hash de senhas |
| MailKit | 4.14 | Envio de e-mails SMTP |
| QuestPDF | 2025.x | Geração de PDFs (boletim) |
| Azure Blob Storage SDK | 12.27 | Upload de documentos |
| Groq API (LLaMA 3) | — | Geração de exercícios com IA |
| Swashbuckle (Swagger) | 6.6 | Documentação da API |

### Infraestrutura
| Serviço | Uso |
|---|---|
| Azure Static Web Apps | Hospedagem do frontend |
| Azure Container Apps | Hospedagem do backend (Docker) |
| Azure Container Registry | Registro de imagem Docker |
| Azure Blob Storage | Armazenamento de documentos de matrícula |
| GitHub Actions | CI/CD (deploy automático no push para `main`) |

---

## 📦 Pré-requisitos

- **Node.js** >= 20.19 (frontend)
- **.NET SDK** 10.0 (backend)
- **SQL Server** (local ou Azure SQL)
- **Conta Azure** (opcional, para Blob Storage)
- **Conta Groq** (opcional, para geração de exercícios com IA)

---

## 🚀 Instalação e Execução

### Frontend

```bash
# 1. Clone o repositório frontend
git clone https://github.com/seu-usuario/tivit-school-react.git
cd tivit-school-react

# 2. Instale as dependências
npm install

# 3. Configure as variáveis de ambiente
# Edite .env.development com a URL do seu backend local

# 4. Inicie o servidor de desenvolvimento
npm run dev
# → http://localhost:5173

# Build para produção
npm run build
```

### Backend

```bash
# 1. Clone o repositório backend
git clone https://github.com/seu-usuario/edu-connect-backend.git
cd edu-connect-backend

# 2. Configure o appsettings.json (veja seção de variáveis de ambiente)

# 3. Aplique as migrations do banco de dados
dotnet ef database update

# 4. Inicie a API
dotnet run
# → http://localhost:5051
# → Swagger UI: http://localhost:5051/swagger
```

---

## ⚙️ Variáveis de Ambiente

### Frontend

Crie os arquivos `.env.development` e `.env.production` na raiz do projeto React:

```env
# .env.development
VITE_API_URL=http://localhost:5051

# .env.production
VITE_API_URL=https://seu-backend.azurecontainerapps.io
```

### Backend

Configure o arquivo `appsettings.json` (não versionado — use variáveis de ambiente ou Azure Key Vault em produção):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=EduConnect;User Id=...;Password=...;"
  },
  "Jwt": {
    "Key": "sua-chave-secreta-de-pelo-menos-32-caracteres"
  },
  "SmtpSettings": {
    "Server": "smtp.seuprovedor.com",
    "Port": 587,
    "SenderName": "Edu Connect",
    "SenderEmail": "noreply@educonnect.com",
    "Username": "seu-usuario-smtp",
    "Password": "sua-senha-smtp",
    "EnableSsl": true
  },
  "AzureBlob": {
    "ConnectionString": "DefaultEndpointsProtocol=https;AccountName=..."
  },
  "Ai": {
    "ApiKey": "sua-chave-groq"
  },
  "edu-connect-variables": {
    "DOMINIO_EMAIL_ALUNO": "@aluno.educonnect.com",
    "DOMINIO_EMAIL_PROFESSOR": "@professor.educonnect.com",
    "SENHA_PADRAO": "Edu@2025"
  }
}
```

---

## 📁 Estrutura de Pastas

### Frontend (`tivit-school-react`)

```
src/
├── assets/
│   ├── images/          # Logotipos e fotos
│   └── styles/          # CSS global e variáveis de tema
├── components/          # Componentes reutilizáveis
│   ├── Button/
│   ├── Card/
│   ├── Modal/
│   ├── Sidebar/
│   ├── Table/
│   ├── Topbar/
│   ├── ExerciciosModal/ # Componente de IA
│   └── ...
├── contexts/            # React Context (Auth, Theme, Dialog)
├── hooks/               # Custom hooks (useAlunos, useNotas, ...)
├── layouts/             # MainLayout (Sidebar + Topbar + Outlet)
├── pages/               # Páginas por rota
│   ├── Dashboard/       # DashboardAluno / Professor / Coordenador
│   ├── Materias/
│   ├── Boletim/
│   ├── Frequencia/
│   ├── Notas/
│   ├── Matricula/       # Wizard de matrícula
│   ├── Alunos/
│   ├── Professores/
│   ├── Turmas/
│   └── ...
├── services/
│   ├── api.js           # Todas as chamadas à API
│   └── axiosInstance.js # Interceptors (auth + erros)
├── constants/           # Enums e constantes compartilhadas
└── utils/               # tokenUtils (decode JWT)
```

### Backend (`edu-connect-backend`)

```
├── Application/
│   ├── DTOs/            # Data Transfer Objects (request/response)
│   ├── Mappers/         # Conversão entre entidades e DTOs
│   └── Services/        # Regras de negócio
├── Domain/
│   ├── Entities/        # Entidades EF Core (Aluno, Nota, Turma...)
│   └── Enums/           # PerfilUsuario, StatusMatricula, Turno...
├── Infrastructure/
│   ├── AI/              # AiService (integração Groq)
│   ├── Blob/            # BlobService (Azure Storage)
│   ├── Email/           # EmailService (MailKit SMTP)
│   └── Persistence/
│       ├── Context/     # ConnectionContext (EF Core DbContext)
│       └── Repositories/ # Acesso a dados por entidade
└── WebAPI/
    ├── Configuration/   # JWT, CORS, Swagger, SMTP settings
    ├── Controllers/     # Endpoints REST
    ├── Middleware/      # GlobalExceptionHandler
    └── Util/            # ColetaInfoToken (helpers JWT)
```

---

## 👥 Perfis de Usuário

O sistema possui três perfis com acesso e rotas distintos:

| Perfil | Descrição | Acesso |
|---|---|---|
| `Aluno` | Estudante matriculado | Dashboard, Boletim, Matérias, Calendário |
| `Professor` | Docente da instituição | Dashboard, Matérias, Frequência, Notas, Calendário |
| `Coordenador` | Gestão administrativa | Dashboard, Alunos, Professores, Turmas, Matrículas, Calendário |

O controle de acesso é implementado via:
- **Frontend** — rotas protegidas por `PrivateRoute` e menus filtrados por `userRole`
- **Backend** — atributos `[Authorize(Roles = "...")]` em cada controller/endpoint

---

## 📡 API Reference

A documentação interativa completa da API está disponível via Swagger:

> **Desenvolvimento:** `http://localhost:5051/swagger`  
> **Produção:** `https://backend-tivitschool.graybush-2b90a918.brazilsouth.azurecontainerapps.io/swagger`

### Principais Grupos de Endpoints

| Prefixo | Descrição |
|---|---|
| `POST /auth/login` | Autenticação e geração de token JWT |
| `POST /auth/esqueci-senha` | Solicitar reset via e-mail (OTP) |
| `POST /auth/resetar-senha` | Redefinir senha com token OTP |
| `GET /api/alunos` | Listar alunos (Coordenador) |
| `GET /api/professores` | Listar professores |
| `GET /api/turmas` | Listar turmas e vínculos |
| `GET /api/disciplinas/listar` | Disciplinas do usuário logado |
| `GET /api/notas/boletim` | Boletim do aluno logado |
| `POST /api/notas/lote` | Lançar notas em lote (Professor) |
| `POST /api/frequencias/chamada` | Registrar chamada (Professor) |
| `GET /api/eventos` | Eventos do calendário (por mês/ano) |
| `GET /api/notificacoes` | Notificações do usuário logado |
| `GET /api/dashboards/coordenador` | KPIs e gráficos para coordenação |
| `POST /api/matriculas/iniciar` | Iniciar solicitação de matrícula |
| `PUT /api/matriculas/{id}/avaliar` | Aprovar/rejeitar matrícula (Coordenador) |
| `POST /api/exercicios/gerar` | Gerar exercícios via IA a partir de PDF |

> **Autenticação:** Todas as rotas (exceto `/auth/*` e `/api/matriculas/iniciar`) requerem o header `Authorization: Bearer <token>`.

---

## ☁️ Deploy

O deploy é realizado automaticamente via **GitHub Actions** ao realizar push na branch `main`.

### Frontend → Azure Static Web Apps

O workflow `.github/workflows/azure-static-web-apps-icy-wave-0e2eac10f.yml`:
1. Faz checkout do repositório
2. Executa o build com `vite build` (usando `.env.production`)
3. Publica a pasta `dist/` no Azure Static Web Apps

O arquivo `staticwebapp.config.json` configura o fallback de navegação para suportar o roteamento SPA do React Router.

### Backend → Azure Container Apps

O workflow `.github/workflows/backend-tivitschool-AutoDeployTrigger-*.yml`:
1. Faz login no Azure via OIDC (sem segredos de longa duração)
2. Builda a imagem Docker usando o `Dockerfile` do projeto
3. Faz push para o **Azure Container Registry** (`acrtivitschool.azurecr.io`)
4. Atualiza o **Azure Container App** (`backend-tivitschool`) com a nova imagem

#### Secrets necessários no GitHub

| Secret | Descrição |
|---|---|
| `AZURE_STATIC_WEB_APPS_API_TOKEN_ICY_WAVE_0E2EAC10F` | Token do Static Web App (frontend) |
| `BACKENDTIVITSCHOOL_AZURE_CLIENT_ID` | Client ID do Service Principal |
| `BACKENDTIVITSCHOOL_AZURE_TENANT_ID` | Tenant ID do Azure AD |
| `BACKENDTIVITSCHOOL_AZURE_SUBSCRIPTION_ID` | Subscription ID do Azure |
| `BACKENDTIVITSCHOOL_REGISTRY_USERNAME` | Usuário do Container Registry |
| `BACKENDTIVITSCHOOL_REGISTRY_PASSWORD` | Senha do Container Registry |

---

## 🤝 Contribuindo

Contribuições são bem-vindas! Para contribuir:

1. Faça um **fork** do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/MinhaFeature`)
3. Faça commit das suas alterações (`git commit -m 'feat: adiciona MinhaFeature'`)
4. Faça push para a branch (`git push origin feature/MinhaFeature`)
5. Abra um **Pull Request**

### Convenção de Commits

Este projeto segue o padrão [Conventional Commits](https://www.conventionalcommits.org/):

```
feat:     nova funcionalidade
fix:      correção de bug
docs:     atualização de documentação
style:    formatação de código (sem mudança de lógica)
refactor: refatoração sem nova feature ou fix
test:     adição ou correção de testes
chore:    tarefas de manutenção (build, deps, ci)
```

---

## 📄 Licença

Distribuído sob a licença MIT. Veja `LICENSE` para mais informações.

---

<div align="center">

Desenvolvido com ❤️ pela equipe **TIVIT School**

</div>
