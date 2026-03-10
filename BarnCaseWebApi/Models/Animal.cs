using System;

namespace BarnCaseWebApi.Models
{
    public class Animal
    {
        public int Id { get; set; }

        public AnimalType Type { get; set; }

        public decimal Price { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public DateTime BirthDate { get; set; }
        public int LifespanInSeconds { get; set; }
        public int UserId { get; set; }
    }
}