using Newtonsoft.Json;

namespace WalletTransfer.Api.Application.Wrappers;

public class ApiErrorResponse
{
    /// <summary>
    /// Succeeded response
    /// </summary>
    /// <example>false</example>
    [JsonProperty("succeeded")]
    public bool Succeeded { get; set; }
    /// <summary>
    ///  Message response
    /// </summary>
    /// <example>One or more validation failures have occurred.</example>
    [JsonProperty("message")]
    public string? Message { get; set; }
    /// <summary>
    /// Error list response
    /// </summary>
    /// <example>["The attrubute field is required."]</example>
    [JsonProperty("errors")]
    public List<string>? Errors { get; set; }
}

public class ApiInternalServerErrorResponse
{
    /// <summary>
    /// Succeeded response
    /// </summary>
    /// <example>false</example>
    [JsonProperty("succeeded")]
    public bool Succeeded { get; set; }
    /// <summary>
    ///  Message response
    /// </summary>
    /// <example>Internal server error</example>
    [JsonProperty("message")]
    public string? Message { get; set; }
    /// <summary>
    /// Error list response
    /// </summary>
    /// <example>null</example>
    [JsonProperty("errors")]
    public List<string>? Errors { get; set; }
}

public class ApiNotFoundResponse
{
    /// <summary>
    /// Succeeded response
    /// </summary>
    /// <example>false</example>
    [JsonProperty("succeeded")]
    public bool Succeeded { get; set; }
    /// <summary>
    ///  Message response
    /// </summary>
    /// <example>Data not found</example>
    [JsonProperty("message")]
    public string? Message { get; set; }
    /// <summary>
    /// Error list response
    /// </summary>
    /// <example>null</example>
    [JsonProperty("errors")]
    public List<string>? Errors { get; set; }
}