import { Component, OnInit, ChangeDetectorRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { PageEvent, MatMenuTrigger } from '@angular/material';

import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';

import { HubConnection } from '@aspnet/signalr';

import { Tag } from '../models/tag.model';
import { TagService } from '../services/tag.service';
import { ContactService } from '../services/contact.service';
import { ContactList } from '../models/contact-list.model';
import { Constants } from '../shared/Constants';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(
    private _contactService: ContactService,
    private _tagService: TagService,
    private _router: Router
  ) { }

  contactList: ContactList;
  tags: Array<Tag>;
  notifications: Array<string> = [];
  selectedContactId: number;

  searchTagId: number = 0;
  searchTerm: string = '';
  searchTerm$: Subject<string> = new Subject<string>();
  paginationOptions = Constants.paginationOptions;

  @ViewChild(MatMenuTrigger) trigger: MatMenuTrigger;

  private _hubConnection: HubConnection;

  ngOnInit(): void {
    //Set up tag hub
    this._setUpTagHub();

    // Get all items in the contact list at loading
    this.search();

    // Delay the search, only start searching if user stops typing for more than 200ms
    this.searchTerm$
      .debounceTime(200)
      .distinctUntilChanged()
      .subscribe(keyWord => {
        this.search(keyWord);
      });
  }


  search(keyWord: string = '', tagId: number = 0, pageEvent?: PageEvent) {
    // If keyword starts with "#", load and display the list of tags
    if (keyWord.indexOf('#') == 0) {
      this._tagService.getTags()
        .subscribe(tags => {
          // Prefix the tag name with a '#', so it's easier to see that it's a tag
          tags.forEach(tag => tag.tagName = '#' + tag.tagName);
          this.tags = tags;
        });
      return;
    }

    // Otherwise, perform search normally
    this.tags = undefined;
    let pageNo = 1;

    if (pageEvent) {
      pageNo = pageEvent.pageIndex + 1;
    }

    // Make a call to the server to search
    this._contactService.getContacts(keyWord, tagId, this.paginationOptions.itemsPerPage, pageNo)
      .subscribe(contactList => {
        this.contactList = contactList;
      });
  }

  // When user selects an option in auto complete, perform searching
  onSelectAutoComplete(event) {
    this.searchTagId = event.option.value.tagId;
    this.search('', this.searchTagId);
  }

  // When a contact card is clicked, navigate to that contact
  onSelectContact(contactId: number) {
    this.selectedContactId = contactId;
    this._router.navigate(["contacts", contactId]);
  }

  // If a new tag is added, broadcast it to all clients using SignalR
  onTagAdded(tag: Tag) {
    this._hubConnection.send('send', tag.tagId, tag.tagName);
  }

  // If a tag name is updated, loop through all the contacts and update its name (without having to reload)
  onTagUpdated(tag: Tag) {
    this.contactList.list.forEach(contact => {
      contact.tags.forEach(contactTag => {
        if (contactTag.tagId === tag.tagId) {
          contactTag.tagName = tag.tagName;
        }
      });
    });
  }

  // If a tag is deleted, loop through all the contacts and remove it (without having to reload)
  onTagDeleted(tagId: number) {
    this.contactList.list.forEach(contact => {
      contact.tags = contact.tags.filter(ct => ct.tagId !== tagId);
    });
  }

  // Displays the name of the tag in the field
  displayAutoComplete(tag?: Tag): string | undefined {
    return tag ? tag.tagName : undefined;
  }

  // Set up tag hub
  private _setUpTagHub() {
    this._hubConnection = new HubConnection(Constants.hubTags);
    this._hubConnection.start()
      .then(() => console.log('Hub connection started.'));

    this._hubConnection.on('Send', (tagId: number, tagName: string) => {
      this.notifications.unshift(`Tag ${tagName} was added.`);
      this.trigger.openMenu();

      let timer = Observable.timer(5000).subscribe(() => {
        this.trigger.closeMenu();
        timer.unsubscribe();
      });
    });
  }

}
