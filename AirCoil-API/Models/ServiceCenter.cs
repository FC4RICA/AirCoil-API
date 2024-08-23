﻿namespace AirCoil_API.Models
{
    public class ServiceCenter
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Branch> Branches { get; set; } = new List<Branch>();
    }
}
