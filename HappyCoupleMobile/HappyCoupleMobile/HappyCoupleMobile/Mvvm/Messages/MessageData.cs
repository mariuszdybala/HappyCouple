using System.Collections.Generic;
using System.Linq;
using HappyCoupleMobile.Mvvm.Messages.Interface;
using HappyCoupleMobile.VM;

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

        public object GetValue(string key) 
        {
            if (!Data.ContainsKey(key))
            {
                return null;
            }

            var value = Data[key];

            return value;
        }

        public TValue GetValue<TValue>() where TValue: new()
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

	    public ProductVm GetFirstOrDefaultProduct()
	    {
		    return GetValue(MessagesKeys.ProductKey) as ProductVm;
	    }
	    
	    public IList<ProductVm> GetFirstOrDefaultProductsRange()
	    {
		    var products = GetValue(MessagesKeys.ProductsRange);

		    return products as IList<ProductVm>;
	    }
    }
}