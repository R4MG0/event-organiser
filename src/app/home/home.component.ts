import { Component, OnInit } from '@angular/core';
import { EventService } from '../shared/service/event.service';
import { Event } from '../shared/interfaces/event';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  events: Event[] = [];

  constructor(private readonly eventService: EventService) { }

  ngOnInit(): void {
    // this.eventService.getEvents().subscribe((data) => {});
    this.events = this.eventService.getEvents();
  }

}
