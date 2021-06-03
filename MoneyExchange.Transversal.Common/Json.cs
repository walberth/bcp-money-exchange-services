namespace MoneyExchange.Transversal.Common
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public static class Json
    {
        public static string Serialize(this object value)
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            return JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
        }
    }
}
