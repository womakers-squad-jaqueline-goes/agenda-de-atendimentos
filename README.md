# Agenda de Atendimentos

## Como executar a API

### Pré-requisito

.NET SDK 10.x instalado.

### Clonar o repositório

```powershell
git clone https://github.com/womakers-squad-jaqueline-goes/agenda-de-atendimentosclear
cd agenda-de-atendimentos
```
### Configurar o banco de dados

O projeto utiliza SQLite. Na raiz do repositório, restaure os pacotes e a ferramenta do Entity Framework Core:

```powershell
dotnet restore
dotnet tool restore
```

Configure a conexão no User Secrets do seu computador:

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Data Source=agenda.db" --project src/Agenda.Api
```

Crie ou atualize o banco aplicando as migrations versionadas:

```powershell
dotnet ef database update --project src/Agenda.Api -- --environment Development
```

O banco local é armazenado no arquivo `agenda.db`, ignorado pelo Git. Cada integrante deve configurar a conexão e aplicar as migrations no próprio computador.

### Compilar e executar

Na raiz do repositório:

```powershell
dotnet build
dotnet run --project src/Agenda.Api
```

### Abrir o Swagger

Com a API em execução, acesse o endereço exibido no terminal
em `Now listening on`, acrescentando `/swagger`.

Exemplo: http://localhost:5123/swagger

A porta pode variar conforme o arquivo
`src/Agenda.Api/Properties/launchSettings.json`.

O Swagger está habilitado no ambiente Development.
Enquanto nenhuma rota funcional estiver implementada,
a mensagem “No operations defined in spec!” é esperada.

Para encerrar a API, pressione Ctrl+C no terminal.

### Convenções

Consulte [as convenções da API](docs/api.md).

### Executar os testes

Na raiz do repositório:

```powershell
dotnet test
```

Os testes de banco utilizam SQLite em memória e aplicam as migrations automaticamente. Cada teste usa um banco independente, descartado ao terminar.