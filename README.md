# Gestran Backend e Frontend

Este repositório contém o **backend** e o **frontend** do projeto **Gestran**, desenvolvido com .NET 8 e Angular/TypeScript, utilizando **Arquitetura Limpa (Clean Architecture)** e **DDD (Domain-Driven Design)**.

Como o escopo era um pouco abrangente, tomei a liberdade para que ele funcionasse da seguinte forma:

Pode se criar checklists;
>Cada checklist tem uma coleção de itens que são adicionados a elas.
>É possivel editar os itens de checklists para criação de novas checklists.
>As checklists ficam salvas em coleções de operadores/supervisores durante as fases de execução e aprovação. 
>As checklists são removidas das coleções após finalização, aprovadas ou não.
>Checklists podem ser editadas a qualquer momento e não perdem a exclusividade de quem iniciou.

>O sistema possui um controle de login básico, as contas podem ser verificadas como:
Executor`<i>`, exec@`<i>`123 ou supervisor`<i>`, sup@<i>123

>As contas e itens de checklist pre-definidos encontram-se na camada de infraestrutura, na pasta Seed, na função :
```
GenerateTestUsers
```

No arquivo "SeedDataHelper.cs"


---

## Estrutura do Projeto

### Backend (.NET)
Organizado na estrutura DDD com arquitetura limpa.

Para rodar, abra a solução na pasta backend/Gestran.Backend,
na pasta de Persistence, localizar o arquivo AppDbContextFactory e alterar a connection string na linha:

```
optionsBuilder.UseSqlServer("Server=localhost;Database=GestranDB;User Id=sa;Password=1234;Integrated Security=True;TrustServerCertificate=True;Encrypt=False;");
```

E depois na pasta Gestran.Backend.API, localizar o arquivo appsettings.json e alterar a connection string para o banco SQL local desejado:
```
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=GestranDB;User Id=sa;Password=1234;Integrated Security=True;TrustServerCertificate=True;Encrypt=False;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

Como não foi definido com muito detalhe quanto a utilização e visualização de usuários, tomei a liberdade e simulei um sistema de tokens, que grava a sessão e separa operador de supervisor.

Estando o banco configurado e o projeto compilado, basta selecionar a api como projeto e iniciar. O Swagger irá abrir em seguida.



### FRONT END
O Front-end foi construido em angular, 14+ porém com algumas modificações leves e uma tentativa de simulação de controle de sessão, em conjunto com o backend. Ali é possivel fazer todas as funções como executor e supervisor.

Para rodar o projeto, basta navegar até a pasta frontend/gestran-frontend e rodar os comandos:
ng build 

Se tudo der certo, então é só rodar o comando 'ng serve' e o projeto irá compilar e iniciar o front-end. 