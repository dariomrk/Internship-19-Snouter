﻿using System.Text.Json;

namespace Data.Models
{
    public class SubCategory : BaseEntity<int>, IDisposable
    {
        public string Name { get; set; } = null!;
        public JsonDocument ValidationSchema { get; set; } = null!;
        public Category Category { get; set; } = null!;
        public int CategoryId { get; set; }

        public void Dispose() => ValidationSchema?.Dispose();
    }
}
