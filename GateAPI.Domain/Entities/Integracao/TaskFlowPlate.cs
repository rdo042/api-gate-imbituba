namespace GateAPI.Domain.Entities.Integracao
{
    public class TaskFlowPlateResponse
    {
        public Header Header { get; set; }
        public List<object> Errors { get; set; }
        public string OcrVehiclePlate { get; set; }
        public object Driver { get; set; }
        public List<VehiclePassage> VehiclePassages { get; set; }
    }

    public class Header
    {
        public int ProductId { get; set; }
        public string Version { get; set; }
        public DateTimeOffset DateTimeGeneration { get; set; }
        public string SystemOrigin { get; set; }
        public string TransactionId { get; set; }
        public string TypeMessage { get; set; }
        public string Company { get; set; }
        public string Flow { get; set; }
        public Checkpoint Checkpoint { get; set; }
    }

    public class Checkpoint
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class VehiclePassage
    {
        public int ProductId { get; set; }
        public int SchedulingId { get; set; }
        public string Mission { get; set; }
        public string CurrentStatusPassage { get; set; }
        public DateTime PlannedDate { get; set; }
        public string Terminal { get; set; }
        public string LicensePlate { get; set; }
        public DateTime? EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public DateTime? OuterEntryTime { get; set; }
        public DateTime? DateTimeInspection { get; set; }
        public string VehicleType { get; set; }
        public string IdentifierTag { get; set; }
        public List<Schedule> Schedules { get; set; }
    }

    public class Schedule
    {
        public int Id { get; set; }
        public string TrailerLicensePlate { get; set; }
        public string Container { get; set; }
    }
}
