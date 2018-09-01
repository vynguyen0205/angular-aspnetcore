import { Component, Input, Output, EventEmitter } from "@angular/core";

import { MatSnackBar } from "@angular/material";
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";

import { Contact } from "../../models/contact.model";
import { Tag } from "../../models/tag.model";
import { TagService } from "../../services/tag.service";
import { ContactService } from "../../services/contact.service";
import { ConfirmDeleteComponent } from "./confirm-delete/confirm-delete.component";

@Component({
  selector: "app-contact-item",
  templateUrl: "./contact-item.component.html",
  styleUrls: ["./contact-item.component.css"]
})
export class ContactItemComponent {
  constructor(
    public snackBar: MatSnackBar,
    public dialog: MatDialog,
    private _tagService: TagService,
    private _contactService: ContactService
  ) {}

  @Input()
  contact: Contact;
  @Input()
  isSelected: boolean;
  @Output()
  onSelectContact = new EventEmitter<number>();
  @Output()
  onTagAdded = new EventEmitter<Tag>();
  @Output()
  onTagUpdated = new EventEmitter<Tag>();
  @Output()
  onTagDeleted = new EventEmitter<number>();

  tags: Array<Tag>;
  customTagName: string;

  selectContact(contactId: number): void {
    this.onSelectContact.emit(contactId);
  }

  getTags($event) {
    // Get list of tags
    this._tagService.getTags().subscribe(tags => {
      this.tags = tags;
      this.tags.forEach(tag => {
        if (
          this.contact.tags.find(contactTag => contactTag.tagId === tag.tagId)
        ) {
          tag.isSelected = true;
        }
      });
    });
  }

  clickTag(tag: Tag) {
    // Prevent changing checkbox if user mis-click in the wrong place while editing
    if (tag.editing) {
      return;
    }

    // Update the contact
    this._contactService
      .modifyContactTag(this.contact.contactId, tag)
      .subscribe(() => {
        // Notfiy user
        this.snackBar.open("Saved successfully.", null, {
          duration: 2000
        });
      });

    // Customer feedback: do not wait for the request to finish, to improve responsiveness
    // Update contact card
    if (tag.isSelected) {
      this.contact.tags.push(tag);
    } else {
      this.contact.tags = this.contact.tags.filter(
        contactTag => contactTag.tagId !== tag.tagId
      );
    }
  }

  addCustomTag() {
    // Quick validation
    if (!this.customTagName) {
      this.snackBar.open("Tag name is required.", null, { duration: 2000 });
      return;
    }

    // Save the tag
    this._tagService.addTag(this.customTagName).subscribe(newTag => {
      //Reset the custom tag field
      this.customTagName = undefined;

      // Update the tag list for current contact
      this.tags.push(newTag);

      // Emit tag added event to parent, so that parent makes a call to SignalR
      this.onTagAdded.emit(newTag);

      // Auto select this tag, to save user one click
      newTag.isSelected = true;
      this.clickTag(newTag);
    });
  }

  updateTag(tag: Tag) {
    // Quick validation
    if (!tag.tagName) {
      this.snackBar.open("Tag name is required.", null, { duration: 2000 });
      return;
    }

    // Save the tag
    this._tagService.updateTag(tag.tagId, tag.tagName).subscribe(() => {
      // Notify the user
      this.snackBar.open("Updated successfully.", null, { duration: 2000 });

      tag.editing = false;

      // When a tag name is updated, emit an event to update the name for all contacts
      // without having to reload the contacts
      this.onTagUpdated.next(tag);
    });
  }

  confirmDelete(tag: Tag) {
    let dialogRef = this.dialog.open(ConfirmDeleteComponent, {
      width: "350px"
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.delete(tag);
      }
    });
  }

  delete(tag: Tag) {
    this._tagService.deleteTag(tag.tagId).subscribe(() => {
      // Notify the user
      this.snackBar.open("Deleted successfully.", null, { duration: 2000 });

      this.tags = this.tags.filter(t => t.tagId !== tag.tagId);

      // When a tag name is deleted, emit an event to update the name for all contacts
      // without having to reload the contacts
      this.onTagDeleted.next(tag.tagId);
    });
  }
}
