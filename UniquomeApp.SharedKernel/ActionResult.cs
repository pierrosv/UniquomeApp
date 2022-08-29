namespace UniquomeApp.SharedKernel
{
    public class ActionResult
    {
        //TODO: This class can be expanded to include more generic structures like list of messages/warnings/errors/etc.
        //TODO: Better naming. It conficts with MVC IActionResult
        internal ActionResult(bool succeeded, int exitCode, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            ExitCode = exitCode;
            Errors = errors.ToArray();
        }

        public bool Succeeded { get; set; }
        public int ExitCode { get; set; }

        public string[] Errors { get; set; }

        protected ActionResult()
        {

        }

        public static ActionResult Success()
        {
            return new ActionResult(true, 0, new string[] { });
        }

        public static ActionResult Failure(IEnumerable<string> errors)
        {
            return new ActionResult(false, -1, errors);
        }
    }

    public class DbOptions
    {
        public string DbProvider { get; set; }
        public string MainConnectionString { get; set; }
        public string SecurityConnectionString { get; set; }
    }
}
