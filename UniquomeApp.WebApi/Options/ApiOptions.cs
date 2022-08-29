namespace UniquomeApp.WebApi.Options
{
    public class ApiOptions
    {
        public string RfPrefix { get; set; }
        public string StorageLocation { get; set; }
        public string ExternalUrl { get; set; }
        public string StudentRegistryUrl { get; set; }
        public string StudentRegistryApiKey { get; set; }
        public string EclassUrl { get; set; }
        public string EclassApiKey { get; set; }
        public string CycleInfoApiKey { get; set; }
        public string PrivacyDisclaimer { get; set; }

        public string PersonDataLocation => $"{StorageLocation}/person-data";
        public string DraftCourseLocation => $"{StorageLocation}/draft-courses";
        public string PublishedCourseLocation => $"{StorageLocation}/published-courses";
        public string ScientificBranchLocation => $"{StorageLocation}/scientific-branches";
        public string ThematicFieldLocation => $"{StorageLocation}/thematic-fields";
    }
}