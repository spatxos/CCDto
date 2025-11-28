using System.Collections.Generic;

namespace DispatchProxyDemo.Proxy.DTOs;

public class DBFieldDto
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}

public class DBTableDto
{
    public string Name { get; set; } = string.Empty;
    public List<DBFieldDto> Fields { get; set; } = new();
}

public class DBConnectionDto
{
    public string Name { get; set; } = string.Empty;
    public List<DBTableDto> Tables { get; set; } = new();
}
