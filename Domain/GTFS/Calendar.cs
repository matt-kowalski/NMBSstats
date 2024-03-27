namespace TrainApp.Domain.GTFS;

public class Calendar
{
    public Calendar()
    {
        
    }

    public Guid Id { get; set; }
    public Monday Monday { get; set; }
    public Tuesday Tuesday { get; set; }
    public Wednesday Wednesday { get; set; }
    public Thursday Thursday { get; set; }
    public Friday Friday { get; set; }
    public Saturday Saturday { get; set; }
    public Sunday Sunday { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public override string ToString()
    {
        return $"Mon\t\tTue\t\tWed\t\tThu\t\tFri\t\tSat\t\tSun\n" +
               $"{Monday}\t{Tuesday}\t{Wednesday}\t{Thursday}\t{Friday}\t{Saturday}\t{Sunday}\n" +
               $"{StartDate} - {EndDate}";
    }
}

public enum Monday : byte
{
    NotAvailable = 0,
    Available = 1
}

public enum Tuesday : byte
{
    NotAvailable = 0,
    Available = 1
}

public enum Wednesday : byte
{
    NotAvailable = 0,
    Available = 1
}

public enum Thursday : byte
{
    NotAvailable = 0,
    Available = 1
}

public enum Friday : byte
{
    NotAvailable = 0,
    Available = 1
}

public enum Saturday : byte
{
    NotAvailable = 0,
    Available = 1
}

public enum Sunday : byte
{
    NotAvailable = 0,
    Available = 1
}