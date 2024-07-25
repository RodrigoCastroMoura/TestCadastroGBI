

# Micro Serviço
***
##Sumário
***
1. [Informações de Ambiente](#informações-de-ambiente)
2. [Informações Gerais](#informações-gerais)
3. [Arquitetura de Solução](#arquitetura-de-solução)
4. API's

   4.1 [API](https://dev.azure.com/grupoltm/Aberto/_wiki/wikis/Aberto.wiki/5398/Template-Readme?anchor=o-que-faz%3F)

   4.2 [API2](https://dev.azure.com/grupoltm/Aberto/_wiki/wikis/Aberto.wiki/5398/Template-Readme?anchor=o-que-faz%3F)


## Informações de Ambiente
***

### API
| Ambiente | DNS do serviço | API Gateway | Grupo de recursos | Plano do Serviço de Aplicativo | Região da Azure|
|:--------:|:----------:|:-----------:|:-----------------:|:------------------------------:|:----:|
| Homolog  |    link     |     link    |       nome        |             nome               | nome |
| Produção |    link     |     link    |       nome        |             nome               | nome |

### API 2
| Ambiente | DNS do serviço | API Gateway | Grupo de recursos | Plano do Serviço de Aplicativo | Região da Azure|
|:--------:|:----------:|:-----------:|:-----------------:|:------------------------------:|:----:|
| Homolog  |    link     |     link    |       nome        |             nome               | nome |
| Produção |    link     |     link    |       nome        |             nome               | nome | 


## Informações Gerais
***

| API                                  | ASP.NET         | Common  | Bagrelog | Health Check | Padrão Health Check | Id do serviço|
|:------------------------------------:|:---------------:|:-------:|:--------:|:------------:|:-------------------:|:-------:|
|                API                   |    net6.0       |Submodule|  ✔️   |    ✔️    | Última atualização em xx/xx/xxxx | Id |
|                API 2                 |    net2.0       |Submodule|  ❌   |   ❌     |         ❌          | Id |



## Arquitetura de Solução
***

Estrutura que usa o [C4 Model](https://www.plantuml.com/plantuml/uml/SyfFKj2rKt3CoKnELR1Io4ZDoSa70000) para gerar uma imagem da arquitetura do MS

_Exemplo:_

_Código utilizado:_

<span style="color:grey">
<i>
@startuml

!include https://raw.githubusercontent.com/plantuml-stdlib/C4-PlantUML/master/C4_Container.puml
!include https://raw.githubusercontent.com/tupadr3/plantuml-icon-font-sprites/master/devicons/mongodb.puml

!define AzurePuml https://raw.githubusercontent.com/plantuml-stdlib/Azure-PlantUML/master/dist 
!includeurl AzurePuml/AzureCommon.puml
!includeurl AzurePuml/Web/AzureWebApp.puml
!includeurl AzurePuml/Databases/AzureSqlDatabase.puml
!includeurl AzurePuml/Integration/AzureServiceBus.puml

!define DEVICONS https://raw.githubusercontent.com/tupadr3/plantuml-icon-font-sprites/master/devicons
!include DEVICONS/angular.puml

title "MS"

Container_Boundary(c1, "TradeBack") {
  Container(API, "API", "C#, ASP.NET MVC", "", "AzureWebApp")
  Container(API2, "API 2", "C#, ASP.NET MVC", "", "AzureWebApp")
  Container(Transacao, "API que gera o request de consulta", "C#, ASP.NET MVC", "", "AzureWebApp")
  Container(Portal, "API que gera o request de cadastro", "C#, ASP.NET MVC","", "AzureWebApp")
  ContainerDb(db, "Banco de Dados", "MongoDB", "Armazena as configurações criadas", "mongodb")
  }

Rel(Transacao, API, "Busca informações")
Rel(API, db, "Consulta")
Rel(Portal, API2, "Configura informações")
Rel(API2, db, "Salva informações")

SHOW_LEGEND()
@enduml 

</i>
</span>


_Imagem gerada:_

<IMG  src="https://www.plantuml.com/plantuml/png/jLFTRjCm5BxFKvXmqqbh8mPNNAr9AfYqTXKPssLr4fzc5ewT7Tj33F4yF05Fi1V3RjrsMg4IbRZeZy_vVixvd9mR2sHT9vFabL2DT1pPdRMzUPzb15z7hR1thdO6gT7AehAZHdTPBq45qj1OBaMT5U-6yr3wFBtCEZ0MoPSMXMU0K4YZtcF_HT-w7ZYztVa8tnqkFM9eUX8MpSQ6uqFe-RDMhUPri0gxS5nwPpP-T8HpNpfYgHLtRoykZ6NRNHp9dSUQKUZEpx9RUHzsYr7S_ndt_I5e2HPgC6ZMXEfURYg7M1VAOajWXLQHX-HZmjoP5_cKavkBucfM_PSxsKotiyb0jKu2nIcii19PEgtI9Da-B8jSEyM1lWsQDwSilIRWc4Fp9JrXtnF6jhZ1U7xX-_uxzJ_5wrCshkQZsUIQJM-AK0gVPp6d9o_PPsiwEpjAm0-c33IWeygzGzOY0TECq1-CPNxpa8EJ5ewoc6ko80_h0mTZIVzH_x1yMGzuxG4vg4O7hHAuDe4o3UzIcQ_OrC4ZAc1WmYvBqJg2fvzFlz2VIGJt08jltyhWHv9yGlayd_LDvSuqm8HQQkgYXCS7wAg_cgM8SSN69e1uMKKSuAygPr6c0lcm3qggZrUtYyl9Xyci79mavwYuw-Hl"/>



<span style="color:grey"> <i>
(A parte a seguir ficará separada da raíz da solução, com isso deverá ser criado um readme para cada API dentro do seu escopo)
</i>
</span>

# API
***

##Sumário
***

1. [O que faz?](https://dev.azure.com/grupoltm/Aberto/_wiki/wikis/Aberto.wiki/5398/Template-Readme?anchor=user-stories/pbis-e-bugs-relacionados)
2. [Variáveis de ambiente](https://dev.azure.com/grupoltm/Aberto/_wiki/wikis/Aberto.wiki/5398/Template-Readme?anchor=como-execultar-o-servi%C3%A7o/webapp)
3. [Como Execultar o serviço/webapp](https://dev.azure.com/grupoltm/Aberto/_wiki/wikis/Aberto.wiki/5398/Template-Readme?anchor=vari%C3%A1veis-de-ambiente)



## O que faz?
***
Breve explicação que deverá conter:
- O objetivo/ que ele faz;
- Quais outros MS ele está relacionado, ou seja, que consomem dados vindos desse serviço, serviços para qual ele envia dados, integrações/ dependências diretas de parceiros;

_Exemplo:_
_O Micro Serviço (MS) API é responsável pela criação, atualização e consulta de cadastro de usuários._

**_MS relacionados:_**
- _[MS API 3](http://link_que_vai_redirecionar_pra_o_readme_desse_MS);_
- _[MS API 4](http://link_que_vai_redirecionar_pra_o_readme_desse_MS);_
- _[Parceiro X](http://link_que_vai_redirecionar_pra_o_readme_desse_MS);_


## Variáveis de ambiente
***

Variáveis chave que estão modificando algum comportamento ou que tenha relação com a regra de negócios

## Como Executar o serviço/webapp com Docker Local
***

Como executar ele no docker caso ele já possua essa configuração. Se não possuir, solicitar para abri uma PBI para ele



