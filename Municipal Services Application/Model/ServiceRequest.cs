using System.Collections.Generic;
using System;

public class ServiceRequest
{
    public int RequestId { get; set; }
    public string Title { get; set; }
    public string Status { get; set; }
    public DateTime DateSubmitted { get; set; }
    public int Progress { get; set; } // Representing progress as a percentage (0-100)
    public int Priority { get; set; } // Priority of the request
    public List<int> Dependencies { get; set; } // Dependent request IDs

    public ServiceRequest(int requestId, string title, string status, DateTime dateSubmitted, int progress, int priority, List<int> dependencies = null)
    {
        RequestId = requestId;
        Title = title;
        Status = status;
        DateSubmitted = dateSubmitted;
        Progress = progress;
        Priority = priority;
        Dependencies = dependencies ?? new List<int>();
    }
}
