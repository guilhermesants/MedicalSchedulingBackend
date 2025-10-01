# 📅 Api para agendamento médico
API para gerenciamento de horários de médicos e agendamentos de pacientes.
Médicos podem disponibilizar horários, e pacientes podem visualizar horários disponíveis e agendar.

## 🚀 Como Executar o Projeto
É possível executar o projeto pelo Docker, disponibilizei o **Docker Compose**.
No Docker compose possuem dois services, um sendo o bando de dados postgres e outro sendo a api.
Utilizei o conceito de Database-first, onde primeiro fiz a modelagem do banco de dados. 
Observe que no projeto possui um diretorio chamado script, onde possui um arquivo .sql.
Este arquivo é executado assim que o container subir, criando o banco de dados e os primeiros inserts.

## ⚙️ Workflow e deploy
Criei um Workflow no GitHubActions, para executar o restore, build e testes do projeto.
E em seguida um build da imagem, aproveitei também e crieri um repositório público no Docker Hub, 
para fazer o push da imagem.

Também disponibilizo essa api em uma VPs Oracle, onde fiz a instalação do docker e toda configuração necessária para subida de banco e api, 
que esta acessível pelo ip público http://132.226.24.60:5000/docs/


## 🛢 Estrutura do banco de dados
1. As seguinte estrutura foi criada no banco de dados
   - A tabela "roles" vai conter os registros dos perfis, que cadastrei tres (admin, doctor, patient)
   - A tabela "users" contem registros de login, como username, password, last_login, active e a referencia para o pefil
   - A tabela "doctors" onde vai conter os registros de um médico, e faz referencia para o usuário
   - A tabela "patients" onde contem os registros referentes a um paciente, e faz referencia para o usuário
   - A tabela "availabilities" que contem os registros das disponibilidades de um médico, possui os campos date, start_time, end_time, available(se esta disponível)
   possui uma constraint onde impede o cadastro quando o start_time for menos que o end_time, e tambem impede que seja registrado mais de uma vez id_doctor, data, start_time e end_time
   - A tabela "appointment_status", que é a tabela de status do agendamento, onde cadastrei tres status (active, canceled, completed)
   - A tabela de "appointments", onde é possivel registrar os agendamentos, tem referencia para o paciente e a tabela de availabilities


## 📚 Documentação do Projeto

O projeto segue uma arquitetura em camadas:

### 🧠 Application Layer
Contém os casos de uso (UseCases), DTOs, validações e manipuladores de comandos e queries com o Cortex.Mediator.

### 🏛 Domain Layer
Define as entidades de negócio, interfaces de repositórios e contratos.

### 🏗 Infrastructure Layer
Implementa os repositórios definidos na camada `Domain`, lida com o acesso a banco de dados (via Entity Framework Core) e configurações relacionadas à persistência e infraestrutura.

### 🎯 Presentation Layer
Responsável por expor a Api: endpoints públicos expostos para consumo externo.

📝 Observações: 
  - No Projeto, utilizo o padrão de projeto (comportamental) Mediator, fazendo uso da lib Cortex.Mediator
    Justamente com CQRS (command and queries responsability segregation), onde separo as intenções de escrita e consulta

  - Optei também por fazer uso do Scalar, documentação da api, que é uma alternativa ao swagger, pessoalmente acho mais elegante.
  - Configurei a api para ter autenticação de usuário e autorização, então, alguns endpoints somente o perfil admin tem acesso, como cadastrar
    médico e paciente, e outros endpoints somente médicos tem acesso, como cadastrar disponibilidades e alterá-las, bem como paciente que tem acesso a obter dados
    de disponibilidade de um determinado médico ou vários e cadastrar agendamento (selecionar uma disponibilidade)
  - Os erros que acontecem na api retornam o padrão Problem Details ou Validation Problem Detais (disponível na documentação), como no caso do status 400

## 📚 Documentação da API
### Segue abaixo a documentação de alguns endpoints, mas todos estão disponíveis na documentação.

#### Healthcheck - retorna a saúde do banco de dados

```http
  GET /health
```
#### Auth - Autenticação de usuários

Inicialmente, criei o usuário admin e senha admin123, com ele será possível cadastrar médicos e pacientes

```http
  POST /api/v1/Auth
```
| Parâmetro   | Tipo       | Local| Descrição                           |
| :---------- | :--------- | :------------ | :---------------------------------- |
| `user` | `string` | `body` | **Obrigatório**. |
| `password` | `string` | `body` |**Obrigatório**. |

#### Doctor - Cadastrar um médico

```http
  POST /api/v1/Doctor
```
| Parâmetro   | Tipo       | Local| Descrição                           |
| :---------- | :--------- | :------------ | :---------------------------------- |
| `Authorization` | `string` | `header` | **Obrigatório** Bearer Token. |
| `name` | `string` | `body` | **Obrigatório**. |
| `licenseNumber` | `string` | `body` |**Obrigatório**. |
| `specialty` | `string` | `body` |**Obrigatório**. |
| `phone` | `string` | `body` |**Obrigatório**. |
| `email` | `string` | `body` |**Obrigatório**. |

#### Doctor - Obter disponibilidades de um médico ou vários

```http
  GET /api/v1/Doctor/availability
```
| Parâmetro   | Tipo       | Local| Descrição                           |
| :---------- | :--------- | :------------ | :---------------------------------- |
| `Authorization` | `string` | `header` | **Obrigatório** Bearer Token. |
| `IdDoctor` | `long` | `query parameter` | **Opcional**. |

## 💡 Tecnologias Utilizadas
- .NET 8  
- PostgreSQL  
- Entity Framework Core  
- Docker e Docker Hub
- Cortex.Mediator
- BCrypt.Net (criptografia de senha)
- FluentValidation  
- Scalar (documentação da api) 
- XUnit e Moq (Testes unitários)
- CI/CD (Github Actions)
