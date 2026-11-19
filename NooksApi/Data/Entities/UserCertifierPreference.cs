namespace NooksApi.Data.Entities;

public class UserCertifierPreference
{
    public Guid UserId { get; set; }
    public int CertifierId { get; set; }

    public Certifier? Certifier { get; set; }
}
