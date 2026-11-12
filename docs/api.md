# Convenções da API

## JSON

Usar camelCase nos nomes das propriedades, seguindo o padrão
do ASP.NET Core. Exemplo: clienteId, dataHora e nomeCompleto.

## Datas e horários

Usar ISO 8601 para datas e horários.

Os horários de atendimento seguem o fuso de São Paulo
(America/Sao_Paulo). Nos campos que representam data e hora
de um agendamento, informar também o deslocamento UTC.

Exemplo fictício:
2026-11-20T14:00:00-03:00

Para campos que representam apenas uma data, usar YYYY-MM-DD.
Exemplo: 2026-11-20.

## Rotas e exemplos

A API ainda não possui rotas funcionais.

Documentar as rotas e seus exemplos conforme cada
funcionalidade for implementada. Usar somente dados fictícios.

## Organização

- Controllers: receber requisições e retornar respostas HTTP.
- Services: implementar regras de negócio.
- Repositories: acessar o banco de dados.

Criar cada pasta ao adicionar seu primeiro arquivo.

## Tratamento de erros

O middleware centraliza o tratamento de exceções e retorna
erros no formato Problem Details (application/problem+json).

Erros inesperados retornam HTTP 500 com uma mensagem genérica.
Os detalhes da exceção ficam somente nos logs da aplicação.

O mapeamento de exceções de negócio para outros status HTTP
será acrescentado conforme as funcionalidades forem implementadas.