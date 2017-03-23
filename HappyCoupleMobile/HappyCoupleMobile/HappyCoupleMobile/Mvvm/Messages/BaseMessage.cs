using System.Collections;
using System.Collections.Generic;
using HappyCoupleMobile.Mvvm.Messages.Interface;

namespace HappyCoupleMobile.Mvvm.Messages
{
    public class BaseMessage<T> : MessageData, IBaseMessage<T>
    {
        public BaseMessage()
        {
        }

        public BaseMessage(string key, object value) : base(key, value)
        {
        }
    }
}
