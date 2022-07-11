using NUnit.Framework;
using Microsoft.Rest.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IO;

namespace RestClientRuntime
{
    public class NonDefaultConstructorTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Can_deserialize_type_dog_in_json()
        {
            var serializer = JsonSerializer.Create(new JsonSerializerSettings()
            {
                Converters = new List<JsonConverter>() { new PolymorphicDeserializeJsonConverter<Animal>("$type") }
            });

            //  System.NullReferenceException : Object reference not set to an instance of an object.
            var dog = serializer.Deserialize<Animal>(new JsonTextReader(new StringReader(@"{""$type"":""Dog"",""Name"":""Bello""}")));
            Assert.IsNotNull(dog);
            Assert.AreEqual("Bello", dog.Name);
        }

        [Test]
        public void Can_deserialize_type_cat_in_json()
        {
            var serializer = JsonSerializer.Create(new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver(),
                Converters = new List<JsonConverter>() { new PolymorphicDeserializeJsonConverter<Animal>("$type") }
            });

            var cat = serializer.Deserialize<Animal>(new JsonTextReader(new StringReader(@"{""$type"":""Cat"",""Name"":""Felix""}")));
            Assert.IsNotNull(cat);
            Assert.AreEqual("Felix", cat.Name);
        }
    }
}