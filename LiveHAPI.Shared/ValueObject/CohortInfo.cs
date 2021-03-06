﻿using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class CohortInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Display { get; set; }
        public int Rank { get; set; }
    }
}
