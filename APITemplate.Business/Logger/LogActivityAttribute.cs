namespace APITemplate.Business.Logger;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class LogActivityAttribute : Attribute
{
    public string ActionType { get; }
    public string Description { get; }

    public LogActivityAttribute(string actionType, string description = null)
    {
        ActionType = actionType;
        Description = description;
    }
}
