namespace MedMonitor24_7.Models;

public enum Gender
{
    Male = 1,
    Female = 2
}

public enum BedStatus
{
    Available = 1,
    Occupied = 2,
    Offline = 3
}

public enum AdmissionStatus
{
    Active = 1,
    Discharged = 2
}

public enum UserStatus
{
    Active = 1,
    Inactive = 2
}

public enum VitalSource
{
    Sensor = 1,
    AI = 2,
    Manual = 3
}

public enum TaskStatus
{
    Open = 1,
    InProgress = 2,
    Done = 3,
    Cancelled = 4,
    Overdue = 5
}

public enum AlertSeverity
{
    Warning = 1,
    Critical = 2
}

public enum AlertStatus
{
    Active = 1,
    Acknowledged = 2,
    Resolved = 3
}

public enum DeviceStatus
{
    Online = 1,
    Offline = 2
}

public enum TaskEventType
{
    Created = 1,
    Started = 2,
    Done = 3,
    Cancelled = 4,
    Overdue = 5
}