import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ContactService } from '../../services/contact.service';
import { Contact } from '../../models/contact.model';

@Component({
  templateUrl: './contact-detail.component.html',
  styleUrls: ['./contact-detail.component.css']
})
export class ContactDetailComponent implements OnInit {

  constructor(
    private _activatedRoute: ActivatedRoute,
    private _contactService: ContactService
  ) { }

  contact: Contact;

  ngOnInit() {
    this._activatedRoute.params.subscribe(params => {
      const contactId = params.contactId;

      if (contactId) {
        this._contactService.getContact(contactId)
          .subscribe(contact => this.contact = contact);
      }
    })
  }
}
