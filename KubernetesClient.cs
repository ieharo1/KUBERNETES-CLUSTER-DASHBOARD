using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class KubernetesClient
{
    private readonly HttpClient http;

    public KubernetesClient(string baseUrl)
    {
        http = new HttpClient { BaseAddress = new System.Uri(baseUrl) };
    }

    public async Task<List<PodInfo>> GetPodsAsync()
    {
        var json = await http.GetStringAsync("/api/v1/pods");
        using var doc = JsonDocument.Parse(json);
        var items = doc.RootElement.GetProperty("items");
        var result = new List<PodInfo>();
        foreach (var item in items.EnumerateArray())
        {
            var meta = item.GetProperty("metadata");
            var status = item.GetProperty("status");
            result.Add(new PodInfo
            {
                Name = meta.GetProperty("name").GetString() ?? "",
                Namespace = meta.GetProperty("namespace").GetString() ?? "",
                Status = status.GetProperty("phase").GetString() ?? ""
            });
        }
        return result;
    }

    public async Task<int> GetWorkerCountAsync()
    {
        var json = await http.GetStringAsync("/api/v1/nodes");
        using var doc = JsonDocument.Parse(json);
        var items = doc.RootElement.GetProperty("items");
        return items.GetArrayLength();
    }

    public async Task<int> GetServiceCountAsync()
    {
        var json = await http.GetStringAsync("/api/v1/services");
        using var doc = JsonDocument.Parse(json);
        var items = doc.RootElement.GetProperty("items");
        return items.GetArrayLength();
    }
}

public class PodInfo
{
    public string Name { get; set; } = "";
    public string Namespace { get; set; } = "";
    public string Status { get; set; } = "";
}

