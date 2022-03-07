namespace EventSourcingCqrs.Core.Todos.Aggregates;

public static class TodoEvents
{
    public static string Created => "created";
    public static string DescriptionChanged => "description-changed";
    public static string ChangedStatusToInProgress => "status-changed-to-in-progress";
    public static string ChangedStatusToDone => "status-changed-to-done";
    public static string UpdateToSomeHistoryState => "undo-to-some-history-state";
}