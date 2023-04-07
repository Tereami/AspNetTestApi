using DomainModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DomainModel
{
    public class TestObject : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; }
        public ICollection<Tag> Tags { get; set; }

        public TestObject(int id, string name, string description, string tags)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedAt = DateTime.UtcNow;
            SetTags(tags);
        }

        public void SetTags(string texttags)
        {
            Tags = texttags.Replace(" ", "").Split(',').Select(i => new Tag { Text = i }).ToList();
        }


        public TestObject()
        {

        }
    }
}
