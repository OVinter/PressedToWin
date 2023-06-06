import { Attachment } from "./attachment";

export class JobList {
    public id: string;
    public jobName: string;
    public jobNumber: string;
    public intent: string;
    public internalIntent: string;
    public status: string;
    public userEmail: string;
    public userName: string;
    public userId: string;
    public attachments: Attachment[];
}