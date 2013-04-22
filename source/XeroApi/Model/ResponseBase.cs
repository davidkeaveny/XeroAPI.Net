﻿using System;
using System.Reflection;

namespace XeroApi.Model
{
    public abstract class ResponseBase
    {
        public DateTime DateTimeUTC { get; set; }
        public string Status { get; set; }
        public string ProviderName { get; set; }

        #region Type Helper Methods

        public IModelList GetTypedProperty(Type elementListType)
        {
            foreach (PropertyInfo property in GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (property.PropertyType.IsAssignableFrom(elementListType))
                {
                    return (IModelList)property.GetValue(this, new object[] { });
                }
            }

            throw new ArgumentException("There are not properties that are of type " + elementListType.Name);
        }

        public IModelList<TModel> GetTypedProperty<TModel>()
            where TModel : ModelBase
        {
            PropertyInfo[] properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (PropertyInfo property in properties)
            {
                if (typeof(IModelList<TModel>).IsAssignableFrom(property.PropertyType))
                {
                    return (IModelList<TModel>)property.GetValue(this, new object[] { });
                }
            }

            throw new ArgumentException("There are no response elements that are a collection of type " + typeof(TModel).Name);
        }

        public object GetSingleTypedProperty(Type elementListType)
        {
            foreach (PropertyInfo property in GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (property.PropertyType.IsAssignableFrom(elementListType))
                {
                    return property.GetValue(this, new object[] { });
                }
            }

            throw new ArgumentException("There are not properties that are of type " + elementListType.Name);
        }

        #endregion
    }
}