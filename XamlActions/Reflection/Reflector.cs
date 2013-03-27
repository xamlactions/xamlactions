using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;

namespace XamlActions.Reflection {
    public class Reflector {

        public static string[] ListAllFields(Type type) {
            return type.GetRuntimeFields().Select(x => x.Name).ToArray();
        }

        public static string[] ListAllProperties(Type type) {
            return type.GetRuntimeProperties().Select(x => x.Name).ToArray();
        }

        public static bool IsNull(object obj, string fieldName) {
            return obj.GetType().GetRuntimeField(fieldName).GetValue(obj) == null;
        }

        public static void Set(object obj, string fieldName, object value) {
            obj.GetType().GetRuntimeField(fieldName).SetValue(obj, value);
        }

        public static T Get<T>(object obj, string fieldName) {
            var value = obj.GetType().GetRuntimeField(fieldName).GetValue(obj);
            return (value == null) ? default(T) : (T)value;
        }

        public static object Get(object obj, string fieldName) {
            return obj.GetType().GetRuntimeField(fieldName).GetValue(obj);
        }

        public static Type GetFieldType(Type type, string fieldName) {
            return type.GetRuntimeField(fieldName).FieldType;
        }

        public static Type GetPropertyType(Type type, string propertyName) {
            return type.GetRuntimeProperty(propertyName).PropertyType;
        }

        public static void SetProperty(object obj, string propertyName, object value) {
            obj.GetType().GetRuntimeProperty(propertyName).SetValue(obj, value);
        }

        public static object GetProperty(object obj, string propertyName) {
            return obj.GetType().GetRuntimeProperty(propertyName).GetValue(obj);
        }

        public static bool HasProperty(Type type, string propertyName) {
            return type.GetRuntimeProperty(propertyName) != null;
        }

        public static object CallMethod(object obj, string methodName, params object[] parameters) {
            Type type = obj.GetType();
            int numParameters = (parameters != null) ? parameters.Count() : 0;
            MethodInfo methodToCall = GetMethodInfo(type, methodName, numParameters);

            if (methodToCall == null) {
                string msg = String.Format("Could not find method '{0}' with {1} parameter(s) on type {2}", methodName,
                                           numParameters, type);
                throw new ReflectorException(msg);
            }
            return methodToCall.Invoke(obj, parameters);
        }

        public static MethodInfo GetMethodInfo(Type type, string methodName, int numParameters) {
            return type.GetRuntimeMethods().FirstOrDefault(x => x.Name.ToLower().Equals(methodName.ToLower()) && x.GetParameters().Count() == numParameters);
        }


       
    }
}
