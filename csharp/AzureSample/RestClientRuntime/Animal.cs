using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        //public Dog()
        //{
        //}

        [JsonConstructor()]
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

    public class Sheep : Animal
    {
        public Sheep() { }

        [JsonConstructor()]
        public Sheep(string name) : base(name)
        {
        }
    }

    public class Rabbit : Animal
    {
        public Rabbit() { }

        [JsonConstructor()]
        public Rabbit(string name) : base(name)
        {
        }

        public int Age { get; set; }
    }
}
