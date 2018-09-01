import { Tag } from "./tag.model";

export class Contact {
    contactId: number;
    name: string;
    company: string;
    phone: string;
    email: string;
    linkedIn: string;
    skype: string;
    tags: Array<Tag>;
}