# MVP da Agenda de Atendimentos

## Para quem é a agenda

Nossa usuária é uma manicure autônoma que atende cerca de três a quatro clientes por semana e combina os horários conforme os pedidos das clientes, deixando alguns retornos quinzenais já marcados. O que mais atrapalha sua rotina é perder um horário com um cancelamento de última hora, quando outras clientes interessadas já marcaram em outro lugar. A primeira versão da agenda vai ajudá-la a consultar os atendimentos do dia em ordem de horário, registrar cancelamentos e evitar agendamentos sobrepostos.

## O que a entrevista mostrou

| O que ela contou | Como isso orienta o projeto |
|---|---|
| O serviço mais comum é pé e mão, com duração de 1 hora e preço de R$ 70. Ela raramente faz apenas um dos dois. | Cadastrar **Pé e mão** como um único serviço no MVP, com 60 minutos e R$ 70. |
| Os horários marcados variam conforme as clientes. Ela leva em conta o horário e o deslocamento. | Mostrar os atendimentos de cada dia em ordem de horário e conferir se um novo agendamento conflita com outro já marcado. |
| Uma cliente já sai com o próximo atendimento marcado para 15 dias depois. | Permitir marcar o próximo atendimento manualmente. A repetição automática pode ficar para uma versão futura. |
| Ela ajusta horários quando há mudanças e cancela quando a cliente falta. | Na agenda, registrar a falta com o status **Faltou**, como previsto no projeto. Para remarcar, propor cancelar a reserva original e criar outra no horário combinado. |
| Costuma receber na hora e não calcula o total do mês. | Manter o faturamento mensal do enunciado, somando o valor dos atendimentos concluídos. Controle de pagamentos pendentes fica fora da primeira versão. |

Ela citou de três a quatro clientes ao responder à pergunta sobre uma semana normal. Ficou em aberto por onde recebe os pedidos e onde anota os horários atualmente.

## Proposta para a primeira entrega

- Cadastro e login da profissional.
- Cadastro de clientes e serviços, com **Pé e mão** como exemplo de serviço.
- Configuração do expediente e agendamento com verificação de sobreposição.
- Consulta da **agenda do dia** com início, fim, cliente, serviço e status. A API retorna os registros ordenados pelo horário de início para facilitar a leitura dos períodos do dia.
- Atualização de status para registrar atendimentos concluídos, cancelados e faltas.
- Consulta do faturamento mensal com base nos atendimentos concluídos.

O primeiro fluxo que a equipe deve conseguir demonstrar é cadastrar **Pé e mão**, marcar um atendimento de 60 minutos, consultar a agenda do dia e registrar um cancelamento. O faturamento fecha o fluxo do enunciado quando o atendimento é concluído.

## O que fica para depois

- Repetição quinzenal automática e edição direta do horário de uma reserva existente.
- Cadastro de vários serviços na mesma reserva. O serviço **Pé e mão** cobre o caso mais frequente.
- Cálculo de tempo de deslocamento e controle de pagamentos pendentes.

## Decisão para a equipe validar

- **I15 · Horários livres.** O planejamento atual trata a busca automática de vagas como extra. A entrevista mostrou que ela consulta horários livres com frequência. Depois de demonstrar a agenda do dia, confirmar se ver os atendimentos marcados já ajuda ou se ela precisa que a API liste as vagas que comportam um serviço de 60 minutos. Se essa segunda opção for indispensável, reavaliar a prioridade da I15.
