# 🚍 Trabalho Prévio com PostgreSQL

## 📊 Domínio

O domínio escolhido para a primeira etapa do trabalho de BAN II foi o de gerenciamento de linhas de ônibus. 

Neste sistema é possível registrar e consultar todas as viagens realizadas em uma empresa de ônibus municipal, adentrando detalhes como o ônibus, motorista, linha e estações de cada uma das viagens.

Cada uma das propriedades de uma viagem (Motorista, Ônibus, Linha (com suas paradas e estações)) são possíveis de serem criadas, editadas, excluídas e lidas, formando o CRUD completo, através dos menus disponíveis.

O sistema foi desenvolvido em formato de console, através das tecnologias do .Net 8 e Entity Framework Core, enquanto o banco PostgreSQL utilizado se encontra hospedado no Neon DB.

[Neon](https://console.neon.tech/)

![image](https://github.com/user-attachments/assets/c4de8521-40cd-47c2-90f8-7f209c366ca6)

A connection string para o banco se encontra no projeto e possui limite de tempo de computação e storage limit, portanto, podem ocorrer erros ao tentar utilizá-lo após muito tempo. Além disso, algumas redes (como a da UDESC) barram requisições ao Neon, portanto, você pode enfrentar problemas quanto à conectividade dependendo da sua rede.

## 📐 Esquema Conceitual

![image](https://github.com/user-attachments/assets/07e59bc3-7881-4cf4-8d08-c6adedd80f61)

## 🗂️ Esquema lógico como dicionário de dados

![image](https://github.com/user-attachments/assets/62225319-8775-4d5e-930e-da35d9e6299d)
![image](https://github.com/user-attachments/assets/e481186b-4918-4254-a177-3c1dc0839e31)
![image](https://github.com/user-attachments/assets/425b3713-58cd-4f58-9134-d10292baaee5)
![image](https://github.com/user-attachments/assets/be7805d1-a5e6-4456-8fd3-2ef1c5d496f2)
![image](https://github.com/user-attachments/assets/47ed0f3d-ea5a-4266-8426-a0c66f61b7b1)
![image](https://github.com/user-attachments/assets/051c4d71-211d-4aa8-beaa-e1164d35c990)

## 🛠️ Como utilizar

O projeto está disponível para o público no repositório do GitHub abaixo

[Repositório](https://github.com/phdguigui/BusManager)

Primeiramente você deve realizar o clone ou então baixar o projeto como zip na sua máquina, como no exemplo abaixo

![image](https://github.com/user-attachments/assets/ca21991b-88d2-4643-8381-e7ae4fba3cea)

Feito isso, você deverá acessar o diretório abaixo e procurar pelo executável do aplicativo (BusManager.exe)

```bash
.\BusManager\bin\Release\net8.0\win-x86
```

![image](https://github.com/user-attachments/assets/3f69ca4a-8d04-404b-a255-80e4d51ae43f)

Executando o .exe você já estará com tudo pronto e com o aplicativo em execução. A conexão ao banco de dados do Neon já está configurada e com a base populada e você não precisa instalar nada previamente, dado que o projeto foi publicado como self-contained, com todas as dlls necessárias já na pasta.

> [!CAUTION]
> Aplicativo disponível apenas para sistemas Windows x86

> [!WARNING]
> Base de dados populada por inteligência artificial, certos registros podem não estar seguindo a regra de negócio ou não seguirem 100% a realidade.

## 💾 Dump

Caso deseje replicar o banco de dados, no link abaixo, apontando para o mesmo repositório, terá o arquivo .sql para o dump da base de dados.

[Dump](https://github.com/phdguigui/BusManager/blob/main/Dump/dump-busmanager-202409141735.sql)

Entretanto, dentro do projeto também há a pasta Migrations, geradas a partir do Entity Framework Core, basta executá-los também.

[Migrations](https://github.com/phdguigui/BusManager/tree/main/BusManager/Migrations)

---

# 🗃️ Trabalho com MongoDB - Fase 2

Para a segunda fase do trabalho de Banco de Dados II, foi substituído o banco de dados anterior com o PostgreSQL para o modelo NoSQL, nesse caso o MongoDB.

## ♻️ Reaproveitamento de Código da Fase 1

Devida a utilização do [Entity Framework](https://learn.microsoft.com/pt-br/ef/core/), as operações ao banco de dados são traduzidas automaticamente. Ao configurar um Select, com Where, OrderBy etc. utilizando
os métodos nativos do EF Core, .Net e LINQ, ele considera a configuração do context, verificando qual o banco de dados de destino (configurado no ApplicationContext.cs) e traduzindo o command ou query para a determinada linguagem empregada e tal banco, sendo ele
SQL ou NoSQL.

Além disso a arquitetura no projeto desde a fase 1, separando-o em camadas, garantiu enorme reaproveitamento de código, tendo somente que alterar o Repository que é onde se tem a comunicação com o banco de dados.

## 🔧 Pacotes Relacionados

Para isso, foram desinstalados os pacotes utilizados anteriormente, por exemplo o Entity Framework Design, dado que não foi possível realizar migrations para o Mongo, e o Entity Framework PostgreSQL, dada a sua inutilidade a partir da fase 2.
Para a configuração e comunicação do EF Core com o banco Mongo, foi instalado o pacote Entity Framework MongoDB.

## 🏗️ Estrutura de Relacionamento Utilizada

Visando o reaproveitamento de código e a estrutura utilizada na fase 1, fez-se o relacionamento entre collections de forma similar a feita anteriormente, com referências de uma collection para outra, como seguem abaixo alguns exemplos:

![image](https://github.com/user-attachments/assets/b7c79638-cc14-4580-835c-c5dfb13e709a)

![image](https://github.com/user-attachments/assets/76df8751-7095-47c7-a294-3ae00d167630)

![image](https://github.com/user-attachments/assets/1200df1b-7d7f-4782-b46b-c16b2dcca054)

![image](https://github.com/user-attachments/assets/cbbe443e-a381-439b-a83e-a5633c062c4a)

## ⚖️ Prós e Contras

Além disso, a mudança para o formato de collections com documentos com propriedades aninhadas, causaria grande mudança na atualização dos documents, tanto 
na estrutura do projeto quanto na performance, dada a necessidade de alteração em diversos locais, por exemplo motoristas que estão presentes em diversas viagens.

Entretanto, a leitura com as propriedades aninhadas teria um grande ganho de performance, dada que todas as propriedades relativas a ela já estariam disponíveis, sendo principalmente eficaz na geração dos relatórios disponíveis.

## 🛠️ Como utilizar 2.0

A forma de execução do projeto foi facilitada, o download foi facilitado, estando agora presente na página de releases do repositório:

![image](https://github.com/user-attachments/assets/cbb15904-a58c-4f2f-9428-a3e57ef15dbe)

O download do projeto com o build já pronto e autossuficiente, sem a necessidade de qualquer instalação estará no arquivo zip indicado logo abaixo:

![image](https://github.com/user-attachments/assets/3d3fc35d-3bde-4d59-8d97-a691b0a3417f)

Descompactando o arquivo, basta a execução do BusManager.exe e o aplicativo estará em execução.

![image](https://github.com/user-attachments/assets/7e78e148-b7d6-4c9b-baef-b8119f327083)

---

> [!NOTE]
> Trabalho realizado para a matéria de Banco de Dados II da professora Rebeca Schroeder da Universidade do Estado de Santa Catarina - Centro de Ciências Tecnológicas

### Developed by [Guilherme Siedschlag](https://github.com/phdguigui)
