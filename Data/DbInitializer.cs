using Microsoft.EntityFrameworkCore;
using Moot.Data;
using Moot.Models;

namespace Moot.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LibraryContext(serviceProvider.GetRequiredService<DbContextOptions<LibraryContext>>()))
            {
                // Verifică dacă baza de date a fost deja populată
                if (context.Property.Any())
                {
                    return; // BD a fost creată anterior
                }

                // Adăugarea proprietăților
                context.Property.AddRange(
                    new Property { PropertyType = "Apartament 2 camere", Price = 135000 },
                    new Property { PropertyType = "Garsoniera", Price = 75000 },
                    new Property { PropertyType = "Duplex", Price = 350000 },
                    new Property { PropertyType = "Casa individuala", Price = 450000 },
                    new Property { PropertyType = "Apartament 3 camere", Price = 180000 },
                    new Property { PropertyType = "Studio modern", Price = 95000 },
                    new Property { PropertyType = "Teren pentru constructie", Price = 120000 },
                    new Property { PropertyType = "Vila luxoasa", Price = 850000 },
                    new Property { PropertyType = "Apartament 4 camere", Price = 210000 },
                    new Property { PropertyType = "Spatiu comercial", Price = 500000 },
                    new Property { PropertyType = "Cabana de munte", Price = 170000 },
                    new Property { PropertyType = "Garaj", Price = 25000 },
                    new Property { PropertyType = "Duplex modern", Price = 380000 },
                    new Property { PropertyType = "Casa batraneasca", Price = 60000 },
                    new Property { PropertyType = "Loft industrial", Price = 320000 },
                    new Property { PropertyType = "Apartament tip penthouse", Price = 650000 },
                    new Property { PropertyType = "Casuta de vacanta", Price = 120000 },
                    new Property { PropertyType = "Pensiune", Price = 900000 },
                    new Property { PropertyType = "Casa pe malul marii", Price = 1500000 },
                    new Property { PropertyType = "Apartament in bloc vechi", Price = 95000 }
                );

                // Adăugarea proprietarilor
                context.Owner.AddRange(
                    new Owner { FirstName = "Adrian", LastName = "Popescu" },
                    new Owner { FirstName = "Marian", LastName = "Tomescu" },
                    new Owner { FirstName = "Ciprian", LastName = "Ardelean" },
                    new Owner { FirstName = "Gabriel", LastName = "Dumitru" },
                    new Owner { FirstName = "Ioana", LastName = "Vasilescu" },
                    new Owner { FirstName = "Mihai", LastName = "Lupu" },
                    new Owner { FirstName = "Alexandra", LastName = "Nistor" },
                    new Owner { FirstName = "Cristina", LastName = "Georgescu" },
                    new Owner { FirstName = "Dorin", LastName = "Cojocaru" },
                    new Owner { FirstName = "Ana", LastName = "Munteanu" }
                );

                // Adăugarea clienților
                context.Client.AddRange(
                    new Client { Name = "Popescu Marcela", ClientAdress = "Str. Plopilor 25, Ploiesti", BirthDate = DateTime.Parse("1979-09-01") },
                    new Client { Name = "Mihailescu Cornel", ClientAdress = "Str. M. Eminescu 5, ClujNapoca", BirthDate = DateTime.Parse("1969-07-08") },
                    new Client { Name = "Diana Mihai", ClientAdress = "Bd. Revolutiei 30, Arad", BirthDate = DateTime.Parse("1990-03-15") },
                    new Client { Name = "Victor Petre", ClientAdress = "Str. Tudor Vladimirescu 14, Iasi", BirthDate = DateTime.Parse("1982-01-22") },
                    new Client { Name = "Raluca Andreescu", ClientAdress = "Str. Calea Bucuresti 1, Craiova", BirthDate = DateTime.Parse("1993-06-10") },
                    new Client { Name = "Bianca Rusu", ClientAdress = "Str. Gheorghe Doja 22, Oradea", BirthDate = DateTime.Parse("1987-11-02") },
                    new Client { Name = "Vlad Dumitru", ClientAdress = "Str. Independentei 3, Arad", BirthDate = DateTime.Parse("1975-02-18") },
                    new Client { Name = "Alexandra Petrescu", ClientAdress = "Bd. Dacia 12, Pitesti", BirthDate = DateTime.Parse("1992-07-14") },
                    new Client { Name = "Florin Pavel", ClientAdress = "Str. Tudor Vladimirescu 8, Sibiu", BirthDate = DateTime.Parse("1988-09-20") },
                    new Client { Name = "Laura Stancu", ClientAdress = "Str. Eminescu 9, Buzau", BirthDate = DateTime.Parse("1990-05-05") },
                    new Client { Name = "Ion Popescu", ClientAdress = "Str. Victoriei 11, Timisoara", BirthDate = DateTime.Parse("1978-01-30") },
                    new Client { Name = "Daniela Vasile", ClientAdress = "Str. Crinilor 18, Galati", BirthDate = DateTime.Parse("1982-10-25") },
                    new Client { Name = "Alexandru Ene", ClientAdress = "Str. Avram Iancu 6, Craiova", BirthDate = DateTime.Parse("1989-04-15") },
                    new Client { Name = "Simona Grigore", ClientAdress = "Str. Primaverii 21, Piatra Neamt", BirthDate = DateTime.Parse("1993-08-12") },
                    new Client { Name = "Paul Toma", ClientAdress = "Bd. Transilvaniei 14, Alba Iulia", BirthDate = DateTime.Parse("1986-06-01") }
                );

                // Adăugarea agenților
                context.Agent.AddRange(
                    new Agent { AgentName = "Alexandrescu Ioan", Title = "Regional Manager" },
                    new Agent { AgentName = "Emilia Popescu", Title = "Broker Senior" },
                    new Agent { AgentName = "Constantin Ioan", Title = "Broker Junior" },
                    new Agent { AgentName = "Marius Diaconu", Title = "Senior Consultant" },
                    new Agent { AgentName = "Irina Banescu", Title = "Marketing Manager" },
                    new Agent { AgentName = "Ana Savulescu", Title = "Customer Relations Specialist" }
                );

                // Adăugarea proprietăților publicate
                context.PublishedProperty.AddRange(
                    new PublishedProperty { PropertyID = 1, AgentID = 1 },
                    new PublishedProperty { PropertyID = 2, AgentID = 2 },
                    new PublishedProperty { PropertyID = 3, AgentID = 3 },
                    new PublishedProperty { PropertyID = 4, AgentID = 1 },
                    new PublishedProperty { PropertyID = 5, AgentID = 4 },
                    new PublishedProperty { PropertyID = 6, AgentID = 5 },
                    new PublishedProperty { PropertyID = 7, AgentID = 1 },
                    new PublishedProperty { PropertyID = 8, AgentID = 2 },
                    new PublishedProperty { PropertyID = 9, AgentID = 6 },
                    new PublishedProperty { PropertyID = 10, AgentID = 4 }
                );

                // Adăugarea ofertelor
                context.Offer.AddRange(
                    new Offer { ClientID = 1, PropertyID = 1, OfferDate = DateTime.Parse("2024-01-15") },
                    new Offer { ClientID = 2, PropertyID = 2, OfferDate = DateTime.Parse("2024-01-20") },
                    new Offer { ClientID = 3, PropertyID = 3, OfferDate = DateTime.Parse("2024-02-01") },
                    new Offer { ClientID = 4, PropertyID = 4, OfferDate = DateTime.Parse("2024-02-10") },
                    new Offer { ClientID = 5, PropertyID = 5, OfferDate = DateTime.Parse("2024-03-05") },
                    new Offer { ClientID = 6, PropertyID = 6, OfferDate = DateTime.Parse("2024-03-15") },
                    new Offer { ClientID = 7, PropertyID = 7, OfferDate = DateTime.Parse("2024-04-01") },
                    new Offer { ClientID = 8, PropertyID = 8, OfferDate = DateTime.Parse("2024-04-20") },
                    new Offer { ClientID = 9, PropertyID = 9, OfferDate = DateTime.Parse("2024-05-01") },
                    new Offer { ClientID = 10, PropertyID = 10, OfferDate = DateTime.Parse("2024-05-15") },
                    new Offer { ClientID = 11, PropertyID = 11, OfferDate = DateTime.Parse("2024-06-10") },
                    new Offer { ClientID = 12, PropertyID = 12, OfferDate = DateTime.Parse("2024-06-25") },
                    new Offer { ClientID = 13, PropertyID = 13, OfferDate = DateTime.Parse("2024-07-05") },
                    new Offer { ClientID = 14, PropertyID = 14, OfferDate = DateTime.Parse("2024-07-15") },
                    new Offer { ClientID = 15, PropertyID = 15, OfferDate = DateTime.Parse("2024-08-01") }
                );

                // Salvează modificările
                context.SaveChanges();
            }
        }
    }
}
