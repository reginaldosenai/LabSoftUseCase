# 🚀 Guia Prático: SISTEMA CLÍNICA ODONTOLÓGICA - GESTÃO DE PROCEDIMENTOS E AGENDAMENTOS

## 📌 Contexto do Projeto: Sistema da Clínica Odontológica "Sorriso & Saúde"

A clínica odontológica **"Sorriso & Saúde"** precisa informatizar o gerenciamento de seus pacientes, procedimentos odontológicos e agendamentos de consultas. 

Durante a reunião inicial com o gestor da clínica, foram definidas as seguintes necessidades para o sistema:
* **Paciente:** Armazena os dados pessoais (Nome, CPF, Telefone e Data de Nascimento). O sistema deve permitir buscar pacientes por termo (nome ou CPF) e editar seus dados.
* **Procedimento:** Representa o procedimento odontológico (Descrição, Valor, Observação) e **deve estar obrigatoriamente associado a um Paciente** previamente cadastrado.
* **Agendamento:** Representa o agendamento da consulta, vinculando o **Paciente** e o **Procedimento** odontológico a uma **Data/Hora** e um **Status** (Ex: Agendada, Realizada, Cancelada). A listagem deve ser exibida **ordenada pela data da consulta**, permitindo também atualizar a data de um agendamento já cadastrado.
* **Autenticação:** O acesso às funcionalidades principais exige autenticação com tempo de expiração (Sessão/Cookies). A tela principal deve exibir o nome do usuário logado, menu de acesso aos recursos e opção para sair do sistema.

---

## 🛠️ Pré-requisitos
- Visual Studio 2022 (com suporte ao .NET instalado)
- Microsoft SQL Server e SQL Server Management Studio (SSMS)

---

## 🗄️ Etapa 1: Levantamento de Requisitos Funcionais

Os requisitos funcionais estão atrelados às regras de negócio definidas pelo gestor da clínica. Abaixo está a lista inicial de requisitos funcionais que guiarão o desenvolvimento:

| ID | Requisito | Descrição |
| :--- | :--- | :--- |
| **R1** | Autenticação e Controle de Acesso | O sistema deve possuir tela de login com validação de falha e tempo de expiração. A tela principal deve exibir o nome do usuário logado, menu de navegação e opção de sair. |
| **R2** | CRUD e Busca de Pacientes | O sistema deve permitir cadastrar, visualizar, editar e excluir pacientes, além de disponibilizar um **campo de busca** na listagem para filtrar pacientes por termo informado. |
| **R3** | CRUD de Procedimentos | O sistema deve permitir cadastrar, visualizar, editar e excluir procedimentos odontológicos, exigindo a associação obrigatória a um **Paciente** previamente cadastrado. |
| **R4** | CRUD e Ordenação de Agendamentos | O sistema deve permitir agendar consultas vinculando **Paciente** e **Procedimento**, exibindo a listagem **ordenada pela data da consulta** e permitindo atualizar a data. |
| **R5** | Integridade Relacional | Não deve ser possível excluir um paciente que possua procedimentos ou agendamentos vinculados, nem um procedimento vinculado a um agendamento. |

---

## 🗄️ Etapa 2: DER - Diagrama de Entidade e Relacionamento

Com base no contexto e nas regras de negócio, nossa estrutura de banco de dados terá três tabelas principais:
1. **Paciente** (`Codigo` PK, `Nome`, `Cpf`, `Telefone`, `DataNascimento`)
2. **Procedimento** (`Codigo` PK, `Descricao`, `Valor`, `PacienteId` FK)
3. **Agendamento** (`Codigo` PK, `DataHora`, `StatusAgendamento`, `PacienteId` FK, `ProcedimentoId` FK)

> **ATENÇÃO:** No mercado de trabalho, o importante é focar no escopo solicitado. Implemente exatamente as tabelas, campos e relacionamentos definidos para atender às regras de negócio.

### Passo a Passo para Criação via Interface (SSMS)

1. Abra o **SQL Server Management Studio (SSMS)** e conecte-se ao seu servidor.
2. Clique com o botão direito em **Bancos de Dados** > **Novo Banco de Dados**.
3. Defina o nome como **`dbOdonto`** e clique em **OK**.
4. Expanda o banco **`dbOdonto`**, clique com o botão direito em **Diagramas de Banco de Dados** > **Novo Diagrama de Banco de Dados** (confirme com *Sim* caso apareça um aviso e feche a janela de adicionar tabelas).
5. **Criando a tabela `Paciente`:**
   - Clique com o botão direito na área em branco > **Nova Tabela** > Nomeie como **`Paciente`**.
   - Adicione as colunas:
     - `Codigo` (`int`, desmarque Permitir Nulos, defina como **Chave Primária** e em Propriedades configure **Especificação de Identidade = Sim**, Incremento = 1, Semente = 1).
     - `Nome` (`varchar(100)`, não nulo).
     - `Cpf` (`varchar(14)`, não nulo).
     - `Telefone` (`varchar(20)`, não nulo).
     - `DataNascimento` (`date`, não nulo).
6. **Criando a tabela `Procedimento`:**
   - Crie uma nova tabela chamada **`Procedimento`**.
   - Adicione as colunas:
     - `Codigo` (`int`, Chave Primária com Identidade/Auto-incremento = Sim).
     - `Descricao` (`varchar(100)`, não nulo).
     - `Valor` (`decimal(10,2)`, não nulo).
     - `PacienteId` (`int`, não nulo — *não colocar identidade neste campo*).
7. **Criando a tabela `Agendamento`:**
   - Crie uma nova tabela chamada **`Agendamento`**.
   - Adicione as colunas:
     - `Codigo` (`int`, Chave Primária com Identidade/Auto-incremento = Sim).
     - `DataHora` (`datetime`, não nulo).
     - `StatusAgendamento` (`varchar(30)`, não nulo).
     - `PacienteId` (`int`, não nulo).
     - `ProcedimentoId` (`int`, não nulo).
8. **Criando os Relacionamentos (Chaves Estrangeiras):**
   - Arraste a chave amarela de `Codigo` (tabela `Paciente`) e solte sobre `PacienteId` (tabela `Procedimento`). Confirme e clique em OK.
   - Arraste a chave amarela de `Codigo` (tabela `Paciente`) e solte sobre `PacienteId` (tabela `Agendamento`). Confirme e clique em OK.
   - Arraste a chave amarela de `Codigo` (tabela `Procedimento`) e solte sobre `ProcedimentoId` (tabela `Agendamento`). Confirme e clique em OK.
9. Salve o diagrama com o nome **`DER_Odonto`**. *(Dica: Caso o SSMS bloqueie a alteração de tabelas salvas, vá em Ferramentas > Opções > Designers e desmarque "Impedir salvar alterações que exijam recriação da tabela").*

---

## 🗄️ Etapa 2B: Criação do Banco de Dados com Script SQL (Alternativa Rápida)

Caso prefira criar toda a estrutura via código e já atender à regra de negócio que exige **pelo menos 3 registros inseridos em cada tabela**, execute o script abaixo no SSMS:

```sql
CREATE DATABASE dbOdonto;
GO
USE dbOdonto;
GO

-- 1. Tabela Paciente
CREATE TABLE Paciente (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Cpf VARCHAR(14) NOT NULL UNIQUE,
    Telefone VARCHAR(20) NOT NULL,
    DataNascimento DATE NOT NULL
);
GO

-- 2. Tabela Procedimento (Associada obrigatoriamente a um Paciente)
CREATE TABLE Procedimento (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    Descricao VARCHAR(100) NOT NULL,
    Valor DECIMAL(10,2) NOT NULL,
    PacienteId INT NOT NULL,
    CONSTRAINT FK_Procedimento_Paciente FOREIGN KEY (PacienteId) 
        REFERENCES Paciente(Codigo)
);
GO

-- 3. Tabela Agendamento (Associada ao Paciente e ao Procedimento)
CREATE TABLE Agendamento (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    DataHora DATETIME NOT NULL,
    StatusAgendamento VARCHAR(30) NOT NULL,
    PacienteId INT NOT NULL,
    ProcedimentoId INT NOT NULL,
    CONSTRAINT FK_Agendamento_Paciente FOREIGN KEY (PacienteId) 
        REFERENCES Paciente(Codigo),
    CONSTRAINT FK_Agendamento_Procedimento FOREIGN KEY (ProcedimentoId) 
        REFERENCES Procedimento(Codigo)
);
GO

-- Inserindo pelo menos 3 registros em cada tabela (Regra de Negócio)
INSERT INTO Paciente (Nome, Cpf, Telefone, DataNascimento) VALUES 
('Maria Oliveira', '111.222.333-44', '(11) 98888-7777', '1990-05-15'),
('João Souza', '555.666.777-88', '(11) 97777-6666', '1985-10-20'),
('Ana Clara Costa', '999.888.777-66', '(11) 96666-5555', '1995-03-12');
GO

INSERT INTO Procedimento (Descricao, Valor, PacienteId) VALUES 
('Limpeza e Profilaxia', 150.00, 1),
('Restauração em Resina', 280.00, 2),
('Clareamento Dental', 600.00, 3);
GO

INSERT INTO Agendamento (DataHora, StatusAgendamento, PacienteId, ProcedimentoId) VALUES 
('2026-11-20 14:00:00', 'Agendada', 1, 1),
('2026-11-18 09:30:00', 'Realizada', 2, 2),
('2026-11-25 16:00:00', 'Agendada', 3, 3);
GO
```

---

## 📦 Etapa 3: Criação do Projeto ASP.NET Core MVC

1. Abra o **Visual Studio 2022** e clique em **Criar um novo projeto**.
2. Escolha o modelo **Aplicativo Web do ASP.NET Core (Model-View-Controller)** e clique em **Próximo**.
3. Nomeie o projeto como **`appOdonto`** e clique em **Próximo**.
4. Selecione o Framework **.NET 8.0** (ou superior instalado) e clique em **Criar**.

### Instalando os Pacotes NuGet

Clique com o botão direito no projeto `appOdonto` > **Gerenciar Pacotes NuGet** > aba **Procurar** e instale os três pacotes abaixo (na mesma versão principal do seu .NET):
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`
- `Microsoft.VisualStudio.Web.CodeGeneration.Design`

---

## ⚙️ Etapa 4: String de Conexão e Engenharia Reversa (Scaffolding)

1. Abra o arquivo **`appsettings.json`** na raiz do projeto e configure a conexão com o banco `dbOdonto`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "ConexaoSqlServer": "Server=.\\SQLEXPRESS; Database=dbOdonto; User Id=sa; Password=SUA_SENHA; TrustServerCertificate=True;"
  }
}
```
*(Nota: Ajuste `Server=.\\SQLEXPRESS` para o nome da sua instância SQL Server, como `.\\SENAI` ou `localhost`, ou utilize `Trusted_Connection=True;` caso use Autenticação do Windows).*

2. Abra o **Console do Gerenciador de Pacotes** (*Ferramentas > Gerenciador de Pacotes do NuGet > Console do Gerenciador de Pacotes*) e execute o comando:

```powershell
Scaffold-DbContext "Name=ConexaoSqlServer" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
```

3. Verifique a pasta **`Models`**: devem ter sido criadas as classes `Paciente.cs`, `Procedimento.cs`, `Agendamento.cs` e `DbOdontoContext.cs`. *(Caso alguma classe fique no plural ou com nome diferente da tabela, ajuste para manter no singular).*

4. **Registre o Contexto no `Program.cs`:** Abra o `Program.cs` e adicione o registro antes de `var app = builder.Build();`:

```csharp
using Microsoft.EntityFrameworkCore;
using appOdonto.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Registrando o DbContext
builder.Services.AddDbContext<DbOdontoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoSqlServer")));
```

5. **Compile a solução** pressionando **`Ctrl + Shift + B`**.

---

## 🎨 Etapa 5: Gerando os CRUDs Automáticos (Scaffolding MVC)

Agora vamos gerar as telas e controladores para **Paciente**, **Procedimento** e **Agendamento**:

1. Clique com o botão direito na pasta **`Controllers`** > **Adicionar** > **Novo Item com Scaffold...**.
2. Selecione **Controlador MVC com exibições, usando o Entity Framework** e clique em **Adicionar**.
3. Configure:
   - **Classe de modelo:** `Paciente (appOdonto.Models)`
   - **Classe do contexto de dados:** `DbOdontoContext (appOdonto.Models)`
   - **Nome do controlador:** `PacienteController` (mantenha no singular)
4. Clique em **Adicionar**.
5. **Repita exatamente o mesmo processo** criando:
   - `ProcedimentoController` (usando o modelo `Procedimento`)
   - `AgendamentoController` (usando o modelo `Agendamento`)

> **Observação importante:** O Scaffolding já cria automaticamente o campo de seleção (`<select>`) para escolher o **Paciente** ao cadastrar um **Procedimento**, e para escolher o **Paciente** e o **Procedimento** ao criar um **Agendamento**!

---

## 🔍 Etapa 6: Implementando a Caixa de Busca na Tela de Pacientes

Para atender à regra de negócio que exige um **recurso de busca por termo na listagem de pacientes**, siga os passos abaixo no Controller e na View:

### Passo 1: Alterar o método `Index` no `PacienteController.cs`

Abra o arquivo `Controllers/PacienteController.cs` e localize o método `Index()`. Substitua-o pelo código abaixo para que ele receba o parâmetro `termoBusca`, filtre pelo Nome ou CPF e devolva o termo digitado para a tela:

```csharp
// GET: Paciente
public async Task<IActionResult> Index(string termoBusca)
{
    // Guarda o termo pesquisado para manter preenchido na caixinha de busca
    ViewData["FiltroAtual"] = termoBusca;

    // Prepara a consulta no banco sem executar ainda
    var pacientes = from p in _context.Pacientes
                    select p;

    // Se o usuário digitou algo na caixa de busca, aplica o filtro
    if (!String.IsNullOrEmpty(termoBusca))
    {
        pacientes = pacientes.Where(p => p.Nome.Contains(termoBusca) 
                                      || p.Cpf.Contains(termoBusca));
    }

    // Executa a consulta e envia a lista filtrada para a View
    return View(await pacientes.ToListAsync());
}
```

### Passo 2: Adicionar o formulário de busca na View `Views/Paciente/Index.cshtml`

Abra o arquivo `Views/Paciente/Index.cshtml`. Logo abaixo do link `<a asp-action="Create">...</a>` e **antes da tag `<table>`**, adicione o formulário HTML com a caixa de texto e os botões de pesquisar e limpar:

```html
<h1>Lista de Pacientes</h1>

<p>
    <a asp-action="Create" class="btn btn-success">Novo Paciente</a>
</p>

<!-- Caixinha de Busca de Pacientes -->
<form asp-action="Index" method="get" class="row g-2 mb-4">
    <div class="col-md-6">
        <input type="text" 
               name="termoBusca" 
               value="@ViewData["FiltroAtual"]" 
               class="form-control" 
               placeholder="Digite o nome ou CPF do paciente..." />
    </div>
    <div class="col-auto">
        <button type="submit" class="btn btn-primary">Buscar</button>
        <a asp-action="Index" class="btn btn-outline-secondary">Limpar</a>
    </div>
</form>

<table class="table">
    <!-- Restante do código da tabela gerado pelo Scaffolding... -->
```

---

## 📅 Etapa 7: Ordenando Agendamentos por Data e Melhorando a Exibição

Conforme definido nas regras de negócio:
1. A listagem de **Agendamentos** deve ser exibida **ordenada pela data da consulta** (`OrderBy`).
2. Na tela de **Procedimentos** e **Agendamentos**, devemos exibir o **Nome** do Paciente e a **Descrição** do Procedimento (em vez de apenas o número do Código/ID).

### 1. Ajustando `AgendamentoController.cs`

Abra `Controllers/AgendamentoController.cs` e atualize o método `Index` para incluir os dados relacionados e ordenar por `DataHora`:

```csharp
// GET: Agendamento
public async Task<IActionResult> Index()
{
    var agendamentosOrdenados = _context.Agendamentos
        .Include(a => a.Paciente)
        .Include(a => a.Procedimento)
        .OrderBy(a => a.DataHora); // Ordena pela data da consulta (mais próxima primeiro)

    return View(await agendamentosOrdenados.ToListAsync());
}
```
*(Dica: Verifique também nos métodos `Create` e `Edit` do `AgendamentoController` e `ProcedimentoController` se o `SelectList` está mostrando o campo `Nome` do Paciente e `Descricao` do Procedimento: ex: `new SelectList(_context.Pacientes, "Codigo", "Nome")`)*

### 2. Ajustando `Views/Agendamento/Index.cshtml`

Na tabela de `Views/Agendamento/Index.cshtml`, certifique-se de exibir `@Html.DisplayFor(modelItem => item.Paciente.Nome)` e `@Html.DisplayFor(modelItem => item.Procedimento.Descricao)` nas colunas correspondentes.

---

## 🔒 Etapa 8: Implementando Autenticação com Tempo de Expiração (Session)

Para proteger o sistema e exibir o nome do usuário logado na barra superior com opção de sair:

### 1. Criar a `LoginViewModel.cs` na pasta `Models`

```csharp
using System.ComponentModel.DataAnnotations;

namespace appOdonto.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [Display(Name = "CPF de Acesso")]
        public string Cpf { get; set; } = string.Empty;
    }
}
```

### 2. Criar o `AccountController.cs` na pasta `Controllers`

Clique com o botão direito em `Controllers` > **Adicionar** > **Controlador** > **Controlador MVC - Vazio** (`AccountController.cs`):

```csharp
using appOdonto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace appOdonto.Controllers
{
    public class AccountController : Controller
    {
        private readonly DbOdontoContext _context;

        public AccountController(DbOdontoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(p => p.Cpf == model.Cpf);

            if (paciente == null)
            {
                ModelState.AddModelError("", "Falha na autenticação: CPF não encontrado.");
                return View(model);
            }

            // Salva os dados na Sessão
            HttpContext.Session.SetInt32("UsuarioId", paciente.Codigo);
            HttpContext.Session.SetString("UsuarioNome", paciente.Nome);

            return RedirectToAction("Index", "Agendamento");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
```

### 3. Criar a View `Views/Account/Login.cshtml`

Crie a pasta `Account` dentro de `Views` e adicione a View `Login.cshtml`:

```html
@model appOdonto.Models.LoginViewModel

@{
    ViewData["Title"] = "Login - Clínica Sorriso & Saúde";
}

<div class="col-md-4 mx-auto mt-5">
    <div class="card shadow-sm">
        <div class="card-header bg-primary text-white text-center">
            <h4>Clínica Sorriso & Saúde</h4>
            <small>Acesso ao Sistema</small>
        </div>
        <div class="card-body">
            <form asp-action="Login" method="post">
                <div asp-validation-summary="ModelOnly" class="text-danger mb-2"></div>

                <div class="mb-3">
                    <label asp-for="Cpf" class="form-label"></label>
                    <input asp-for="Cpf" class="form-control" placeholder="111.222.333-44" />
                    <span asp-validation-for="Cpf" class="text-danger"></span>
                </div>

                <button type="submit" class="btn btn-primary w-100 mb-2">Entrar</button>
            </form>
        </div>
        <div class="card-footer text-center">
            <a asp-controller="Paciente" asp-action="Create">Cadastrar novo paciente</a>
        </div>
    </div>
</div>

@section Scripts {
    @{ await Html.RenderPartialAsync("_ValidationScriptsPartial"); }
}
```

### 4. Ativar a Session com Tempo de Expiração no `Program.cs`

No arquivo `Program.cs`, adicione a configuração de sessão (com tempo de expiração de 20 minutos) e o `IHttpContextAccessor` para exibir o nome no menu:

```csharp
// Antes de var app = builder.Build();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Tempo de expiração da sessão
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Depois de app.UseRouting();
app.UseRouting();
app.UseSession();
app.UseAuthorization();
```

### 5. Proteger os Controllers (`PacienteController`, `ProcedimentoController` e `AgendamentoController`)

Adicione a verificação de sessão no início das ações (ou sobrescrevendo `OnActionExecuting`) nos Controllers protegidos:

```csharp
public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
{
    if (HttpContext.Session.GetInt32("UsuarioId") == null)
    {
        context.Result = new RedirectToActionResult("Login", "Account", null);
    }
    base.OnActionExecuting(context);
}
```
*(Dica: Se quiser permitir que alguém se cadastre sem estar logado, não coloque essa trava no `PacienteController` ou libere apenas as ações `Create` e `Index`).*

### 6. Atualizando o Menu Principal (`Views/Shared/_Layout.cshtml`)

Abra `Views/Shared/_Layout.cshtml`, adicione `@using Microsoft.AspNetCore.Http` e `@inject IHttpContextAccessor HttpContextAccessor` no topo do arquivo, e atualize a barra de navegação (`<nav>`) para exibir o menu de recursos, o nome do usuário logado e o botão **Sair**:

```html
@using Microsoft.AspNetCore.Http
@inject IHttpContextAccessor HttpContextAccessor

<!-- Dentro da <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between"> -->
@{
    var usuarioNome = HttpContextAccessor.HttpContext?.Session.GetString("UsuarioNome");
}

<ul class="navbar-nav flex-grow-1">
    @if (!string.IsNullOrEmpty(usuarioNome))
    {
        <li class="nav-item">
            <a class="nav-link text-dark" asp-controller="Paciente" asp-action="Index">Pacientes</a>
        </li>
        <li class="nav-item">
            <a class="nav-link text-dark" asp-controller="Procedimento" asp-action="Index">Procedimentos</a>
        </li>
        <li class="nav-item">
            <a class="nav-link text-dark" asp-controller="Agendamento" asp-action="Index">Agendamentos</a>
        </li>
    }
</ul>

<ul class="navbar-nav ms-auto">
    @if (!string.IsNullOrEmpty(usuarioNome))
    {
        <li class="nav-item">
            <span class="nav-link text-primary">Olá, <strong>@usuarioNome</strong></span>
        </li>
        <li class="nav-item">
            <a class="nav-link text-danger fw-bold" asp-controller="Account" asp-action="Logout">Sair</a>
        </li>
    }
    else
    {
        <li class="nav-item">
            <a class="nav-link text-primary" asp-controller="Account" asp-action="Login">Entrar</a>
        </li>
    }
</ul>
```

---

## 🎯 Etapa 9: Checklist Final de Validação

Execute o projeto (`F5`) e valide todas as entregas solicitadas:
- [ ] Banco de dados criado com pelo menos **3 registros** em `Paciente`, `Procedimento` e `Agendamento`.
- [ ] Tela de **Login** validando CPF inexistente e redirecionando após sucesso.
- [ ] Barra superior exibindo **nome do usuário logado**, links para **Pacientes**, **Procedimentos**, **Agendamentos** e opção **Sair**.
- [ ] Tela de **Pacientes** permitindo **buscar por termo**, cadastrar e **editar** dados.
- [ ] Tela de **Procedimentos** obrigando a seleção de um **Paciente** cadastrado.
- [ ] Tela de **Agendamentos** listando os registros **ordenados pela data da consulta**, exibindo os dados do Paciente e do Procedimento, e permitindo **editar a data** da consulta.