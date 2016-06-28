using System;

namespace FCP.Configuration
{
    public class ConfigEntry<TName, TValue>
    {
        public ConfigEntry(TName name, TValue value)
            : this(name, null, value)
        { }

        public ConfigEntry(TName name, string region, TValue value)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (value == null)
                throw new ArgumentNullException(nameof(value));            

            Name = name;
            Region = region;
            Value = value;
        }

        public TName Name { get; private set; }

        public TValue Value { get; private set; }

        public string Region { get; private set; }
    }
}