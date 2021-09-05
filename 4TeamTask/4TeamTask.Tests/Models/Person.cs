using System.Collections.Generic;

namespace _4TeamTask.Tests.Models
{
    public class Person
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public List<Contact> Contacts { get; set; }

        public Person(long id, string name, List<Contact> contacts)
        {
            this.Id = id;
            this.Name = name;
            this.Contacts = contacts;
        }
    }
}
