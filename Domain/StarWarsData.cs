using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;

namespace Domain
{
    public class StarWarsData
    {
        private readonly List<Human> _humans = new List<Human>();
        private readonly List<Matter> _matters = new List<Matter>();
        private readonly List<Droid> _droids = new List<Droid>();
        private readonly List<Contact> _clients = new List<Contact>();
        private readonly List<Role> _roles = new List<Role>();

        public StarWarsData()
        {
            _humans.Add(new Human
            {
                Id = "1",
                Name = "Luke",
                Friends = new[] { "3", "4" },
                AppearsIn = new[] { 4, 5, 6 },
                HomePlanet = "Tatooine"
            });
            _humans.Add(new Human
            {
                Id = "2",
                Name = "Vader",
                AppearsIn = new[] { 4, 5, 6 },
                HomePlanet = "Tatooine"
            });

            _droids.Add(new Droid
            {
                Id = "3",
                Name = "R2-D2",
                Friends = new[] { "1", "4" },
                AppearsIn = new[] { 4, 5, 6 },
                PrimaryFunction = "Astromech"
            });
            _droids.Add(new Droid
            {
                Id = "4",
                Name = "C-3PO",
                AppearsIn = new[] { 4, 5, 6 },
                PrimaryFunction = "Protocol"
            });

            _humans.Add(new Human
            {
                Id = "Lando",
                Name = "Lando",
                AppearsIn = new[] { 5, 6 },
                HomePlanet = "Socorro"
            });

            _matters.Add(new Matter()
            {
                Reference = "A00001-0001",
                Description = "A new matter",
                Roles = new[] { 1, 2 }
            });

            _clients.Add(new Contact()
            {
                Reference = "A0001",
                Forename = "John"
            });

            _roles.AddRange(new[]
            {
                new Role
                {
                    Id = 1,
                    Description = "Primary Client"
                },
                new Role
                {
                    Id = 2,
                    Description = "Debtor"
                }
            });

        }

        public IEnumerable<StarWarsCharacter> GetFriends(StarWarsCharacter character)
        {
            if (character == null)
            {
                return null;
            }

            var friends = new List<StarWarsCharacter>();
            var lookup = character.Friends;
            if (lookup != null)
            {
                _humans.Where(h => lookup.Contains(h.Id)).Apply(friends.Add);
                _droids.Where(d => lookup.Contains(d.Id)).Apply(friends.Add);
            }
            return friends;
        }

        public IEnumerable<Role> GetRoles(Matter matter)
        {
            if (matter == null)
            {
                return null;
            }

            var roles = new List<Role>();
            var lookup = matter.Roles;
            if (lookup != null)
            {
                _roles.Where(h => lookup.Contains(h.Id)).Apply(roles.Add);
            }
            return roles;
        }

        public Task<Human> GetHumanByIdAsync(string id)
        {
            return Task.FromResult(_humans.FirstOrDefault(h => h.Id == id));
        }

        public Task<Droid> GetDroidByIdAsync(string id)
        {
            return Task.FromResult(_droids.FirstOrDefault(h => h.Id == id));
        }

        public Task<Matter> GetMatterByReferenceAsync(string reference)
        {
            return Task.FromResult(_matters.FirstOrDefault(h => h.Reference == reference));
        }

        public Task<Contact> GetClientByReferenceAsync(string reference)
        {
            return Task.FromResult(_clients.FirstOrDefault(h => h.Reference == reference));
        }
    }
}
