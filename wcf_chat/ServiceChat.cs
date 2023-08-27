using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace wcf_chat
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        List<ServerUser> users = new List<ServerUser>();
        public Guid Connect(String name)
        {
            ServerUser user = new ServerUser()
            {
                Name = name,
                Id = Guid.NewGuid(),
                operationContext = OperationContext.Current
            };
            SendMsg($": {user.Name}: подключился к чату!",default); 

            users.Add(user);
            return user.Id;
        }

        public void Disconnect(Guid id)
        {
            var user = users.FirstOrDefault(x => x.Id ==id);
            if (user != null)
            {
                users.Remove(user);
                SendMsg($": {user.Name}: отключился от чата!",default);
            }
        }


        public void SendMsg(string message, Guid id)
        {
            foreach (var item in users)
            {
                String answer = DateTime.Now.ToShortTimeString();

                var user = users.FirstOrDefault(x => x.Id == id);
                if (user != null)
                {
                    answer += $": {user.Name} ";
                }
                answer += message;

                item.operationContext.GetCallbackChannel<IServerChatCallback>().MsgCallback(answer);
            }
        }
    }
}
