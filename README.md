# Apache Kafka

### App para visualização gráfica do cluster 
<a href="https://www.kafkatool.com/" target="_blank">OffsetExplore</a>

### Definição ouvintes(*Ouvinte é uma combinação de host+porta+protocolo*)
- Interno: Comunicação entre os clusters kafka
- Externo: Produtores e consumidores

### Tipos de protocolo
- PLAINTEXT: canal de comunicação sem precisar está autenticado e não criptografado.
- SASL_PLAINTEXT: autenticado mas não criptográfado.
- SSL: criptografado e autenticado.

# Cluster Apache Kafka:
### Broker
- É o servidor do Kafka, responsável por receber as mensagens dos producers, escrever as mensagens no disco e disponibilizar para os consumers
   
### Tópicos
- É uma forma de rotular ou categorizar uma mensagem.
- Quanto mais partições em um tópico aumenta a performance de leitura e escrita daquele tópico.
  
### Partição
- É uma sequência imutável e ordenada de mensagens e cada mensagem recebe uma identificação numérica sequencial que chamamos de offset

### Offsets
- Posição da mensagem dentro de uma partição

### Fator de replicação(replication factor)
- Replicação de informação de uma partição para os demais brokers.
- Nós proporciona garantia de uma maior disponibilidade do sistema.
- Tendo seu tamanho máx até a quantidade de brokers do cluster.
  
## Kafka com dotnet
*executar docker-compose-schema-registry*

**- Instalar dependências para o projeto Consumidor**

     dotnet add Projeto package Confluent.Consumer

**- Instalar dependências para o projeto Produtor**

     dotnet add Projeto package Confluent.Producer

**- Instalar tool dotnet para contratos AVRO**

    dotnet tool install --global Apache.Avro.Tools

**- Instalar  Ferramenta que informa ao produtor e consumidor qual serializar ele irá deve utilizar e responsável por se comunicar com servidor do schemaRegistry**

    dotnet add Projeto package Confluent.SchemaRegistry.Serdes.Avro

<a href="https://avro.apache.org/" target="_blank">doc-apache-avro</a>

**- Criar classe a partir de um schema AVRO(.avsc)**

    avrogen -s ../Avros/Arquivos.avsc CaminhoOndeCriarClasse

## Avançado
- Criar serializado/deserializador customizado
- Producer Acknowledgements
    **- None**:
        * Não possuimos a garantia da entrega da mensagem, mas garantimos uma melhor peformance.

    **- Leader**:
        * Kafka garante que a mensagem foi recebida e registrada no broker.

    **- All**:
        * Maior garantia que a mensagem foi recebida. Pois a mesma é replicada em mais de um broker.
        Garatindo que o replication factory estejam sincronizadas. Essa opção possui deadação da performance, porém sua garantia de entrega é muito eficiênte.

- AutoOffsetReset
    **- Earliest**:
        * Ao inserir um novo grupo de consumidor, e declaro esse prop no AutoOffSetReset, todas as mensagens um dia gerada naquele tópico serão reprocessadas e lidas por esse novo grupo.
    **- Latest**:
        * Ao inserir um novo grupo de consumidor, e declaro esse prop no AutoOffSetReset, as mensagens so serão consumidas a partir daquele momento, tudo que virá a ser produzido.

- Ordernação de mensagens no kafka

- Consumir mensagens no kafka mais de uma vez
    * uma mensagem que foi lida mas não commitada, seu offset no kafka é deslocado, logo, o consumidor não consiguirar ler novamente, a não ser que reinicie o serviço. Porque uma mensagem lida mas não commitada ela fica como **não processada no kafka**. Podendo ser lida novamente quando o consumidor utiliza o método **Seek** para voltar o ponteiro para a mensagem não processada, assim não precisa reiciar o serviço.
    
- Idempotência
    * Habilitando a idempotência Kafka garante que a mensagem vai ser entregue somente uma vez. Evitamos a duplicidade.
- Trabalhar com transações
- Headers e Traincing


### Apoio
<a href="https://renatogroffe.medium.com/net-apache-kafka-guia-de-refer%C3%AAncia-3f82512df4c" target="_blank">artigo</a>