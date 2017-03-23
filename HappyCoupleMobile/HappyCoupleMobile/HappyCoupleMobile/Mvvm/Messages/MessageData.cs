﻿using System.Collections.Generic;
using System.Linq;
using HappyCoupleMobile.Mvvm.Messages.Interface;

namespace HappyCoupleMobile.Mvvm.Messages
{
    public class MessageData : IMessageData
    {
        public IDictionary<string, object> Data { get; private set; }

        public MessageData()
        {
            Data = new Dictionary<string, object>();
        }

        public MessageData(string key, object value) : this()
        {
            AddData(key, value);
        }

        public void AddData(string key, object value)
        {
            if (Data.ContainsKey(key))
            {
                return;
            }

            Data.Add(key, value);
        }

        public TValue GetValue<TValue>(string key) where TValue : new()
        {
            if (Data.ContainsKey(key))
            {
                return new TValue();
            }

            var value = Data[key];

            return (TValue)value;
        }

        public TValue GetValue<TValue>() where TValue : new()
        {
            foreach (var data in Data)
            {
                if (data.Value.GetType() == typeof(TValue))
                {
                    return (TValue)data.Value;
                }
            }

            return new TValue();
        }
    }
}