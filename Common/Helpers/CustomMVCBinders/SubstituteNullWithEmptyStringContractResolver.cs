using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Common.Helpers.CustomMVCBinders
{
    /// <inheritdoc />
    /// <summary>
    /// Courtesy https://stackoverflow.com/questions/23830206/json-convert-empty-string-instead-of-null
    /// </summary>
    public sealed class SubstituteNullWithEmptyStringContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (property.PropertyType == typeof(string))
            {
                // Wrap value provider supplied by Json.NET.
                property.ValueProvider = new NullToEmptyStringValueProvider(property.ValueProvider);
            }

            return property;
        }

        sealed class NullToEmptyStringValueProvider : IValueProvider
        {
            private readonly IValueProvider _provider;

            public NullToEmptyStringValueProvider(IValueProvider provider)
            {
                _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            }

            public object GetValue(object target)
            {
                return _provider.GetValue(target) ?? "";
            }

            public void SetValue(object target, object value)
            {
                _provider.SetValue(target, value);
            }
        }
    }
}
