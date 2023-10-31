namespace DIContainer
{
    public class Container
    {
        private Dictionary<Type, InjectionType> _container;
        private Dictionary<Type, List<Type>> _inheritances;
        private Dictionary<Type, object> _singletons;
        private Type entryPoint;
        private string entryPointMethod;

        public Container()
        {
            _container = new Dictionary<Type, InjectionType>();
            _inheritances = new Dictionary<Type, List<Type>>();
            _singletons = new Dictionary<Type, object>();

        }
        public void AddTransient<T>() where T : class
        {
            _container.Add(typeof(T), InjectionType.Transient);
        }

        public void AddTransient<TT, T>() where T : TT
        {
            _container.Add(typeof(T),InjectionType.Transient);
            if (_inheritances.ContainsKey(typeof(TT)))
            {
                _inheritances[typeof(TT)].Add(typeof(T));
            }
            else
            {
                _inheritances[typeof(TT)] = new List<Type>() { typeof(T) };
            }
        }

        public void AddSingleton<T>() where T : class
        {
            _container.Add(typeof(T), InjectionType.Singleton);            
        }

        public void AddEntryPoint<T>(string methodName = "Run", bool asSingleton = true)
        {
            _container.Add(typeof(T), InjectionType.Singleton);
            entryPoint = typeof(T);
            entryPointMethod = methodName;
        }

        public void Run() => entryPoint.GetMethod(entryPointMethod).Invoke(GetService(entryPoint), null);
        
        public T GetService<T>() => (T)GetService(typeof(T));
        
        private object GetService(Type type)
        {
            InjectionType injectionType = _container[type];
            if (injectionType == InjectionType.Singleton && _singletons.ContainsKey(type))
            {
                return _singletons[type];
            } 
            
            var constructors = type.GetConstructors();
            if (constructors.Length != 1) throw new Exception("[type.FullName] must be 1 constructor");
            var constructor = constructors[0];
            var parameters = constructor.GetParameters();
            object[] arguments = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                Type parameterType = parameters[i].ParameterType;
                if (parameterType.IsInterface)
                {
                    List<Type> types = _inheritances[parameterType];
                    if (types.Count != 1) throw new Exception("[parameterType.FullName] must be 1 realisation");
                    parameterType = _inheritances[parameterType][0];
                }
                else
                {
                    if (parameterType.IsArray)
                    {
                        Type elementType = parameterType.GetElementType();
                        List<Type> types = _inheritances[elementType];
                        Array arrayOfInstance = (Array)Activator.CreateInstance(parameterType, types.Count);
                        for (int j = 0; j < types.Count; j++)
                        {
                            arrayOfInstance.SetValue(GetService(types[j]),j);
                        }
                        
                        
                        arguments[i] = arrayOfInstance;
                        continue;
                    }
                }
                arguments[i] = GetService(parameters[i].ParameterType);
            }

            object instance = Activator.CreateInstance(type, args: arguments);
            
            if (injectionType == InjectionType.Singleton) _singletons[type] = instance;
            return instance;
        }
    }
}

