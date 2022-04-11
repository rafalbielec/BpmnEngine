namespace BpmnEngine.Services;

public static class ServicesConstants
{
    public const int DefaultLockDuration = 10_000;

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

    public static class FormHandlingVariables
    {
        public const string Start = "start";
        public const string LastStep = "last_step";
        public const string Supervisor = "supervisor";
        public const string Manager = "manager";
        public const string None = "none";
        public const string Employee = "employee";
        public const string Director = "director";
        public const string BouVerification = "bou_verification";
        public const string BouDirector = "bou_director";
        public const string Position = "position";

        public const string PhoneNumber = "phone_number";
        public const string Destination = "destination";
    }

    public static class ProcessNames
    {
        public const string Forms = "FormHandlingExtended";
        public const string Test = "TestProcess";
    }

    public enum Processes
    {
        Test,
        CarHire
    }
}