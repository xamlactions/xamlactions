using System;
using System.Linq;
using System.Reflection;

namespace XamlActions.Reflection {
    public class Reflector {
        public static BindingFlags NoRestrictions = BindingFlags.Public |
                                                    BindingFlags.NonPublic |
                                                    BindingFlags.Static |
                                                    BindingFlags.Instance;

        public static BindingFlags NoRestrictionsIgnoreCase = NoRestrictions | BindingFlags.IgnoreCase;

        public static string[] ListAllFields(Type type) {
            FieldInfo[] fields = type.GetFields(NoRestrictions);

            var resp = new string[fields.Length];
            for (int i = 0; i < resp.Length; i++) {
                resp[i] = fields[i].Name;
            }
            return resp;
        }

        public static string[] ListAllProperties(Type type) {
            PropertyInfo[] props = type.GetProperties(NoRestrictions);

            var resp = new string[props.Length];
            for (int i = 0; i < resp.Length; i++) {
                resp[i] = props[i].Name;
            }
            return resp;
        }

        public static string[] ListAllPublicProperties(Type type) {
            PropertyInfo[] props = type.GetProperties(BindingFlags.Public |
                                                      BindingFlags.Instance);

            var resp = new string[props.Length];
            for (int i = 0; i < resp.Length; i++) {
                resp[i] = props[i].Name;
            }
            return resp;
        }

        public static bool IsNull(object obj, string fieldName) {
            Type type = obj.GetType();
            FieldInfo field = type.GetField(fieldName, NoRestrictions);
            return (field.GetValue(obj) == null);
        }

        public static void Set(object obj, string fieldName, object value) {
            Type type = obj.GetType();
            FieldInfo field = type.GetField(fieldName, NoRestrictions);
            field.SetValue(obj, value);
        }

        public static T Get<T>(object obj, string fieldName) {
            Type type = obj.GetType();
            FieldInfo field = type.GetField(fieldName, NoRestrictions);
            return (T)field.GetValue(obj);
        }

        public static object Get(object obj, string fieldName) {
            Type type = obj.GetType();
            FieldInfo field = type.GetField(fieldName, NoRestrictions);

            return field.GetValue(obj);
        }

        public static Type GetFieldType(Type type, string fieldName) {
            FieldInfo info = type.GetField(fieldName, NoRestrictions);
            return info.FieldType;
        }

        public static Type GetPropertyType(Type type, string fieldName) {
            PropertyInfo info = type.GetProperty(fieldName, NoRestrictions);
            return info.PropertyType;
        }

        public static void SetProperty(object obj, string propertyName, object value) {
            Type type = obj.GetType();
            PropertyInfo property = type.GetProperty(propertyName, NoRestrictions);
            property.SetValue(obj, value, null);
        }

        public static object GetProperty(object obj, string propertyName) {
            Type type = obj.GetType();
            PropertyInfo property = type.GetProperty(propertyName, NoRestrictions);
            return property.GetValue(obj, null);
        }

        public static bool HasProperty(Type type, string propertyName) {
            PropertyInfo info = type.GetProperty(propertyName, NoRestrictions);
            return info != null;
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
            MethodInfo[] methods = type.GetMethods(NoRestrictionsIgnoreCase);
            return methods.FirstOrDefault(p => p.Name == methodName &&
                                               p.GetParameters().Count() == numParameters);
        }

        public static void RaiseEvent(object obj, string eventName, params object[] eventArgs) {
            var eventDelagate = (MulticastDelegate)Get(obj, eventName);
            Delegate[] delegates = eventDelagate.GetInvocationList();
            foreach (Delegate dlg in delegates) {
                dlg.Method.Invoke(dlg.Target, eventArgs);
            }
        }

        private static EventInfo GetEventInfo(object obj, string eventName) {
            Type type = obj.GetType();
            return type.GetEvent(eventName);
        }
    }
}