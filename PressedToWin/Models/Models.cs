using Newtonsoft.Json;

namespace PressedToWin.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Attachment
    {
        public string attachmentId { get; set; }
        public string fileName { get; set; }
        public int fileSize { get; set; }
        public string thumbnailId { get; set; }
        public string shortPdlModelId { get; set; }
        public string fullPdlModelId { get; set; }
        public object cjeData { get; set; }
        public object composeData { get; set; }
        public object composeDataFromCJEContext { get; set; }
        public int processingState { get; set; }
        public object printStatus { get; set; }
        public object printErrorStatusCode { get; set; }
        public object printerId { get; set; }
        public object printerName { get; set; }
        public int noOfPages { get; set; }
        public object printJobType { get; set; }
        public bool errorWithPdl { get; set; }
        public object jdfFileId { get; set; }
        public object renderedFileId { get; set; }
        public bool isMergedAttachment { get; set; }
        public List<object> mergedAttachmentsFileName { get; set; }
    }
    

    public class JobDto
    {
        public string id { get; set; }
        public string jobName { get; set; }
        public string jobNumber { get; set; }
        public string intent { get; set; }
        public string internalIntent { get; set; }
        public string status { get; set; }
        public int processingStatus { get; set; }
        public string processingUserId { get; set; }
        public DateTime processingTime { get; set; }
        public string label { get; set; }
        public string userEmail { get; set; }
        public string userName { get; set; }
        public string userId { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime completionDate { get; set; }
        public List<Attachment> attachments { get; set; }
        public int preflightStatus { get; set; }
        public object printStatus { get; set; }
        public bool notifyOthers { get; set; }
        public object intentDocument { get; set; }
        public bool isNewJob { get; set; }
        public bool generateLinkToJob { get; set; }
    }


    public class Root
    {
        public int jobCount { get; set; }
        public List<JobDto> jobDtos { get; set; }
    }


}
