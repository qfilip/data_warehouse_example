namespace DwHouse.DataAccess.Records;

public class Audit : ReadOnlyAudit
{
    public DateTime ModifiedAt { get; set; }
    public Guid ModifiedBy { get; set; }
}
