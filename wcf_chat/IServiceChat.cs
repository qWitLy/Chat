using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace wcf_chat
{
    // ПРИМЕЧАНИЕ. Можно использовать команду "Переименовать" в меню "Рефакторинг", чтобы изменить имя интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract(CallbackContract =typeof(IServerChatCallback))]
    public interface IServiceChat
    {
        [OperationContract]
        Guid Connect(String name);

        [OperationContract]
        void Disconnect(Guid id);

        [OperationContract(IsOneWay = true)]
        void SendMsg(String message, Guid id);
    }

    public interface IServerChatCallback
    {
        [OperationContract(IsOneWay =true)]
        void MsgCallback(String message);
    }
}
