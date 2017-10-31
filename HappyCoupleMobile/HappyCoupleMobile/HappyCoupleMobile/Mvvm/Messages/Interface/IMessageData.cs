using System.Collections.Generic;
using HappyCoupleMobile.VM;

namespace HappyCoupleMobile.Mvvm.Messages.Interface
{
    public interface IMessageData
    {
        void AddData(string key, object value);
        object GetValue(string key);
        TValue GetValue<TValue>() where TValue : new();
	    ProductVm GetFirstOrDefaultProduct();
	    IList<ProductVm> GetFirstOrDefaultProductsRange();
    }
}