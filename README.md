# 🛠️ Procedure Generator - SQL Automation

## 📌 Sobre o Projeto
O **Procedure Generator** é uma ferramenta desenvolvida em **C#**, que **automatiza a geração de scripts SQL** para:
- 📌 **Criação de tabelas (`CREATE TABLE`)** com base nas entidades do domínio.
- 📌 **Procedures de manipulação de dados (`INSERT`, `UPDATE`, `DELETE`, `SELECT`, `GETBYID`)**.
- 📌 **Procedures específicas para cada propriedade (`EXISTSBY_PROPERTY` e `GETBY_PROPERTY`)**.

O projeto busca **todas as classes na camada `Domain.Entity` automaticamente**, gerando os scripts SQL e salvando-os na pasta **`GeneratedSQL/`**.

---

## 🚀 **Funcionalidades**
✅ **Busca automaticamente todas as classes dentro da pasta `Domain.Entity`**  
✅ **Gera o script de criação da tabela (`CREATE TABLE`)**  
✅ **Cria procedures para todas as operações CRUD**  
✅ **Cria procedures para validar e buscar registros com base em propriedades específicas**  
✅ **Salva os scripts SQL gerados em arquivos `.sql` individuais por entidade**  
✅ **Nenhuma configuração manual é necessária para adicionar novas entidades!**

---

## 📂 **Estrutura do Projeto**
📦 ProcedureGenerator │── 📂 1-Domain │ ├── 📂 Entity │ │ ├── Produto.cs │ │ ├── Cliente.cs │ │ ├── Pedido.cs │ │ ├── BaseEntity.cs │ ├── 📂 Interfaces │ │ ├── IProcedureGeneratorService.cs │ │── 📂 2-Application │ ├── 📂 Services │ │ ├── ProcedureGeneratorService.cs │ │── 📂 3-Infrastructure │ ├── 📂 Data │ │ ├── SqlTypeMapper.cs │ │── 📂 4-Presentation │ ├── ConsoleApp.cs │ │── 📂 GeneratedSQL │ ├── Procedure_Produto.sql │ ├── Procedure_Cliente.sql │ ├── Procedure_Pedido.sql │ │── README.md │── ProcedureGenerator.sln

---

## 💻 **Como Rodar o Projeto**
### 📌 **Pré-requisitos**
- **.NET 6+**
- **Visual Studio 2022** ou **VS Code**
- **SQL Server** (para testar os scripts gerados)


