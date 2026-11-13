# 🚀 Guia Prático: Construindo o Sistema de Escolha de Cursos Universitários em ASP.NET Core MVC (.NET 9)

## 📌 Contextualização e Objetivo
No desenvolvimento de software corporativo e acadêmico, é muito comum recebermos um problema de negócio e precisarmos **modelar o banco de dados do zero** antes de iniciar a codificação da aplicação. 

Nesta atividade prática, você atuará como **Desenvolvedor Full-Stack**, passando por todas as etapas do ciclo de desenvolvimento:
1. **Modelagem de Dados (DER):** Interpretar o cenário de negócio, desenhar o Diagrama Entidade-Relacionamento e criar as tabelas no **SQL Server**.
2. **Engenharia Reversa (Database First):** Importar a estrutura do banco criado para o **ASP.NET Core MVC (.NET 9)** utilizando o **Entity Framework Core**.
3. **CRUD e Controle de Acesso:** Gerar as telas automaticamente (*Scaffolding*) e implementar uma tela de **Login** validando os usuários cadastrados no banco de dados.

---

## 🏢 O Cenário de Negócio: Sistema de Escolha de Cursos Universitários
Uma instituição de ensino superior está abrindo seu processo seletivo interno e precisa de um sistema web para gerenciar a inscrição dos estudantes nos cursos disponíveis.

### 📋 Regras de Negócio e Entidades
Analise atentamente o cenário abaixo para modelar seu banco de dados (`dbCursosUniversitarios`):

1. **Cadastro de Alunos (`Aluno`):** 
   - O sistema deve permitir o cadastro dos estudantes interessados (Ex: Código/Id, Nome Completo, CPF ou Matrícula, E-mail, Telefone).
2. **Cadastro de Cursos (`Curso`):** 
   - A universidade oferece diversas opções de graduação (Ex: Código/Id, Nome do Curso, Turno, Carga Horária, Vagas Disponíveis).
3. **Escolha de Cursos (`EscolhaCurso`):** 
   - Cada aluno cadastrado pode escolher **até 3 cursos** diferentes (Ex: 1ª Opção, 2ª Opção e 3ª Opção, ou registros vinculados indicando a ordem de preferência e a data da escolha).
   - *Atenção:* Esta é a tabela associativa/transacional que conecta o **Aluno** aos **Cursos** escolhidos.
4. **Controle de Acesso (`Usuario`):** 
   - O sistema não pode ser acessado por qualquer pessoa. Deve existir uma tabela independente para armazenar os operadores do sistema (Ex: Id, Nome, Login/Email, Senha, Perfil). Apenas usuários cadastrados nesta tabela poderão efetuar login e acessar os cadastros.

---

## 🛠️ Pré-requisitos
- Visual Studio 2022 (com suporte ao .NET 9 instalado)
- Microsoft SQL Server e SQL Server Management Studio (SSMS)
- Ferramenta de modelagem de dados (brModelo, Draw.io, MySQL Workbench ou o próprio Diagrama do SSMS)

---

## 🗄️ Etapa 1: Modelagem (DER) e Criação do Banco de Dados no SQL Server
Diferente de tutoriais onde o script já vem pronto, **aqui você é o arquiteto do banco!**

1. **Desenhe o DER (Diagrama Entidade-Relacionamento):**
   - Represente as **3 tabelas de negócio** (`Aluno`, `Curso` e `EscolhaCurso`) com suas respectivas Chaves Primárias (`PK`) e Chaves Estrangeiras (`FK`), garantindo a cardinalidade correta para que um aluno possa escolher até 3 cursos.
   - Represente a **1 tabela de segurança/acesso** (`Usuario`).
2. **Crie o Banco no SQL Server:**
   - Abra o **SSMS**, crie o banco de dados `dbCursosUniversitarios`.
   - Escreva e execute os comandos DDL (`CREATE TABLE`) criando as 4 tabelas, definindo tipos de dados adequados (`INT`, `VARCHAR`, `DATETIME`, etc.) e aplicando as devidas `CONSTRAINTS` (`PRIMARY KEY`, `FOREIGN KEY`, `UNIQUE` para CPF/Login).
3. **Popule o Banco para Testes (`INSERT`):**
   - Insira pelo menos **2 usuários** na tabela `Usuario` (para testar o login).
   - Insira pelo menos **5 cursos** universitários na tabela `Curso` (ex: Ciência da Computação, Engenharia de Software, Direito, Administração, Enfermagem).
   - Insira pelo menos **2 alunos** na tabela `Aluno`.

---

## 📁 Etapa 2: Criação do Projeto no Visual Studio
1. Abra o **Visual Studio 2022**.
2. Clique em **Criar um novo projeto**.
3. Selecione o modelo **Aplicativo Web do ASP.NET Core (Model-View-Controller)** e clique em **Próximo**.
4. Defina o nome da solução como `AppCursosUniversitarios` e clique em **Próximo**.
5. Selecione o Framework **.NET 9.0 (Suporte de Termo Padrão)** e clique em **Criar**.

---

## 📦 Etapa 3: Instalação dos Pacotes do Entity Framework
Abra o **Gerenciador de Pacotes NuGet** (ou o Console do Gerenciador de Pacotes) e instale os seguintes pacotes na versão compatível com o .NET 9:

- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`
- `Microsoft.VisualStudio.Web.CodeGeneration.Design`

---

## ⚙️ Etapa 4: Configuração da String de Conexão no `appsettings.json`
Abra o arquivo `appsettings.json` na raiz do projeto e configure a propriedade `ConnectionStrings` apontando para o banco que você criou na Etapa 1.

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
    "ConexaoSqlServer": "Server=localhost; Database=dbCursosUniversitarios; Trusted_Connection=True; TrustServerCertificate=True;"
  }
}
```
*(Nota: Caso utilize autenticação via usuário/senha do SQL Server, ajuste a string com `User Id=...; Password=...;` conforme o ambiente do laboratório).*

---

## 🔨 Etapa 5: Engenharia Reversa (Scaffolding do Banco)
Agora vamos importar as 4 tabelas que você modelou no SQL Server para dentro da pasta `Models` do projeto.

Abra o **Console do Gerenciador de Pacotes** (*Ferramentas > Gerenciador de Pacotes NuGet > Console do Gerenciador de Pacotes*) e execute:

```powershell
Scaffold-DbContext "Name=ConexaoSqlServer" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
```
Verifique se na pasta `Models` foram geradas as classes `Aluno.cs`, `Curso.cs`, `EscolhaCurso.cs`, `Usuario.cs` e o arquivo de contexto `DbCursosUniversitariosContext.cs`.

---

## 🔨 Etapa 6: Compilação Obrigatória da Solução
Antes de gerar as telas e controllers, o projeto precisa estar limpo e compilado:
- Pressione `Ctrl + Shift + B` ou clique com o botão direito na Solução e selecione **Recompilar (Rebuild)**.

---

## 🔌 Etapa 7: Registro do DbContext na Injeção de Dependência (`Program.cs`)
Abra o arquivo `Program.cs` e registre o contexto do banco de dados antes da linha `var app = builder.Build();`:

```csharp
using Microsoft.EntityFrameworkCore;
using AppCursosUniversitarios.Models;

var builder = WebApplication.CreateBuilder(args);

// Adicionar os serviços ao contêiner
builder.Services.AddControllersWithViews();

// Registrando o DbContext com a String de Conexão
builder.Services.AddDbContext<DbCursosUniversitariosContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoSqlServer")));

var app = builder.Build();
```

---

## 🎨 Etapa 8: Gerando o CRUD Automático (Scaffolding MVC)
Vamos criar os **Controllers** e **Views Razor** para as entidades do sistema:

1. No **Gerenciador de Soluções**, clique com o botão direito na pasta `Controllers` > **Adicionar** > **Novo Item com Scaffolding...**.
2. Selecione **Controlador MVC com exibições, usando o Entity Framework** e clique em **Adicionar**.
3. Na janela de configuração:
   - **Classe de modelo:** Selecione `Aluno (AppCursosUniversitarios.Models)`.
   - **Classe do contexto de dados:** Selecione `DbCursosUniversitariosContext (AppCursosUniversitarios.Models)`.
   - **Exibições:** Mantenha marcada a opção de gerar views.
4. Clique em **Adicionar**.
5. **Repita o mesmo procedimento** para as classes de modelo:
   - `Curso`
   - `EscolhaCurso`
   - `Usuario`

---

## 🔐 Etapa 9: Implementando o Controle de Acesso (Login)
Para garantir que apenas usuários cadastrados na tabela `Usuario` acessem o sistema:

1. Crie um `LoginController.cs` com uma View `Index.cshtml` contendo um formulário simples de **Login** e **Senha**.
2. Na Action `[HttpPost]`, consulte o `_context.Usuarios` verificando se existe um registro com o login e senha informados.
   - **Se válido:** Salve a informação do usuário na Sessão (`HttpContext.Session`) ou utilize Cookies de Autenticação e redirecione para `Home/Index`.
   - **Se inválido:** Exiba uma mensagem de erro (`ViewBag.Erro = "Usuário ou senha inválidos."`) na tela de login.
3. Configure a rota padrão no `Program.cs` para abrir primeiramente a tela de `Login` e proteja os Controllers (`Aluno`, `Curso`, `EscolhaCurso`, `Usuario`) para redirecionar ao Login caso não haja usuário autenticado.

---

## 🎯 Resultado Esperado e Entregáveis
Ao finalizar a atividade, você deverá entregar:
1. **Arquivo de Imagem ou PDF do DER** modelado na Etapa 1.
2. **Script SQL (`.sql`)** contendo a criação do banco, tabelas, constraints e `INSERTs` iniciais.
3. **Projeto ASP.NET Core MVC completo** contendo:
   - Tela de Login funcional validando na tabela `Usuario`.
   - CRUD completo para `Aluno`, `Curso`, `Usuario` e `EscolhaCurso` (com `SelectList` / Dropdowns carregando os nomes dos alunos e dos cursos na hora da escolha).

---

# 📝 Exercícios de Fixação: Modelagem, POO e Entity Framework

Com base na modelagem que você construiu para o `dbCursosUniversitarios` e nas classes geradas pelo Entity Framework, responda às questões abaixo:

---

### 1. Cardinalidade e Regra das 3 Escolhas
No cenário proposto, um aluno pode escolher até 3 cursos diferentes e um curso pode ser escolhido por vários alunos. Na modelagem relacional (DER) e no SQL Server, como essa relação **N:N (Muitos-para-Muitos)** foi resolvida?

- [ ] **A)** Criando três colunas fixas de texto (`Curso1`, `Curso2`, `Curso3`) dentro da tabela `Aluno`, sem necessidade de chave estrangeira.
- [ ] **B)** Criando uma tabela associativa (`EscolhaCurso`) contendo chaves estrangeiras (`AlunoId` e `CursoId`) que se relacionam em **1:N** com `Aluno` e **1:N** com `Curso`.
- [ ] **C)** Colocando a chave estrangeira `AlunoId` diretamente dentro da tabela `Curso`.
- [ ] **D)** Utilizando apenas a tabela `Usuario` para armazenar os cursos escolhidos.

---

### 2. Propriedades de Navegação no C#
Após rodar o comando `Scaffold-DbContext`, como o Entity Framework Core representa o relacionamento entre `EscolhaCurso`, `Aluno` e `Curso` dentro da classe `EscolhaCurso.cs`?

- [ ] **A)** Apenas com propriedades primitivas do tipo `int` (`AlunoId` e `CursoId`), ignorando os objetos relacionados.
- [ ] **B)** Através de propriedades de navegação (`public virtual Aluno Aluno { get; set; }` e `public virtual Curso Curso { get; set; }`), permitindo acessar dados como `escolha.Aluno.Nome` diretamente via código C#.
- [ ] **C)** Criando uma lista `List<Aluno>` dentro da classe `Usuario`.
- [ ] **D)** Transformando todas as chaves estrangeiras em propriedades do tipo `string`.

---

### 3. Integridade Referencial (Constraints)
Durante a criação das tabelas no SQL Server, por que é fundamental definir `FOREIGN KEY` (Chave Estrangeira) na tabela `EscolhaCurso`?

- [ ] **A)** Para deixar a consulta SQL mais lenta e segura contra ataques externos.
- [ ] **B)** Para garantir que o banco de dados impeça o cadastro de uma escolha vinculada a um `AlunoId` ou `CursoId` que não exista nas tabelas originais.
- [ ] **C)** Para permitir que campos obrigatórios fiquem nulos (`NULL`) automaticamente.
- [ ] **D)** Porque o Visual Studio não compila projetos C# se o banco não tiver chaves estrangeiras.

---

### 4. Validação de Login com LINQ e DbContext
Na Etapa 9, ao verificar no `LoginController` se o usuário existe no banco de dados utilizando o `_context` (instância de `DbCursosUniversitariosContext`), qual método LINQ é mais adequado para buscar o primeiro usuário que corresponda ao login e senha informados (ou retornar `null` caso não encontre)?

- [ ] **A)** `_context.Usuarios.Add(u => u.Login == login && u.Senha == senha);`
- [ ] **B)** `_context.Usuarios.FirstOrDefault(u => u.Login == login && u.Senha == senha);`
- [ ] **C)** `_context.Usuarios.RemoveAll();`
- [ ] **D)** `_context.Database.EnsureDeleted();`

---

# 🏆 DESAFIO PRÁTICO (Regra de Negócio Extra)

A coordenação acadêmica identificou uma falha durante os testes do sistema: **alguns alunos estão conseguindo cadastrar mais de 3 cursos** ou **escolhendo o mesmo curso duas vezes**!

Sua missão é blindar o sistema implementando as duas regras de negócio abaixo diretamente no `EscolhaCursosController.cs` (nos métodos `Create`):

- **RN01 (Limite de Opções):** Antes de salvar uma nova escolha (`_context.Add(escolhaCurso)`), verifique no banco quantos cursos aquele `AlunoId` já possui cadastrados. Se já possuir **3 cursos**, impeça o salvamento e retorne uma mensagem de erro na tela (`ModelState.AddModelError`).
- **RN02 (Sem Duplicidade):** Verifique se o `AlunoId` já escolheu aquele mesmo `CursoId` anteriormente. Caso positivo, bloqueie o cadastro informando que o curso já foi selecionado pelo aluno.

### 💡 Dicas de Implementação:
1. Utilize o método `CountAsync()` do Entity Framework filtrado pelo `AlunoId` para validar a **RN01**.
2. Utilize o método `AnyAsync()` filtrado por `AlunoId` e `CursoId` para validar a **RN02**.
3. Lembre-se de recarregar as `ViewData["AlunoId"]` e `ViewData["CursoId"]` antes de retornar `return View(escolhaCurso);` quando ocorrer erro de validação, para que os Dropdowns não quebrem na tela!

**BONS ESTUDOS E BOM CÓDIGO! 🚀**