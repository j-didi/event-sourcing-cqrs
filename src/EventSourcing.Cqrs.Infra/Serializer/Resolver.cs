using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EventSourcing.Cqrs.Infra.Serializer;

internal class Resolver: DefaultContractResolver {
    protected override JsonProperty CreateProperty(
        MemberInfo member, 
        MemberSerialization memberSerialization
    )
    {
        var jsonProperty = base.CreateProperty(member, memberSerialization);
        if (jsonProperty.Writable) 
            return jsonProperty;
        
        var property = member as PropertyInfo;
        jsonProperty.Writable = property?.GetSetMethod(true) != null;
        return jsonProperty;
    }
}