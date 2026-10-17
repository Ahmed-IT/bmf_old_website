using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace BMF.Shared.Core.Helpers
{
    public static class EnumHelper<T>
    {
        #region Display Attribute

        #region Description

        public static string GetDisplayDescription(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes[0].ResourceType != null)
                return lookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Description);

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Description : value.ToString();
        }

        public static string GetDisplayDescription(string value)
        {
            var fieldInfo = Parse(value).GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes[0].ResourceType != null)
                return lookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Description);

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Description : value.ToString();
        }

        public static List<string> GetDisplayDescriptions()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(s => GetDisplayDescription(s).ToString()).ToList();
        }

        public static Dictionary<string, string> GetDisplayDescriptionsWithNames()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(s => new { key = s.ToString(), value = GetDisplayDescription(s).ToString() }).ToDictionary(s => s.key, v => v.value);
        }

        #endregion

        #region Prompt

        public static string GetDisplayPrompt(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes[0].ResourceType != null)
                return lookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Prompt);

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Prompt : value.ToString();
        }

        public static string GetDisplayPrompt(string value)
        {
            var fieldInfo = Parse(value).GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes[0].ResourceType != null)
                return lookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Prompt);

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Prompt : value.ToString();
        }

        public static List<string> GetDisplayPrompts()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(s => GetDisplayPrompt(s).ToString()).ToList();
        }

        #endregion

        #region Group

        public static string GetDisplayGroupName(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes[0].ResourceType != null)
                return lookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].GroupName);

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? (descriptionAttributes[0].GroupName ?? string.Empty) : value.ToString();
        }

        public static string GetDisplayGroupName(string value)
        {
            var fieldInfo = Parse(value).GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes[0].ResourceType != null)
                return lookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].GroupName);

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? (descriptionAttributes[0].GroupName ?? string.Empty) : value.ToString();
        }

        public static List<string> GetDisplayGroupNames()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(s => GetDisplayGroupName(s).ToString()).ToList();
        }

        public static SelectList GetSelectList_ByGroupName(string GroupIdentifier, object SelectedValue = null)
        {
            var _List = Enum.GetValues(typeof(T)).Cast<T>().Where(w => GetDisplayGroupName(w).ToLower() == GroupIdentifier.ToLower()).Select(s => new SelectListItem
            {
                Value = s.ToString(),
                Text = GetDisplayName(s).ToString()
            }).ToList();
            return new SelectList(_List, "Value", "Text", SelectedValue);
        }

        public static SelectList GetSelectListByGroupName(T GroupIdentifier, object SelectedValue = null)
        {
            string _GroupIdentifier = GetDisplayGroupName(GroupIdentifier);
            var _List = Enum.GetValues(typeof(T)).Cast<T>().Where(w => GetDisplayGroupName(w).ToLower() == _GroupIdentifier.ToLower()).Select(s => new SelectListItem
            {
                Value = s.ToString(),
                Text = GetDisplayGroupName(s).ToString()
            }).ToList();
            return new SelectList(_List, "Value", "Text", SelectedValue);
        }

        #endregion

        #region Name

        public static string GetDisplayName(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes[0].ResourceType != null)
                return lookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? (descriptionAttributes[0].Name ?? string.Empty) : value.ToString();
        }

        public static string GetDisplayName(string value)
        {
            var fieldInfo = Parse(value).GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes[0].ResourceType != null)
                return lookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? (descriptionAttributes[0].Name ?? string.Empty) : value.ToString();
        }

        public static List<string> GetDisplayNames()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(s => GetDisplayName(s).ToString()).ToList();
        }

        public static SelectList GetSelectList(object SelectedValue = null)
        {
            var _List = Enum.GetValues(typeof(T)).Cast<T>().Select(v => new SelectListItem
            {
                Value = v.ToString(),
                Text = GetDisplayName(v).ToString()
            }).ToList();
            return new SelectList(_List, "Value", "Text", SelectedValue);
        }

        public static MultiSelectList GetMultiSelectList(IEnumerable<T> SelectedValue = null)
        {
            var _List = Enum.GetValues(typeof(T)).Cast<T>().Select(v => new SelectListItem
            {
                Value = v.ToString(),
                Text = GetDisplayName(v).ToString()
            }).ToList();
            return new MultiSelectList(_List, "Value", "Text", SelectedValue);
        }

        #endregion

        #endregion

        #region Enum

        public static T Parse(string value)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            catch (Exception LogError)
            {
                return GetValueFromDisplay(value);
            }
        }

        public static IList<T> GetValues(Enum value)
        {
            var enumValues = new List<T>();

            foreach (FieldInfo fi in value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                enumValues.Add((T)Enum.Parse(value.GetType(), fi.Name, false));
            }
            return enumValues;
        }

        public static IList<string> GetNames(Enum value)
        {
            return value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToList();
        }

        public static T GetValueFromDisplay(string name)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DisplayAttribute)) as DisplayAttribute;
                if (attribute != null)
                {
                    if (attribute.Name == name)
                    {
                        return (T)field.GetValue(null);
                    }
                }
                else
                {
                    if (field.Name == name)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentOutOfRangeException("name");
        }

        #endregion

        private static string lookupResource(Type resourceManagerProvider, string resourceKey)
        {
            foreach (PropertyInfo staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (staticProperty.PropertyType == typeof(System.Resources.ResourceManager))
                {
                    System.Resources.ResourceManager resourceManager = (System.Resources.ResourceManager)staticProperty.GetValue(null, null);
                    return resourceManager.GetString(resourceKey);
                }
            }

            return resourceKey; // Fallback with the key name
        }

        #region String Conversion

        private static readonly Dictionary<string, T> Dict;

        static EnumHelper()
        {
            Dict = Enum.GetNames(typeof(T)).ToDictionary(x => x, x => (T)Enum.Parse(typeof(T), x), StringComparer.OrdinalIgnoreCase);
        }

        public static T Convert(string value)
        {
            return Dict[value];
        }

        #endregion
    }
}