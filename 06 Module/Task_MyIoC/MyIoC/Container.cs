using MyIoC.Attributes;
using MyIoC.Exceptions;
using MyIoC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MyIoC
{
	public class Container : IContainer
	{
		private readonly IDictionary<Type, Type> typesDict;

		public Container()
		{
			typesDict = new Dictionary<Type, Type>();			
		}
		public void AddAssembly(Assembly assembly)
		{		

			var types = assembly.ExportedTypes;
			foreach (var type in types)
			{
				var constructorImportAttribute = type.GetCustomAttribute<ImportConstructorAttribute>();
				if (constructorImportAttribute != null || HasImportProperties(type))
				{
					typesDict.Add(type, type);
				}

				var exportAttributes = type.GetCustomAttributes<ExportAttribute>();
				foreach (var exportAttribute in exportAttributes)
				{
					typesDict.Add(exportAttribute.Contract ?? type, type);
				}
			}
		}
		public void AddType(Type type)
		{
			typesDict.Add(type, type);
		}

		public void AddType(Type type, Type baseType)
		{
			typesDict.Add(baseType, type);
		}

		public object CreateInstance(Type type)
		{
			Type dependendType = typesDict[type];
			var instance = GetInstanceOfType(dependendType);
			return instance;
		}

		public T CreateInstance<T>()
		{
			var type = typeof(T);
			var instance = (T)GetInstanceOfType(type);
			return instance;
		}

		private object GetInstanceOfType(Type type)
		{
			if (!typesDict.ContainsKey(type))
			{
				throw new NoInDLLException($"Type {type.Name} cann't find in  DLL");
			}

			Type dependendType = typesDict[type];
			var constructors = dependendType.GetConstructors();

			if(constructors.Length == 0)
			{
				throw new NoPublicConstructorException($"Type {type.Name} don't have public constructor");
			}

			var ctorInfo = constructors.First();

			var instance = CreateTargetType(dependendType, ctorInfo);			

			if (dependendType.GetCustomAttribute<ImportConstructorAttribute>() != null)
			{
				return instance;
			}

			ResolveProperties(dependendType, instance);
			return instance;
		}

		private object CreateTargetType(Type type, ConstructorInfo ctorInfo)
		{
			ParameterInfo[] parameters = ctorInfo.GetParameters();
			List<object> parametersInstances = new List<object>();
			foreach (var item in parameters)
			{
				parametersInstances.Add(GetInstanceOfType(item.ParameterType));
			}
			
			object instance = CreateInstance(type, parametersInstances.ToArray());
			return instance;
		}

		private bool HasImportProperties(Type type)
		{
			var propertiesInfo = type.GetProperties().Where(p => p.GetCustomAttribute<ImportAttribute>() != null);
			return propertiesInfo.Any();
		}
				
		private void ResolveProperties(Type type, object instance)
		{
			var propertiesInfo = type.GetProperties().Where(p => p.GetCustomAttribute<ImportAttribute>() != null);
			foreach (var property in propertiesInfo)
			{
				var resolvedProperty = GetInstanceOfType(property.PropertyType);
				property.SetValue(instance, resolvedProperty);
			}
		}

		private object CreateInstance(Type type, params object[] parameters)
		{
			return Activator.CreateInstance(type, parameters);
		}

	}
}
