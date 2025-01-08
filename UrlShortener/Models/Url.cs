using System;
using System.Collections.Generic;

namespace UrlShortener.Models;

public partial class Url
{
    public int Id { get; set; }

    public string? Url1 { get; set; }

    public string? Shorturl { get; set; }

    public string Code { get; set; } = null!;

    public DateOnly? Creationdate { get; set; }
}
