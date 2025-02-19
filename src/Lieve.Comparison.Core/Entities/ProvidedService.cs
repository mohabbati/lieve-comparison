﻿using Lieve.Comparison.Domain.Shared.Enums;

namespace Lieve.Comparison.Domain.Entities;

public sealed class ProvidedService
{
    public required ServiceType ServiceType { get; set; }
    public required string UriTemplate { get; set; }
}