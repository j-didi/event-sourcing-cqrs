using Newtonsoft.Json;

namespace EventSourcing.Cqrs.Infra.Serializer;

internal static class JsonExtensions
{
    public static T CustomDeserializeObject<T>(this string json) =>
        JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
        {
            ContractResolver = new Resolver(),
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
        });
}