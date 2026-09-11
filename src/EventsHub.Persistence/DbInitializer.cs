using EventsHub.Domain;
using Microsoft.EntityFrameworkCore;

namespace EventsHub.Persistence;

public static class DbInitializer
{
    public static async Task SeedData(AppDbContext context)
    {
        if (await context.Events.AnyAsync()) return;

        var events = new List<Event>
        {
            new() {
                Title = "Past Event 1",
                Date = DateTime.Now.AddMonths(-1),
                Description = "Event 1 months ago",
                Category = "culture",
                City = "Tinúm, Yucatán",
                Venue = "Chichén Itzá",
                latitude = "20.6843",
                longitude = "-88.5678"
            },
            new() {
                Title = "Past Event 2",
                Date = DateTime.Now.AddMonths(-3),
                Description = "Event 3 months ago",
                Category = "music",
                City = "San Juan Teotihuacán, Estado de México",
                Venue = "Pirámide del Sol, Teotihuacán",
                latitude = "19.6925",
                longitude = "-98.8438"
            },
            new() {
                Title = "Past Event 3",
                Date = DateTime.Now.AddMonths(-5),
                Description = "Event 5 months ago",
                Category = "drinks",
                City = "Ciudad de México",
                Venue = "Zócalo (Plaza de la Constitución)",
                latitude = "19.4326",
                longitude = "-99.1332"
            },
            new() {
                Title = "Past Event 4",
                Date = DateTime.Now.AddMonths(-7),
                Description = "Event 7 months ago",
                Category = "culture",
                City = "Ciudad de México",
                Venue = "Palacio de Bellas Artes",
                latitude = "19.4352",
                longitude = "-99.1412"
            },
            new() {
                Title = "Past Event 5",
                Date = DateTime.Now.AddMonths(-9),
                Description = "Event 9 months ago",
                Category = "music",
                City = "Cancún, Quintana Roo",
                Venue = "Playa Delfines",
                latitude = "21.0997",
                longitude = "-86.7561"
            },
            new() {
                Title = "Future Event 1",
                Date = DateTime.Now.AddMonths(1),
                Description = "Event 1 months in future",
                Category = "drinks",
                City = "Guanajuato, Guanajuato",
                Venue = "Callejón del Beso",
                latitude = "21.0190",
                longitude = "-101.2574"
            },
            new() {
                Title = "Future Event 2",
                Date = DateTime.Now.AddMonths(2),
                Description = "Event 2 months in future",
                Category = "culture",
                City = "Ciudad de México",
                Venue = "Xochimilco (Trajineras)",
                latitude = "19.2828",
                longitude = "-99.1036"
            },
            new() {
                Title = "Future Event 3",
                Date = DateTime.Now.AddMonths(4),
                Description = "Event 4 months in future",
                Category = "music",
                City = "Ciudad de México",
                Venue = "Basílica de Guadalupe",
                latitude = "19.4847",
                longitude = "-99.1176"
            },
            new() {
                Title = "Future Event 4",
                Date = DateTime.Now.AddMonths(6),
                Description = "Event 6 months in future",
                Category = "drinks",
                City = "Tulum, Quintana Roo",
                Venue = "Zona Arqueológica de Tulum",
                latitude = "20.2114",
                longitude = "-87.4287"
            },
            new() {
                Title = "Future Event 5",
                Date = DateTime.Now.AddMonths(8),
                Description = "Event 8 months in future",
                Category = "culture",
                City = "Guadalajara, Jalisco",
                Venue = "Palacio de Gobierno de Jalisco",
                latitude = "20.6767",
                longitude = "-103.3475"
            }
        };

        context.Events.AddRange(events);
        await context.SaveChangesAsync();
    }
}
