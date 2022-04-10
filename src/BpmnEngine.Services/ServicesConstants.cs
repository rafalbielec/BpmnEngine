namespace BpmnEngine.Services;

public static class ServicesConstants
{
    public static class Messages
    {
        public const string ManagerApproved = "MGRAPPROVED";
        public const string ManagerRejected = "MGRREJECTED";
        public const string DirectorApproved = "DIRAPPROVED";
        public const string DirectorRejected = "DIRREJECTED";
        public const string VerificationDone = "VERIFICATIONDONE";
        public const string BouDirectorApproved = "BOUDIRAPPROVEDVERIFIED";
        public const string BouDirectorRejected = "BOUDIRREJECTEDVERIFIED";
    }

    public static class Positions
    {
        public const string Supervisor = nameof(Supervisor);
        public const string Manager = nameof(Manager);
        public const string Director = nameof(Director);
        public const string BouDirector = nameof(BouDirector);
        public const string Position = nameof(Position);
    }
}