# 🚀 Guia Prático: SISTEMA PETSHOP - AGENDAMENTO DE SERVIÇOS

## 📌 Contexto do Projeto: Sistema de Gestão do PetShop "Patas & Carinho"

O PetShop "Patas & Carinho" precisa informatizar o atendimento de serviços prestados aos pets para melhorar a organização da agenda diária. Atualmente, os tutores/clientes ligam ou vão presencialmente ao estabelecimento para agendar atendimentos de especialidades veterinárias e procedimentos gerais.

No sistema, um **Cliente** cadastra seus dados pessoais (Nome, CPF, Telefone e Data de Nascimento). O **Veterinario** possui seu cadastro contendo Nome, CRMV e Especialidade. A **ReservaServico** representa o agendamento em si, onde é registrado o Cliente, o Veterinario responsável pelo atendimento, a Data/Hora agendada, o Valor do serviço informado no momento do agendamento e o Status do atendimento (Ex: Agendada, Realizada, Cancelada).

> **Regra do Sistema:** O cliente só consegue realizar uma reserva de serviço se estiver logado. Ao abrir o sistema, a primeira tela apresentada deve ser a de login, contendo também a opção para cadastro de novos clientes.

---

## 🛠️ Pré-requisitos

* Visual Studio 2022 (com suporte ao .NET 9 instalado)
* Microsoft SQL Server e SQL Server Management Studio (SSMS)

---

## 🗄️ Etapa 1: Levantamento de Requisitos Funcionais

Os requisitos funcionais referem-se às regras de negócio da aplicação. Diferenciam-se dos requisitos não funcionais, que tratam das tecnologias empregadas (como linguagem de programação ou banco de dados).

| ID | Requisito | Descrição |
| :--- | :--- | :--- |
| **R1** | Cadastro de Clientes | O sistema deve permitir cadastrar, visualizar, atualizar e excluir (CRUD) os dados dos clientes. |
| **R2** | Cadastro de Veterinários | O sistema deve permitir cadastrar, visualizar, atualizar e excluir (CRUD) os dados dos veterinários (incluindo CRMV e Especialidade). |
| **R3** | Reserva de Serviço | O sistema deve permitir agendar reservas vinculando obrigatoriamente 1 Cliente e 1 Veterinario a uma data/hora e informando o Valor do serviço. |
| **R4** | Controle de Status | Toda reserva deve iniciar com o status "Agendada", permitindo alteração para "Realizada" ou "Cancelada". |
| **R5** | Integridade Relacional | Não deve ser possível excluir um cliente ou veterinário que já possua reservas vinculadas em seu histórico. |

---

## 🗄️ Etapa 2: Diagrama de Entidade e Relacionamento (DER)

A estrutura do nosso banco de dados contempla as seguintes entidades e relacionamentos:

* **Cliente** (<u>Codigo</u>, Nome, Cpf, Telefone, DataNascimento)
* **Veterinario** (<u>Codigo</u>, Nome, Crmv, Especialidade)
* **ReservaServico** (<u>Codigo</u>, DataHora, Valor, StatusServico, ClienteId, VeterinarioId)

> ⚠️ **Atenção Profissional:** No mercado de trabalho, o foco deve ser na entrega do que foi solicitado no escopo. Evite adicionar tabelas ou campos sem o devido alinhamento com sua supervisão.

---

## 🗄️ Etapa 2B: Criação do Banco de Dados via Script SQL

Execute o script SQL abaixo no SSMS para criar o banco `dbPetShop`, suas tabelas, chaves primárias/estrangeiras e dados de teste iniciais:

```sql
CREATE DATABASE dbPetShop;
GO
USE dbPetShop;
GO

-- Tabela Cliente
CREATE TABLE Cliente (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Cpf VARCHAR(14) NOT NULL UNIQUE,
    Telefone VARCHAR(20) NOT NULL,
    DataNascimento DATE NOT NULL
);
GO

-- Tabela Veterinario
CREATE TABLE Veterinario (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Crmv VARCHAR(20) NOT NULL UNIQUE,
    Especialidade VARCHAR(50) NOT NULL
);
GO

-- Tabela ReservaServico
CREATE TABLE ReservaServico (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    DataHora DATETIME NOT NULL,
    Valor DECIMAL(10,2) NOT NULL,
    StatusServico VARCHAR(30) NOT NULL,
    ClienteId INT NOT NULL,
    VeterinarioId INT NOT NULL,
    CONSTRAINT FK_ReservaServico_Cliente FOREIGN KEY (ClienteId) 
        REFERENCES Cliente(Codigo),
    CONSTRAINT FK_ReservaServico_Veterinario FOREIGN KEY (VeterinarioId) 
        REFERENCES Veterinario(Codigo)
);
GO

-- Inserindo Dados Iniciais para Teste
INSERT INTO Cliente (Nome, Cpf, Telefone, DataNascimento) VALUES 
('Maria Oliveira', '111.222.333-44', '(11) 98888-7777', '1990-05-15'),
('João Souza', '555.666.777-88', '(11) 97777-6666', '1985-10-20');

INSERT INTO Veterinario (Nome, Crmv, Especialidade) VALUES 
('Dra. Helena Rios', 'CRMV/SP 12345', 'Dermatologia Veterinaria'),
('Dr. Roberto Alves', 'CRMV/SP 65432', 'Ortopedia Veterinaria');

INSERT INTO ReservaServico (DataHora, Valor, StatusServico, ClienteId, VeterinarioId) VALUES 
('2026-10-15 14:00:00', 180.00, 'Agendada', 1, 1),
('2026-10-16 09:30:00', 250.00, 'Agendada', 2, 2);
GO
```

### 📁 Importação de Dados via Arquivo CSV (Exemplo: `veterinarios.csv`)

Para praticar a importação de dados no SQL Server via SSMS (menu *Tarefas > Importar Dados* com `Flat File Source`), utilize o modelo CSV abaixo para cadastrar veterinários em lote:

```csv
Nome,Crmv,Especialidade
Dra. Camila Mendes,CRMV/SP 11223,Clínica Geral
Dr. Fernando Costa,CRMV/SP 44556,Oftalmologia Veterinária
Dra. Patricia Lima,CRMV/SP 77889,Cardiologia Veterinária
Dr. Marcelo Santos,CRMV/SP 99001,Odontologia Veterinária
Dra. Juliana Rocha,CRMV/SP 33445,Oncologia Veterinária
```

---

## 📦 Etapa 3: Criando o Projeto ASP.NET Core no Visual Studio

1. Abra o Visual Studio 2022.
2. Clique em **Criar um novo projeto**.
3. Selecione o modelo **Aplicação Web do ASP.NET Core (Model-View-Controller)** e avance.
4. Nomeie o projeto como `appPetShop`.
5. Selecione o Framework **.NET 9.0** e clique em **Criar**.

---

## 📦 Etapa 4: Instalação dos Pacotes do Entity Framework via NuGet

Instale os pacotes NuGet adequados à versão do seu projeto (.NET 9):

* `Microsoft.EntityFrameworkCore.SqlServer`
* `Microsoft.EntityFrameworkCore.Tools`
* `Microsoft.VisualStudio.Web.CodeGeneration.Design`

---

## ⚙️ Etapa 5: Configuração da String de Conexão no `appsettings.json`

Atualize o arquivo `appsettings.json` com as credenciais do seu banco de dados:

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
    "ConexaoSqlServer": "Server=.\\SENAI; Database=dbPetShop; User Id=sa; Password=senai.123; TrustServerCertificate=True;"
  }
}
```

---

## 🔨 Etapa 6: Engenharia Reversa (Scaffolding Database First)

Abra o **Console do Gerenciador de Pacotes** e execute o comando abaixo para gerar os Models a partir do banco de dados:

```powershell
Scaffold-DbContext "Name=ConexaoSqlServer" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
```

> **Ajuste de Nomeclatura:** Caso a classe de contexto seja gerada como `DbPetShopContext` e a model do serviço seja gerada no singular como `ReservaServico`, certifique-se de que os nomes das classes estejam padronizados e alinhados com as tabelas.

---

## 🔨 Etapa 7: Compilação e Registro no `Program.cs`

Recompile a solução (`Ctrl + Shift + B`) e registre o contexto na injeção de dependência no arquivo `Program.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using appPetShop.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DbPetShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoSqlServer")));

var app = builder.Build();
```

---

## 🎨 Etapa 8: Gerando o CRUD Automático (Scaffolding MVC)

1. Na pasta `Controllers`, clique com o botão direito > **Adicionar** > **Item do Scaffolding...**
2. Escolha **Controlador MVC com exibições, usando o Entity Framework**.
3. Gere os controllers para `Cliente`, `Veterinario` e `ReservaServico`.

---

## 🔒 Etapa 9: Implementando Autenticação por Cookie para Clientes

### 1. View Model de Login (`LoginViewModel.cs`)

```csharp
using System.ComponentModel.DataAnnotations;

namespace appPetShop.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [Display(Name = "CPF do Cliente")]
        public string Cpf { get; set; } = string.Empty;
    }
}
```

### 2. Controller de Autenticação (`AccountController.cs`)

```csharp
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using appPetShop.Models;

namespace appPetShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly DbPetShopContext _context;

        public AccountController(DbPetShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "ReservaServico");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Cpf == model.Cpf);

            if (cliente == null)
            {
                ModelState.AddModelError("", "CPF não encontrado. Faça seu cadastro primeiro.");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, cliente.Codigo.ToString()),
                new Claim(ClaimTypes.Name, cliente.Nome),
                new Claim("CPF", cliente.Cpf)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "ReservaServico");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
```

### 3. Configuração no `Program.cs` e Proteção com `[Authorize]`

```csharp
// No Program.cs:
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

// Ativação dos middlewares:
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
```

Adicione o atributo na controller de reservas (`ReservaServicoController.cs`):

```csharp
[Authorize]
public class ReservaServicoController : Controller
{
    // Ações do controller protegidas...
}
```

---

## 📝 Questionário Interativo de Fixação (10 Questões)

Responda às questões abaixo para testar seus conhecimentos obtidos na aula prática de PetShop:

1. **Qual das alternativas representa um Requisito Funcional no sistema do PetShop?**
   * A) O banco de dados deve ser o SQL Server.
   * **B) O sistema deve permitir agendar uma reserva vinculando 1 Cliente e 1 Veterinário.**
   * C) A aplicação deve ser desenvolvida em C# com .NET 9.
   * D) A página deve responder em menos de 2 segundos.

2. **No comando de criação da tabela ReservaServico, qual instrução garante a integridade referencial com a tabela Cliente?**
   * A) `PRIMARY KEY (ClienteId)`
   * **B) `CONSTRAINT FK_ReservaServico_Cliente FOREIGN KEY (ClienteId) REFERENCES Cliente(Codigo)`**
   * C) `UNIQUE (ClienteId)`
   * D) `CHECK (ClienteId IS NOT NULL)`

3. **Qual tipo de dado do SQL Server foi utilizado para armazenar o campo 'Valor' da reserva?**
   * A) `VARCHAR(10)`
   * B) `INT`
   * **C) `DECIMAL(10,2)`**
   * D) `FLOAT`

4. **Qual é o objetivo da ferramenta de Engenharia Reversa (Scaffold-DbContext) no Entity Framework Core?**
   * A) Criar o banco de dados SQL Server a partir das classes C#.
   * B) Gerar as tabelas do banco de dados automaticamente durante a execução.
   * **C) Mapear um banco de dados existente gerando as classes de modelo (Models) e o DbContext.**
   * D) Compilar o projeto ASP.NET Core sem erros.

5. **Para proteger um Controller e exigir que o usuário esteja autenticado no ASP.NET MVC, qual atributo é utilizado?**
   * A) `[HttpGet]`
   * **B) `[Authorize]`**
   * C) `[Required]`
   * D) `[ValidateAntiForgeryToken]`

6. **No arquivo Program.cs, qual a ordem correta na chamada dos middlewares de segurança?**
   * A) `app.UseAuthorization();` depois `app.UseAuthentication();`
   * **B) `app.UseAuthentication();` depois `app.UseAuthorization();`**
   * C) A ordem não importa no pipeline do ASP.NET Core.
   * D) Apenas `app.UseAuthorization()` é necessário.

7. **Para preencher automaticamente a chave primária sequencial da tabela Cliente no SQL Server, qual propriedade é aplicada à coluna Codigo?**
   * A) `AUTO_INCREMENT`
   * **B) `IDENTITY(1,1)`**
   * C) `SEQUENCE`
   * D) `DEFAULT(1)`

8. **Qual recurso do ASP.NET MVC é utilizado para validar o formulário no lado do cliente e no servidor a partir das propriedades das Models?**
   * **A) Data Annotations**
   * B) Scaffolding
   * C) DbContext
   * D) Connection Strings

9. **Qual função das Claims no mecanismo de CookieAuthentication?**
   * A) Salvar os scripts SQL de migração.
   * **B) Guardar declarações/informações sobre o usuário autenticado (como ID, Nome e CPF).**
   * C) Conectar a aplicação com o servidor SQL Server.
   * D) Gerar o layout HTML das views automaticamente.

10. **Ao tentar excluir um Veterinário que já possui reservas cadastradas, o que deve ocorrer com base no requisito R5 (Integridade Relacional)?**
    * A) O banco de dados exclui o veterinário e todas as suas reservas silenciosamente.
    * **B) O banco lança um erro de restrição de chave estrangeira bloqueando a exclusão.**
    * C) O sistema substitui o veterinário excluído por um valor nulo na reserva.
    * D) A tabela de reservas é apagada por completo.