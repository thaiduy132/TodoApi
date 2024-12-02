﻿using System.Diagnostics.Eventing.Reader;

namespace TodoApi.Models.DTOs
{
    public class TodoItemDTO
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
