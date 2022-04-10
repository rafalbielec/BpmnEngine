using Newtonsoft.Json;

namespace BpmnEngine.Camunda.Models;

public class ProcessDefinition
{
    [JsonConstructor]
    public ProcessDefinition(string id, string key, string description, string name, string resource)
    {
        Name = Guard.NotNull(name, nameof(name));
        Id = Guard.NotNull(id, nameof(id));
        Key = Guard.NotNull(key, nameof(key));
        Description = Guard.NotNull(description, nameof(description));
        Resource = resource;
    }

    [JsonProperty("id")] public string Id { get; set; }
    [JsonProperty("key")] public string Key { get; set; }
    [JsonProperty("category")] public string? Category { get; set; }
    [JsonProperty("description")] public string? Description { get; set; }
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("version")] public long Version { get; set; }
    [JsonProperty("resource")] public string Resource { get; set; }
    [JsonProperty("deploymentId")] public Guid DeploymentId { get; set; }
    [JsonProperty("suspended")] public bool Suspended { get; set; }
    [JsonProperty("versionTag")] public string? VersionTag { get; set; }
}