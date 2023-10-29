# Apache Kafka

**- Tipos de ouvintes:**
*Ouvinte é uma combinação de host+porta+protocolo*
    * Interno: Comunicação entre os clusters kafka
    * Externo: Produtores e consumidores

**- Tipos de protocolo:**
    * PLAINTEXT: canal de comunicação sem precisar está autenticado e não criptografado.
    * SASL_PLAINTEXT: autenticado mas não criptográfado.
    * SSL: criptografado e autenticado.