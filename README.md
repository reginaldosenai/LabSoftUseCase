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
   - O sistema deve permitir o cadastro dos estudantes interesados (Ex: Código/Id, Nome Completo, CPF ou Matrícula, E-mail, Telefone).
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