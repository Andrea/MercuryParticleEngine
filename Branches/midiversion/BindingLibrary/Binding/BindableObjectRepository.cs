using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BindingLibrary
{
    public class BindableObjectRepository
    {


        private static BindableObjectRepository instance;
        private readonly Dictionary<string, IBindableObject> repository;
        private readonly IList<IBindableObjectFactory> factoryList;


        protected BindableObjectRepository()
        {
            repository = new Dictionary<string, IBindableObject>();
            factoryList = new List<IBindableObjectFactory>();
        }

        public static BindableObjectRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BindableObjectRepository();
                }

                return instance;
            }
        }


        public Dictionary<string, IBindableObject> Repository
        {
            get { return repository; }
        }


        public static void RegisterBindableObject(IBindableObject bindableObject)
        {
            if (bindableObject == null || string.IsNullOrEmpty(bindableObject.BindingId)) return;

            UnRegisterBindableObject(bindableObject);

            Instance.Repository.Add(bindableObject.BindingId, bindableObject);
        }


        public static void UnRegisterBindableObject(IBindableObject bindableObject)
        {
            if (bindableObject == null || string.IsNullOrEmpty(bindableObject.BindingId)) return;


            // search repository
            IBindableObject obj = null;

            if (!string.IsNullOrEmpty(bindableObject.BindingId))
            {
                Instance.Repository.TryGetValue(bindableObject.BindingId, out obj);
            }

            // if object in repository -> remove it.
            if (obj != null)
            {
                Instance.Repository.Remove(bindableObject.BindingId);
            }
        }
       
        
        public static void RegisterBindableObjectFactory(IBindableObjectFactory factory)
        {
            if (factory == null) return;

            if (!Instance.factoryList.Contains(factory))
            {
                Instance.factoryList.Add(factory);
            }
        }


        public static void UnRegisterBindableObjectFactory(IBindableObjectFactory factory)
        {
            if (factory == null) return;

            if (!Instance.factoryList.Contains(factory))
            {
                Instance.factoryList.Remove(factory);
            }
        }


        public static IBindableObject GetBindableObject(string id)
        {
            IBindableObject obj = null;

            // search repository
            if (! string.IsNullOrEmpty(id))
            {
                Instance.Repository.TryGetValue(id, out obj);
            }

            // fallback on registered factories
            if (obj == null)
            {
                foreach (IBindableObjectFactory objectFactory in Instance.factoryList)
                {
                    obj = objectFactory.CreateObject(id);

                    if (obj != null) return obj;
                }
            }

            return obj;
        }

    }
}
