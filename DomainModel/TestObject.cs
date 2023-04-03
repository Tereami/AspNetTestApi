using System;
using System.Collections.Generic;

namespace DomainModel
{
    public class TestObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; }
        public List<string> Tags { get; set; }
        public TestObject(int id, string name, string description, List<string> tags)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedAt = DateTime.UtcNow;
            Tags = tags;
        }
    }
}
