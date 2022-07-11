using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestClientRuntime
{
    public abstract class Animal
    {
        protected Animal()
        {
        }

        protected Animal(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }

    public class Dog : Animal
    {
        [JsonConstructor]
        public Dog(string name) : base(name)
        {
        }
    }

    public class Cat : Animal
    {
        public Cat() : base(string.Empty)
        {
        }
    }
}
