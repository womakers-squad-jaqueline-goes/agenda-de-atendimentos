# Agenda de Atendimentos

## Como executar a API

### Pré-requisito

.NET SDK 10.x instalado.

### Clonar o repositório

```powershell
git clone https://github.com/womakers-squad-jaqueline-goes/agenda-de-atendimentosclear
cd agenda-de-atendimentos
```

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