using IBM.WMQ;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace QueueIbm
{
    public class IbmQueue : IIbmQueue
    {
        private Hashtable _queueManagerProperties = null;

        public IbmQueue()
        {
            InitializeQueueManagerConnectionProperties();
        }

        public string ReadOneMensage()
        {
            MQQueueManager qMgr = new MQQueueManager(IbmConection.DbConection[5], _queueManagerProperties);

            // Agora especifique a fila que queremos abrir e as opções de abertura
            MQQueue fila = qMgr.AccessQueue(IbmConection.DbConection[6], MQC.MQQA_GET_ALLOWED + MQC.MQQA_GET_INHIBITED + MQC.MQOO_INPUT_AS_Q_DEF + MQC.MQOO_OUTPUT + MQC.MQOO_FAIL_IF_QUIESCING + MQC.MQOO_INQUIRE + MQC.MQOO_BROWSE);

            MQMessage retrievedMessage = new MQMessage();

            // Defina as opções de obtenção de mensagens
            MQGetMessageOptions gmo = new MQGetMessageOptions(); // aceita os padrões
                                                                 // mesmo que MQGMO_DEFAULT

            string msgText = "";

            if (fila.CurrentDepth > 0)
            {
                // Tire a mensagem da fila
                fila.Get(retrievedMessage, gmo);

                msgText = retrievedMessage.ReadUTF();

                return msgText;
            }
            else
            {
                return msgText;
            }
        }
        
        public List<string> ReadManyMensage()
        {
            MQQueueManager qMgr = new MQQueueManager(IbmConection.DbConection[5], _queueManagerProperties);

            // Agora especifique a fila que queremos abrir e as opções de abertura
            MQQueue fila = qMgr.AccessQueue(IbmConection.DbConection[6], MQC.MQQA_GET_ALLOWED + MQC.MQQA_GET_INHIBITED + MQC.MQOO_INPUT_AS_Q_DEF + MQC.MQOO_OUTPUT + MQC.MQOO_FAIL_IF_QUIESCING + MQC.MQOO_INQUIRE + MQC.MQOO_BROWSE);

            MQMessage retrievedMessage = new MQMessage();

            // Defina as opções de obtenção de mensagens
            MQGetMessageOptions gmo = new MQGetMessageOptions(); // aceita os padrões
                                                                 // mesmo que MQGMO_DEFAULT

            List<string> vs = new List<string>();
            
            if (fila.CurrentDepth > 0)
            {

                do
                {
                    // Tire a mensagem da fila
                    fila.Get(retrievedMessage, gmo);

                    string msgText = retrievedMessage.ReadUTF();

                    vs.Add(msgText);

                    retrievedMessage = new MQMessage();
                } while (fila.CurrentDepth >= 1);
                
                return vs;
            }
            else
            {
                return vs;
            }
        }

        public void Write(string menssage)
        {

            // Crie uma conexão com o gerenciador de filas usando a conexão
            // propriedades recém-definidas
            MQQueueManager qMgr = new MQQueueManager(IbmConection.DbConection[5], _queueManagerProperties);

            // Especifica a fila que quere abrir com as opçoes de abertura
            MQQueue fila = qMgr.AccessQueue(IbmConection.DbConection[6], MQC.MQOO_INPUT_AS_Q_DEF + MQC.MQOO_OUTPUT + MQC.MQOO_FAIL_IF_QUIESCING);

            // Defina uma mensagem do IBM MQ, escrevendo algum objeto
            MQMessage item = new MQMessage();
            item.WriteUTF(JsonConvert.SerializeObject(menssage));

            // Especifique as opções de mensagem
            MQPutMessageOptions pmo = new MQPutMessageOptions(); // aceita os padrões,
                                                                 // mesmo que MQPMO_DEFAULT

            // Coloque a mensagem na fila
            fila.Put(item, pmo);

            fila.Close();
        }

        private void InitializeQueueManagerConnectionProperties()
        {
            _queueManagerProperties = new Hashtable
            {
                { MQC.TRANSPORT_PROPERTY, MQC.TRANSPORT_MQSERIES_MANAGED },
                { MQC.USER_ID_PROPERTY, IbmConection.DbConection[0] },
                { MQC.PASSWORD_PROPERTY, IbmConection.DbConection[1] },
                { MQC.PORT_PROPERTY,  Convert.ToInt32(IbmConection.DbConection[2]) },
                { MQC.CHANNEL_PROPERTY, IbmConection.DbConection[3] },
                { MQC.HOST_NAME_PROPERTY, IbmConection.DbConection[4] }
            };

        }
    }
}
